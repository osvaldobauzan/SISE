using CJF.Sgcpj.Judicatura.Common.Application.Common.Security;
using CJF.Sgcpj.Judicatura.Common.Domain.Common;
using FluentValidation;
using Microsoft.Extensions.Configuration;

namespace CJF.Sgcpj.Judicatura.Tramite.Application.Tramites.Comandos.SubirAcuerdoComando;
public class SubirAcuerdoComandoValidator : AbstractValidator<SubirAcuerdoComando>
{
    private readonly IConfiguration _configuration;
    private readonly IWordValidator _wordValidator;

    public SubirAcuerdoComandoValidator(IConfiguration configuration, IWordValidator wordValidator)
    {
        _configuration = configuration;
        _wordValidator = wordValidator;

        int tamanioArchivo = Convert.ToInt32(_configuration[Constants.NAS_FILESIZEFROMSISE3NAST]);

        RuleForEach(s => s.Archivos).ChildRules(archivo =>
        {
            archivo.RuleFor(a => a)
                .Must(arc => string.IsNullOrEmpty(arc.NombreArchivo) ||
                    _wordValidator.IsNameAndValidatedExtension(arc.NombreArchivo))
                .WithMessage(Constants.MSG_FILEINVALID);

            archivo.RuleFor(a => a.Data)
                .Must(item => item == null || Judicatura.Application.Utils.Functions.TamanioArchivoEnMB(item.Length) <= tamanioArchivo)
                .WithMessage(string.Format(Constants.MSG_FILELENGTHINVALID, tamanioArchivo));

            archivo.RuleFor(a => a)
                .Must(u => _wordValidator.IsValidSignatureFromDocx(u.NombreArchivo, u.Data))
                .WithMessage(string.Format(Constants.MSG_INVALIDFILESIGNATURE, tamanioArchivo));
        });
    }
}
