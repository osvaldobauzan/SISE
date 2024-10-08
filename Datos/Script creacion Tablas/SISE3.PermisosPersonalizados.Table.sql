USE [SISE_NEW]
GO
/****** Object:  Table [SISE3].[PermisosPersonalizados]    Script Date: 29/02/2024 10:05:38 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [SISE3].[PermisosPersonalizados](
	[IdPermisosPersonalizados] [int] IDENTITY(1,1) NOT NULL,
	[IdEmpleadoRol] [int] NOT NULL,
	[IdPrivilegio] [int] NOT NULL,
	[bDeniega_Agrega] [smallint] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdPermisosPersonalizados] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [SISE3].[PermisosPersonalizados]  WITH CHECK ADD  CONSTRAINT [FK_REL_RolEmpleadoXOrganismo_CatPrivilegio] FOREIGN KEY([IdPrivilegio])
REFERENCES [SISE3].[CatPrivilegio] ([IdPrivilegio])
GO
ALTER TABLE [SISE3].[PermisosPersonalizados] CHECK CONSTRAINT [FK_REL_RolEmpleadoXOrganismo_CatPrivilegio]
GO
ALTER TABLE [SISE3].[PermisosPersonalizados]  WITH CHECK ADD  CONSTRAINT [FK_REL_RolEmpleadoXOrganismo_IdEmpleadoRol] FOREIGN KEY([IdEmpleadoRol])
REFERENCES [SISE3].[REL_RolEmpleadoXOrganismo] ([IdEmpleadoRol])
GO
ALTER TABLE [SISE3].[PermisosPersonalizados] CHECK CONSTRAINT [FK_REL_RolEmpleadoXOrganismo_IdEmpleadoRol]
GO
