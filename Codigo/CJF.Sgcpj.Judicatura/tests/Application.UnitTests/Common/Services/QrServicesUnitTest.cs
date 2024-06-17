using NUnit.Framework;
using FluentAssertions;

using CJF.Sgcpj.Judicatura.Infrastructure.Services;
using CJF.Sgcpj.Judicatura.Application.Common.Interfaces;

namespace CJF.Sgcpj.Judicatura.Application.UnitTests.Common.Services;

public class QrGeneratorUnitTest
{

    [Test]
    public void ShouldGenerateaQr()
    {
        //Arrage
        IGeneradorQR generadorQR = new GeneradorQRService();
        var textoACodificar = "{\r\n\t\"Expediente\": {\r\n\t\t\"AsuntoNeunId\": 23058224,\r\n\t\t\"AsuntoAlias\": \"10/2023\",\r\n\t\t\"CatTipoAsuntoId\": \"10\",\r\n\t\t\"CatTipoAsunto\": \"Amparo indirecto\",\r\n\t\t\"TipoProcedimientoId\": \"\",\r\n\t\t\"TipoProcedimiento\": \"\",\r\n\t\t\"CatOrganismoId\": 1494,\r\n\t\t\"CatOrganismo\": \"Juzgado Segundo de Distrito en Materia Civil en la Ciudad de Mexico\"\r\n\t},\r\n\t\"Promocion\":{\r\n\t\t\"CuadernoId\": 5,\r\n\t\t\"Cuaderno\": \"Principal\",\r\n\t\t\"FechaPresentacion\": \"2023-05-04T00:00:00\",\r\n\t\t\"NumeroRegistro\": \"1230\",\r\n\t\t\"YearPromocion\": 2023\r\n\t}\r\n}";
        int pixelesPorModulo = 4;
        //Act
        var resultado = generadorQR.GenerarQr(textoACodificar,pixelesPorModulo);

        string utfString = Convert.ToBase64String(resultado);
        //Assert
        resultado.Should().NotBeNull();
        utfString.Should().NotBeNullOrEmpty();
        
    }
}
