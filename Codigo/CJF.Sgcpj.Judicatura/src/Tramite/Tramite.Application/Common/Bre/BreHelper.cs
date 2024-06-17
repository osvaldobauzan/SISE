using CJF.Sgcpj.Judicatura.Tramite.Application.Common.WorkFlows;
using Newtonsoft.Json;
using RulesEngine.Models;

namespace CJF.Sgcpj.Judicatura.Tramite.Application.Common.Bre;
public class BreHelper
{
    private readonly IWorkflowsRepository _workflowsRepository;

    public BreHelper(IWorkflowsRepository workflowsRepository)
    {
        _workflowsRepository = workflowsRepository;
    }
    public async Task<RulesEngine.RulesEngine> ObtenerFlujoDeTrabajo(string nombreFlujo)
    {
        var wfJson = await _workflowsRepository.ObtenerFlujoDeTrabajo(nombreFlujo);
        var workFlows = JsonConvert.DeserializeObject<Workflow[]>(wfJson);
        return new RulesEngine.RulesEngine(workFlows, null);
    }
}
