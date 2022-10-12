﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ProjectWebMVC.Models;

namespace ProjectWebMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Filters()
        {
            return View();
        }

        public IActionResult AboutUs()
        {
            return View();
        }

        public IActionResult HowToUse()
        {
            IEnumerable<Program> model = new List<Program>();
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Delete()
        {
            return View();
        }

        public IActionResult Edit()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}