using System;
using System.Collections.Generic;
using System.Linq;
using Zed.Core.Utilities;

namespace Zed.Core.Domain {
    /// <summary>
    /// Service that knows how to operate with DomainObjects <see cref="IDomainObjectSignature{T}" />
    /// </summary>
    public static class DomainObjectsSrv {

        #region Fields and Properties

        /// <summary>
        /// Cache for DomainObject(ValueObject) properties
        /// </summary>
        [ThreadStatic]
        private static Dictionary<Type, IEnumerable<DomainObjectSignatureProperty>> domainObjectsPropertiesDictionary;

        #endregion

        #region Constructors and Init
        #endregion

        #region Methods

        /// <summary>
        /// Gets domain object signature properties of the current instance
        /// </summary>
        /// <typeparam name="T">domain object which implements <see cref="IDomainObjectSignature{T}"/></typeparam>
        /// <returns>Domain object signature properties of the current instance</returns>
        public static IEnumerable<DomainObjectSignatureProperty> GetDomainObjectSignatureProperties<T>(IDomainObjectSignature<T> domainObject) where T : class, IDomainObjectSignature<T> {
            IEnumerable<DomainObjectSignatureProperty> properties = null;

            if (domainObjectsPropertiesDictionary == null) {
                domainObjectsPropertiesDictionary = new Dictionary<Type, IEnumerable<DomainObjectSignatureProperty>>();
            }

            if (domainObjectsPropertiesDictionary.TryGetValue(domainObject.GetDomainObjectSignatureType(), out properties)) {
                return properties;
            } else {
                return domainObjectsPropertiesDictionary[domainObject.GetDomainObjectSignatureType()] = domainObject.GetTypeSpecificDomainObjectSignatureProperties();
            }
        }


        /// <summary>
        /// Determines whether specified two domain objects have equal signature properties by comparing values of domain object signature properties
        /// </summary>
        /// <typeparam name="T">domain object which implements <see cref="IDomainObjectSignature{T}"/></typeparam>
        /// <param name="domainObjectA">The first domain object to compare</param>
        /// <param name="domainObjectB">The second domain object to compare</param>
        /// <returns>true if the specified objects have equal domain signature properties, otherwise false.</returns>
        public static bool AreDomainObjectSignaturePropertiesEqual<T>(IDomainObjectSignature<T> domainObjectA, IDomainObjectSignature<T> domainObjectB) where T : class, IDomainObjectSignature<T> {
            // Check if both objects have same DomainObjectSignatureType
            if (domainObjectA.GetDomainObjectSignatureType().Equals(domainObjectB.GetDomainObjectSignatureType())) {

                IEnumerable<DomainObjectSignatureProperty> valueMemberProperties = GetDomainObjectSignatureProperties(domainObjectA);

                if (valueMemberProperties.Any()) {

                    Func<object, object, PrecisionAttribute, bool> equalsFunc = (objA, objB, precision) => {
                        if (objA is float && precision != null) {
                            return NumericHelper.AreNearlyEqual((float)objA, (float)objB, precision.EpsilonFloat);
                        } else if (objA is double && precision != null) {
                            return NumericHelper.AreNearlyEqual((double)objA, (double)objB, precision.EpsilonFloat);
                        } else {
                            return objA.Equals(objB);
                        }
                    };

                    // True if there is at least one property whose values of both objects are NOT equal, false if all property values of both objects are equal
                    // (Using inverted logic(true/false) because of short-circuit evaluation, faster evaluation)
                    // Where parts explanations:
                    // - First where
                    //	- if the values of the properties of both objects are null, seek further
                    // - Second where
                    //	- if one property value is not null and the other is null, then values are not equal
                    //	- if property values are not null, check equality with Equals method
                    bool conditionOfInequality = (from property in valueMemberProperties
                                                  let valueOfThisObject = property.GetValue(domainObjectA)
                                                  let valueOfCompareTo = property.GetValue(domainObjectB)
                                                  where valueOfThisObject != null || valueOfCompareTo != null
                                                  where
                                                  (valueOfThisObject == null ^ valueOfCompareTo == null) ||
                                                  (!equalsFunc(valueOfThisObject, valueOfCompareTo, property.Precision))
                                                  select valueOfThisObject).Any();
                    return !conditionOfInequality;
                }

            }

            return false;

        }

        #endregion

    }
}
