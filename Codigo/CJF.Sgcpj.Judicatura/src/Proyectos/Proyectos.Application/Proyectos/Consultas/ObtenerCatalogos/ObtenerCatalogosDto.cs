namespace CJF.Sgcpj.Judicatura.Proyectos.Application.Proyectos.Consultas.ObtenerCatalogos;

public class ObtenerCatalogosDto
{
    public int TipoCatalogo { get; set; }

    public List<CatalogoDTO> Datos { get; set; }
}

public class CatalogoDTO
{
    public int Id { get; set; }

    public string Descripcion { get; set; }
}
