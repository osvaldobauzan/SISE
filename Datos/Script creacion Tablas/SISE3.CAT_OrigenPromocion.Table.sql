USE [SISE_NEW]
GO
/****** Object:  Table [SISE3].[CAT_OrigenPromocion]    Script Date: 29/02/2024 10:05:38 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [SISE3].[CAT_OrigenPromocion](
	[kIdOrigenPromocion] [int] NOT NULL,
	[sNombreOrigenPromocion] [varchar](25) NULL,
 CONSTRAINT [PK_CAT_OrigenPromocion] PRIMARY KEY CLUSTERED 
(
	[kIdOrigenPromocion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
