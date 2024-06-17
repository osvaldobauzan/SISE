using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace CJF.Sgcpj.Judicatura.Seguimiento.Application.Common.Models;
[DataContract]
public enum OperationType : short
{
    [EnumMember]
    Create = 0,
    [EnumMember]
    Update = 1,
    [EnumMember]
    Delete = 2,
    [EnumMember]
    ChangeStatus = 3
}
