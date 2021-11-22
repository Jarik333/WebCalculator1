using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebCalculator1;
using Microsoft.EntityFrameworkCore;
using EFDataApp.Models;
using System.Net;
using UAParser;

namespace EFDataApp.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationContext db;
        public HomeController(ApplicationContext context)
        {
            db = context;
        }
        Calculator calculator = new Calculator();

        private readonly ILogger<HomeController> _logger;

       
        [HttpGet]
        public IActionResult Index()
        {

            return View();

        }
        [HttpPost]
        public ActionResult Index(string expression, Operation operation)
        {
            
            String host = System.Net.Dns.GetHostName();
            
            System.Net.IPAddress ip = System.Net.Dns.GetHostByName(host).AddressList[0];
            
            var userAgent = HttpContext.Request.Headers["User-Agent"];
            var uaParser = Parser.GetDefault();
            ClientInfo c = uaParser.Parse(userAgent);
            operation.IP = $"Browser: {c.UA.Family.ToString()} IP: {ip.ToString()}";
            DateTime now = DateTime.Now;
           
           
            db.Operations.Add(operation);
       
            double result = 0;
            try
            {
                result = calculator.Evaluate(expression);
                
                if ((result != double.PositiveInfinity) && (result != double.NegativeInfinity))
                {
                   
                    ViewData["Message"] = $"Ответ: {result}";
                    operation.Result = $"Ответ: {result}";
                }
                else
                {
                    ViewData["Message"] = "Ошибка: попытка деления на 0";
                    operation.Result = "Ошибка: попытка деления на 0";
                }
            }
            catch (System.FormatException)
            {
                ViewData["Message"] = "Ошибка: неверный формат ввода";
                operation.Result = "Ошибка: неверный формат ввода";
            }
            operation.Date = now.ToString();

       
            db.SaveChangesAsync();
            return View();
        }
        public async Task<IActionResult> Info()
        {
            
            return View(await db.Operations.ToListAsync());
        }
    }
}

