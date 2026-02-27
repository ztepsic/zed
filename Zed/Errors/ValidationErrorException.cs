namespace Zed.Errors {
    /// <summary>
    /// Represents an exception that wraps a <see cref="ValidationError"/>.
    /// </summary>
    /// <remarks>
    /// This type is intended for use at application boundaries (e.g., middleware, gRPC interceptors)
    /// to convert a failed <see cref="FluentResults.Result"/> into an exception for interop with
    /// exception-based infrastructure. Prefer returning <see cref="FluentResults.Result"/> failures
    /// within the application instead of throwing this exception directly.
    /// </remarks>
    public class ValidationErrorException : ErrorException {

        /// <summary>
        /// Gets the <see cref="ValidationError"/> associated with this exception.
        /// </summary>
        public ValidationError ValidationError => (ValidationError)Error;

        /// <summary>
        /// Initializes a new instance of <see cref="ValidationErrorException"/> with the specified <see cref="ValidationError"/>.
        /// </summary>
        /// <param name="validationError">The validation error associated with this exception.</param>
        public ValidationErrorException(ValidationError validationError) : base(validationError) { }

    }
}
