using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebCalculator1.Models;

namespace WebCalculator1.Controllers
{
    public class HomeController : Controller
    {
        Calculator calculatorModel = new Calculator();

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        public IActionResult Index()
        {

            return View();

        }
        [HttpPost]
        public ActionResult Index(string expression)
        {
            double result = 0;
            try
            {
                result = calculatorModel.Evaluate(expression);
                if ((result != double.PositiveInfinity) && (result != double.NegativeInfinity))
                {
                    
                    ViewData["Message"] = $"Ответ: {result}";
                }
                else
                {
                    ViewData["Message"] = "Ошибка: попытка деления на 0";
                    
                }
            }
            catch (System.FormatException)
            {
                ViewData["Message"] = "Ошибка: неверный формат ввода";
            }
         return View();
        }
    }
}

