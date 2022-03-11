using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace JSON_CRUD.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            //https://youtu.be/PUqw_FpI97g?list=PLDAPLmR-A0XQmqUPwziBhmNtH_nz1CIYF
            //https://www.chartjs.org/docs/latest/getting-started/
            return View();
        }
    }
}
