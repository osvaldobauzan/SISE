using Actuaria.Functions.Funciones;
using Alertas.Functions.Funciones;
using Azure.Identity;
using Catalogos.Functions.Funciones;
using CJF.Sgcpj.Judicatura.Actuaria.Application;
using CJF.Sgcpj.Judicatura.Actuaria.Application.Common.Repositories;
using CJF.Sgcpj.Judicatura.Alertas.Application;
using CJF.Sgcpj.Judicatura.Application.IntegrationTests.Infrastructure;
using CJF.Sgcpj.Judicatura.Application.IntegrationTests.RepositoryMocks;
using CJF.Sgcpj.Judicatura.Common.Application;
using CJF.Sgcpj.Judicatura.ExpedienteElectronico.Application;
using CJF.Sgcpj.Judicatura.ExpedienteElectronico.Application.Common.Repositories;
using CJF.Sgcpj.Judicatura.LibretaOficios.Application;
using CJF.Sgcpj.Judicatura.LibretaOficios.Infrastructure;
using CJF.Sgcpj.Judicatura.Oficialia.Infrastructure;
using CJF.Sgcpj.Judicatura.Proyectos.Application;
using CJF.Sgcpj.Judicatura.Proyectos.Infrastructure;
using CJF.Sgcpj.Judicatura.Seguimiento.Infrastructure;
using CJF.Sgcpj.Judicatura.Seguridad.Application;
using CJF.Sgcpj.Judicatura.Seguridad.Infrastructure;
using CJF.Sgcpj.Judicatura.Tramite.Application;
using CJF.Sgcpj.Judicatura.Tramite.Infrastructure;
using CJF.Sgcpj.Judicatura.Usuarios.Application;
using CJF.Sgcpj.Judicatura.Usuarios.Infrastructure;
using ExpedienteElectronico.Functions.Funciones;
using FluentValidation.AspNetCore;
using Functions;
using LibretaOficios.Functions.Funciones;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Oficialia.Functions.Funciones;
using PolyCache;
using PolyCache.Cache;
using Proyectos.Functions.Funciones;
using Seguimiento.Functions.Funciones;
using Seguridad.Functions.Funciones;
using Sentencia.Functions.Funciones;
using Tramite.Functions.Funciones;
using Tramites.Functions.Funciones;
using Usuarios.Functions.Funciones;
using CJF.Sgcpj.Judicatura.Sentencias.Application;
using CJF.Sgcpj.Judicatura.Sentencias.Infrastructure;
using Sentencias.Functions.Funciones;

