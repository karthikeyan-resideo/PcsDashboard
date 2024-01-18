CREATE VIEW [dbo].[vw_PcsIssueDetail]
AS
  SELECT TOP 7 ProjectId
        ,CodeSmells
		,Bugs
		,Vulnerabilities
		,Minor
		,Major
		,Critical
		,Blocker
		,Info
		,CreatedDate
  FROM PcsSonarQubeDetail
  ORDER BY CreatedDate DESC
GO