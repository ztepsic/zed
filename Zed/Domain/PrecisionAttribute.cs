using System;

namespace Zed.Domain {
    /// <summary>
    /// Attribute that indicates the precission of the property for float and double types.
    /// Used for easier comparison between float and double types
    /// </summary>
    /// <remarks>
    /// This is intended for use with <see cref="float" /> and <see cref="double"/> types.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class PrecisionAttribute : Attribute {

        #region Fields and Properties

        /// <summary>
        /// The float error margin that the difference is compared to.
        /// </summary>
        private readonly float epsilonFloat;

        /// <summary>
        /// Gets the float error margin that the difference is compared to.
        /// </summary>
        public float EpsilonFloat {
            get { return epsilonFloat; }
        }

        /// <summary>
        /// The double error margin that the difference is compared to.
        /// </summary>
        private readonly double epsilonDouble;

        /// <summary>
        /// Gets the double error margin that the difference is compared to.
        /// </summary>
        public double EpsilonDouble => epsilonDouble;

        #endregion

        #region Constructors and Init

        /// <summary>
        /// Constructor for float epsilon
        /// </summary>
        /// <param name="epsilon">The float error margin that the difference is compared to.</param>
        public PrecisionAttribute(float epsilon) {
            epsilonFloat = epsilon;
            epsilonDouble = epsilon;
        }

        /// <summary>
        /// Constructor for decimal epsilon
        /// </summary>
        /// <param name="epsilon">The double error margin that the difference is compared to.</param>
        public PrecisionAttribute(double epsilon) {
            epsilonDouble = epsilon;
            epsilonFloat = (float)epsilon;
        }

        #endregion
    }
}
