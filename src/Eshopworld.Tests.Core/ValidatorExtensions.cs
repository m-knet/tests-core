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
        /// <param name="errorMessage">error message for the exception</param>
        /// <returns></returns>
        public static Mock<IValidator<T>> MockValidateAsync<T> (this Mock<IValidator<T>> mock, string errorMessage = null)
        {
            mock
                .Setup (validator =>
                    validator.ValidateAsync (It.IsAny<IValidationContext> () ,It.IsAny<CancellationToken> ()))
                .ThrowsAsync (new ValidationException (errorMessage))
                .Verifiable ();

            return mock;
        }
    }
}
