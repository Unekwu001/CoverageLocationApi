CREATE TABLE [dbo].[CoverageLocations]
(
	[Id] INT PRIMARY KEY IDENTITY (1, 1),
	[state] VARCHAR(255) NOT NULL,
	[coverageName] VARCHAR(500)NOT NULL,
	[lga] VARCHAR(255) NOT NULL,
	[longitude] FLOAT NOT NULL,
	[latitude] FLOAT NOT NULL
)
