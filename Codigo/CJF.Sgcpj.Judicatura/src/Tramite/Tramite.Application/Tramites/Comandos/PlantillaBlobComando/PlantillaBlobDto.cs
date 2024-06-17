using CJF.Sgcpj.Judicatura.Common.Application.Common.Mappings;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Models;

namespace CJF.Sgcpj.Judicatura.Tramite.Application.Tramites.Comandos.PlantillaBlobComando;
public class PlantillaBlobDto : IMapFrom<PlantillaBlob>
{
    public string ArchivoId { get; set; }
}
