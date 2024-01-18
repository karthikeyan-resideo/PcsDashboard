CREATE PROCEDURE [dbo].[PcsSonarqubeCoverage_Add]
@ProjectId VARCHAR(200),
@Coverage VARCHAR(10)
AS
BEGIN
	DECLARE @ProjectIdInt INT
	IF NOT EXISTS (SELECT 1 FROM PcsSonarqubeProject WHERE SonarqubeProjectId = @ProjectId)
	BEGIN 
		RETURN -1
	END
    SELECT @ProjectIdInt = Id FROM PcsSonarqubeProject WHERE SonarqubeProjectId = @ProjectId
	INSERT INTO PcsSonarqubeCoverage(ProjectId,CoveragePercentage,BuildTime) VALUES (@ProjectIdInt,@Coverage,GETUTCDATE())
	RETURN 0
END
GO