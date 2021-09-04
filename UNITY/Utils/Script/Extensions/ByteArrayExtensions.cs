using System.Text;

namespace Nostrum.Extensions
{
    /// <summary>
    /// Extension methods for the byte[] type.
    /// </summary>
    public static class ByteArrayExtensions
    {
        /// <summary>
        /// <para>
        /// Converts each byte to its hex string representation.
        /// </para>
        /// Example:
        /// <para>
        /// <code>
        ///     new byte[] { 0, 255 }.ToHexString() // returns "00ff"
        /// </code>
        /// </para>
        /// </summary>
        public static string ToHexString(this byte[] ba)
        {
            var hex = new StringBuilder(ba.Length * 2);
            foreach (var b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }

        /// <summary>
        /// <para>
        /// Converts each byte to an UTF8 character.
        /// </para>
        /// Example:
        /// <para>
        /// <code>
        ///     new byte[] { 65, 66 }.ToUTF8String() // returns "AB"
        /// </code>
        /// </para>
        /// </summary>
        public static string ToUTF8String(this byte[] ba)
        {
            return Encoding.UTF8.GetString(ba);
        }
    }
}