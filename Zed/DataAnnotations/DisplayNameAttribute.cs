using System;

namespace Zed.DataAnnotations {
    /// <summary>
    /// Specifies the display name for a field or property
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    public class DisplayNameAttribute : Attribute {

        #region Fields and Properties

        /// <summary>
        /// Gets the display name for a field or property.
        /// </summary>
        public string DisplayName { get; }

        #endregion

        #region Constructors and Init

        /// <summary>
        /// Initializes a new instance of the <see cref="DisplayNameAttribute"/> class using the display name.
        /// </summary>
        /// <param name="displayName">The display name.</param>
        public DisplayNameAttribute(string displayName) {
            DisplayName = displayName;
        }

        #endregion

    }
}
