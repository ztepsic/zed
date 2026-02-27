using FluentResults;

namespace Zed.Errors {
    /// <summary>
    /// Represents a validation error associated with a specific property.
    /// </summary>
    public class ValidationError : Error {

        #region Fields and Properties

        /// <summary>
        /// Property name metadata key.
        /// </summary>
        public const string PropertyNameMetadataKey = "PropertyName";

        /// <summary>
        /// Gets the name of the property that failed validation.
        /// </summary>
        public string PropertyName => (string)Metadata[PropertyNameMetadataKey];

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of <see cref="ValidationError"/> with a property name, a message, and a causing error.
        /// </summary>
        /// <param name="propertyName">The name of the property that failed validation.</param>
        /// <param name="message">The validation error message.</param>
        /// <param name="causedBy">The underlying error that caused this error.</param>
        public ValidationError(string propertyName, string message, IError causedBy) : base(message, causedBy) {
            Metadata.Add(PropertyNameMetadataKey, propertyName);
        }

        /// <summary>
        /// Initializes a new instance of <see cref="ValidationError"/> with a property name and a message.
        /// </summary>
        /// <param name="propertyName">The name of the property that failed validation.</param>
        /// <param name="message">The validation error message.</param>
        public ValidationError(string propertyName, string message) : base(message) {
            Metadata.Add(PropertyNameMetadataKey, propertyName);
        }

        /// <summary>
        /// Initializes a new instance of <see cref="ValidationError"/> with a property name.
        /// </summary>
        /// <param name="propertyName">The name of the property that failed validation.</param>
        public ValidationError(string propertyName) : base() {
            Metadata.Add(PropertyNameMetadataKey, propertyName);
        }

        #endregion

    }
}
