using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CJF.Sgcpj.Judicatura.Seguimiento.Application.Common.Models;

namespace CJF.Sgcpj.Judicatura.Seguimiento.Application.Common.Repositories;
/// <summary>
/// ///////////INTERFASE  PARA DOCUMENTOS DE SEGUIMIENTO//////////////////////////////////////
/// </summary>
public interface IDocumentosRepository
{


    Task<Documentos> getDocumentos(Documentos documentos);


}
