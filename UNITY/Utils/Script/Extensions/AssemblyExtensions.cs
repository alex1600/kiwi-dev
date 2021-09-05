using System.IO;
using System.Linq;
using System.Reflection;

namespace Nostrum.Extensions
{
    /// <summary>
    /// Extension methods for the <see cref="Assembly"/> type.
    /// </summary>
    public static class AssemblyExtensions
    {
        /// <summary>
        /// Returns the resource <see cref="Stream"/> using the specified <paramref name="name"/>.
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Stream? GetResourceStream(this Assembly assembly, string name)
        {
            return assembly.GetManifestResourceStream(assembly.GetManifestResourceNames().Single(x => x.EndsWith(name)));
        }
    }
}