﻿CREATE TABLE [dbo].[Data]
(
	[Id]   INT          IDENTITY (1, 1) NOT NULL, 
    [data] VARCHAR(MAX) NOT NULL
	PRIMARY KEY CLUSTERED ([Id] ASC)
)
