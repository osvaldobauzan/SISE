USE [SISE_NEW]
GO
/****** Object:  Table [SISE3].[CAT_TipoArea]    Script Date: 29/02/2024 10:05:38 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [SISE3].[CAT_TipoArea](
	[kIdTipoArea] [int] IDENTITY(1,1) NOT NULL,
	[sTipoArea] [varchar](50) NOT NULL,
 CONSTRAINT [PK_SISE3_kId_TipoArea] PRIMARY KEY CLUSTERED 
(
	[kIdTipoArea] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionColumn', @value=N'Identificador del tipo de area' , @level0type=N'SCHEMA',@level0name=N'SISE3', @level1type=N'TABLE',@level1name=N'CAT_TipoArea', @level2type=N'COLUMN',@level2name=N'kIdTipoArea'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionColumn', @value=N'Nombre tipo de area' , @level0type=N'SCHEMA',@level0name=N'SISE3', @level1type=N'TABLE',@level1name=N'CAT_TipoArea', @level2type=N'COLUMN',@level2name=N'sTipoArea'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionTable', @value=N'Identificador del tipo de area' , @level0type=N'SCHEMA',@level0name=N'SISE3', @level1type=N'TABLE',@level1name=N'CAT_TipoArea'
GO
