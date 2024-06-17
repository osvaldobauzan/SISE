using CJF.Sgcpj.Judicatura.Common.Application.Common.Alertas;

namespace CJF.Sgcpj.Judicatura.Common.Application.Common.Models;
public class OficiosAlertDTO : IValidAlertMessages
{
    public Guid Id { get; set; }
    public Origenes OrigenId { get; set; }
    public string Origen { get; set; }
    public AvanceProceso TipoProcesoId { get; set; }
    public string TipoProceso { get; set; }
    public bool EsAlertaSesion { get; set; }
    public string Titulo { get; set; }
    public string Contenido { get; set; }
    public DateTime FechaHora { get; set; }
}

public enum Origenes
{
    Módulo,
    Oficialía,
    Trámite,
    Expediente,
    Sentencias,
    Actuaría
}

public enum AvanceProceso
{
    Inicio,
    Avance,
    Fin,
    Error
}
