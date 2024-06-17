namespace CJF.Sgcpj.Judicatura.Catalogos.Application.Common.Models;
public class Sesion
{
    public int EmpleadoId { get; set; }
    public string? Completo { get; set; }
    public int CargoId { get; set; }
    public string? CargoDescripcion { get; set; }
    public int CatOrganismoId { get; set; }
    public string? NombreOficial { get; set; }
    public int CatTipoOrganismoId { get; set; }
    public int CatCircuitoId { get; set; }
    public int CatMateriaId { get; set; }
    public int CatCircuitoClasificacionId { get; set; }
    public bool estatusactivacion { get; set; }
    public string? Nombre { get; set; }
    public string? ApellidoPaterno { get; set; }
    public string? ApellidoMaterno { get; set; }
    public int StatusReg { get; set; }
    public int consultaexp { get; set; }
    public int vetramite { get; set; }
    public int veridcampocaptura { get; set; }
    public int CatClasificacionOrganismoId { get; set; }
    public int registro { get; set; }
    public string? EMail { get; set; }

    public List<Rol> Roles { get; set; }
    public List<Privilegio> Privilegios { get; set; }

    public bool PuedeAutorizar { get; set; }
    public bool PuedePreAutorizar { get; set; }
    public string Nonce { get; set; }
    public string RefrehToken { get; set; }

    public DateTime ExpiracionUtc { get; set; }

}

public class Rol
{
    public int RolId { get; set; }
    public string Nombre { get; set; }
}

public class Privilegio
{
    public int PrivilegioId { get; set; }
    public string Modulo { get; set; }
    public string Api { get; set; }
    public string TipoPrivilegio { get; set; }
    public string Metodo { get; internal set; }
}

