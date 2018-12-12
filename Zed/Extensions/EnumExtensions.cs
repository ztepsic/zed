using System;
using Zed.DataAnnotations;

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
        [Obsolete("Method is deprecated, please use Zed.DataAnnodations.DataAnnotationExtensions.GetEnumDescription")]
        public static string GetEnumDescription<T>(this T enumValue) where T : struct {
            return DataAnnotationExtensions.GetEnumDescription(enumValue);
        }


        /// <summary>
        /// Gets a display name of enum
        /// </summary>
        /// <typeparam name="T">Enum type</typeparam>
        /// <param name="enumValue">Current enum</param>
        /// <returns>Enum display name</returns>
        [Obsolete("Method is deprecated, please use Zed.DataAnnodations.DataAnnotationExtensions.GetEnumDisplayName")]
        public static string GetEnumDisplayName<T>(this T enumValue) where T : struct {
            return DataAnnotationExtensions.GetEnumDisplayName(enumValue);
        }

    }
}
