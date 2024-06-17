using Documentos.Application.Common.Model.GenerarEvidenciaFirma;

namespace Documentos.Application.FirmadorDocumentos.Comandos.GenerarEvidenciaFirma;
public interface IHojaFirmasService
{
    GenerarEvidienciaFirmaDTO GenerarHojaFirmas(GenerarEvidienciaFirmaFiltro request);
}