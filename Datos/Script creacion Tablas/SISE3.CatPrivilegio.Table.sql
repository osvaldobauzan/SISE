USE [SISE_NEW]
GO
/****** Object:  Table [SISE3].[CatPrivilegio]    Script Date: 29/02/2024 10:05:38 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [SISE3].[CatPrivilegio](
	[IdPrivilegio] [int] IDENTITY(1,1) NOT NULL,
	[sNombrePrivilegio] [nvarchar](150) NULL,
	[sDescripcion] [nvarchar](150) NULL,
	[sModulo] [nvarchar](50) NULL,
	[bEstatus] [smallint] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdPrivilegio] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
