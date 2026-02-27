using FluentResults;
using System;
using System.Net;

namespace Zed.Errors
{
    /// <summary>
    /// Represents an application error associated with an HTTP status code.
    /// </summary>
    public class HttpStatusCodeAppError : AppError
    {

        #region Fields and Properties


        /// <summary>
        /// Gets the http status error code associated with this error, or <see langword="null"/> if not set.
        /// </summary>
        public HttpStatusCode? HttpStatusCode => Code.HasValue ? (HttpStatusCode)Code : null;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of <see cref="HttpStatusCodeAppError"/> with a http status error code, a message, and a causing error.
        /// </summary>
        /// <param name="code">The http status error code associated with the error.</param>
        /// <param name="message">The error message.</param>
        /// <param name="causedBy">The underlying error that caused this error.</param>
        public HttpStatusCodeAppError(HttpStatusCode code, string message, IError causedBy)
            : base((int)code, message, causedBy) { }

        /// <summary>
        /// Initializes a new instance of <see cref="HttpStatusCodeAppError"/> with a http status error code, a message, and a causing exception.
        /// </summary>
        /// <param name="code">The http status error code associated with the error.</param>
        /// <param name="message">The error message.</param>
        /// <param name="causedBy">The exception that caused this error.</param>
        public HttpStatusCodeAppError(HttpStatusCode code, string message, Exception causedBy)
            : base((int)code, message, causedBy) { }

        /// <summary>
        /// Initializes a new instance of <see cref="HttpStatusCodeAppError"/> with a http status error code and a message.
        /// </summary>
        /// <param name="code">The error code associated with the error.</param>
        /// <param name="message">The error message.</param>
        public HttpStatusCodeAppError(HttpStatusCode code, string message)
            : base((int)code, message) { }

        /// <summary>
        /// Initializes a new instance of <see cref="HttpStatusCodeAppError"/> with a http status error code.
        /// </summary>
        /// <param name="code">The http status error code associated with the error.</param>
        public HttpStatusCodeAppError(HttpStatusCode code)
            : base((int)code) { }

        #endregion

        #region Factory Methods

        /// <summary>Creates an <see cref="HttpStatusCodeAppError"/> with http status error code 400 (Bad Request).</summary>
        /// <param name="message">The error message.</param>
        public static HttpStatusCodeAppError BadRequest(string message) => new(System.Net.HttpStatusCode.BadRequest, message);

        /// <summary>Creates an <see cref="HttpStatusCodeAppError"/> with http status error code 401 (Unauthorized).</summary>
        /// <param name="message">The error message.</param>
        public static HttpStatusCodeAppError Unauthorized(string message) => new(System.Net.HttpStatusCode.Unauthorized, message);

        /// <summary>Creates an <see cref="AppError"/> with error code 403 (Forbidden).</summary>
        /// <param name="message">The error message.</param>
        public static HttpStatusCodeAppError Forbidden(string message) => new(System.Net.HttpStatusCode.Forbidden, message);

        /// <summary>Creates an <see cref="AppError"/> with http status error code 404 (Not Found).</summary>
        /// <param name="message">The error message.</param>
        public static HttpStatusCodeAppError NotFound(string message) => new(System.Net.HttpStatusCode.NotFound, message);

        /// <summary>Creates an <see cref="AppError"/> with http status error code 409 (Conflict).</summary>
        /// <param name="message">The error message.</param>
        public static HttpStatusCodeAppError Conflict(string message) => new(System.Net.HttpStatusCode.Conflict, message);

        /// <summary>Creates an <see cref="AppError"/> with http status error code 422 (Unprocessable Entity).</summary>
        /// <param name="message">The error message.</param>
        public static HttpStatusCodeAppError UnprocessableEntity(string message) => new(System.Net.HttpStatusCode.UnprocessableEntity, message);

        /// <summary>Creates an <see cref="AppError"/> with http status error code 500 (Internal Server Error).</summary>
        /// <param name="message">The error message.</param>
        public static HttpStatusCodeAppError InternalServerError(string message) => new(System.Net.HttpStatusCode.InternalServerError, message);

        #endregion

    }
}
