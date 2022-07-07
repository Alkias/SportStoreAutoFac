using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SportStoreAutoFac.Models;
using SportStoreAutoFac.Services;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using SportStoreAutoFac.Data;

namespace SportStoreAutoFac.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITestService _testService;
        private readonly IAsyncRepository<Product> _productRepository;

        public HomeController(ILogger<HomeController> logger,
            ITestService testService,
            IAsyncRepository<Product> productRepository) {
            _logger = logger;
            _testService = testService;
            _productRepository = productRepository;
        }

        public async Task<IActionResult> Index() {
            IEnumerable<Product> model;
            model = await _productRepository.GetAll();

            return View(model);
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
