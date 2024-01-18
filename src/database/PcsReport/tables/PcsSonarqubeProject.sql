CREATE TABLE [dbo].[PcsSonarqubeProject]
(
	[Id] INT IDENTITY(1,1) NOT NULL,
	[ProjectName] varchar(200) NOT NULL,
	[SonarqubeProjectId] varchar(200) NOT NULL,
	CONSTRAINT [PK_PcsSonarQubeProject] PRIMARY KEY CLUSTERED ([Id] ASC)
)
