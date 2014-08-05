using System;
using System.Text;
using NUnit.Framework;
using Zed.Core.Utilities;

namespace Zed.Core.Tests.Utilities {
    [TestFixture]
    public class TextHelperExtensionTests {

        #region LimitWords Test

        [Test]
        public void LimitWords_StringWithWords_StringWithLimitedNumberOfWords() {
            // Arrange
            const int numberOfWords = 4;

            // Act
            var result01 = "Here is a nice text string consisting of eleven words.".LimitWords(numberOfWords);
            var result02 = "He////re 5555 is $$&%$$ a nice text string consisting of eleven words.".LimitWords(numberOfWords);
            var result03 = "Here is a.".LimitWords(numberOfWords);
            var result04 = "Šđžćč čćlš đščć đšžćč ššđđ ćčđ.".LimitWords(numberOfWords);

            // Assert
            Assert.AreEqual("Here is a nice…", result01);
            Assert.AreEqual("He////re 5555 is $$&%$$…", result02);
            Assert.AreEqual("Here is a.", result03);
            Assert.AreEqual("Šđžćč čćlš đščć đšžćč…", result04);

        }

        [Test]
        public void LimitWords_StringWithWordsAndWhiteSpaces_LimitingWordsPreserveWhiteSpaceCharacters() {
            // Arrange
            const int numberOfWords = 4;

            // Act
            var result01 = "  Here is a nice text string consisting of eleven words.".LimitWords(numberOfWords);
            var result02 = "  Here\t\t\t is    a nice text string consisting of eleven words.".LimitWords(numberOfWords);

            // Assert
            Assert.AreEqual("  Here is a nice…", result01);
            Assert.AreEqual("  Here\t\t\t is    a nice…", result02);
        }

        #endregion

        #region LimitCharacters Tests

        [Test]
        public void Can_Limit_Characters() {
            // Arrange
            const int numberOfCharacters = 20;

            // Act
            var result01 = "Here is a nice text string consisting of eleven words.".LimitCharacters(numberOfCharacters);

            // Assert
            Assert.AreEqual("Here is a nice text…", result01);
            Assert.IsTrue(result01.Length <= numberOfCharacters, "Truncated text should have a number of characters less then or equal to a specified number of characters.");
        }

        [Test]
        public void Limit_Characters_Should_Remove_WhiteSpace_Characters() {
            // Arrange
            const int numberOfCharacters = 20;

            // Act
            var result01 = "   Here is a nice\r\n text string\n\t\t consisting of eleven words.   ".LimitCharacters(numberOfCharacters);

            // Assert
            Assert.AreEqual("Here is a nice text…", result01);
        }

        #endregion

        [Test]
        public void Ellipsize_LongString_EllipsizedString() {
            // Arrange
            const int maxLength = 32;

            // Act
            var result02 = "this_string_is_entirely_too_long_and_might_break_my_design.jpg".Ellipsize(maxLength, 0.2);
            var result05 = "this_string_is_entirely_too_long_and_might_break_my_design.jpg".Ellipsize(maxLength, 0.5);
            var result10 = "this_string_is_entirely_too_long_and_might_break_my_design.jpg".Ellipsize(maxLength, 1);
            var result25 = "this_string_is_entirely_too_long_and_might_break_my_design.jpg".Ellipsize(maxLength, 2.5);

            var resultOneChar = "t".Ellipsize(2, 0.5f);

            // Assert
            Assert.AreEqual("this_s…_might_break_my_design.jpg", result02);
            Assert.AreEqual(maxLength + TextHelper.CHAR_ELLIPSIS.Length, result02.Length);

            Assert.AreEqual("this_string_is_e…ak_my_design.jpg", result05);
            Assert.AreEqual(maxLength + TextHelper.CHAR_ELLIPSIS.Length, result05.Length);

            Assert.AreEqual("this_string_is_entirely_too_long…", result10);
            Assert.AreEqual(maxLength + TextHelper.CHAR_ELLIPSIS.Length, result10.Length);

            Assert.AreEqual("this_string_is_entirely_too_long…", result25);
            Assert.AreEqual(maxLength + TextHelper.CHAR_ELLIPSIS.Length, result25.Length);

            Assert.AreEqual("t", resultOneChar);
        }

