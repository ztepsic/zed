using System;
using System.Collections.Generic;
using System.Linq;
using Zed.Domain;

namespace Zed.Tests.Domain.Entities.DomainObjectsSignatureImpl {

    /// <summary>
    /// DomainObjectSignatureEntity
    /// Example of implementation of IDomainObjectSignature for Entity class
    /// </summary>
    public abstract class DosEntity : Entity, IDomainObjectSignature<DosEntity>, IEquatable<DosEntity> {
        #region IDomainObjectSignature<DosEntity> Members

        public IEnumerable<DomainObjectSignatureProperty> GetTypeSpecificDomainObjectSignatureProperties() {
            return (GetType().GetProperties().Where(p => Attribute.IsDefined(p, typeof(BusinessKeyAttribute), true))) // exclude not value member properties
                .Select(property => new DomainObjectSignatureProperty(property)).ToArray();
        }

        public bool AreDomainObjectSignaturePropertiesEqualTo(DosEntity other) {
            return DomainObjectsSrv.AreDomainObjectSignaturePropertiesEqual(this, other);
        }

        public virtual bool Equals(DosEntity other) {
            // For any non-null reference value x, x.Equals(null) must return false
            if (other == null) {
                return false;
            }

            // if current reference is equal to other's reference then they are referring to the same instance so they are equal
            if (ReferenceEquals(this, other)) {
                return true;
            }

            // if both objects are persistent objects and have the same domain object signature type, then compare them based on identifier
            if (this.IsTransient() && other.IsTransient()) {
                return AreDomainObjectSignaturePropertiesEqualTo(other);
            } else {
                if (GetDomainObjectSignatureType().Equals(other.GetDomainObjectSignatureType())) {
                    return Id.Equals(other.Id);
                }
            }

            return false;
        }

        #endregion
    }
}
