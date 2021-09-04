using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Nostrum.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Converts an hex string to a byte array.
        /// </summary>
        public static byte[] ToByteArrayHex(this string hexStr)
        {
            var numberChars = hexStr.Length / 2;
            var bytes = new byte[numberChars];
            using var sr = new StringReader(hexStr);
            for (var i = 0; i < numberChars; i++)
                bytes[i] = Convert.ToByte(new string(new[] { (char)sr.Read(), (char)sr.Read() }), 16);
            return bytes;
        }

        /// <summary>
        /// Converts a string to a byte array.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static byte[] ToByteArray(this string str)
        {
            var ret = new byte[str.Length];
            for (var i = 0; i < str.Length; i++)
            {
                ret[i] = Convert.ToByte(str[i]);
            }
            return ret;
        }

        /// <summary>
        /// Replaces the most common HTML escape sequences.
        /// </summary>
        public static string UnescapeHtml(this string str)
        {
            str = str.Replace("&lt;", "<");
            str = str.Replace("&gt;", ">");
            str = str.Replace("&#xA", "\n");
            str = str.Replace("&quot;", "\"");
            str = str.Replace("&amp;", "&");
            return str;
        }
        
        /// <summary>
        /// Escape the most common HTML syntax symbols with their escape sequence.
        /// </summary>
        public static string EscapeHtml(this string str)
        {
            str = str.Replace("<", "&lt;");
            str = str.Replace(">", "&gt;");
            str = str.Replace("\n", "&#xA");
            str = str.Replace("\"", "&quot;");
            str = str.Replace("&", "&amp;");
            return str;
        }

        /// <summary>
        /// Replaces the first occurrence of <paramref name="search"/> with <paramref name="replacement"/> and returns the resulting string. The replacement is case-insensitive.
        /// </summary>
        public static string ReplaceFirstOccurrenceCaseInsensitive(this string input, string search, string replacement)
        {
            var pos = input.IndexOf(search, StringComparison.InvariantCultureIgnoreCase);
            if (pos < 0) return input;
            var result = input.Substring(0, pos) + replacement + input.Substring(pos + search.Length);
            return result;
        }

        /// <summary>
        /// Replaces all the occurrences of <paramref name="search"/> with <paramref name="replacement"/> and returns the resulting string. The replacement is case-insensitive.
        /// </summary>
        public static string ReplaceCaseInsensitive(this string input, string search, string replacement)
        {
            var result = Regex.Replace(
                input,
                Regex.Escape(search),
                replacement.Replace("$", "$$"), // ???
                RegexOptions.IgnoreCase
            );
            return result;
        }

        /// <summary>
        /// Wraps the string into &lt;font/&gt; tags if missing or mismatching and returns it.
        /// </summary>
        public static string AddFontTagsIfMissing(this string msg)
        {
            var sb = new StringBuilder();
            if (!msg.StartsWith("<font", StringComparison.InvariantCultureIgnoreCase))
            {
                if (msg.IndexOf("<font", StringComparison.OrdinalIgnoreCase) > 0)
                {
                    sb.Append("<font>");
                    sb.Append(msg.Substring(0, msg.IndexOf("<font", StringComparison.OrdinalIgnoreCase)));
                    sb.Append("</font>");
                    sb.Append(msg.Substring(msg.IndexOf("<font", StringComparison.OrdinalIgnoreCase)));
                }
                else
                {
                    sb.Append("<font>");
                    sb.Append(msg);
                    sb.Append("</font>");
                }
            }
            else sb.Append(msg);
            var openCount = Regex.Matches(msg, "<font").Count;
            var closeCount = Regex.Matches(msg, "</font>").Count;
            if (openCount > closeCount) sb.Append("</font>");
            return sb.ToString();
        }

        /// <summary>
        /// Makes first letter of the word uppercase, while forcing the rest to be lowercase and returns the result.
        /// </summary>
        public static string ToCapital(this string str)
        {
            var sb = new StringBuilder(str[0].ToString().ToUpper());
            sb.Append(str.Substring(1).ToLower());
            return sb.ToString();
        }
    }
}