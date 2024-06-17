using FluentValidation.Results;

namespace CJF.Sgcpj.Judicatura.Common.Application.Common.Exceptions;

public class ValidationException : Exception
{
    public ValidationException()
        : base("One or more validation failures have occurred.")
    {
        Errors = new List<string>();
    }

    public ValidationException(IEnumerable<ValidationFailure> failures)
        : this()
    {
        Errors = failures
            .Select(s => s.ErrorMessage).ToList();
    }

    public List<string> Errors { get; }
}
