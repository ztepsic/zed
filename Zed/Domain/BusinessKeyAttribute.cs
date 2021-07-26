using System;

namespace Zed.Domain {
    /// <summary>
    /// Facilitates indicating which property describe the unique business key of an entity.
    /// </summary>
    /// <remarks>
    /// This is intended for use with <see cref="IDomainObjectSignature{T}"/> and <see cref="Entity" />.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class BusinessKeyAttribute : Attribute { }
}
