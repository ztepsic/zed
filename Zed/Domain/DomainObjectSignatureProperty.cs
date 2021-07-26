using System;
using System.Reflection;

namespace Zed.Domain {
    /// <summary>
    /// Wrapper class of <see cref="PropertyInfo"/> representing DomainObjectSignatureProperty
    /// </summary>
    public class DomainObjectSignatureProperty {

        #region Fields and Properties

        private readonly PropertyInfo propertyInfo;
        private readonly PrecisionAttribute precisionAttribute;

        /// <summary>
        /// Gets precision attribute of domain object signature property
        /// </summary>
        public PrecisionAttribute Precision => precisionAttribute;

        #endregion

        #region Constructors and Init

        /// <summary>
        /// Creates DomainObjectSignatureProperty based on provided propertyInfo.
        /// </summary>
        /// <param name="propertyInfo"></param>
        public DomainObjectSignatureProperty(PropertyInfo propertyInfo) {
            this.propertyInfo = propertyInfo;
            if (Attribute.IsDefined(propertyInfo, typeof(PrecisionAttribute), true)) {
                precisionAttribute = Attribute.GetCustomAttribute(propertyInfo, typeof(PrecisionAttribute)) as PrecisionAttribute;
            } else {
                precisionAttribute = null;
            }
        }


        #endregion

        #region Methods

        /// <summary>
        /// Returns the property value of a specified object
        /// </summary>
        /// <param name="obj">The object whose property value will be returned. </param>
        /// <returns>The property value of the specified object.</returns>
        public object GetValue(object obj) {
            return propertyInfo.GetValue(obj, null);
        }

        #endregion

    }
}
