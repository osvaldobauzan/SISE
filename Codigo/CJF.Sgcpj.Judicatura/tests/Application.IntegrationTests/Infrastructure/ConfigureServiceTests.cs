using CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using CJF.Sgcpj.Judicatura.Common.Infrastructure.Files;
using CJF.Sgcpj.Judicatura.Common.Infrastructure.Persistence.Interceptors;
using CJF.Sgcpj.Judicatura.Common.Infrastructure.Persistence;
using CJF.Sgcpj.Judicatura.Common.Infrastructure.Services;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Microsoft.Extensions.DependencyInjection;
using PolyCache.Cache;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using CJF.Sgcpj.Judicatura.Application.IntegrationTests.ServiceMocks;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Repositories;
using CJF.Sgcpj.Judicatura.Common.Infrastructure.Persistence.Repositories;
using CJF.Sgcpj.Judicatura.Application.IntegrationTests.Models;
using Common.Functions;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Alertas;

namespace CJF.Sgcpj.Judicatura.Application.IntegrationTests.Infrastructure;
public static class ConfigureServiceTests
{
    public static IServiceCollection AddInfrastructureServicesTests(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<AuditableEntitySaveChangesInterceptor>();

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration["SISE3:BackEnd:SISEDBConnStr"],
                builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)), ServiceLifetime.Transient);

        services.AddTransient<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        services.AddLogging();
        services.AddTransient<IDateTime, DateTimeService>();

        services.AddTransient<INasArchivo, NasConSMB>();


        services.AddSingleton<IHttpRequestProcessor, HttpRequestProcessor>();
        services.AddSingleton<ICurrentUserService, CurrentUserServiceMock>();
        services.AddSingleton<IUserConnectionsHandler, UserConnectionsHandlerMock>();
        services.AddSingleton<IRutasChunkService, RutasChunkService>();

        services.AddSingleton<IRutasChunkRepository, RutasChunkRepository>();
        services.AddTransient<ISesionService, SesionServiceMock>();
        services.AddTransient<IListSignatureFiles, ListSignatureFiles>();
        services.AddSingleton<IWordValidator, WordValidator>();
        services.AddTransient<IDocumentoBlob, DocumentoBlobService>();
        services.AddScoped<IWordsUtil, WordUtilsForAspose>();
        services.AddSingleton<IGeneradorQR, GeneradorQRService>();
        services.AddSingleton<IAcuerdosDocxsHelpers, AcuerdosDocxsHelpers>();
        services.AddSingleton<IWordContenido, WordContenido>();
        services.AddSingleton<ISanitizerService, SanitizerService>();
        services.AddSingleton<AsposeLicense>();
        services.AddSingleton<IPrivilegiosService, PrivilegiosService>();
        var cnnCacheRedis = configuration["SISE3:BackEnd:RedisCacheConnStr"];
        services.AddTransient<IStaticCacheManager, DistributedCacheManager>();
        services.AddStackExchangeRedisCache(delegate (RedisCacheOptions options)
        {
            options.Configuration = cnnCacheRedis;

        });

        return services;
    }
}
