namespace CJF.Sgcpj.Judicatura.Application.Promociones.Comandos.SubirAnexosComando;

public class CaracterDto
{
    public int Id { get; set; }
    public string Descripcion { get; set; }
    public int Elementos { get; set; }
}

public class DescripcionDto
{
    public int Id { get; set; }
    public string Descripcion { get; set; }
    public int Elementos { get; set; }
}

public class FileDto
{
    public string __key { get; set; }
}

public class SubirAnexosDto
{
    public int Id { get; set; }
    public DescripcionDto Descripcion { get; set; }
    public TipoAnexoDto TipoAnexo { get; set; }
    public CaracterDto Caracter { get; set; }
   
    public byte[] Data { get; set; }
    public string NombreArchivo { get; set; }
    
    public bool guardadoEnBD { get; set; }

    public int Consecutivo { get; set; }
}

public class TipoAnexoDto
{
    public int Id { get; set; }
    public string Descripcion { get; set; }
    public int Elementos { get; set; }
}

