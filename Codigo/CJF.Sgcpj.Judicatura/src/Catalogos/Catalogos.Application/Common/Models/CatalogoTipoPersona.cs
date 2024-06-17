namespace CJF.Sgcpj.Judicatura.Catalogos.Application.Common.Models;
public class CatalogoTipoPersona
{
    public int CatTipoPersonaId => CatCatalogoElementoId;
    public int CatCatalogoElementoId { get; set; }
    public string Descripcion { get; set; }
}
