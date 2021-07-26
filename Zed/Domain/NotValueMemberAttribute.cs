using System;

namespace Zed.Domain {
    /// <summary>
    /// Facilitates indicating which property is not value member property.
    /// </summary>
    /// <remarks>
    /// This is intended for use with <see cref="ValueObject" />
    /// </remarks>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class NotValueMemberAttribute : Attribute { }
}
