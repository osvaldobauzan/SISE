USE [SISE_NEW]
GO
/****** Object:  Table [SISE3].[UNCXOrganismo]    Script Date: 29/02/2024 10:05:38 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [SISE3].[UNCXOrganismo](
	[IdUNC] [int] IDENTITY(1,1) NOT NULL,
	[CatOrganismoId] [int] NOT NULL,
	[FechaAlta] [datetime] NULL,
	[FechaBaja] [datetime] NULL,
	[StatusReg] [smallint] NULL,
	[Descripcion] [varchar](150) NULL
) ON [PRIMARY]
GO
ALTER TABLE [SISE3].[UNCXOrganismo]  WITH CHECK ADD  CONSTRAINT [FK_CatOrganismoId] FOREIGN KEY([CatOrganismoId])
REFERENCES [dbo].[CatOrganismos] ([CatOrganismoId])
GO
ALTER TABLE [SISE3].[UNCXOrganismo] CHECK CONSTRAINT [FK_CatOrganismoId]
GO
