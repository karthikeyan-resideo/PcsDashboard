CREATE VIEW [dbo].[vw_PcsProject]
AS
  SELECT SP.Id 
		,SP.ProjectName
		,SP.SonarqubeProjectId
  FROM PcsSonarqubeProject SP
GO