USE [pokemon_db]
GO

/****** Object:  Table [dbo].[pokemon]    Script Date: 4/5/2023 8:25:13 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[pokemon](
	[pokemon_number] [int] NOT NULL,
	[name] [nchar](30) NOT NULL,
	[type1] [nchar](15) NOT NULL,
	[type2] [nchar](15) NULL,
	[total] [int] NOT NULL,
	[hp] [int] NOT NULL,
	[attack] [int] NOT NULL,
	[defense] [int] NOT NULL,
	[sp_attack] [int] NOT NULL,
	[sp_defense] [int] NOT NULL,
	[speed] [int] NOT NULL,
	[generation] [int] NOT NULL,
	[legendary] [bit] NOT NULL
) ON [PRIMARY]
GO


