namespace CJF.Sgcpj.Judicatura.Application.Promociones.Comandos.CargaMasivaComando;
public class ResultadoCargaMasivaArchivoDto
{
    public string NumeroRegistro { get; set; }
    public bool Correcto { get; set; } = false;
    public string Mensaje { get; set; }
    public string ExpedienteProcesado { get; set; }

}
