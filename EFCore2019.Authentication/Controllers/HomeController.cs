using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EFCore2019.Authentication.Controllers
{
   
    public class HomeController : Controller
    {

        [HttpGet]
        [Route("/")]
        public ActionResult<string> Index()
        {
            return "Server Authentication";
        }
    }
}