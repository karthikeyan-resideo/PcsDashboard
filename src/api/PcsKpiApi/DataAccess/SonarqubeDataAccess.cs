using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using PcsKpiApi.Models;
using System.Data;
using Titan.UFC.Common.ExceptionMiddleWare;

namespace PcsKpiApi.DataAccess
{
    public class SonarqubeDataAccess : ISonarqubeDataAccess
    {
        readonly ApplicationDbContext _context;
        readonly DatabaseFacade _database;
        readonly IConfiguration _configuration;
        public SonarqubeDataAccess(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _database = context.Database;
            _configuration = configuration;
        }
        public async Task AddSonarqubeCoverage(string projectId, string coverage)
        {
            SqlParameter projectParams = new("@ProjectId", SqlDbType.VarChar)
            {
                Value = projectId
            };
            SqlParameter coverageParams = new("@Coverage", SqlDbType.VarChar)
            {
                Value = coverage
            };
            int result = await _database.ExecuteSqlRawAsync("[dbo].[PcsSonarqubeCoverage_Add] @ProjectId,@Coverage", projectParams, coverageParams);
            if (result < 0)
            {
                throw new TitanCustomException(StatusCodes.Status400BadRequest, "New Project need to be added in Db");

            }
        }
        public async Task<List<CodeCoverageDataModel>> GetCoverage()
        {
            List<CodeCoverageDataModel> codeCoverages = new();
            var result = await _context.CoverageViews.AsNoTracking().GroupBy(x => x.ProjectId)
                           .Select(group => group.OrderByDescending(x => x.BuildTime).FirstOrDefault()).ToListAsync();
            var projects = await _context.ProjectViews.AsNoTracking().AsQueryable().ToListAsync();
            result.ForEach(coverageData =>
            {
                CoverageCount coverageCount = GetColor(Convert.ToDecimal(coverageData.CoveragePercentage));
                codeCoverages.Add(new()
                {
                    BranchName = _configuration["BranchName"],
                    BuildTime = coverageData.BuildTime,
                    Color = coverageCount.Color,
                    PercentageName = coverageCount.PercentageName,
                    CoveragePercentage = coverageData.CoveragePercentage,
                    ProjectName = projects.FirstOrDefault(x => x.Id == coverageData.ProjectId).ProjectName
                });
            });
            return codeCoverages.OrderBy(x => x.ProjectName).ToList();
        }
        public async Task<List<SonarqubeCoverageDetail>> GetSonarqubeCoverageDetail()
        {
            List<SonarqubeCoverageDetail> coverageDetails = new();
            var result = await _context.DetailViews.AsNoTracking().GroupBy(x => x.ProjectId)
                            .Select(group => group.OrderByDescending(x => x.CreatedDate).FirstOrDefault()).ToListAsync();
            var projects = await _context.ProjectViews.AsNoTracking().AsQueryable().ToListAsync();
            result.ForEach(detailData =>
            {
                coverageDetails.Add(new()
                {
                    ProjectName = projects.FirstOrDefault(x => x.Id == detailData.ProjectId).ProjectName,
                    Info = detailData.Info,
                    CodeSmells = detailData.CodeSmells,
                    Blocker = detailData.Blocker,
                    Critical = detailData.Critical,
                    Minor = detailData.Minor,
                    Major = detailData.Major,
                    Vulnerabilities = detailData.Vulnerabilities,
                    Bugs = detailData.Bugs,
                    SonarqubeProjectId = projects.FirstOrDefault(x => x.Id == detailData.ProjectId).SonarqubeProjectId
                });
            });
            return coverageDetails.OrderBy(x => x.ProjectName).ToList();
        }
        public async Task AddSonarqubeIssues(string projectId, int info, int blocker, int minor, int major, int critical, int codesmell, int bug, int vul)
        {
            SqlParameter projectParam = new("@ProjectId", SqlDbType.VarChar)
            {
                Value = projectId
            };
            SqlParameter infoParam = new("@Info", SqlDbType.Int)
            {
                Value = info
            };
            SqlParameter blockerParam = new("@Blocker", SqlDbType.Int)
            {
                Value = blocker
            };
            SqlParameter minorParam = new("@Minor", SqlDbType.Int)
            {
                Value = minor
            };
            SqlParameter majorParam = new("@Major", SqlDbType.Int)
            {
                Value = major
            };
            SqlParameter criticalParam = new("@Critical", SqlDbType.Int)
            {
                Value = critical
            };
            SqlParameter codesmellParam = new("@CodeSmell", SqlDbType.Int)
            {
                Value = codesmell
            };
            SqlParameter bugParam = new("@Bug", SqlDbType.Int)
            {
                Value = bug
            };
            SqlParameter vulParam = new("@Vul", SqlDbType.Int)
            {
                Value = vul
            };

            int result = await _database.ExecuteSqlRawAsync("[dbo].[PcsIssues_Add] @ProjectId,@Info,@Blocker,@Minor,@Major,@Critical,@CodeSmell,@Bug,@Vul",
                       projectParam, infoParam, blockerParam, minorParam, majorParam, criticalParam, codesmellParam, bugParam, vulParam);
            if (result < 0)
            {
                throw new TitanCustomException(StatusCodes.Status400BadRequest, "New Project need to be added in Db");
            }
        }
        static CoverageCount GetColor(decimal coveragePercentage)
        {
            CoverageCount coverageCount = new CoverageCount();
            if (coveragePercentage <= 69)
            {
                coverageCount.PercentageName = "Less Than 70%";
                coverageCount.Color = "red";
            }
            else if (coveragePercentage >= 70 && coveragePercentage <= 79)
            {
                coverageCount.PercentageName = "Greater than 70% and Less Than 79%";
                coverageCount.Color = "yellow";
            }
            else if (coveragePercentage >= 80 && coveragePercentage <= 89)
            {
                coverageCount.PercentageName = "Greater than 80% and Less Than 89%";
                coverageCount.Color = "lightgreen";
            }
            else
            {
                coverageCount.PercentageName = "Greater Than 90%";
                coverageCount.Color = "green";
            }
            return coverageCount;
        }
    }
}
