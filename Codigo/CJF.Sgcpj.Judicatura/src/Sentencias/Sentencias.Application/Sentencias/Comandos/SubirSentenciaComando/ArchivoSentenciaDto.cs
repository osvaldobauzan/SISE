namespace CJF.Sgcpj.Judicatura.Sentencias.Application.Sentencias.Comandos.SubirSentenciaComando;

public class ArchivoSentenciaDto
{
    public long AsuntoNeunId { get; set; }

    public string NombreArchivo { get; set; }

    public byte[] Archivo { get; set; }
}
