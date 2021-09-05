namespace Nostrum.Extensions
{
    /// <summary>
    /// Extension methods for the <see cref="byte"/> type.
    /// </summary>
    public static class ByteExtensions
    {
        /// <summary>
        /// Converts the byte to its hex representation.
        /// </summary>
        public static string ToHexString(this byte b)
        {
            return $"{b:x2}";
        }
    }
}