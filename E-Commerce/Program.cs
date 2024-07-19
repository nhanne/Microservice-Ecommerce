#region Using

using System.Globalization;
using System.Reflection;
using E_Commerce;
using E_Commerce.Databases;
using E_Commerce.Interfaces;
using E_Commerce.Models;
using E_Commerce.RabbitMQ;
using E_Commerce.Repositories;
using E_Commerce.Resources;
using E_Commerce.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

#endregion

#region Builder

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOptions();
builder.Services.AddRazorPages();
builder.Services.AddControllers();
builder.Services.AddControllersWithViews();
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = "localhost";
    options.InstanceName = "RedisInstance";
});
builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

builder.Services.AddSwaggerGen(c => c.SwaggerDoc("v1", 
    new OpenApiInfo { Title = "My API", Version = "v1" })
);
builder.Services.AddCors(options =>options.AddPolicy("AllowSpecificOrigin",
                                                     b => b.WithOrigins("https://localhost:5077/")
                                                                                     .AllowAnyHeader()
                                                                                     .AllowAnyMethod()));

builder.Services.AddScoped<IColorService, ColorService>();
builder.Services.AddScoped<IRepository<Color>, ColorRepository>();
builder.Services.AddScoped<IMessageProducer, RabbitMqProducer>();


#endregion

#region Database

builder.Services.AddDbContext<StoreDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("SqlDB")));
builder.Services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<StoreDbContext>()
                .AddDefaultTokenProviders();

#endregion

#region Localizations

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.AddMvc().AddViewLocalization().AddDataAnnotationsLocalization(options =>
{
    options.DataAnnotationLocalizerProvider = (_, factory) =>
    {
        var assemblyName = new AssemblyName(typeof(SharedResource).GetTypeInfo().Assembly.FullName!);
        return factory.Create("ShareResource", assemblyName.Name!);
    };
});
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new List<CultureInfo>
    {
        new CultureInfo("vi-VI"),
        new CultureInfo("en-US")
    };
    options.DefaultRequestCulture = new RequestCulture(culture: "vi-VI", uiCulture: "vi-VI");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
    options.RequestCultureProviders.Insert(0, new QueryStringRequestCultureProvider());
});

#endregion

#region App

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    });
}

app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseRequestLocalization(app.Services
    .GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.UseCors("AllowSpecificOrigin");
app.Run();

#endregion