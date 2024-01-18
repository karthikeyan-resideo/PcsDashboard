using Microsoft.AspNetCore.Mvc;
using PcsKpiApi.Services;
using Titan.UFC.Logger;

namespace PcsKpiApi.Controllers
{
    [ApiController]
    [Route("api/sonarqube")]
    public class SonarqubeController : ControllerBase
    {
        readonly ISonarqubeService _service;
        readonly ICustomLogger _logger;
        public SonarqubeController(ISonarqubeService service, ICustomLogger logger)
        {
            _service = service;
            _logger = logger;
        }
        [HttpPost("{projectId}/coverage/{coverage}")]
        public async Task<IActionResult> AddSonarqubeCoverage(string projectId, string coverage)
        {
            await _service.AddSonarqubeCoverage(projectId, coverage);
            return Ok();
        }
        [HttpPost("{projectId}/issues/{info}/{blocker}/{minor}/{major}/{critical}/{codesmell}/{bug}/{vul}")]
        public async Task<IActionResult> AddSonarIssues(string projectId, int info, int blocker, int minor, int major, int critical, int codesmell, int bug, int vul)
        {
            await _service.AddSonarqubeIssues(projectId, info, blocker, minor, major, critical, codesmell, bug, vul);
            return Ok();
        }
        [HttpGet("coverage")]
        public async Task<IActionResult> GetCoverage()
        {
            return Ok(await _service.GetCoverage());
        }
        [HttpGet]
        public async Task<IActionResult> GetSonarqubeCoverageDetail()
        {
            return Ok(await _service.GetSonarqubeCoverageDetail());
        }
        [HttpGet("~/privateapi/sonarqube/totalcount")]
        public async Task<IActionResult> GetTotalIssueCount()
        {
            return Ok(await _service.GetTotalIssuesCount());
        }
    }
}
