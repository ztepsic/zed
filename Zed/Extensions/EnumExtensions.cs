using System;
using System.ComponentModel;

namespace Zed.Extensions {
    /// <summary>
    /// Enum extensions
    /// </summary>
    public static class EnumExtensions {

        /// <summary>
        /// Gets a description of enum
        /// </summary>
        /// <typeparam name="T">Enum type</typeparam>
        /// <param name="enumValue">Current enum</param>
        /// <returns>Enum description</returns>
        public static string GetEnumDescription<T>(this T enumValue) where T : struct {
            var type = enumValue.GetType();
            if (!type.IsEnum) {
                throw new ArgumentException($"{nameof(enumValue)} must be of Enum type", nameof(enumValue));
            }

            string description = enumValue.ToString();

            var memberInfo = type.GetMember(enumValue.ToString());
            if (memberInfo.Length > 0) {
                var attributes = memberInfo[0].GetCustomAttributes(typeof (DescriptionAttribute), false);
                if (attributes.Length > 0) {
                    var descriptionAttribute = attributes[0] as DescriptionAttribute;
                    if (descriptionAttribute != null)
                        description = descriptionAttribute.Description;
                }
            }

            return description;
        }

    }
}
