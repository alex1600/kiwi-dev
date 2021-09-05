using Nostrum.WinAPI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Nostrum.Extensions
{
    /// <summary>
    /// Extensions methods for the <see cref="Process"/> class.
    /// </summary>
    public static class ProcessExtensions
    {
        /// <summary>
        /// Returns the windows owned by this process.
        /// </summary>
        public static IEnumerable<IntPtr> GetProcessWindows(this Process p)
        {
            var windows = new List<IntPtr>();
            User32.EnumWindows((hwnd, _) =>
            {
                User32.GetWindowThreadProcessId(hwnd, out var proc);
                if (p.Id != proc) return true;
                windows.Add(hwnd);
                return true;
            }, IntPtr.Zero);

            return windows;
        }

        /// <summary>
        /// Returns the full path to the given Process' image using Kernel32 process APIs. Does not require elevated privileges.
        /// </summary>
        public static string GetFilePath(this Process p)
        {
            var capacity = 2000;
            var builder = new StringBuilder(capacity);
            var ptr = Kernel32.OpenProcess(Kernel32.ProcessAccessFlags.QueryLimitedInformation, false, p.Id);
            return !Kernel32.QueryFullProcessImageName(ptr, 0, builder, ref capacity) ? string.Empty : builder.ToString();
        }

    }
}