using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using Zed.Utilities;

namespace Zed.DataAnnotations {
    /// <summary>
    /// Data annotation extension
    /// </summary>
    public static class DataAnnotationExtensions {

        /// <summary>
        /// Gets property display name
        /// </summary>
        /// <typeparam name="T">Class with property</typeparam>
        /// <param name="propertyExpression">Property expression</param>
        /// <returns>Property display name</returns>
        public static string GetPropertyDisplayName<T>(Expression<Func<T, object>> propertyExpression) where T:class {
            var memberInfo = ReflectionHelper.GetPropertyInfo(propertyExpression.Body);
            if (memberInfo == null) {
                throw new ArgumentException("No property reference epxression was found.", nameof(propertyExpression));
            }

            string displayName = memberInfo.Name;

            var attributes = memberInfo.GetCustomAttributes(false);
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

                if (displayNameFound) break;
            }

            return displayName;
        }

        /// <summary>
        /// Gets property display name
        /// </summary>
        /// <typeparam name="T">Class with property</typeparam>
        /// <param name="type">Type which contains property</param>
        /// <param name="propertyExpression">Property expression</param>
        /// <returns>Property display name</returns>
        public static string GetPropertyDisplayName<T>(this T type, Expression<Func<T, object>> propertyExpression) where T : class {
            return GetPropertyDisplayName(propertyExpression);
        }

        /// <summary>
        /// Gets property description
        /// </summary>
        /// <typeparam name="T">Class with property</typeparam>
        /// <param name="propertyExpression">Property expression</param>
        /// <returns>Property description</returns>
        public static string GetPropertyDescription<T>(Expression<Func<T, object>> propertyExpression) where T : class {
            var memberInfo = ReflectionHelper.GetPropertyInfo(propertyExpression.Body);
            if (memberInfo == null) {
                throw new ArgumentException("No property reference epxression was found.", nameof(propertyExpression));
            }

            string description = memberInfo.Name;

            var attributes = memberInfo.GetCustomAttributes(false);
            var descriptionFound = false;
            foreach (var attribute in attributes) {
                switch (attribute) {
                    case DescriptionAttribute da:
                        description = da.Description;
                        descriptionFound = true;
                        break;
                    case DisplayAttribute da:
                        descriptionFound = !string.IsNullOrEmpty(da.Description);
                        description = da.Description;
                        break;
                }

                if (descriptionFound) break;
            }

            return description;
        }

        /// <summary>
        /// Gets property description
        /// </summary>
        /// <typeparam name="T">Class with property</typeparam>
        /// <param name="type">Type which contains property</param>
        /// <param name="propertyExpression">Property expression</param>
        /// <returns>Property description</returns>
        public static string GetPropertyDescription<T>(this T type, Expression<Func<T, object>> propertyExpression) where T : class {
            return GetPropertyDescription(propertyExpression);
        }

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
                var decriptionFound = false;
                foreach (var attribute in attributes) {
                    switch (attribute) {
                        case DescriptionAttribute da:
                            description = da.Description;
                            decriptionFound = true;
                            break;
                        case DisplayAttribute da:
                            decriptionFound = !string.IsNullOrEmpty(da.Description);
                            description = da.Description;
                            break;
                    }

                    if (decriptionFound) break;
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

                    if (displayNameFound) break;
                }
            }

            return displayName;
        }
    }
}
