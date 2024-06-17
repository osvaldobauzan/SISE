namespace CJF.Sgcpj.Judicatura.Tramite.Application.Tramites.Comandos.SubirAcuerdoComando;
public class SubirAcuerdoDto
{
    public byte[] Data { get; set; }
    public string NombreArchivo { get; set; }
    public int Clase { get; set; }
    public int Descripcion { get; set; }
    public int Caracter { get; set; }
    public string NoRegistro { get; internal set; }


}
