create database Fun_FightersGOUSE [Fun_Fighters]GO/****** Object:  Table [dbo].[fighters]    Script Date: 3/7/2016 9:19:08 AM ******/SET ANSI_NULLS ONGOSET QUOTED_IDENTIFIER ONGOSET ANSI_PADDING ONGOCREATE TABLE [dbo].[fighters](	[id] [int] IDENTITY(1,1) NOT NULL,	[name] [varchar](255) NULL,	[wins] [int] NULL,	[losses] [int] NULL,	[image] [varchar](255) NULL,	[hp] [int] NULL,	[mp] [int] NULL,	[attack] [int] NULL,	[speed] [int] NULL,	[accuracy] [int] NULL,	[luck] [int] NULL) ON [PRIMARY]GOSET ANSI_PADDING OFFGO