using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using CJF.Sgcpj.Judicatura.Common.Domain.Common;
using FluentValidation;
using Microsoft.Extensions.Configuration;

namespace CJF.Sgcpj.Judicatura.Proyectos.Application.Proyectos.Comandos.SubirProyectoConAudienciaComando;

public class SubirProyectoConAudienciaComandoValidator : AbstractValidator<SubirProyectoConAudienciaComando>
{
    private readonly IConfiguration _configuration;
    private readonly IWordValidator _wordValidator;

    public SubirProyectoConAudienciaComandoValidator(IConfiguration configuration, IWordValidator wordValidator)
    {
        _configuration = configuration;
        _wordValidator = wordValidator;

        int tamanioArchivo = Convert.ToInt32(_configuration[Constants.NAS_FILESIZEFROMSISE3NAST]);

        RuleFor(a => a.NombreArchivo).Must(arc => string.IsNullOrEmpty(arc) || _wordValidator.IsNameAndValidatedExtension(arc))
            .WithMessage(Constants.MSG_FILEINVALID);

        RuleFor(a => a.Archivo).Must(item => item == null || Judicatura.Application.Utils.Functions.TamanioArchivoEnMB(item.Length) <= tamanioArchivo)
            .WithMessage(string.Format(Constants.MSG_FILELENGTHINVALID, tamanioArchivo));

        RuleFor(a => a).Must(u => _wordValidator.IsValidSignatureFromDocx(u.NombreArchivo, u.Archivo))
            .WithMessage(string.Format(Constants.MSG_INVALIDFILESIGNATURE, tamanioArchivo));

        RuleForEach(a => a.Motivos).NotEmpty().WithMessage("Información obligatoria");
    }
}
