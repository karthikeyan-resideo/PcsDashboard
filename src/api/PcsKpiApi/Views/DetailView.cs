namespace PcsKpiApi.Views
{
    public class DetailView
    {
        public int ProjectId { get; set; }
        public int CodeSmells { get; set; }
        public int Bugs { get; set; }
        public int Vulnerabilities { get; set; }
        public int Minor { get; set; }
        public int Major { get; set; }
        public int Critical { get; set; }
        public int Blocker { get; set; }
        public int Info { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
