namespace PcsKpiApi.Views
{
    public class CoverageView
    {
        public int ProjectId { get; set; }
        public string CoveragePercentage { get; set; }
        public DateTime BuildTime { get; set; }
    }
}
