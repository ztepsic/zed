using System;

namespace Zed.Utilities {
    /// <summary>
    /// Word Separator
    /// </summary>
    public enum WordSeparator {
        /// <summary>
        /// -
        /// </summary>
        Dash,

        /// <summary>
        /// _
        /// </summary>
        Underscore
    }

    /// <summary>
    /// Class provides additional data from WordSeparator enum.
    /// </summary>
    internal static class WordSeparatorEnumProvider {

        /// <summary>
        /// Gets WordSeparator Enum Value
        /// </summary>
        /// <param name="wordSeparator">word separator</param>
        /// <returns>enum value</returns>
        public static string GetValue(WordSeparator wordSeparator) {
            string value = null;
            switch (wordSeparator) {
                case WordSeparator.Dash:
                    value = "-";
                    break;
                case WordSeparator.Underscore:
                    value = "_";
                    break;
                default:
                    throw new ArgumentException("Unsupported word separator.");
            }

            return value;
        }

    }
}
