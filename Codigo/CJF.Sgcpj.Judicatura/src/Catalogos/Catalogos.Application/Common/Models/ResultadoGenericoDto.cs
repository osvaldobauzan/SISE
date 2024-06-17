namespace CJF.Sgcpj.Judicatura.Catalogos.Application.Common.Models;

public class ResultadoGenericoDto
{
    internal ResultadoGenericoDto(bool tuvoExito, string mensaje, IEnumerable<string> errors)
    {
        TuvoExito = tuvoExito;
        Errores = errors.ToArray();
        Mensaje = mensaje;
    }

    public string Mensaje { get; set; }
    public bool TuvoExito { get; set; }

    public string[] Errores { get; set; }

    public static ResultadoGenericoDto Exito()
    {
        return new ResultadoGenericoDto(true, string.Empty, Array.Empty<string>());
    }

    public static ResultadoGenericoDto Falla(string mensaje, IEnumerable<string> errors)
    {
        return new ResultadoGenericoDto(false, mensaje, errors);
    }
}
