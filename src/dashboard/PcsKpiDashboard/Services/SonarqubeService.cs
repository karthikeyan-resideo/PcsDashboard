using PcsKpiDashboard.DataAccess;
using PcsKpiDashboard.Models;

namespace PcsKpiDashboard.Services
{
    public class SonarqubeService : ISonarqubeService
    {
        readonly ISonarqubeDataAccess _dataAccess;
        readonly IConfiguration _configuration;
        public SonarqubeService(ISonarqubeDataAccess dataAccess, IConfiguration configuration)
        {
            _dataAccess = dataAccess;
            _configuration = configuration;
        }
        public async Task AddSonarqubeCoverage(string projectId, string coverage)
        {
            await _dataAccess.AddSonarqubeCoverage(projectId, coverage.Replace("-", "."));
        }
        public async Task<CoverageResult> GetCoverage()
        {
            return new CoverageResult
            {
                Count = _configuration.GetValue<int>("ProjectCount"),
                CodeCoverageDatas = await _dataAccess.GetCoverage()
            };
        }
        public async Task AddSonarqubeIssues(string projectId, int info, int blocker, int minor, int major, int critical, int codesmell, int bug, int vul)
        {
            await _dataAccess.AddSonarqubeIssues(projectId, info, blocker, minor, major, critical, codesmell, bug, vul);
        }
        public async Task<List<SonarqubeCoverageDetail>> GetSonarqubeCoverageDetail()
        {
            return await _dataAccess.GetSonarqubeCoverageDetail();
        }
        public async Task<TotalIssuesCount> GetTotalIssuesCount()
        {
            TotalIssuesCount totalIssues = new();
            var coverageDetails = await GetSonarqubeCoverageDetail();
            coverageDetails.ForEach(detail =>
            {
                totalIssues.TotalBlocker += detail.Blocker;
                totalIssues.TotalBugs += detail.Bugs;
                totalIssues.TotalCritical += detail.Critical;
                totalIssues.TotalMajor += detail.Major;
                totalIssues.TotalMinor += detail.Minor;
                totalIssues.TotalInfo += detail.Info;
                totalIssues.TotalVulnerabilities += detail.Vulnerabilities;
            });
            return totalIssues;
        }
        public async Task<DataResult> GetDashBoardData()
        {
            decimal totalPercentage = 0;
            var coverageDatas = await GetCoverage();
            foreach (var coverageData in coverageDatas.CodeCoverageDatas)
            {
                totalPercentage += Convert.ToDecimal(coverageData.CoveragePercentage);
            }
            return new()
            {
                CoverageDataResults = coverageDatas.CodeCoverageDatas,
                Average = Math.Round((totalPercentage / coverageDatas.Count), 2),
                TotalIssuesCount = await GetTotalIssuesCount()
            };
        }
        public async Task<List<SonarQubeDetailResult>> GetSonarqubeReport()
        {
            var result = new List<SonarQubeDetailResult>();
            var sonarqubeReport = await GetSonarqubeCoverageDetail();
            sonarqubeReport.ForEach(x =>
            {
                result.Add(new()
                {
                    ProjectName = x.ProjectName,
                    Blocker = x.Blocker,
                    CodeSmells = x.CodeSmells,
                    Info = x.Info,
                    Bugs = x.Bugs,
                    Critical = x.Critical,
                    Minor = x.Minor,
                    Major = x.Major,
                    Vulnerabilities = x.Vulnerabilities,
                    ReferenceUrl = $"https://sonarqube.resideo.com/dashboard?branch=develop&id={x.SonarqubeProjectId}"
                });
            });
            return result;
        }
    }
}
