using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CJF.Sgcpj.Judicatura.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Common.Infrastructure.Persistence;
using CJF.Sgcpj.Judicatura.Oficialia.Application.Common.Repositories;
using CJF.Sgcpj.Judicatura.Oficialia.Application.Oficialia.Consultas.ObtenerPromociones;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace CJF.Sgcpj.Judicatura.Oficialia.Infrastructure.Persistence.Repositories;
internal class OficialiaPartesRepository: IOficialiaPartesRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public OficialiaPartesRepository(ApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<List<OficialiaPartesDTO>> ObtenerPromociones(OficialiaPartesFiltro filtro)
    {
        var parametros = new[]
        {
            new SqlParameter("@pi_CatOrganismoId", filtro.IdOrganismo),
            new SqlParameter("@pi_FechaPresentacionIni", filtro.FechaInicio),
            new SqlParameter("@pi_FechaPresentacionFin", filtro.FechaFin),
            new SqlParameter("@pi_UsuariId", filtro.IdUsuario)
        };

        var sqlQuery = "uspx_op_getPromociones";
        return await _dbContext.ExecuteStoredProc<OficialiaPartesDTO>(sqlQuery, parametros);
    }
}
