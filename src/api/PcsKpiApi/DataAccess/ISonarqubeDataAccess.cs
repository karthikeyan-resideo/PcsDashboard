using PcsKpiApi.Models;

namespace PcsKpiApi.DataAccess
{
    public interface ISonarqubeDataAccess
    {
        Task AddSonarqubeCoverage(string projectId, string coverage);
        Task<List<CodeCoverageDataModel>> GetCoverage();
        Task AddSonarqubeIssues(string projectId, int info, int blocker, int minor, int major, int critical, int codesmell, int bug, int vul);
        Task<List<SonarqubeCoverageDetail>> GetSonarqubeCoverageDetail();
    }
}
