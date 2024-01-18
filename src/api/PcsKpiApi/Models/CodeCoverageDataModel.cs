namespace PcsKpiApi.Models
{
    public class CodeCoverageDataModel
    {
        public string ProjectName { get; set; }
        public string BranchName { get; set; }
        public string CoveragePercentage { get; set; }
        public DateTime BuildTime { get; set; }
        public string Color { get; set; }
        public string PercentageName { get; set; }
    }
    public class CoverageResult
    {
        public int Count { get; set; }
        public List<CodeCoverageDataModel> CodeCoverageDatas { get; set; }
    }
    public class CoverageCount
    {
        public string PercentageName { get; set; }
        public string Color { get; set; }
    }
    public class SonarqubeCoverageDetail
    {
        public string ProjectName { get; set; }
        public string SonarqubeProjectId { get; set; }
        public int CodeSmells { get; set; }
        public int Bugs { get; set; }
        public int Vulnerabilities { get; set; }
        public int Securityhotspots { get; set; }
        public int Minor { get; set; }
        public int Major { get; set; }
        public int Critical { get; set; }
        public int Blocker { get; set; }
        public int Info { get; set; }
    }
    public class TotalIssuesCount
    {
        public int TotalBugs { get; set; } = 0;
        public int TotalVulnerabilities { get; set; } = 0;
        public int TotalMinor { get; set; } = 0;
        public int TotalMajor { get; set; } = 0;
        public int TotalCritical { get; set; } = 0;
        public int TotalBlocker { get; set; } = 0;
        public int TotalInfo { get; set; } = 0;
    }
}
