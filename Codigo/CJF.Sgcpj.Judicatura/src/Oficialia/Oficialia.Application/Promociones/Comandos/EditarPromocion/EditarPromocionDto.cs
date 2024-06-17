using CJF.Sgcpj.Judicatura.Application.Catalogos.Contenido.Consulta;
using CJF.Sgcpj.Judicatura.Application.Catalogos.ParteExistente.Consulta;
using CJF.Sgcpj.Judicatura.Application.Common.Mappings;
using CJF.Sgcpj.Judicatura.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Application.Cuadernos.Consulta;
using CJF.Sgcpj.Judicatura.Application.Usuarios.PromoventeExistente.Consulta;
using CJF.Sgcpj.Judicatura.Oficialia.Application.Common.Models;

namespace CJF.Sgcpj.Judicatura.Application.Promociones.Comandos
{
    public class EditarPromocionDto : IMapFrom<EditarPromocion>
    {
        public long AsuntoNeunId { get; set; }
        public int Origen { get; set; }
        public string NumeroExpediente { get; set; }
        public CatalogoAsuntoDto TipoAsunto { get; set; }
        public string NumeroOCC { get; set; }
        public ProcedimientoDto TipoProcedimiento { get; set; }
        public CuadernoDto Cuaderno { get; set; }
        public ContenidoDto Contenido { get; set; }
        public int Registro { get; set; }
        public int TipoId { get; set; }
        public string FechaPresentacion { get; set; }
        public string HoraPresentacion { get; set; }
        public int? Copias { get; set; }
        public int? Fojas { get; set; }
        //public int? Anexos { get; set; }
        public int? SecretarioId { get; set; }
        public int IdEmpleado { get; set; }
        public string Observaciones { get; set; }
        public int Ip { get; set; }
        public int NumeroOrden { get; set; }
        public int NumeroAnexos { get; set; }
        public CatalogoTipoPromovente? TipoPromoventeCat { get; set; }
        public string? TipoPromovente { get; set; }
        public bool EsPromoventeExistente { get; set; }
        public int YearPromocion { get; set; }
        public long? AsuntoNeunIdNuevo { get; set; }
        public CatalogoPromoventeExistenteDto PromoventeExistente { get; set; }
        public CatalogoParteExistenteDto PromoventeAutoridadExistente { get; set; }
        public CatalogoParteAutoridadDto PromoventeAutoridad { get; set; }
        public SecretarioPromocion Secretario { get; set; }
        public dynamic ArchivoAVincular { get; set; }
    }

}
