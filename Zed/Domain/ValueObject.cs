using System;
using System.Collections.Generic;
using System.Linq;

namespace Zed.Domain {
    /// <summary>
    /// ValueObject is an immutable object that is used to describe certain aspects of a domain,
    /// and which does not have identity. Value Objects should be kept thin and simple.
    /// 
    /// Two instances of a value object of the same type are said to be the same if all of their properties
    /// are the same.
    /// </summary>
    [Serializable]
    public abstract class ValueObject : IEquatable<ValueObject>, IDomainObjectSignature<ValueObject> {

        #region Fields

        /// <summary>
        /// To ensure the uniqueness of hash code, carefully selected multiplier is choesen to be used in calculating hashcode.
        /// Goodrich and Tamassia's Data Structures an Algorithms in Java states that the numbers
        /// 31, 33, 37, 39 and 41 produce a minimum number collision.
        /// See: http://computinglife.wordpress.com/2008/11/20/why-do-hash-functions-use-prime-numbers/
        /// </summary>
        protected const int ODD_PRIME_HASH_MULTIPLIER = 31;

        /// <summary>
        /// Initial hash code value
        /// </summary>
        protected const int INTITIAL_HASH_CODE_VALUE = 17;

        /// <summary>
        /// Caches calculated hash code, since an instance can't change its hash code during its life
        /// </summary>
        protected int? CachedHashCode;

        #endregion

        #region Constructors and Init
        #endregion

        #region Methods

        /// <summary>
        /// Determines whether current instance has equal domain signature properties as provided Value object
        /// </summary>
        /// <param name="other">The Value object to compare with the current object</param>
        /// <returns>true if the currenct isntance has equal domain signature properties, otherwise false.</returns>
        public virtual bool AreDomainObjectSignaturePropertiesEqualTo(ValueObject other) {
            return DomainObjectsSrv.AreDomainObjectSignaturePropertiesEqual(this, other);
        }

        #region IDomainObjectSignature Members

        /// <summary>
        /// Gets the type of object which define domain signature.
        /// </summary>
        public virtual Type GetDomainObjectSignatureType() {
            return GetType();
        }

        /// <summary>
        /// Gets type specific domain object signature properties.
        /// </summary>
        /// <returns>Type specific domain object signature properties</returns>
        public virtual IEnumerable<DomainObjectSignatureProperty> GetTypeSpecificDomainObjectSignatureProperties() {
            return (GetType().GetProperties().Where(p => !Attribute.IsDefined(p, typeof(NotValueMemberAttribute), true))) // exclude not value member properties
                .Select(property => new DomainObjectSignatureProperty(property)).ToArray();
        }

        #endregion

        #region IEquatable<ValueObject> Members

        /// <summary>
        /// Determines whether specified object is equal to the current object.
        /// Two instances of a value object of the same type are said to be the same if all of their
        /// properties(ValueMembers) are the same.
        /// </summary>
        /// <param name="other">The object to compare with the current object.</param>
        /// <returns>true if the specified object is equal to the current object, otherwise false.</returns>
        public virtual bool Equals(ValueObject other) {
            // For any non-null reference value x, x.Equals(null) must return false
            if (other == null) {
                return false;
            }

            // if current reference is equal to other's reference then they are referring to the same instance so they are equal
            if (ReferenceEquals(this, other)) {
                return true;
            }

            return AreDomainObjectSignaturePropertiesEqualTo(other);
        }

        #endregion

        /// <summary>
        /// Determines whether specified object is equal to the current objet.
        /// Two instances of a value object of the same type are said to be the same if all of their
        /// properties are the same.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>true if the specified object is equal to the current object, otherwise false.</returns>
        public override bool Equals(object obj) {
            return Equals(obj as ValueObject); // if obj is not Value object argument to Equals(ValueObject) will be null
        }

        /// <summary>
        /// Gets hash code of a current instance
        /// Once the hash code is calculated for the current instance, method allways returns the same code.
        /// </summary>
        /// <returns>Hash code of a current object instance</returns>
        public override int GetHashCode() {
            if (!CachedHashCode.HasValue) {
                IEnumerable<DomainObjectSignatureProperty> valueMemberProperties = DomainObjectsSrv.GetDomainObjectSignatureProperties(this);

                if (valueMemberProperties.Any()) {
                    unchecked {
                        int hashCode = INTITIAL_HASH_CODE_VALUE;
                        hashCode = (ODD_PRIME_HASH_MULTIPLIER * hashCode) + GetType().GetHashCode();

                        hashCode = valueMemberProperties
                            .Select(property => property.GetValue(this))
                            .Where(value => value != null)
                            .Aggregate(hashCode, (current, value) => (ODD_PRIME_HASH_MULTIPLIER * current) + value.GetHashCode());

                        CachedHashCode = hashCode;

                    }
                } else {
                    CachedHashCode = base.GetHashCode();
                }


            }

            return CachedHashCode.Value;
        }

        #endregion

    }
}
