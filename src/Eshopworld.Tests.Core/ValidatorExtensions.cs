using System.Threading;
using FluentValidation;
using FluentValidation.Results;
using Moq;

namespace Eshopworld.Tests.Core
{
    /// <summary>
    /// Extensions for <see cref="IValidator"/>
    /// </summary>
    public static class ValidatorExtensions
    {
        /// <summary>
        /// Mocks ValidateAsync method
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="mock"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public static Mock<IValidator<T>> MockValidateAsync<T> (this Mock<IValidator<T>> mock, string propertyName =null, string errorMessage = null)
        {
            var result = new ValidationResult();
            if (!string.IsNullOrEmpty(errorMessage))
                result.Errors.Add(new ValidationFailure(propertyName ?? Lorem.GetWord(), errorMessage));

            mock
                .Setup (validator =>
                    validator.ValidateAsync (It.IsAny<IValidationContext> (), It.IsAny<CancellationToken> ()))
                .ReturnsAsync (result)
                .Verifiable ();

            return mock;
        }
    }
}
