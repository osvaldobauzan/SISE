namespace CJF.Sgcpj.Judicatura.Application.Promociones.Comandos.SubirArchivoComando;

public  class SubirArchivoDto
{
    
    public byte[] Data { get; set; }

    public string NombreArchivo { get; set;}
    public int Consecutivo { get; set; }
    public int Clase { get; set; }
    public int Descripcion { get; set; }
    public int Caracter { get; set; }
    public string NoRegistro { get; internal set; }
}