using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using CJF.Sgcpj.Judicatura.Common.Domain.Common;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Proyectos.Application.Common.Enums;

namespace CJF.Sgcpj.Judicatura.Proyectos.Application.Proyectos.Comandos.ActualizarEstadoProyectoComando;

public class ActualizarEstadoProyectoComandoValidator : AbstractValidator<ActualizarEstadoProyectoComando>
{
    private readonly IConfiguration _configuration;
    private readonly IWordValidator _wordValidator;

    public ActualizarEstadoProyectoComandoValidator(IConfiguration configuration, IWordValidator wordValidator)
    {
        _configuration = configuration;
        _wordValidator = wordValidator;

        int tamanioArchivo = Convert.ToInt32(_configuration[Constants.NAS_FILESIZEFROMSISE3NAST]);

        When(x => x.NombreArchivoCorrecciones != null, () =>
            RuleFor(a => a.NombreArchivoCorrecciones).Must(arc => string.IsNullOrEmpty(arc) || _wordValidator.IsNameAndValidatedExtension(arc))
                .WithMessage(Constants.MSG_FILEINVALID));

        When(x => x.NombreArchivoCorrecciones != null && x.ArchivoCorreciones != null, () =>
            RuleFor(a => a.ArchivoCorreciones).Must(item => item == null || Judicatura.Application.Utils.Functions.TamanioArchivoEnMB(item.Length) <= tamanioArchivo)
                .WithMessage(string.Format(Constants.MSG_FILELENGTHINVALID, tamanioArchivo)));

        When(x => x.NombreArchivoCorrecciones != null && x.ArchivoCorreciones != null, () =>
            RuleFor(a => a).Must(u => _wordValidator.IsValidSignatureFromDocx(u.NombreArchivoCorrecciones, u.ArchivoCorreciones))
                .WithMessage(string.Format(Constants.MSG_INVALIDFILESIGNATURE, tamanioArchivo)));

        RuleFor(x => x).Custom((x, context) =>
        {
            if (x.EstadoId != (int)EstadoProyectoEnum.Aprobado && x.Correcciones is null && x.ArchivoCorreciones is null)
            {
                context.AddFailure("Debe indicar las observaciones o subir un archivo");
            }
        });
    }
}
