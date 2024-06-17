USE [SISE_NEW]
GO
/****** Object:  UserDefinedTableType [SISE3].[PromocionesAcuerdo_type]    Script Date: 29/02/2024 10:05:38 p. m. ******/
CREATE TYPE [SISE3].[PromocionesAcuerdo_type] AS TABLE(
	[NumeroOrden] [int] NULL,
	[YearPromocion] [int] NULL,
	[EstadoPromocionId] [int] NULL,
	[Proceso] [smallint] NULL
)
GO
