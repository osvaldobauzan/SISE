USE [SISE_NEW]
GO
/****** Object:  Table [SISE3].[REL_PrivilegioXRol]    Script Date: 29/02/2024 10:05:38 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [SISE3].[REL_PrivilegioXRol](
	[IdPrivilegioxRol] [int] IDENTITY(1,1) NOT NULL,
	[IdRol] [int] NOT NULL,
	[IdPrivilegio] [int] NOT NULL,
	[bEstatus] [bit] NULL,
	[fFechaAlta] [datetime] NULL,
	[fFechaBaja] [datetime] NULL
) ON [PRIMARY]
GO
ALTER TABLE [SISE3].[REL_PrivilegioXRol]  WITH CHECK ADD  CONSTRAINT [FK_REL_PrivilegioXRol_IdPrivilegio] FOREIGN KEY([IdPrivilegio])
REFERENCES [SISE3].[CatPrivilegio] ([IdPrivilegio])
GO
ALTER TABLE [SISE3].[REL_PrivilegioXRol] CHECK CONSTRAINT [FK_REL_PrivilegioXRol_IdPrivilegio]
GO
ALTER TABLE [SISE3].[REL_PrivilegioXRol]  WITH CHECK ADD  CONSTRAINT [FK_REL_PrivilegioXRol_IdRol] FOREIGN KEY([IdRol])
REFERENCES [SISE3].[CatRol] ([IdRol])
GO
ALTER TABLE [SISE3].[REL_PrivilegioXRol] CHECK CONSTRAINT [FK_REL_PrivilegioXRol_IdRol]
GO
