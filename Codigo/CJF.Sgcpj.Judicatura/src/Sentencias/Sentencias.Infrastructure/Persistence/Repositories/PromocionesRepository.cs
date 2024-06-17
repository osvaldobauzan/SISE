using AutoMapper;
using CJF.Sgcpj.Judicatura.Common.Infrastructure.Persistence;
using CJF.Sgcpj.Judicatura.Sentencias.Application.Common.Repositories;
using CJF.Sgcpj.Judicatura.Sentencias.Application.Sentencias.Consultas.ObtenerPromociones;
using CJF.Sgcpj.Judicatura.Sentencias.Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace CJF.Sgcpj.Judicatura.Sentencias.Infrastructure.Persistence.Repositories;
public class PromocionesRepository : IPromocionesRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly ILogger<PromocionesRepository> _logger;

    public PromocionesRepository(ApplicationDbContext dbContext, IMapper mapper, ILogger<PromocionesRepository> logger)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<ListadoPromocionesDTO> ObtenerPromocionesCuaderno(int asuntoId, long asuntoNeunId, int tipoCuaderno, int sintesisOrden)
    {
        try
        {
            var listaParametros = new[]
            {
                new SqlParameter("@pi_AsuntoId",asuntoId ),
                new SqlParameter("@pi_AsuntoNeunId", asuntoNeunId),
                new SqlParameter("@pi_TipoCuaderno", tipoCuaderno),
                new SqlParameter("@pi_SintesisOrden", sintesisOrden)
            };

            var datos = await _dbContext.ExecuteStoredProc<Promocion>("usp_PromocionesTodasXNeunTipoCuadernoSel", listaParametros.ToArray());
            var listado = datos.Select(x => _mapper.Map<Application.Sentencias.Consultas.ObtenerPromociones.PromocionSentenciaDto>(x)).ToList();
            var response = new ListadoPromocionesDTO
            {
                PromocionesSentencia = listado
            };
            return response;

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }

    }

}




