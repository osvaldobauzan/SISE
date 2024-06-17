namespace CJF.Sgcpj.Judicatura.Common.Application.Common.Models;

public class DatosLog
{
    public TipoMovimiento TipoMovimiento { get; set; }

    public long IdUsuario { get; set; }

    public string DatosEntrada { get; set; }

    public string? DatosSalida { get; set; }

    public string ModuloOrigen { get; set; }
}

public enum TipoMovimiento
{
    Crear,
    Leer,
    Actualizar,
    Eliminar
}
