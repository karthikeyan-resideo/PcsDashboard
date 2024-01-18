using PcsKpiDashboard.Models;

namespace PcsKpiDashboard.Services
{
    public interface ISonarqubeService
    {
        Task AddSonarqubeCoverage(string projectId, string coverage);
        Task<CoverageResult> GetCoverage();
        Task AddSonarqubeIssues(string projectId, int info, int blocker, int minor, int major, int critical, int codesmell, int bug, int vul);
        Task<List<SonarqubeCoverageDetail>> GetSonarqubeCoverageDetail();
        Task<TotalIssuesCount> GetTotalIssuesCount();
        Task<DataResult> GetDashBoardData();
        Task<List<SonarQubeDetailResult>> GetSonarqubeReport();
    }
}
