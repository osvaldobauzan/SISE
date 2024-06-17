USE [SISE_NEW]
GO
/****** Object:  Table [SISE3].[REL_RolEmpleadoXOrganismo]    Script Date: 29/02/2024 10:05:38 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [SISE3].[REL_RolEmpleadoXOrganismo](
	[IdEmpleadoRol] [int] IDENTITY(1,1) NOT NULL,
	[IdRol] [int] NULL,
	[IdOrganismo] [int] NULL,
	[IdCatEmpleado] [bigint] NULL,
	[fFechaAlta] [datetime] NOT NULL,
	[fFechaBaja] [datetime] NULL,
	[bStatus] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdEmpleadoRol] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [Unique_Rel_EmpleadoOrganismo] UNIQUE NONCLUSTERED 
(
	[IdRol] ASC,
	[IdOrganismo] ASC,
	[IdCatEmpleado] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [SISE3].[REL_RolEmpleadoXOrganismo]  WITH CHECK ADD  CONSTRAINT [FK_IdRol] FOREIGN KEY([IdRol])
REFERENCES [SISE3].[CatRol] ([IdRol])
GO
ALTER TABLE [SISE3].[REL_RolEmpleadoXOrganismo] CHECK CONSTRAINT [FK_IdRol]
GO
ALTER TABLE [SISE3].[REL_RolEmpleadoXOrganismo]  WITH CHECK ADD  CONSTRAINT [FK_REL_Rol_CatEmpleados] FOREIGN KEY([IdCatEmpleado])
REFERENCES [dbo].[CatEmpleados] ([EmpleadoId])
GO
ALTER TABLE [SISE3].[REL_RolEmpleadoXOrganismo] CHECK CONSTRAINT [FK_REL_Rol_CatEmpleados]
GO
ALTER TABLE [SISE3].[REL_RolEmpleadoXOrganismo]  WITH CHECK ADD  CONSTRAINT [FK_REL_Rol_CatOrganismoId] FOREIGN KEY([IdOrganismo])
REFERENCES [dbo].[CatOrganismos] ([CatOrganismoId])
GO
ALTER TABLE [SISE3].[REL_RolEmpleadoXOrganismo] CHECK CONSTRAINT [FK_REL_Rol_CatOrganismoId]
GO
