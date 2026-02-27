using FluentResults;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Zed.Errors {
    /// <summary>
    /// Provides extension methods for converting <see cref="IError"/> instances to exceptions.
    /// </summary>
    public static class ErrorExtensions {

        /// <summary>
        /// Converts an <see cref="IError"/> to its corresponding <see cref="ErrorException"/>.
        /// </summary>
        /// <param name="error">The error to convert.</param>
        /// <returns>An <see cref="ErrorException"/> representing the error.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="error"/> is <see langword="null"/>.</exception>
        public static ErrorException ToErrorException(this IError error) {
            ArgumentNullException.ThrowIfNull(error);

            var exception = error switch {
                AppError appError => appError.ToAppErrorException(),
                ValidationError validationError => validationError.ToValidationErrorException(),
                Error e => new ErrorException(e),
                _ => new ErrorException(new Error(error.Message))
            };

            return exception;
        }

        /// <summary>
        /// Converts an <see cref="AppError"/> to an <see cref="AppErrorException"/>.
        /// </summary>
        /// <param name="appError">The application error to convert.</param>
        /// <returns>An <see cref="AppErrorException"/> wrapping the <paramref name="appError"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="appError"/> is <see langword="null"/>.</exception>
        public static AppErrorException ToAppErrorException(this AppError appError) {
            ArgumentNullException.ThrowIfNull(appError);
            return new AppErrorException(appError);
        }

        /// <summary>
        /// Converts a <see cref="ValidationError"/> to a <see cref="ValidationErrorException"/>.
        /// </summary>
        /// <param name="validationError">The validation error to convert.</param>
        /// <returns>A <see cref="ValidationErrorException"/> wrapping the <paramref name="validationError"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="validationError"/> is <see langword="null"/>.</exception>
        public static ValidationErrorException ToValidationErrorException(this ValidationError validationError) {
            ArgumentNullException.ThrowIfNull(validationError);
            return new ValidationErrorException(validationError);
        }

        /// <summary>
        /// Converts a collection of <see cref="ValidationFailure"/> instances to a list of <see cref="ValidationError"/> instances.
        /// </summary>
        /// <param name="errors">The validation failures to convert.</param>
        /// <returns>A list of <see cref="ValidationError"/> instances.</returns>
        public static IList<ValidationError> ToValidationErrors(this IEnumerable<ValidationFailure> errors)
            => [.. errors.Select(e => new ValidationError(e.PropertyName, e.ErrorMessage))];

        /// <summary>
        /// Wraps an <see cref="IError"/> in a failed <see cref="Result"/>.
        /// </summary>
        /// <param name="error">The error to wrap.</param>
        /// <returns>A failed <see cref="Result"/> containing <paramref name="error"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="error"/> is <see langword="null"/>.</exception>
        public static Result ToFailResult(this IError error) {
            ArgumentNullException.ThrowIfNull(error);
            return Result.Fail(error);
        }

        /// <summary>
        /// Throws an <see cref="ErrorException"/> if the <paramref name="result"/> has failed.
        /// </summary>
        /// <param name="result">The result to inspect.</param>
        /// <remarks>
        /// Use this method at application boundaries (e.g., middleware, gRPC interceptors) to convert
        /// a failed <see cref="ResultBase"/> into an exception for interop with exception-based
        /// infrastructure. Within the application, prefer propagating <see cref="Result"/> failures
        /// instead of throwing.
        /// </remarks>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="result"/> is <see langword="null"/>.</exception>
        /// <exception cref="ErrorException">Thrown when <paramref name="result"/> has failed.</exception>
        public static void ThrowIfFailed(this ResultBase result) {
            ArgumentNullException.ThrowIfNull(result);
            if (result.IsFailed)
                throw result.Errors[0].ToErrorException();
        }

    }
}
