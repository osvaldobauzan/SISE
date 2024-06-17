namespace CJF.Sgcpj.Judicatura.Common.Application.Common.Security;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
public class SesionNoRequiredAttribute : Attribute
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AuthorizeAttribute"/> class. 
    /// </summary>
    public SesionNoRequiredAttribute() { }

}
