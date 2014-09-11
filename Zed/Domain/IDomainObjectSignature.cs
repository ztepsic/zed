using System;
using System.Collections.Generic;

namespace Zed.Domain {
    /// <summary>
    /// Represents domain object signature interface
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDomainObjectSignature<in T> where T : class, IDomainObjectSignature<T> {
        /// <summary>
        /// Gets type specific domain object signature properties.
        /// </summary>
        /// <returns>Type specific domain object signature properties</returns>
        IEnumerable<DomainObjectSignatureProperty> GetTypeSpecificDomainObjectSignatureProperties();

        /// <summary>
        /// Gets the type of object which define domain signature.
        /// </summary>
        Type GetDomainObjectSignatureType();

        /// <summary>
        /// Determines whether current instance has equal domain signature properties as provided object
        /// </summary>
        /// <param name="other">The object to compare domain signature properties with the current object</param>
        /// <returns>true if the currenct instance has equal domain signature properties, otherwise false.</returns>
        bool AreDomainObjectSignaturePropertiesEqualTo(T other);
    }
}
