using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CJF.Sgcpj.Judicatura.ExpedienteElectronico.Application.Common.Models;
/// <summary>
/// Esta clase representa el Header que contiene la ficha técnica en el expediente electrónico.
/// </summary>
public class FichaTecnicaExpedienteElectronico
{
    /// <summary>
    /// Obtiene o establece el nombre del campo a mostrar.
    /// </summary>
    public string Campo { get; set; }
    /// <summary>
    /// Obtiene o establece el valor del campo a mostrar.
    /// </summary>
    public string Valor { get; set; }
    /// <summary>
    /// Obtiene o establece el orden en el que se deberá mostrar el campo.
    /// </summary>
    public int Orden { get; set; }
}
