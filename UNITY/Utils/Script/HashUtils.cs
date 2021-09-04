using Nostrum.Extensions;
using System.IO;
using System.Security.Cryptography;

namespace Nostrum
{
    public static class HashUtils
    {
        /// <summary>
        /// Generates the SHA256 of the given file.
        /// </summary>
        /// <param name="fileName">the path of the file</param>
        /// <returns>a string containing the SHA256 hash if executed correctly, empty string otherwise</returns>
        public static string GenerateFileHash(string fileName)
        {
            if (!File.Exists(fileName)) return string.Empty;
            byte[] fileBuffer;
            try
            {
                fileBuffer = File.ReadAllBytes(fileName);
            }
            catch
            {
                return string.Empty;
            }
            return SHA256.Create().ComputeHash(fileBuffer).ToHexString();

        }

        /// <summary>
        /// Generates the SHA256 hash of the given input string.
        /// </summary>
        /// <param name="input">the input string</param>
        /// <returns>a string containing the SHA256 hash</returns>
        public static string GenerateHash(string input)
        {
            return SHA256.Create().ComputeHash(input.ToByteArray()).ToHexString();
        }

    }
}
