USE [SISE_NEW]
GO
/****** Object:  Table [SISE3].[PromocionPromoventeParte]    Script Date: 29/02/2024 10:05:38 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [SISE3].[PromocionPromoventeParte](
	[PromoventeParteId] [int] IDENTITY(1,1) NOT NULL,
	[CatOrganismoId] [int] NULL,
	[YearPromocion] [int] NULL,
	[NumeroOrden] [int] NULL,
	[AsuntoNeunId] [bigint] NULL,
	[PromoventeId] [int] NULL,
	[PersonaId] [int] NULL,
	[FechaAlta] [datetime] NULL,
	[FechaBaja] [datetime] NULL,
	[StatusReg] [smallint] NULL,
	[UsuarioCaptura] [bigint] NULL,
	[IsMigrated] [bit] NULL
) ON [PRIMARY]
GO
