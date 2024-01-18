CREATE TABLE [dbo].[PcsSonarQubeDetail]
(
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProjectId] [int] NOT NULL,
	[CodeSmells] [int] NOT NULL,
	[Bugs] [int] NOT NULL,
	[Vulnerabilities] [int] NOT NULL,
	[Securityhotspots] [int] NOT NULL,
	[Minor] [int] NOT NULL,
	[Major] [int] NOT NULL,
	[Critical] [int] NOT NULL,
	[Blocker] [int] NOT NULL,
	[Info] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	CONSTRAINT [PK_PcsSonarQubeDetail] PRIMARY KEY CLUSTERED ([Id] ASC)
)