namespace CJF.Sgcpj.Judicatura.Application.IntegrationTests;
public class TestStartup : Startup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        base.Configure(builder);
        var configuration = new ConfigurationBuilder()
           //.SetBasePath(builder.GetContext().ApplicationRootPath)
           //.AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
           .AddEnvironmentVariables()
           .AddAzureAppConfiguration(options =>
           {
               options.Connect(Environment.GetEnvironmentVariable("AppConfigurationConnStr"))
                       .ConfigureKeyVault(kv =>
                       {
                           kv.SetCredential(new DefaultAzureCredential());
                       });
           })
           .Build();

        builder.Services.AddApplicationServices();
        builder.Services.AddInfrastructureServicesTests(configuration);
        builder.Services.AddPolyCache(configuration);

        var cnnCacheRedis = configuration["SISE3:BackEnd:RedisCacheConnStr"];
        builder.Services.AddTransient<IStaticCacheManager, DistributedCacheManager>();
        builder.Services.AddStackExchangeRedisCache(delegate (RedisCacheOptions options)
        {
            options.Configuration = cnnCacheRedis;

        });
        builder.Services.AddFluentValidation(conf =>
        {
            conf.RegisterValidatorsFromAssembly(typeof(Startup).Assembly);
        });
        builder.Services.AddScoped<CatalogoAsuntoFunction>();
        builder.Services.AddScoped<CatalogoContenidoFunction>();
        builder.Services.AddScoped<CatalogoCuadernoFunction>();
        builder.Services.AddScoped<CatalogoEstadosPromocionesFunction>();
        builder.Services.AddScoped<CatalogoProcedimientoFunction>();
        builder.Services.AddScoped<CatalogosAnexoFunction>();
        builder.Services.AddScoped<CatalogoTipoFunction>();
        builder.Services.AddScoped<CatalogoTipoPersonaFunction>();
        builder.Services.AddScoped<CatalogoTipoPersonaCaracterFunction>();
        builder.Services.AddScoped<CatalogoTipoPromoventeFunction>();
        builder.Services.AddScoped<CatalogoClasificacionAutoridadGenericaFunction>();
        builder.Services.AddScoped<CatalogoContenidoTramiteFunction>();
        builder.Services.AddScoped<CatalogoGenericoFunction>();
        builder.Services.AddScoped<CatalogoSexoFunction>();
        builder.Services.AddScoped<CatalogoTipoPersonaJuridicaFunction>();
        builder.Services.AddScoped<CatalogoZonaFunction>();

        builder.Services.AddOficialiaApplicationServices();
        builder.Services.AddOficialiaInfrastructureServices(configuration);
        builder.Services.AddScoped<VinculaExpedienteFunction>();
        builder.Services.AddScoped<CalcularRegistroFunction>();
        builder.Services.AddScoped<ObtenerDetalleAlertaFunction>();
        builder.Services.AddScoped<ObtenerExpedienteFunction>();
        builder.Services.AddScoped<ObtenerLecturaPromocionFunction>();
        builder.Services.AddScoped<ObtenerNumeroExpedienteFunction>();
        builder.Services.AddScoped<ObtenerNumeroPromocionFunction>();
        builder.Services.AddScoped<ObtenerPromocionDetalleTablero>();
        builder.Services.AddScoped<PromocionesFunction>();


        builder.Services.AddApplicationTramitesServices();
        builder.Services.AddInfrastructureTramitesServices(configuration);
        builder.Services.AddScoped<AcuerdoFunction>();
        builder.Services.AddScoped<FiltrosTramiteFunction>();
        builder.Services.AddScoped<TramitesFunction>();
        builder.Services.AddScoped<FirmadorFunction>();


        builder.Services.AddApplicationActuariaServices();
        //builder.Services.AddInfrastructureActuariaServices(configuration);
        builder.Services.AddSingleton<IActuariaRepository, ActuariaRepositoryMock>();
        builder.Services.AddScoped<AgregarActuarioFunction>();
        builder.Services.AddScoped<ActuariaFunction>();
        builder.Services.AddScoped<FiltrosFunction>();
        builder.Services.AddScoped<RecibirOficiosFunction>();
        builder.Services.AddScoped<AgregarActuarioMasivoFunction>();
        builder.Services.AddScoped<NotificacionesFunction>();
        builder.Services.AddScoped<DetalleAcuerdoFunction>();
        builder.Services.AddScoped<NotificacionesFunction>();
        builder.Services.AddScoped<SintesisFunction>();


        builder.Services.AddAlertasApplicationServices();
        builder.Services.AddAlertasInfrastructureServices(configuration);
        builder.Services.AddScoped<AlertsFunction>();


        builder.Services.AddApplicationLibretaOficiosServices();
        builder.Services.AddInfrastructureLibretaOficiossServices(configuration);
        builder.Services.AddScoped<ObtenerLibretaOficioFunction>();


        builder.Services.AddApplicationSeguridadServices();
        builder.Services.AddInfrastructureSeguridadServices(configuration);
        builder.Services.AddScoped<ObtieneOrganismosFunction>();

        builder.Services.AddApplicationUsuariosServices();
        builder.Services.AddInfrastructureUsuariosServices(configuration);
        builder.Services.AddScoped<ObtieneSecretarioFunction>();
        builder.Services.AddScoped<CatalogoSecretarioFunction>();
        builder.Services.AddScoped<CatalogoPromoventeExistenteFunction>();
        builder.Services.AddScoped<CatalogoParteExistenteFunction>();
        builder.Services.AddScoped<AutoridadJudicialFunction>();
        builder.Services.AddScoped<AutoridadFunction>();

        builder.Services.AddSeguimientoApplicationServices();
        builder.Services.AddSeguimientoInfrastructureServices(configuration);
        builder.Services.AddScoped<ObtenerSeguimientoXExpedienteFunction>();
        builder.Services.AddScoped<ObtenerSeguimientoXAsuntoAliasTipoAsuntoFunction>();
        builder.Services.AddScoped<ObtenerSeguimientoFunction>();
        builder.Services.AddScoped<ObtenerComboAsuntoFunction>();
        builder.Services.AddScoped<ObtenerSeguimientoComboExpFunction>();
        builder.Services.AddScoped<ObtenerComboPartesFunction>();
        builder.Services.AddScoped<ObtenerComboTipoAsuntoFunction>();

        builder.Services.AddApplicationExpedienteElectronicoServices();
        //builder.Services.AddInfrastructureExpedienteElectronicoServices(configuration);
        builder.Services.AddTransient<IExpedienteElectronicoRepository, ExpedienteElectronicoRepositoryMock>();
        builder.Services.AddScoped<ObtenerParteFunction>();
        builder.Services.AddScoped<ObtenerFichaTecnicaExpedienteElectronicoFunction>();
        builder.Services.AddScoped<ObtenerExpedienteElectronicoFunction>();
        builder.Services.AddScoped<InformacionParteFunction>();
        builder.Services.AddScoped<EstadoSentenciaFunction>();
        builder.Services.AddScoped<DatosGeneralesFunction>();
        builder.Services.AddScoped<AudienciaFunction>();
        builder.Services.AddScoped<ActualizarParteFunction>();
        builder.Services.AddScoped<EliminarParteFunction>();
        builder.Services.AddScoped<InsertarParteFunction>();

        builder.Services.AddApplicationProyectosServices();
        builder.Services.AddInfrastructureProyectosServices(configuration);
        builder.Services.AddScoped<ProyectosFunction>();

        builder.Services.AddScoped<CatalogoEmpleadosProyectoFunction>();
        builder.Services.AddScoped<CatalogoTipoSentenciaFunction>();
        builder.Services.AddScoped<CatalogoTipoSentidoFunction>();
        builder.Services.AddScoped<ObtenerVersionesProyectoFunction>();
        builder.Services.AddScoped<ObtenerArchivosFunction>();

        builder.Services.AddApplicationSentenciasServices();
        builder.Services.AddInfrastructureSentenciasServices(configuration);
        builder.Services.AddScoped<ObtenerSentenciasFunction>();
        builder.Services.AddScoped<ObtenerCatalogosFunction>();
        builder.Services.AddScoped<ObtenerDetallePartesFunction>();
        builder.Services.AddScoped<SentenciasFunction>();
        builder.Services.AddScoped<ReasignarSecretarioFunction>();
        builder.Services.AddScoped<ObtenerPromocionesFunction>();
    }
}