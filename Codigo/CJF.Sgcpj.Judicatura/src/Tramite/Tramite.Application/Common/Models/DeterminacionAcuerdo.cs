namespace CJF.Sgcpj.Judicatura.Tramite.Application.Common.Models;
public class DeterminacionAcuerdoRoot{
    public DeterminacionAcuerdo DeterminacionAcuerdo { get; set; }
}
public class DeterminacionAcuerdo
{
    public Guid GUID { get; set; }
    public bool Firmado { get; set; }
    public int? IdRuta { get; set; }
    public string? Nombre { get; set; }
	public string? Extension { get; set; }
}
