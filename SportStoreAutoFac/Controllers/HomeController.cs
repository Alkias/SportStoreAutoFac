using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SportStoreAutoFac.Models;
using SportStoreAutoFac.Services;
using System.Collections.Generic;
using System.Diagnostics;
using SportStoreAutoFac.Data;

namespace SportStoreAutoFac.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITestService _testService;
        private readonly IStoreRepository _storeRepository;

        public HomeController(ILogger<HomeController> logger,
            ITestService testService,
            IStoreRepository storeRepository) {
            _logger = logger;
            _testService = testService;
            _storeRepository = storeRepository;
        }

        public IActionResult Index() {
            var model = _storeRepository.Products;
            
            return View();
        }

        [HttpGet("TestList")]
        public ActionResult<List<TestModel>> GetTestList()
        {
            var model = _testService.GetTestList();
            return Ok(model);
        }

        public IActionResult Privacy()
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