        #region WrapWord Tests

        [Test]
        public void Can_WordWrap() {
            // Arange

            // Act
            var result20 = "The quick brown fox jumped over the lazy dog.".WordWrap(20);
            var result25 = "Here is a simple string of text that will help us demonstrate this function.".WordWrap(25);

            // Assert
            const string expected20 = "The quick brown fox"
                + "\njumped over the lazy"
                + "\ndog.";
            Assert.AreEqual(expected20, result20);

            const string expected25 = "Here is a simple string"
                + "\nof text that will help us"
                + "\ndemonstrate this"
                + "\nfunction.";
            Assert.AreEqual(expected25, result25);
        }

        [Test]
        public void Can_WordWrap_Including_Long_Words() {
            // Arange

            // Act
            var result08 = "A very long woooooooooooord.".WordWrap(8);

            // Assert
            const string expected08 = "A very"
                + "\nlong"
                + "\nwooooooo"
                + "\nooooord.";
            Assert.AreEqual(expected08, result08);

        }

        [Test]
        public void Can_Preserve_LineBreak_With_WordWrap() {
            // Arrange

            // Act
            var unixResult08MultipleLines = ("The quick"
                + "\nbrown fox"
                + "\n"
                + "\r\njumped over"
                + "\nthe lazy dog").WordWrap(8, LineBreak.Unix);

            var windowsResult08MultipleLines = ("The quick"
                + "\r\nbrown fox"
                + "\n"
                + "\r\njumped over"
                + "\r\nthe lazy dog").WordWrap(8, LineBreak.Windows);

            // Assert
            const string unixExpected08MultipleLines = "The"
                + "\nquick"
                + "\nbrown"
                + "\nfox"
                + "\n"
                + "\njumped"
                + "\nover"
                + "\nthe lazy"
                + "\ndog";
            Assert.AreEqual(unixExpected08MultipleLines, unixResult08MultipleLines);

            const string windowsExpected08MultipleLines = "The"
                + "\r\nquick"
                + "\r\nbrown"
                + "\r\nfox"
                + "\r\n"
                + "\r\njumped"
                + "\r\nover"
                + "\r\nthe lazy"
                + "\r\ndog";
            Assert.AreEqual(windowsExpected08MultipleLines, windowsResult08MultipleLines);
        }

        [Test]
        public void Can_WordWrap_Reduce_Multiple_Line_Spaces() {
            // Arrange

            // Act
            var result20 = "   The  \t\t quick brown fox     jumped \t over the lazy dog.  ".WordWrap(20);

            // Assert
            const string expected20 = "The quick brown fox"
                + "\njumped over the lazy"
                + "\ndog.";
            Assert.AreEqual(expected20, result20);
        }

        [Test]
        public void Can_WordWrap_With_Word_Break_If_Word_Length_Is_Greater_Than_Line_Length() {
            // Arrange

            // Act
            var result10 = "The quick brown fox jumped over the lazy dog.".WordWrap(3, LineBreak.Unix, true);

            // Assert
            const string expected10 = "The"
                + "\nqui"
                + "\nck"
                + "\nbro"
                + "\nwn"
                + "\nfox"
                + "\njum"
                + "\nped"
                + "\nove"
                + "\nr"
                + "\nthe"
                + "\nlaz"
                + "\ny"
                + "\ndog"
                + "\n.";
            Assert.AreEqual(expected10, result10);
        }

        [Test]
        public void Can_WordWrap_Without_Word_Break_If_Word_Length_Is_Greater_Than_Line_Length() {
            // Arrange

            // Act
            var result10 = "The quick brown fox jumped over the lazy dog.".WordWrap(3, LineBreak.Unix, false);

            // Assert
            const string expected10 = "The"
                + "\nquick"
                + "\nbrown"
                + "\nfox"
                + "\njumped"
                + "\nover"
                + "\nthe"
                + "\nlazy"
                + "\ndog.";
            Assert.AreEqual(expected10, result10);
        }

        #endregion

