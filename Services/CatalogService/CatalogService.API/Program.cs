#region Using

using System.Globalization;
using System.Reflection;
using CatalogService.Common.Constants;
using CatalogService.Application.Interfaces;
using CatalogService.Domain.Entities;
using CatalogService.Application.RabbitMQ;
using CatalogService.Application.Repositories;
using CatalogService.Common.Resources;
using CatalogService.Application.Services;
using Hangfire;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using CatalogService.Infrastructure;
using CatalogService.Application;
using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;

#endregion

#region Builder

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOptions();
builder.Services.AddRazorPages();
builder.Services.AddControllers();
builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter("Fixed", options =>
    {
        options.PermitLimit = 10;
        options.Window = TimeSpan.FromMinutes(1);
        options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        options.QueueLimit = 20;
    });
});

#region Database

builder.Services.AddDbContext<StoreDbContext>(options => 
                        options.UseSqlServer(builder.Configuration.GetConnectionString("SqlDB")), ServiceLifetime.Scoped);
builder.Services.AddHangfire(x =>
{
    x.UseSqlServerStorage(builder.Configuration.GetConnectionString("SqlDB"));
});
builder.Services.AddHangfireServer();

//builder.Services.AddIdentity<User, IdentityRole>()
//                .AddEntityFrameworkStores<StoreDbContext>()
//                .AddDefaultTokenProviders();

#endregion

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = "localhost:6379";
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

#region Localizations

builder.Services.AddLocalization(options => options.ResourcesPath = ClothingConstants.Resource);
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
        c.SwaggerEndpoint(ClothingConstants.ApiSwaggerRoute, ClothingConstants.ApiSwaggerVersion);
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
app.UseHangfireDashboard();
app.UseRateLimiter();
app.Run();

#endregion