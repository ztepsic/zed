using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Zed.Utilities {

    /// <summary>
    /// TextHelper is the helper/extension class that contains methods that assist in working with text.
    /// Based on: http://codeigniter.com/user_guide/helpers/text_helper.html
    /// </summary>
    public static class TextHelper {

        #region Constants and Enums

        /// <summary>
        /// HTML Entity Name => <![CDATA[&hellip;]]>
        /// Ellipsis is a series of marks that usually indicate an intentional omission of a word, sentence or whole
        /// section from the original text being quoted.
        /// HTML Entity Name: <![CDATA[&hellip;]]>
        /// Numeric character reference: <![CDATA[&#x2026;]]> or <![CDATA[&#8230;]]>
        /// </summary>
        public const string HTML_ELLIPSIS = "&hellip";

        /// <summary>
        /// Character Ellipsis => …
        /// </summary>
        public const string CHAR_ELLIPSIS = "…";

        /// <summary>
        /// Length of space character
        /// </summary>
        private const int SPACE_WIDTH = 1;

        #endregion

        #region Fields

        /// <summary>
        /// Mapping between foreignCharacters to ASCII charaters (English aplhabet)
        /// </summary>
        private static readonly IDictionary<string, string> foreignCharacters = new Dictionary<string, string>() {
				{"Á|À|Â|Ä|Ǎ|Ă|Ā|Ã|Å|Ǻ|Ą", "A"},
				{"á|à|â|ä|ǎ|ă|ā|ã|å|ǻ|ą|ª", "a"},
				{"Æ|Ǽ|Ǣ", "AE"},
				{"æ|ǽ|ǣ", "ae"},
				{"Ɓ", "B"},
				{"ɓ", "b"},
				{"Ć|Ċ|Ĉ|Č|Ç", "C"},
				{"ć|ċ|ĉ|č|ç", "c"},
				{"Ď|Ḍ|Đ|Ɗ|Ð", "D"},
				{"ď|ḍ|đ|ɗ|ð", "d"},
				{"É|È|Ė|Ê|Ë|Ě|Ĕ|Ē|Ę|Ẹ|Ǝ|Ə|Ɛ", "E"},
				{"é|è|ė|ê|ë|ě|ĕ|ē|ę|ẹ|ǝ|ə|ɛ", "e"},
				{"ƒ", "f"},
				{"Ġ|Ĝ|Ǧ|Ğ|Ģ|Ɣ", "G"},
				{"ġ|ĝ|ǧ|ğ|ģ|ɣ", "g"},
				{"Ĥ|Ḥ|Ħ", "H"},
				{"ĥ|ḥ|ħ", "h"},
				{"I|Í|Ì|İ|Î|Ï|Ǐ|Ĭ|Ī|Ĩ|Į|Ị", "I"},
				{"ı|í|ì|i|î|ï|ǐ|ĭ|ī|ĩ|į|ị", "i"},
				{"Ĳ", "IJ"},
				{"ĳ", "ij"},
				{"Ĵ", "J"},
				{"ĵ", "j"},
				{"Ķ|Ƙ", "K"},
				{"ķ|ƙ", "k"},
				{"Ĺ|Ļ|Ł|Ľ|Ŀ", "L"},
				{"ĺ|ļ|ł|ľ|ŀ", "l"},
				{"Ń|N̈|Ň|Ñ|Ņ|Ŋ", "N"},
				{"ŉ|ń|n̈|ň|ñ|ņ|ŋ", "n"},
				{"Ó|Ò|Ô|Ö|Ǒ|Ŏ|Ō|Õ|Ő|Ǫ|Ọ|Ø|Ǿ|Ơ", "O"},
				{"ó|ò|ô|ö|ǒ|ŏ|ō|õ|ő|ǫ|ọ|ø|ǿ|ơ|º", "o"},
				{"Œ", "OE"},
				{"œ", "oe"},
				{"Ŕ|Ř|Ŗ", "R"},
				{"ŕ|ř|ŗ|ſ", "r"},
				{"Ś|Ŝ|Š|Ş|Ș|Ṣ", "S"},
				{"ś|ŝ|š|ş|ș|ṣ", "s"},
				{"ẞ", "SS"},
				{"ß", "ss"},
				{"Ť|Ţ|Ṭ|Ŧ|Þ", "T"},
				{"ť|ţ|ṭ|ŧ|þ", "t"},
				{"Ú|Ù|Û|Ü|Ǔ|Ŭ|Ū|Ũ|Ű|Ů|Ų|Ụ|Ư", "U"},
				{"ú|ù|û|ü|ǔ|ŭ|ū|ũ|ű|ů|ų|ụ|ư", "u"},
				{"Ẃ|Ẁ|Ŵ|Ẅ|Ƿ", "W"},
				{"ẃ|ẁ|ŵ|ẅ|ƿ", "w"},
				{"Ý|Ỳ|Ŷ|Ÿ|Ȳ|Ỹ|Ƴ", "Y"},
				{"ý|ỳ|ŷ|ÿ|ȳ|ỹ|ƴ", "y"},
				{"Ź|Ż|Ž|Ẓ", "Z"},
				{"ź|ż|ž|ẓ", "z"}
			};

        #endregion

        #region Methods

        /// <summary>
        /// Truncates a string to the number of words specified and adds an sufix to the end of
        /// the truncated string. It preserves white space characters. Default sufix is CHAR_ELLIPSIS.
        /// </summary>
        /// <param name="text">str to truncate</param>
        /// <param name="numberOfWords">number of words</param>
        /// <param name="sufix">string which is added to the end of the truncated str. Default sufix is CHAR_ELLIPSIS.</param>
        /// <returns>truncated str with sufix on the end of the truncated str</returns>
        public static string LimitWords(this string text, int numberOfWords, string sufix = CHAR_ELLIPSIS) {
            if (string.IsNullOrEmpty(text)) {
                return text;
            }

            // '/^\s*+(?:\S++\s*+){1,'.(int) $limit.'}/'
            var pattern = @"^(?>\s*)(?:(?>\S+\s*)){1," + numberOfWords + "}";
            var match = Regex.Match(text, pattern);
            var newText = match.Value.TrimEnd();
            if (newText.Length == text.Length) {
                return text;
            } else {
                return newText + sufix;
            }

        }

        /// <summary>
        /// Truncates a string to the number of characters specified. It maintains the integrity of the word.
        /// Not counting characters used for the sufix. It removes white space characters.
        /// Default sufix is CHAR_ELLIPSIS.
        /// </summary>
        /// <param name="text">str to truncate</param>
        /// <param name="numberOfCharacters">number of characters</param>
        /// <param name="sufix">string which is added to the end of the truncated str. Default sufix is CHAR_ELLIPSIS.</param>
        /// <returns>truncated string with sufix on the end of the truncated str</returns>
        public static string LimitCharacters(this string text, int numberOfCharacters, string sufix = CHAR_ELLIPSIS) {
            if (text.Length <= numberOfCharacters) {
                return text;
            }

            const string pattern = @"\s+";
            text = Regex.Replace(text, pattern, " ");
            text = text.Trim();

            if (text.Length <= numberOfCharacters) {
                return text;
            }

            StringBuilder stringBuilder = new StringBuilder();
            var words = text.Split(' ');
            foreach (var word in words) {
                if ((stringBuilder.Length + word.Length + (stringBuilder.Length > 0 ? SPACE_WIDTH : 0)) <= numberOfCharacters) {
                    if (stringBuilder.Length > 0) {
                        stringBuilder.Append(" ");
                    }

                    stringBuilder.Append(word);
                } else {
                    stringBuilder.Append(sufix);
                    text = stringBuilder.ToString();
                    break;
                }
            }

            return text;
        }

        /// <summary>
        /// This method will split string it at a defined maximum length and insert an ellipsis.
        /// By default CHAR_ELLIPSIS will be inserted.
        /// </summary>
        /// <param name="str">string to ellipsize</param>
        /// <param name="maxLength">number of characters in the final string (max length of string)</param>
        /// <param name="position">
        ///		position to split - where in the string the ellipsis should appear from 0 (left) to 1 (right).
        ///		For example, a value of 1 will place the ellipsis at the right of the string, 0.5 in the middle and
        ///		0 at the left.
        /// </param>
        /// <param name="ellipsis">ellipsis sign - the kind of ellipsis. By default CHAR_ELLIPSIS will be inserted.</param>
        /// <returns>ellipsized string</returns>
        /// <example>
        ///		var str = "this_string_is_entirely_too_long_and_might_break_my_design.jpg";
        ///		var result = str.Ellipsize(32, 0.5);
        /// 
        ///		Produces: this_string_is_e…ak_my_design.jpg
        /// </example>
        /// <see href="http://en.wikipedia.org/wiki/Ellipsis"/>
        public static string Ellipsize(this string str, int maxLength, double position, string ellipsis = CHAR_ELLIPSIS) {

            // Is the string long enough to ellipsize?
            if (str.Length <= maxLength) {
                return str;
            }

            // scale to 0 - 1 range
            if (position < 0) {
                position = 0;
            } else if (position > 1) {
                position = 1;
            }

            string start = str.Substring(0, (int)Math.Floor(maxLength * position));
            string end = str.Substring(str.Length - (maxLength - start.Length));

            return start + ellipsis + end;
        }

        /// <summary>
        /// Wraps a text to a given number of characters using a line break character.
        /// Not counting characters used for line break character.
        /// </summary>
        /// <param name="text">input string</param>
        /// <param name="lineWidth">The line lineWidth. The number of characters to wrap at.</param>
        /// <param name="lineBreak">The line is broken using the optional break parameter. Default is Unix line break.
        /// </param>
        /// <param name="splitWord">If the splitWord is set to TRUE, the string is always wrapped at or before the specified lineWidth.
        ///	So if you have a word that is larger than the given lineWidth, it is broken apart.
        /// Default is TRUE.
        /// </param>
        /// <returns>Returns the given string wrapped at the specified column.</returns>
        /// <example>
        ///		var result = "Here is a simple string of text that will help us demonstrate this function.".WrapWord(25);
        /// 
        ///		Would produce:
        ///			Here is a simple string
        ///			of text that will help us
        ///			demonstrate this
        ///			function.
        /// </example>
        /// <see href="http://en.wikipedia.org/wiki/Word_wrap"/>
        public static string WordWrap(this string text, int lineWidth, LineBreak lineBreak = LineBreak.Unix, bool splitWord = true) {
            // Reduce multiple spaces
            text = text.Trim();
            text = Regex.Replace(text, @"( |\t)+", " ");

            string lineBreakValue = LineBreakEnumProvider.GetValue(lineBreak);

            // Standardize new line
            switch (lineBreak) {
                case LineBreak.Unix:
                    text = new Regex("\\r\\n|\\r").Replace(text, lineBreakValue);
                    break;
                case LineBreak.Windows:
                    text = new Regex("(?<!\\r)\\n").Replace(text, lineBreakValue);
                    break;
            }

            StringBuilder stringBuilder = new StringBuilder();

            string[] lines = text.Split(new[] { lineBreakValue }, StringSplitOptions.None);
            foreach (var line in lines) {
                if (stringBuilder.Length > 0) {
                    stringBuilder.Append(lineBreakValue);
                }

                StringBuilder lineSB = new StringBuilder();
                //  the remaining width of space on the line to fill
                int spaceLeft = lineWidth;
                string[] words = line.Split(' ');
                foreach (var word in words) {
                    if ((word.Length + (lineSB.Length > 0 ? SPACE_WIDTH : 0)) > spaceLeft) {
                        var tempWord = word;

                        if (splitWord) {
                            // if word is too long then split it
                            while (tempWord.Length > lineWidth) {
                                lineSB.Append(lineBreakValue);
                                lineSB.Append(tempWord.Substring(0, lineWidth));
                                tempWord = tempWord.Substring(lineWidth);
                            }
                        }

                        lineSB.Append(lineBreakValue);
                        lineSB.Append(tempWord);
                        spaceLeft = lineWidth - tempWord.Length;

                    } else {
                        // if we added one word add space before the new word
                        if (lineSB.Length > 0) {
                            lineSB.Append(" ");
                            spaceLeft -= (word.Length + SPACE_WIDTH);
                        } else {
                            spaceLeft -= word.Length;
                        }

                        lineSB.Append(word);

                    }
                }

                stringBuilder.Append(lineSB.ToString());
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Translates accented foreign characters to ASCII equivalents, useful when non-English characters need to be used
        /// where only standard ASCII characters are safely used, for instance, in URLs.
        /// </summary>
        /// <param name="text">text which contains accented foreign characters</param>
        /// <returns>translated text with only ASCII characters</returns>
        public static string ToAscii(this string text) {
            return foreignCharacters.Aggregate(text,
                (current, foreignCharacter) => Regex.Replace(current, foreignCharacter.Key, foreignCharacter.Value));
        }

        /// <summary>
        /// Method generates human-readable lowercase string without white spaces. All special characters are
        /// removed or replaced to be ASCII compliant. For instance, accented characters are replaced by letters
        /// from the English alphabet, punctation marks are removed and spaces are replaced by dashes or underscores.
        /// </summary>
        /// <param name="text">text from which is slug generated</param>
        /// <param name="wordSeparator">replacement for white spaces</param>
        /// <returns>human-readable lowercase string without white spaces</returns>
        /// <see href="http://en.wikipedia.org/wiki/Slug_%28web_publishing%29" />
        public static string ToSlug(this string text, WordSeparator wordSeparator = WordSeparator.Dash) {
            string slug = text.Trim()
                .ToLower()
                .ToAscii();

            string replace = WordSeparatorEnumProvider.GetValue(wordSeparator);

            IDictionary<string, string> transformations = new Dictionary<string, string>();
            transformations[@"&\#\d+?;"] = string.Empty;
            transformations[@"&\S+?;"] = string.Empty;
            transformations[@"\s+"] = replace;
            transformations[@"\.+"] = replace;
            //transformations[@"[^a-zA-Z0-9\-\._]"] = string.Empty; // supports dots
            transformations[@"[^a-zA-Z0-9\-_]"] = string.Empty; // supports dots
            transformations[replace + "+"] = replace;
            transformations[replace + "$"] = replace;
            transformations["^" + replace] = replace;
            transformations[@"[\.\-_]+$"] = string.Empty;

            slug = transformations.Aggregate(slug, (current, transformation) => Regex.Replace(current, transformation.Key, transformation.Value));

            return slug;
        }

        #endregion


    }
}
