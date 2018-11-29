using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using DisplayNameAttribute = Zed.DataAnnotations.DisplayNameAttribute;

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
                var attributes = memberInfo[0].GetCustomAttributes(false);
                var displayNameFound = false;
                foreach (var attribute in attributes) {
                    switch (attribute) {
                        case DescriptionAttribute da:
                            description = da.Description;
                            displayNameFound = true;
                            break;
                        case DisplayAttribute da:
                            displayNameFound = !string.IsNullOrEmpty(da.Description);
                            description = da.Description;
                            break;
                    }

                    if (displayNameFound) break;
                }

            }

            return description;
        }


        /// <summary>
        /// Gets a display name of enum
        /// </summary>
        /// <typeparam name="T">Enum type</typeparam>
        /// <param name="enumValue">Current enum</param>
        /// <returns>Enum display name</returns>
        public static string GetEnumDisplayName<T>(this T enumValue) where T : struct {
            var type = enumValue.GetType();
            if (!type.IsEnum) {
                throw new ArgumentException($"{nameof(enumValue)} must be of Enum type", nameof(enumValue));
            }

            string displayName = enumValue.ToString();

            var memberInfo = type.GetMember(enumValue.ToString());
            if (memberInfo.Length > 0) {
                var attributes = memberInfo[0].GetCustomAttributes(false);
                var displayNameFound = false;
                foreach (var attribute in attributes) {
                    switch (attribute) {
                        case System.ComponentModel.DisplayNameAttribute dna:
                            displayName = dna.DisplayName;
                            displayNameFound = true;
                            break;
                        case DisplayNameAttribute dna:
                            displayName = dna.DisplayName;
                            displayNameFound = true;
                            break;
                        case DisplayAttribute da:
                            displayNameFound = !string.IsNullOrEmpty(da.Name);
                            displayName = da.Name;
                            break;
                    }

                    if(displayNameFound) break;
                }
            }

            return displayName;
        }

    }
}
