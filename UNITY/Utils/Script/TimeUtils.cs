using System;

namespace Nostrum
{
    /// <summary>
    /// Utilities for time calculations and string formatting of time-related data types.
    /// </summary>
    public static class TimeUtils
    {
        /// <summary>
        /// Returns a formatted string using the following criteria:
        /// <list type="bullet">
        ///    <item>
        ///     <term>
        ///         XX
        ///     </term>
        ///     <description>
        ///         if the amount is less than 99 seconds
        ///     </description>
        ///    </item>
        ///    <item>
        ///     <term>
        ///       XXm
        ///     </term>
        ///    </item>
        ///    <description>
        ///     if the amount is less than 99 minutes
        ///    </description>
        ///    <item>
        ///     <term>
        ///       XXh
        ///     </term>
        ///    </item>
        ///    <description>
        ///     if the amount is less than 99 hours
        ///    </description>
        ///    <item>
        ///     <term>
        ///       XXd
        ///     </term>
        ///    </item>
        ///    <description>
        ///     if the amount is greater than 99 hours
        ///    </description>
        /// </list>
        /// </summary>
        /// <param name="seconds"></param>
        /// <returns></returns>
        public static string FormatSeconds(ulong seconds)
        {
            var ts = TimeSpan.FromSeconds(seconds);

            if (seconds < 99) return $"{seconds}";

            var minutes = Math.Floor(ts.TotalMinutes);
            if (minutes < 99) return $"{minutes}m";

            var hours = Math.Floor(ts.TotalHours);
            if (hours < 99) return $"{hours}h";

            var days = Math.Floor(ts.TotalDays);
            return $"{days}d";
        }

        /// <summary>
        /// Returns <c>"sec.ms"</c> if the amount is less than 10 seconds, <see cref="FormatSeconds(ulong)"/> otherwise. Milliseconds are omitted by default.
        /// </summary>
        /// <param name="ms"></param>
        /// <param name="showDecimals"></param>
        /// <returns></returns>
        public static string FormatMilliseconds(ulong ms, bool showDecimals = false)
        {
            var ts = TimeSpan.FromMilliseconds(ms);

            var seconds = ts.TotalSeconds;
            if (seconds < 10) return showDecimals ? $"{seconds:N1}" : $"{seconds:N0}";
            return FormatSeconds(ms / 1000);
        }

        /// <summary>
        /// <inheritdoc cref="FormatSeconds(ulong)"/>
        /// </summary>
        public static string FormatSeconds(long seconds)
        {
            return FormatSeconds((ulong)Math.Abs(seconds));
        }

        /// <summary>
        /// <inheritdoc cref="FormatMilliseconds(ulong,bool)"/>
        /// </summary>
        public static string FormatMilliseconds(long ms, bool showDecimals = false)
        {
            return FormatMilliseconds((ulong)Math.Abs(ms), showDecimals);
        }

        /// <summary>
        /// Creates a new <see cref="DateTime"/> from Unix time.
        /// </summary>
        /// <param name="unixTime"></param>
        /// <returns></returns>
        public static DateTime FromUnixTime(long unixTime)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return epoch.AddSeconds(unixTime);
        }

        #region Obsolete

        [Obsolete("Use FormatSeconds() and FormatMilliseconds() instead.")]
        public static string FormatTime(long seconds)
        {
            if (Math.Abs(seconds) < 99) return $"{seconds}";
            if (Math.Abs(seconds) < 99 * 60) return $"{seconds / 60}m";
            if (Math.Abs(seconds) < 99 * 60 * 60) return $"{seconds / (60 * 60)}h";
            return $"{seconds / (60 * 60 * 24)}d";
        }

        [Obsolete("Use FormatSeconds() and FormatMilliseconds() instead.")]
        public static string FormatTime(ulong seconds)
        {
            if (seconds < 99) return $"{seconds}";
            if (seconds < 99 * 60) return $"{seconds / 60}m";
            if (seconds < 99 * 60 * 60) return $"{seconds / (60 * 60)}h";
            return seconds / (60 * 60 * 24) + "d";
        }

        #endregion Obsolete
    }
}