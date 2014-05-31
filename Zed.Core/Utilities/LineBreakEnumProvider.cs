using System;

namespace Zed.Core.Utilities {
    /// <summary>
    /// Line breaks / New lines
    /// </summary>
    public enum LineBreak {
        /// <summary>
        /// Windows line break - CR+LF
        /// </summary>
        Windows,
        /// <summary>
        /// Unix line break - CR
        /// </summary>
        Unix,
        /// <summary>
        /// Html line break -  <![CDATA[<br />]]>
        /// </summary>
        Html
    }

    /// <summary>
    /// Class provides additional data from LineBreak enum.
    /// </summary>
    internal static class LineBreakEnumProvider {

        /// <summary>
        /// Gets LineBreak enum Value
        /// </summary>
        /// <param name="lineBreak">word separator</param>
        /// <returns>enum value</returns>
        public static string GetValue(LineBreak lineBreak) {
            string value = null;
            switch (lineBreak) {
                case LineBreak.Windows:
                    value = "\r\n";
                    break;
                case LineBreak.Unix:
                    value = "\n";
                    break;
                case LineBreak.Html:
                    value = "<br />";
                    break;
                default:
                    throw new ArgumentException("Unsupported line break.");
            }

            return value;
        }

    }
}
