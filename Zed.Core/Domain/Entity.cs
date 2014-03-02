using System;

namespace Zed.Core.Domain {

    /// <summary>
    /// Entity class is a base class for objects (entities) which will be persisted to the database.
    /// Benefits include the addition of an Id property along with a consistent manner for comparing
    /// entities.
    /// 
    /// Entities of this base class will have a type of int identifier (Id). For entities wih a type other than int,
    /// then use <see cref="Entity{TId}"/> instead.
    /// </summary>
    public abstract class Entity : Entity<int> { }

    /// <summary>
    /// Entity class is a base class for objects (entities) which will be persisted to the database.
    /// Benefits include the addition of an Id property along with a consistent manner for comparing
    /// entities.
    /// </summary>
    /// <typeparam name="TId">Identifier (Id) type</typeparam>
    [Serializable]
    public abstract class Entity<TId> : IEquatable<Entity<TId>> {

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

        /// <summary>
        /// Entity identifier.
        /// Once set it should never change.
        /// </summary>
        public virtual TId Id { get; protected set; }

        #endregion

        #region Constructors and Init
        #endregion

        #region Methods

        /// <summary>
        /// Gets the type of object which define domain signature.
        /// </summary>
        public virtual Type GetDomainObjectSignatureType() {
            return GetType();
        }

        /// <summary>
        /// Is the current instance transient.
        /// Transient entity instances do not have assigned identifier. 
        /// E.g. value of unassigned identifier could be, depending on the type, null or 0 or default value.
        /// </summary>
        /// <returns>True if the current instance is transient, otherwise is false.</returns>
        public virtual bool IsTransient() {
            return Equals(Id, default(TId));
        }


        #region IEquatable<T> Members

        /// <summary>
        /// Determines whether specified object is equal to the current object.
        /// Two entities are equal if they are both reffering to the same instance, or
        /// they are both of the same type and are persistent objects with the same identifier.
        /// </summary>
        /// <param name="other">The object to compare with the current object.</param>
        /// <returns>true if the specified object is equal to the current object, otherwise false.</returns>
        public virtual bool Equals(Entity<TId> other) {
            // For any non-null reference value x, x.Equals(null) must return false
            if (other == null) {
                return false;
            }

            // if current reference is equal to other's reference then they are referring to the same instance so they are equal
            if (ReferenceEquals(this, other)) {
                return true;
            }

            // if both objects are persistent objects and have the same domain object signature type, then compare them based on identifier
            if (!this.IsTransient() && !other.IsTransient() && GetDomainObjectSignatureType().Equals(other.GetDomainObjectSignatureType())) {
                return Id.Equals(other.Id);
            }

            return false;
        }

        #endregion

        /// <summary>
        /// Determines whether specified object is equal to the current object.
        /// Two entities are equal if they are both reffering to the same instance, or
        /// they are both of the same type and are persistent objects with the same identifier.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>true if the specified object is equal to the current object, otherwise false.</returns>
        public override bool Equals(object obj) {
            return Equals(obj as Entity<TId>); // if obj is not Entity object argument to Equals(Entity) will be null
        }

        /// <summary>
        /// Serves as a hash function for a particular type.
        /// </summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode() {
            if (!CachedHashCode.HasValue) {
                if (IsTransient()) {
                    CachedHashCode = base.GetHashCode();
                } else {
                    unchecked {
                        int hashCode = INTITIAL_HASH_CODE_VALUE;
                        hashCode = (ODD_PRIME_HASH_MULTIPLIER * hashCode) + GetDomainObjectSignatureType().GetHashCode();
                        hashCode = (ODD_PRIME_HASH_MULTIPLIER * hashCode) + Id.GetHashCode();
                        CachedHashCode = hashCode;
                    }
                }
            }

            return CachedHashCode.Value;
        }

        #endregion

    }
}
