using System;

namespace Nostrum.Extensions
{
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Returns the epoch time representation of the <see cref="DateTime"/>.
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static long ToEpoch(this DateTime dt)
        {
            var t = dt - new DateTime(1970, 1, 1);
            return (long)t.TotalSeconds;
        }
    }
}