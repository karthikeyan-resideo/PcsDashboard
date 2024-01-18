using Microsoft.EntityFrameworkCore;
using PcsKpiDashboard.Models;

namespace PcsKpiDashboard
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public virtual DbSet<CoverageView> CoverageViews { get; set; }
        public virtual DbSet<ProjectView> ProjectViews { get; set; }
        public virtual DbSet<DetailView> DetailViews { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder == null)
            {
                throw new ArgumentNullException(nameof(modelBuilder));
            }
            modelBuilder.Entity<CoverageView>().ToView("vw_PcsCoverage", "dbo").HasNoKey();
            modelBuilder.Entity<ProjectView>().ToView("vw_PcsProject", "dbo").HasNoKey();
            modelBuilder.Entity<DetailView>().ToView("vw_PcsIssueDetail", "dbo").HasNoKey();
            base.OnModelCreating(modelBuilder);
        }
    }
}
