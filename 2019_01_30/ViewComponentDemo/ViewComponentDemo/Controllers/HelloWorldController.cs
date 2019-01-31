using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ViewComponentDemo.Controllers
{
    public class HelloWorldController : Controller
    {
        public IActionResult Index()
        {
            return ViewComponent("HelloWorld", new { message = "Invoked From Controller" });
        }
    }
}