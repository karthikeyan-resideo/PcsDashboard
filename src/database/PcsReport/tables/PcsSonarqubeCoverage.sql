CREATE TABLE [dbo].[PcsSonarqubeCoverage]
(
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProjectId] [int] NOT NULL,
	[CoveragePercentage] [varchar](10) NOT NULL,
	[BuildTime] [datetime] NOT NULL,
	CONSTRAINT [PK_PcsSonarqubeCoverage] PRIMARY KEY CLUSTERED ([Id] ASC)
)