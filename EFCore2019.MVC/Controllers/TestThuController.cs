using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFCore2019.Domain.Entities;
using EFCore2019.Domain.Models;
using EFCore2019.Domain.Models.PagingInfo;
using EFCore2019.Domain.Services.Test;
using EFCore2019.Libraries.Utils;
using EFCore2019.MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace EFCore2019.MVC.Controllers
{
    public class TestThuController : Controller
    {
        private readonly ITestService _testService;

        public TestThuController(ITestService testService)
        {
            _testService = testService;
        }
        public IActionResult Index()
        {
            var search = new SearchModel<TestThu>();
            var result = _testService.GetData().ToList().CloneToListModels<TestThuModel, TestThuViewModel>();
            return View(result);
        }

        


    }
}