namespace Zed.Errors {
    /// <summary>
    /// Represents an exception that wraps an <see cref="AppError"/>.
    /// </summary>
    /// <remarks>
    /// This type is intended for use at application boundaries (e.g., middleware, gRPC interceptors)
    /// to convert a failed <see cref="FluentResults.Result"/> into an exception for interop with
    /// exception-based infrastructure. Prefer returning <see cref="FluentResults.Result"/> failures
    /// within the application instead of throwing this exception directly.
    /// </remarks>
    public class AppErrorException : ErrorException {

        /// <summary>
        /// Gets the <see cref="AppError"/> associated with this exception.
        /// </summary>
        public AppError AppError => (AppError)Error;

        /// <summary>
        /// Initializes a new instance of <see cref="AppErrorException"/> with the specified <see cref="AppError"/>.
        /// </summary>
        /// <param name="appError">The application error associated with this exception.</param>
        public AppErrorException(AppError appError) : base(appError) {
        }
    }
}
