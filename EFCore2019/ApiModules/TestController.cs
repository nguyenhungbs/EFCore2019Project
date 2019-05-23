using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFCore2019.Domain.Entities;
using EFCore2019.Domain.Models.PagingInfo;
using EFCore2019.Domain.Models.Tests;
using EFCore2019.Domain.Services.Test;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EFCore2019.Api.ApiModules
{
    [Route("api/[controller]")]
    public class TestController : Controller
    {
        private readonly ITestService _testService;

        public TestController(ITestService testService)
        {
            _testService = testService;
        }

        /// <summary>
        /// Tìm kiếm kết hợp phân trang
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpPost("/api/v1/search-test")]
        [SwaggerResponse((int)System.Net.HttpStatusCode.OK, Type = typeof(BaseSearchResult<TestThu>))]
        public IActionResult SearchTest([FromBody]SearchModel<TestThu> search)
        {          
            var result = _testService.SearchTest(search);
            return Json(new { success = true, data = result });
        }
        [HttpGet("/api/v1/test")]
        public IActionResult Get()
        {
            var result = _testService.GetData();
            return Json(new { success = true, data = result });
        }

        [HttpGet("/api/v1/test-find/{id}")]
        public IActionResult FindTestByCondition(int id)
        {
            var result = _testService.FindTestByCondition(id);
            return Json(new { success = true, data = result });
        }

        [HttpPost("/api/v1/test-update")]
        public IActionResult UpdateTest([FromBody]UpdateTestModel model)
        {
            var result = _testService.SaveTestModel(model);
            return Json(new { success = result });
        }

        [HttpDelete("/api/v1/test-delete")]
        public IActionResult DeleteTest([FromBody] DeleteTestModel model)
        {
            var result = _testService.DeleteTest(model);
            return Json(new { success = true });
        }
    }
}
