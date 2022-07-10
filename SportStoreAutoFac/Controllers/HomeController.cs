using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SportStoreAutoFac.Data;
using SportStoreAutoFac.Models;
using SportStoreAutoFac.Services;
using SportStoreAutoFac.UI.Paging;

namespace SportStoreAutoFac.Controllers
{
    public class HomeController : Controller
    {
        #region Fields

        private readonly ILogger<HomeController> _logger;
        private readonly ITestService _testService;
        private readonly IAsyncRepository<Product> _productRepository;

        #endregion

        #region Ctor

        public HomeController(ILogger<HomeController> logger,
            ITestService testService,
            IAsyncRepository<Product> productRepository) {
            _logger = logger;
            _testService = testService;
            _productRepository = productRepository;
        }

        #endregion

        #region Methods

        public async Task<IActionResult> Index(int pageIndex = 1,
            int pageSize = 3) {
            var query = _productRepository.GetTable();

            var model = await PaginatedList<Product>.CreateAsync(query.AsNoTracking(), pageIndex, pageSize);

            return View(model);
        }

        [HttpGet("TestList")]
        public ActionResult<List<TestModel>> GetTestList() {
            var model = _testService.GetTestList();
            return Ok(model);
        }

        public IActionResult Privacy() {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }

        #endregion
    }
}