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
        /// <param name="errorMessage">When an error is specified then validation will fail</param>
        /// <param name="propertyName">Used only when <paramref name="errorMessage"/> is specified.
        /// When null a random value is assigned to the property name validation error </param>
        /// <returns></returns>
        public static Mock<IValidator<T>> MockValidateAsync<T> (this Mock<IValidator<T>> mock, string errorMessage = null, string propertyName =null)
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
