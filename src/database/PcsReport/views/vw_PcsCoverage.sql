CREATE VIEW [dbo].[vw_PcsCoverage]
AS  
   SELECT SC.ProjectId
		 ,SC.CoveragePercentage
		 ,SC.BuildTime
   FROM [dbo].[PcsSonarqubeCoverage] SC
GO