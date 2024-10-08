USE [SISE_NEW]
GO
/****** Object:  Table [SISE3].[HIS_RecepcionOficio]    Script Date: 29/02/2024 10:05:38 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [SISE3].[HIS_RecepcionOficio](
	[kIdRecepcionOficio] [bigint] IDENTITY(1,1) NOT NULL,
	[FechaRecepcion] [datetime] NULL,
	[IdEmpleadoRecepcion] [bigint] NULL,
	[fkAnexoId] [bigint] NULL,
	[AsuntoNeunId] [bigint] NULL,
	[fkCatOrganismoId] [int] NULL,
	[StatusReg] [bit] NULL,
 CONSTRAINT [pk_kIdRecepcionOficio_HIS_RecepcionOficio] PRIMARY KEY CLUSTERED 
(
	[kIdRecepcionOficio] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