        [Test]
        public void ToAscii_AccentedForeignCharacters_TranslatedToAsciiCharacters() {
            // Arrange
            StringBuilder foreignCharacters = new StringBuilder();
            StringBuilder asciiCharacters = new StringBuilder();

            Func<string, int, string> func = (letter, count) => {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < count; i++) {
                    sb.Append(letter);
                }
                return sb.ToString();
            };

            const string A = "ÁÀÂÄǍĂĀÃÅǺĄ";
            foreignCharacters.Append(A);
            asciiCharacters.Append(func("A", A.Length));

            const string a = "áàâäǎăāãåǻąª";
            foreignCharacters.Append(a);
            asciiCharacters.Append(func("a", a.Length));

            const string AE = "ÆǼǢ";
            foreignCharacters.Append(AE);
            asciiCharacters.Append(func("AE", AE.Length));

            const string ae = "æǽǣ";
            foreignCharacters.Append(ae);
            asciiCharacters.Append(func("ae", ae.Length));

            const string B = "Ɓ";
            foreignCharacters.Append(B);
            asciiCharacters.Append(func("B", B.Length));

            const string b = "ɓ";
            foreignCharacters.Append(b);
            asciiCharacters.Append(func("b", b.Length));

            const string C = "ĆĊĈČÇ";
            foreignCharacters.Append(C);
            asciiCharacters.Append(func("C", C.Length));

            const string c = "ćċĉčç";
            foreignCharacters.Append(c);
            asciiCharacters.Append(func("c", c.Length));

            const string D = "ĎḌĐƊÐ";
            foreignCharacters.Append(D);
            asciiCharacters.Append(func("D", D.Length));

            const string d = "ďḍđɗð";
            foreignCharacters.Append(d);
            asciiCharacters.Append(func("d", d.Length));

            const string E = "ÉÈĖÊËĚĔĒĘẸƎƏƐ";
            foreignCharacters.Append(E);
            asciiCharacters.Append(func("E", E.Length));

            const string e = "éèėêëěĕēęẹǝəɛ";
            foreignCharacters.Append(e);
            asciiCharacters.Append(func("e", e.Length));

            const string f = "ƒ";
            foreignCharacters.Append(f);
            asciiCharacters.Append(func("f", f.Length));

            const string G = "ĠĜǦĞĢƔ";
            foreignCharacters.Append(G);
            asciiCharacters.Append(func("G", G.Length));

            const string g = "ġĝǧğģɣ";
            foreignCharacters.Append(g);
            asciiCharacters.Append(func("g", g.Length));

            const string H = "ĤḤĦ";
            foreignCharacters.Append(H);
            asciiCharacters.Append(func("H", H.Length));

            const string h = "ĥḥħ";
            foreignCharacters.Append(h);
            asciiCharacters.Append(func("h", h.Length));

            const string I = "IÍÌİÎÏǏĬĪĨĮỊ";
            foreignCharacters.Append(I);
            asciiCharacters.Append(func("I", I.Length));

            const string ii = "ıíìiîïǐĭīĩįị";
            foreignCharacters.Append(ii);
            asciiCharacters.Append(func("i", ii.Length));

            const string IJ = "Ĳ";
            foreignCharacters.Append(IJ);
            asciiCharacters.Append(func("IJ", IJ.Length));

            const string ij = "ĳ";
            foreignCharacters.Append(ij);
            asciiCharacters.Append(func("ij", ij.Length));

            const string J = "Ĵ";
            foreignCharacters.Append(J);
            asciiCharacters.Append(func("J", J.Length));

            const string j = "ĵ";
            foreignCharacters.Append(j);
            asciiCharacters.Append(func("j", j.Length));

            const string K = "ĶƘ";
            foreignCharacters.Append(K);
            asciiCharacters.Append(func("K", K.Length));

            const string k = "ķƙ";
            foreignCharacters.Append(k);
            asciiCharacters.Append(func("k", k.Length));

            const string L = "ĹĻŁĽĿ";
            foreignCharacters.Append(L);
            asciiCharacters.Append(func("L", L.Length));

            const string l = "ĺļłľŀ";
            foreignCharacters.Append(l);
            asciiCharacters.Append(func("l", l.Length));

            const string N = "ŃN̈ŇÑŅŊ";
            foreignCharacters.Append(N);
            asciiCharacters.Append(func("N", N.Length - 1));

