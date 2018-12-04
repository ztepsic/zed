using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Zed.Utilities {
    /// <summary>
    /// Reflection helper class
    /// </summary>
    public static class ReflectionHelper {

        /// <summary>
        /// Gets Attribute from member by specifying attribute type
        /// </summary>
        /// <typeparam name="T">Attribute type</typeparam>
        /// <param name="member">Member from which attribute is gathered</param>
        /// <param name="isRequired">An indicator if attribute must be defined on member</param>
        /// <returns>Required attribute</returns>
        public static T GetAttribute<T>(this MemberInfo member, bool isRequired = false) where T : Attribute {
            var attribute = member.GetCustomAttributes(typeof(T), false).SingleOrDefault();

            if (attribute == null && isRequired) {
                throw new ArgumentException($"The {typeof(T).Name} attribute must be defined on member {member.Name}");
            }

            return attribute as T;
        }

        /// <summary>
        /// Gets property info for provided property expression
        /// </summary>
        /// <param name="propertyExpression">Property expression</param>
        /// <returns>Property info</returns>
        public static MemberInfo GetPropertyInfo(Expression propertyExpression) {
            MemberInfo memberInfo = null;

            MemberExpression memberExpression = propertyExpression as MemberExpression;
            if (memberExpression == null) {
                UnaryExpression unaryExpression = propertyExpression as UnaryExpression;
                if (unaryExpression != null && unaryExpression.NodeType == ExpressionType.Convert) {
                    memberExpression = unaryExpression.Operand as MemberExpression;
                }
            }

            if (memberExpression != null && memberExpression.Member.MemberType == MemberTypes.Property) {
                memberInfo = memberExpression.Member;
            }

            return memberInfo;
        }

    }
}
