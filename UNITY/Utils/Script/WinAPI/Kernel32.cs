using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Nostrum.WinAPI
{
    public static class Kernel32
    {
        /// <summary>
        /// Retrieves the thread identifier of the calling thread.
        /// </summary>
        [DllImport("kernel32.dll")]
        public static extern int GetCurrentThreadId();

        /// <summary>
        /// Allocates a new console for the calling process.
        /// </summary>
        [DllImport("kernel32.dll")]
        public static extern bool AllocConsole();

        /// <summary>
        /// Detaches the calling process from its console.
        /// </summary>
        [DllImport("kernel32.dll")]
        public static extern void FreeConsole();

        /// <summary>
        /// Retrieves the window handle used by the console associated with the calling process.
        /// </summary>
        /// <returns>The return value is a handle to the window used by the console associated with the calling process or NULL if there is no such associated console.</returns>
        [DllImport("kernel32.dll")]
        public static extern IntPtr GetConsoleWindow();

        /// <summary>
        /// Retrieves the full name of the executable image for the specified process.
        /// </summary>
        /// <param name="hProcess">a handle to the process. This handle must be created with the PROCESS_QUERY_INFORMATION or PROCESS_QUERY_LIMITED_INFORMATION access right.</param>
        /// <param name="dwFlags">this parameter can be one of the following values: 0, the name should use the Win32 path format; 1, the name should use the native system path format</param>
        /// <param name="lpExeName">the path to the executable image. If the function succeeds, this string is null-terminated.</param>
        /// <param name="lpdwSize">on input, specifies the size of the lpExeName buffer, in characters. On success, receives the number of characters written to the buffer, not including the null-terminating character.</param>
        /// <returns></returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool QueryFullProcessImageName([In] IntPtr hProcess, [In] int dwFlags, [Out] StringBuilder lpExeName, ref int lpdwSize);

        /// <summary>
        /// Opens an existing local process object.
        /// </summary>
        /// <param name="processAccess">the access to the process object. This access right is checked against the security descriptor for the process. This parameter can be one or more of <see cref="ProcessAccessFlags"/>.</param>
        /// <param name="bInheritHandle">if this value is TRUE, processes created by this process will inherit the handle. Otherwise, the processes do not inherit this handle.</param>
        /// <param name="processId">the identifier of the local process to be opened.</param>
        /// <returns></returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr OpenProcess(ProcessAccessFlags processAccess, bool bInheritHandle, int processId);

        [Flags]
        public enum ProcessAccessFlags : uint
        {
            CreateProcess = 0x0080,
            CreateThread = 0x0002,
            DupHandle = 0x0040,
            QueryInformation = 0x0400,
            QueryLimitedInformation = 0x1000,
            SetInformation = 0x0200,
            SetQuota = 0x0100,
            SuspendResume = 0x0800,
            Terminate = 0x0001,
            Operation = 0x0008,
            Read = 0x0010,
            Write = 0x0020,
            Synchronize = 0x00100000,
        }
    }
}
