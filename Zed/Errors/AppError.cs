using FluentResults;
using System;

namespace Zed.Errors {
    /// <summary>
    /// Represents an application error with an associated domain-level error code.
    /// </summary>
    public class AppError : Error {

        #region Fields and Properties

        /// <summary>
        /// Error code metadata key.
        /// </summary>
        public const string CodeMetadataKey = "Code";

        /// <summary>
        /// Gets the error code associated with this error, or <see langword="null"/> if not set.
        /// </summary>
        public int? Code => Metadata.TryGetValue(CodeMetadataKey, out object? value)
            ? (int)value : null;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of <see cref="AppError"/> with an error code, a message, and a causing error.
        /// </summary>
        /// <param name="code">The error code associated with the error.</param>
        /// <param name="message">The error message.</param>
        /// <param name="causedBy">The underlying error that caused this error.</param>
        public AppError(int code, string message, IError causedBy) : base(message, causedBy) {
            Metadata.Add(CodeMetadataKey, code);
        }

        /// <summary>
        /// Initializes a new instance of <see cref="AppError"/> with an error code, a message, and a causing exception.
        /// </summary>
        /// <param name="code">The error code associated with the error.</param>
        /// <param name="message">The error message.</param>
        /// <param name="causedBy">The exception that caused this error.</param>
        public AppError(int code, string message, Exception causedBy) : base(message, new ExceptionalError(causedBy)) {
            Metadata.Add(CodeMetadataKey, code);
        }

        /// <summary>
        /// Initializes a new instance of <see cref="AppError"/> with an error code and a message.
        /// </summary>
        /// <param name="code">The error code associated with the error.</param>
        /// <param name="message">The error message.</param>
        public AppError(int code, string message) : base(message) {
            Metadata.Add(CodeMetadataKey, code);
        }

        /// <summary>
        /// Initializes a new instance of <see cref="AppError"/> with an error code.
        /// </summary>
        /// <param name="code">The error code associated with the error.</param>
        public AppError(int code) : base() {
            Metadata.Add(CodeMetadataKey, code);
        }

        #endregion

    }
}