            const string n = "ŉńn̈ňñņŋ";
            foreignCharacters.Append(n);
            asciiCharacters.Append(func("n", n.Length - 1));

            const string O = "ÓÒÔÖǑŎŌÕŐǪỌØǾƠ";
            foreignCharacters.Append(O);
            asciiCharacters.Append(func("O", O.Length));

            const string o = "óòôöǒŏōõőǫọøǿơº";
            foreignCharacters.Append(o);
            asciiCharacters.Append(func("o", o.Length));

            const string OE = "Œ";
            foreignCharacters.Append(OE);
            asciiCharacters.Append(func("OE", OE.Length));

            const string oe = "œ";
            foreignCharacters.Append(oe);
            asciiCharacters.Append(func("oe", oe.Length));

            const string R = "ŔŘŖ";
            foreignCharacters.Append(R);
            asciiCharacters.Append(func("R", R.Length));

            const string r = "ŕřŗſ";
            foreignCharacters.Append(r);
            asciiCharacters.Append(func("r", r.Length));

            const string S = "ŚŜŠŞȘṢ";
            foreignCharacters.Append(S);
            asciiCharacters.Append(func("S", S.Length));

            const string s = "śŝšşșṣ";
            foreignCharacters.Append(s);
            asciiCharacters.Append(func("s", s.Length));

            const string SS = "ẞ";
            foreignCharacters.Append(SS);
            asciiCharacters.Append(func("SS", SS.Length));

            const string ss = "ß";
            foreignCharacters.Append(ss);
            asciiCharacters.Append(func("ss", ss.Length));

            const string T = "ŤŢṬŦÞ";
            foreignCharacters.Append(T);
            asciiCharacters.Append(func("T", T.Length));

            const string t = "ťţṭŧþ";
            foreignCharacters.Append(t);
            asciiCharacters.Append(func("t", t.Length));

            const string U = "ÚÙÛÜǓŬŪŨŰŮŲỤƯ";
            foreignCharacters.Append(U);
            asciiCharacters.Append(func("U", U.Length));

            const string u = "úùûüǔŭūũűůųụư";
            foreignCharacters.Append(u);
            asciiCharacters.Append(func("u", u.Length));

            const string W = "ẂẀŴẄǷ";
            foreignCharacters.Append(W);
            asciiCharacters.Append(func("W", W.Length));

            const string w = "ẃẁŵẅƿ";
            foreignCharacters.Append(w);
            asciiCharacters.Append(func("w", w.Length));

            const string Y = "ÝỲŶŸȲỸƳ";
            foreignCharacters.Append(Y);
            asciiCharacters.Append(func("Y", Y.Length));

            const string y = "ýỳŷÿȳỹƴ";
            foreignCharacters.Append(y);
            asciiCharacters.Append(func("y", y.Length));

            const string Z = "ŹŻŽẒ";
            foreignCharacters.Append(Z);
            asciiCharacters.Append(func("Z", Z.Length));

            const string z = "źżžẓ";
            foreignCharacters.Append(z);
            asciiCharacters.Append(func("z", z.Length));

            // Act
            var result = foreignCharacters.ToString().ToAscii();

            // Assert
            Assert.AreEqual(asciiCharacters.ToString(), result);
        }

        #region ToSlug Tests

        [Test]
        public void ToSlug_StringWithSpacesAndSpecialCharacters_TranslatedToNormalizedString() {
            // Arrange
            string text = "What's wrong with **CSS**?";

            // Act
            string resultWithDashSeparator = text.ToSlug(WordSeparator.Dash);
            string resultWithUnderscoreSeparator = text.ToSlug(WordSeparator.Underscore);

            // Assert
            Assert.AreEqual("whats-wrong-with-css", resultWithDashSeparator);
            Assert.AreEqual("whats_wrong_with_css", resultWithUnderscoreSeparator);
        }

        [Test]
        public void ToSlug_StringWithDots_DotsReplacedWithValidSeparator() {
            // Arrange
            const string text = "string.with dots.";

            // Act
            var result = text.ToSlug();

            // Assert
            Assert.AreEqual("string-with-dots", result);
        }

        #endregion
    }
}
