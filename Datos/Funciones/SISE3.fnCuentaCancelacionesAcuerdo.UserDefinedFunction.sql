USE [SISE_NEW]
GO
/****** Object:  UserDefinedFunction [SISE3].[fnCuentaCancelacionesAcuerdo]    Script Date: 29/02/2024 10:05:38 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Sergio Orozco
-- Create date: 29/02/2024
-- Description:	Cuenta las cancelaciones de un acuerdo
-- Example: SELECT SISE3.fnCuentaCancelacionesAcuerdo( )
-- =============================================
CREATE   FUNCTION [SISE3].[fnCuentaCancelacionesAcuerdo]
(
	@pi_AsuntoNeunId BIGINT,
    @pi_AsuntoDocumentoId BIGINT
)
RETURNS INT
AS
BEGIN

	DECLARE @Count INT
    
    SET @Count = (Select count(*)
        from SISE_NEWLOG.dbo.BitacoraAutorizacionDocumentos bad
        where 
        bad.CatAutorizacionDocumentosId in (4,8,9)
        and bad.AsuntoNeunId = @pi_AsuntoNeunId
        and bad.AsuntoDocumentoId = @pi_AsuntoDocumentoId
        group by bad.AsuntoNeunId, bad.AsuntoDocumentoId
        )

	RETURN @Count

END
GO
