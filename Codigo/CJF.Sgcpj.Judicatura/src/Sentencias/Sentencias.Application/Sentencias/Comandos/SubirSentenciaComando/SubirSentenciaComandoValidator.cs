using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using CJF.Sgcpj.Judicatura.Common.Domain.Common;
using CJF.Sgcpj.Judicatura.Sentencias.Domain.Entities;
using FluentValidation;
using Microsoft.Extensions.Configuration;

namespace CJF.Sgcpj.Judicatura.Sentencias.Application.Sentencias.Comandos.SubirSentenciaComando;

public class SubirSentenciaComandoValidator : AbstractValidator<Sentencia>
{
    private readonly IConfiguration _configuration;
    private readonly IWordValidator _wordValidator;

    public SubirSentenciaComandoValidator(IConfiguration configuration, IWordValidator wordValidator)
    {
        _configuration = configuration;
        _wordValidator = wordValidator;

        int tamanioArchivo = Convert.ToInt32(_configuration[Constants.NAS_FILESIZEFROMSISE3NAST]);

        RuleFor(a => a.NomArchivoReal).Must(arc => string.IsNullOrEmpty(arc) || _wordValidator.IsNameAndValidatedExtension(arc))
            .WithMessage(Constants.MSG_FILEINVALID);

        RuleFor(a => a.NomArchivoReal).Must(item => item == null || Judicatura.Application.Utils.Functions.TamanioArchivoEnMB(item.Length) <= tamanioArchivo)
            .WithMessage(string.Format(Constants.MSG_FILELENGTHINVALID, tamanioArchivo));

        RuleFor(a => a).Must(u => _wordValidator.IsValidSignatureFromDocx(u.NomArchivoReal, u.ArchivoBytes))
            .WithMessage(string.Format(Constants.MSG_INVALIDFILESIGNATURE, tamanioArchivo));

        RuleFor(s => s.AsuntoNeunId).NotNull().GreaterThan(0).WithMessage("Sentencia - Debe indicar el AsuntoNeunId");

        RuleFor(s => s.Contenido).NotNull().GreaterThan(0).WithMessage("Sentencia - Debe indicar el Contenido");

        RuleFor(s => s.TipoCuadernoId).NotNull().GreaterThan(0).WithMessage("Sentencia - Debe indicar el TipoCuadernoId");

        RuleFor(c => c.FechaAuto).NotNull().WithMessage("Sentencia - Debe indicar la FechaAcuerdo");

        RuleFor(c => c.TipoArchivo).NotNull().GreaterThan(0).WithMessage("Sentencia - Debe indicar el TipoArchivo");

        RuleFor(c => c.TitularId).NotNull().GreaterThan(0).WithMessage("Sentencia - Debe indicar el TitularId");

        RuleFor(c => c.SecretarioCId).NotNull().GreaterThan(0).WithMessage("Sentencia - Debe indicar el SecretarioCId");

        RuleFor(c => c.SecretarioPId).NotNull().GreaterThan(0).WithMessage("Sentencia - Debe indicar el SecretarioPId");

        RuleFor(c => c.Resumen).NotNull().NotEmpty().WithMessage("Sentencia - Debe indicar el Resumen");

        RuleFor(c => c.SolicitudReparacion).NotNull().GreaterThan(0).WithMessage("Sentencia - Debe indicar la SolicitudReparacion");

        RuleFor(c => c.EsJDA).NotNull().WithMessage("Sentencia - Debe indicar el EsJDA");

        RuleFor(c => c.Criterio).NotNull().WithMessage("Sentencia - Debe indicar el Criterio");

        RuleFor(c => c.Trascedental).NotNull().WithMessage("Sentencia - Debe indicar el Trascedental");

        RuleFor(c => c.EsTratadoInternacional).NotNull().WithMessage("Sentencia - Debe indicar el EsTratadoInternacional");
    }
}

public class SentenciaVersionPublicaValidator : AbstractValidator<SentenciaVersionPublicaDto>
{
    public SentenciaVersionPublicaValidator()
    {
        RuleFor(s => s.AsuntoNeunId).NotNull().GreaterThan(0).WithMessage("SentenciaVersiónPublica - Debe indicar el AsuntoNeunId");
    }
}
