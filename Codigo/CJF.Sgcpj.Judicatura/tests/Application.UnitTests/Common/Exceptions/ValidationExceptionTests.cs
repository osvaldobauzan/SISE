using CJF.Sgcpj.Judicatura.Application.Common.Exceptions;
using FluentAssertions;
using FluentValidation.Results;
using NUnit.Framework;

namespace CleanArchitecture.Application.UnitTests.Common.Exceptions
{
    public class ValidationExceptionTests
    {
        [Test]
        public void DefaultConstructorCreatesAnEmptyErrorDictionary()
        {
            var actual = new ValidationException().Errors;

            //actual.Keys.Should().BeEquivalentTo(Array.Empty<string>());
        }

        [Test]
        public void SingleValidationFailureCreatesASingleElementErrorDictionary()
        {
            var failures = new List<ValidationFailure>
            {
                new ValidationFailure("FechaInicial", "La fecha inicial es requerida"),
            };

            var actual = new ValidationException(failures).Errors;

            
            actual.Should().BeEquivalentTo(new string[] { "La fecha inicial es requerida" });
        }

        [Test]
        public void MulitpleValidationFailureForMultiplePropertiesCreatesAMultipleElementErrorDictionaryEachWithMultipleValues()
        {
            var failures = new List<ValidationFailure>
            {
                new ValidationFailure("FechaInicial", "La fecha inicial es requerida"),
                new ValidationFailure("FechaFinal", "La fecha inicial es requerida"),
            };

            var actual = new ValidationException(failures).Errors;


            actual.Should().BeEquivalentTo(new string[]
            {
                "La fecha inicial es requerida",
                "La fecha inicial es requerida"
            });
        }
    }
}
