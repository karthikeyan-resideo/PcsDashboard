using Microsoft.AspNetCore.Mvc;
using PcsKpiDashboard.Models;
using PcsKpiDashboard.Services;
using System.Diagnostics;
using System.Text.Json;

namespace PcsKpiDashboard.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        readonly ISonarqubeService _sonarqubeService;

        public HomeController(ILogger<HomeController> logger, ISonarqubeService sonarqubeService)
        {
            _logger = logger;
            _sonarqubeService = sonarqubeService;
        }

        public async Task<IActionResult> Index()
        {
            _logger.LogInformation($"Main page");
            var result = await _sonarqubeService.GetDashBoardData();
            ViewBag.Coverage = JsonSerializer.Serialize(result.CoverageDataResults);
            ViewBag.Average = result.Average;
            ViewBag.TotalIssueCount = JsonSerializer.Serialize(result.TotalIssuesCount);
            return View();
        }
        public async Task<IActionResult> UtcReport()
        {
            var result = await _sonarqubeService.GetCoverage();
            return View(result.CodeCoverageDatas);
        }
        public async Task<IActionResult> SonarqubeReport()
        {
            return View(await _sonarqubeService.GetSonarqubeReport());
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
