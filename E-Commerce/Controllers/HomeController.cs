﻿using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}