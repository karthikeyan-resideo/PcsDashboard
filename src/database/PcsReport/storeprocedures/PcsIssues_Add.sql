CREATE PROCEDURE [dbo].[PcsIssues_Add]
@ProjectId VARCHAR(200),
@Info INT,
@Blocker INT,
@Minor INT,
@Major INT,
@Critical INT,
@CodeSmell INT,
@Bug INT,
@Vul INT
AS
BEGIN
  DECLARE @ProjectIdInt INT
	IF NOT EXISTS (SELECT 1 FROM PcsSonarqubeProject WHERE SonarqubeProjectId = @ProjectId)
	BEGIN 
		RETURN -1
	END
    SELECT @ProjectIdInt = Id FROM PcsSonarqubeProject WHERE SonarqubeProjectId = @ProjectId
	INSERT INTO PcsSonarQubeDetail(ProjectId,CodeSmells,Bugs,Vulnerabilities,Minor,Major,[Critical],Blocker,Info,CreatedDate)
	VALUES (@ProjectIdInt,@CodeSmell,@Bug,@Vul,@Minor,@Major,@Critical,@Blocker,@Info,GETUTCDATE())
	RETURN 0
END
GO