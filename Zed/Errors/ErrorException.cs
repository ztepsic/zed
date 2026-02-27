using FluentResults;
using System;

namespace Zed.Errors {
    /// <summary>
    /// Represents an exception that wraps a <see cref="Error"/>.
    /// </summary>
    /// <remarks>
    /// This type is intended for use at application boundaries (e.g., middleware, gRPC interceptors)
    /// to convert a failed <see cref="FluentResults.Result"/> into an exception for interop with
    /// exception-based infrastructure. Prefer returning <see cref="FluentResults.Result"/> failures
    /// within the application instead of throwing this exception directly.
    /// </remarks>
    public class ErrorException : Exception {

        /// <summary>
        /// Gets the <see cref="IError"/> associated with this exception.
        /// </summary>
        public IError Error { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="ErrorException"/> with the specified <see cref="Error"/>.
        /// </summary>
        /// <param name="error">The error associated with this exception.</param>
        public ErrorException(Error error) : base(error.Message)
            => Error = error;
    }
}
