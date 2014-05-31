using System;

namespace Zed.Core.Objects {
    /// <summary>
    /// Exception stating that the object is immutable, that it can't be changed.
    /// </summary>
    public class ImmutableObjectException : Exception {

        #region Fields

        /// <summary>
        /// Poruka
        /// </summary>
        private const string MESSAGE = "Object {0} is immutable, so it can't be changed.";

        #endregion

        #region Constructors and Init

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="immutableObject">object that wants to change</param>
        public ImmutableObjectException(ImmutableObject immutableObject) : base(string.Format(MESSAGE, immutableObject.GetType().FullName)) { }

        #endregion

        #region Methods
        #endregion
    }
}
