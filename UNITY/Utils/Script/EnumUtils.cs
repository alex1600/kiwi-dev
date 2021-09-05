using System;
using System.Collections.Generic;
using System.Linq;

namespace Nostrum
{
    /// <summary>
    /// Utilities for Enums.
    /// </summary>
    public static class EnumUtils
    {
        /// <summary>
        /// Returns a list of enum values of the given type.
        /// </summary>
        /// <typeparam name="T">the enum type</typeparam>
        /// <returns>the List of enum values</returns>
        public static List<T> ListFromEnum<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>().ToList();
        }
    }
}
