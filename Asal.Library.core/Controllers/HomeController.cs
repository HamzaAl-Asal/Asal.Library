using Asal.Library.core.Interfaces;
using Asal.Library.core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Asal.Library.core.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly IPackageService packageService;

        public HomeController(ILogger<HomeController> logger, IPackageService packageService)
        {
            this.logger = logger;
            this.packageService = packageService;
        }

        public async Task<IActionResult> Index()
        {
            var packages = await Task.FromResult(await packageService.GetPackages());
            return View(packages);
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
