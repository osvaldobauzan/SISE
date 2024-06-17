using FluentValidation;
using Microsoft.Extensions.Configuration;
using CJF.Sgcpj.Judicatura.Application.Utils;
using CJF.Sgcpj.Judicatura.Oficialia.Domain.Common;
using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using CJF.Sgcpj.Judicatura.Application.Promociones.Comandos.SubirArchivoComando;

namespace CJF.Sgcpj.Judicatura.Oficialia.Application.Promociones.Comandos.SubirArchivoComando;

public class SubirArchivosComandoValidator : AbstractValidator<SubirArchivoCommando>
{
    private readonly IConfiguration _configuration;
    private readonly IWordValidator _wordValidator;

    public SubirArchivosComandoValidator(IConfiguration configuration, IWordValidator wordValidator)
    {
        _configuration = configuration;
        _wordValidator = wordValidator;

        int tamanioArchivo = Convert.ToInt32(_configuration[Constants.NAS_FILESIZEFROMSISE3NAST]);

        RuleFor(s => s.Archivos).NotEmpty().WithMessage(Constants.MSG_UPLOADFILEREQUIRED);

        RuleForEach(s => s.Archivos).ChildRules(archivo =>
        {
            archivo.RuleFor(a => a)
                .Must(arc => string.IsNullOrEmpty(arc.NombreArchivo) ||
                    _wordValidator.IsNameAndValidatedExtension(arc.NombreArchivo))
                .WithMessage(Constants.MSG_FILEINVALID);

            archivo.RuleFor(a => a.Data)
                .Must(item => item == null || Functions.TamanioArchivoEnMB(item.Length) <= tamanioArchivo)
                .WithMessage(string.Format(Constants.MSG_FILELENGTHINVALID, tamanioArchivo));

            archivo.RuleFor(a => a)
                .Must(u => _wordValidator.IsValidSignatureFromDocx(u.NombreArchivo, u.Data))
                .WithMessage(string.Format(Constants.MSG_INVALIDFILESIGNATURE, tamanioArchivo));
        });
    }
}