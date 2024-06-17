using AutoMapper;
using CJF.Sgcpj.Judicatura.Catalogos.Application.Common.Models;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Interfaces;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using MediatR;

namespace CJF.Sgcpj.Judicatura.Catalogos.Application.Catalogos.OrganismosOCC.Consulta;
[SesionNoRequiredAttribute]
public class ObtieneOrganismosCompletoConsulta: IRequest<List<OrganismoCompletoDto>>
    {
    }
    public class ObtieneOrganismosCompletoConsultaHandler : IRequestHandler<ObtieneOrganismosCompletoConsulta, List<OrganismoCompletoDto>>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly ICurrentUserService _currentUserService;

        public ObtieneOrganismosCompletoConsultaHandler(IMapper mapper, IApplicationDbContext applicationDbContext, ICurrentUserService currentUserService)
        {
            _mapper = mapper;
            _applicationDbContext = applicationDbContext;
            _currentUserService = currentUserService;
        }

        async Task<List<OrganismoCompletoDto>> IRequestHandler<ObtieneOrganismosCompletoConsulta, List<OrganismoCompletoDto>>.Handle(ObtieneOrganismosCompletoConsulta request, CancellationToken cancellationToken)
        {
            var listaOrganismosPorUsuario = new List<OrganismoCompletoDto>();
            var itemsCatalogo = await _applicationDbContext.ExecuteStoredProc<CatalogoOrganismosOCC>("[SISE3].[pcOrganismosOCC]");
            listaOrganismosPorUsuario = _mapper.Map<List<OrganismoCompletoDto>>(itemsCatalogo);
            return await Task.FromResult(listaOrganismosPorUsuario);
        }
}
