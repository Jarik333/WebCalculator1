using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using WebCalculator1;
using EFDataApp.Models;
using UAParser;
using Microsoft.AspNetCore.Http;
using PagedList;
using System.Net.Sockets;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using AuthApp.ViewModels;

namespace EFDataApp.Controllers
{
    
    public class HomeController : Controller
    {
        public ApplicationContext db;
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
            HostString httpRequest = HttpContext.Request.Host;
            operation.IP = httpRequest.ToString();
           
            var userAgent = HttpContext.Request.Headers["User-Agent"];
            var uaParser = Parser.GetDefault();
            ClientInfo c = uaParser.Parse(userAgent);
            operation.Browser = c.UA.Family.ToString();

            DateTime now = DateTime.Now;
            operation.Date = now;

            db.Operations.Add(operation);
       
            
            try
            {
                double result = 0;
                result = calculator.Evaluate(expression);
                
                if ((result != double.PositiveInfinity) && (result != double.NegativeInfinity))
                {
                   
                    ViewData["Message"] = $"Ответ: {result}";
                    operation.Result = $"Ответ: {result}";
                    operation.Error = "-";
                }
                else
                {
                    ViewData["Message"] = "Ошибка: попытка деления на 0";
                    operation.Error = "Попытка деления на 0";
                    operation.Result = "Не получен";
                }
            }
            catch (System.FormatException)
            {
                ViewData["Message"] = "Ошибка: неверный формат ввода";
                operation.Error = "Неверный формат ввода";
                operation.Result = "Не получен";
            }

            db.SaveChangesAsync();
           
                return View();
           
        }
       
        public  ViewResult Info(int ? page)
        {
            
            var operations = from s in db.Operations
            select s;
            operations = operations.OrderByDescending(s => s.Date);
            int pageSize = 10;
            int pageIndex = (page ?? 1);
            return View(operations.ToPagedList(pageIndex, pageSize));

            
        }
    }
}

