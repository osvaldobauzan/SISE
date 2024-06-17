using CJF.Sgcpj.Judicatura.ExpedienteElectronico.Application.Common.Models;
using CJF.Sgcpj.Judicatura.ExpedienteElectronico.Application.Common.Repositories;
using CJF.Sgcpj.Judicatura.ExpedienteElectronico.Application.ExpedienteElectronico.Consultas.Audiencia;
using CJF.Sgcpj.Judicatura.ExpedienteElectronico.Application.ExpedienteElectronico.Consultas.Sentencia;
using ExpedienteElectronico.Application.Common.Models;
using ExpedienteElectronico.Application.ExpedienteElectronico.Consultas.DatosGenerales;
using ExpedienteElectronico.Application.ExpedienteElectronico.Consultas.InformacionParte;

namespace CJF.Sgcpj.Judicatura.Application.IntegrationTests.RepositoryMocks;
public class ExpedienteElectronicoRepositoryMock : IExpedienteElectronicoRepository
{
    public Task<bool> ActualizarPersonaAsunto(PersonaAsuntoUpdate param)
    {
        return Task.Run(() => { return true; });
    }

    public Task<bool> EliminarPersonaAsunto(PersonaAsuntoDelete param)
    {
        return Task.Run(() => { return true; });
    }

    public Task<long> InsertarPersonaAsunto(PersonaAsuntoInsert param)
    {
        return Task.Run(() => { return long.Parse("1"); });
    }

    public Task<DatosGeneralesM?> ObtenerDatosGenerales(DatosGeneralesConsulta request)
    {
        return Task.Run(() => { return new DatosGeneralesM()
        {
            FechaOCC = "FechaOCC",
            FechaOrg = "FechaOrg",
            Mesa = "Mesa",
            Secretario = "Secretario"
        }; 
        });
    }

    public Task<DetalleAudiencia> ObtenerDetalleAudiencia(AudienciaConsulta request)
    {
        return Task.Run(() => {
            return new DetalleAudiencia()
            {
                TipoAudiencia = "TipoAudiencia",
                Fecha = "10/10/2023",
                Hora = "19:00",
                Resultado = "ResultadoTests"
            };
        });
    }

    public Task<EstadoSentencia> ObtenerEstadoSentencia(EstadoSentenciaConsulta request)
    {
        return Task.Run(() => {
            return new EstadoSentencia()
            {
                Ejecucion = "Ejecucion",
                Estado = "Estado",
                FechaSentencia = "02/02/2023"
            };
        });
    }

    public Task<List<DetalleExpedienteElectronico>> ObtenerExpedienteElectronico(ExpedienteElectronicoFiltro param)
    {
        return Task.Run(() => {
            return new List<DetalleExpedienteElectronico>()
            {
                new DetalleExpedienteElectronico()
                {
                    AnioPromocion = 2023,
                    ArchivoPromocion = "ArchivoPromocion",
                    AsuntoDocumentoId = 1,
                    AsuntoDocumentoNombre = "DocumentoNombre",
                    AsuntoNeunId = 1,
                    CatTipoAsuntoId = 180,
                    EstadoAutorizacion = 1,
                    FechaAcuerdo = DateTime.UtcNow,
                    FechaAcuerdo_F = "02/02/2023",
                    RutaArchivoNAS = 1,
                    CaracterPromovente = "CaracterPromovente",
                    Color = "Color",
                    Cuaderno = "Cuaderno",
                    CuadernoId = 1,
                    Electronica = 2,
                    FechaPresentacion = DateTime.UtcNow,
                    FechaPresentacion_F = "02/02/2023",
                    Folio = 180,
                    Mesa = "mesa",
                    NombreCorto = "NombreCorto",
                    NombreDocumento = "NombreDocumento",
                    No_Exp = "NoExp",
                    NumeroOrden = 1,
                    NumeroRegistro = 1,
                    Observaciones = "Observaciones",
                    OrigenCorto = "OrigenCorto",
                    OrigenPromo = "OrigenPromo",
                    OrigenPromocion = 1,
                    Personal = 1,
                    PlantillaDocumento = "PlantillaDocumento",
                    PorLista = DateTime.UtcNow,
                    PorLista_F = "F",
                    PorOficio = 1,
                    Promovente = "Promovente",
                    SecretarioDescripcion = "Descripción",
                    TipoContenidoDescripcion = "ContenidoDescripción",
                    UserNameSecretario = "Secretario"
                }
            };
        });
    }

    public Task<List<FichaTecnicaExpedienteElectronico>> ObtenerFichaTecnicaExpediente(FichaTecnicaExpedienteElectronicoFiltro param)
    {
        return Task.Run(() => {
            return new List<FichaTecnicaExpedienteElectronico>()
            {
                new FichaTecnicaExpedienteElectronico()
                {
                    Campo = "Campo",
                    Orden = 2,
                    Valor = "Valor"
                }
            };
        });
    }

    public Task<List<InformacionParteM>> ObtenerInformacionParte(InformacionParteConsulta request)
    {
        return Task.Run(() => {
            return new List<InformacionParteM>()
            {
                new InformacionParteM()
                {
                    TipoAsuntoId = 1,
                    CampoDatosGenerales = 1,
                    Descripcion = "Descripcion",
                    NoBloque = 1,
                    NombreParte = "NombreParte",
                    Orden = 1,
                    Padre = 1,
                    PadreDescripcion = "PadreDescripción",
                    PadreOrden = 1,
                    PersonaId = 1,
                    Valor = "Valor"
                }
            };
        });
    }

    public Task<PersonaAsuntoDTO> ObtenerPersonaAsunto(PersonaAsuntoFiltro param)
    {
        return Task.Run(() => {
            return new PersonaAsuntoDTO()
            {
                AceptaOponePublicarDatos = 1,
                Alias = "Alias",
                AMaterno = "AMaterno",
                APaterno = "APaterno",
                CatCaracterPersonaAsuntoId = 1,
                ClasificaAutoridadGenericaId = 1,
                DenominacionDeAutoridad = "Denominacion",
                FechaAceptaOponePublicarDatos = "02/02/2023",
                ParteAdhesivaApelacion = 1,
                SujetoDerechoAgrario = 1,
                CaracterPromueveNombre = 1,
                CatTipoPersonaId = 1,
                CatTipoPersonaJuridicaId = 1,
                EdadMenor = 1,
                EsParteGrupoVulnerable = 1,
                Foraneo = 1,
                GrupoVulnerable = 1,
                HablaLengua = 1,
                Lengua = 1,
                MayorEdad = 1,
                Nombre = "Nombre",
                Recurrente = 1,
                Sexo = 1,
                Traductor = 1,
                VictimaOfendidoDelito = 1
            };
        });
    }
}
