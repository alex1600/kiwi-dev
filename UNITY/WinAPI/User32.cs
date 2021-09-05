using System;
using System.Runtime.InteropServices;
using System.Text;

    public static class User32
    {

	[DllImport("user32.dll")]
	public static extern IntPtr GetForegroundWindow();

	[DllImport("user32.dll")]
	public static extern bool SetForegroundWindow(IntPtr hWnd);

	[DllImport("user32.dll")]
	public static extern uint GetWindowLong(IntPtr hwnd, int index);

	[DllImport("user32.dll")]
	public static extern uint SetWindowLong(IntPtr hwnd, int index, uint newStyle);

	[DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
	public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

	[DllImport("user32.dll")]
	public static extern IntPtr GetDesktopWindow();

	[DllImport("user32.dll")]
	public static extern IntPtr GetWindowDC(IntPtr hWnd);

	[DllImport("user32.dll")]
	[return: MarshalAs(UnmanagedType.Bool)]
	public static extern bool ShowWindow(IntPtr hWnd, CmdShow nCmdShow);

	[DllImport("user32.dll")]
	public static extern IntPtr ReleaseDC(IntPtr hWnd, IntPtr hDC);

	[DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
	public static extern bool PostMessage(IntPtr hWnd, uint msg, int wParam, int lParam);

	[DllImport("user32.dll", SetLastError = true)]
	[return: MarshalAs(UnmanagedType.Bool)]
	public static extern bool GetWindowRect(IntPtr hWnd, ref RECT lpRect);

	[DllImport("user32.dll")]
	public static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

	[DllImport("user32.dll")]
	public static extern bool UnregisterHotKey(IntPtr hWnd, int id);

	[DllImport("user32.dll")]
	public static extern uint GetGuiResources(IntPtr hProcess, uint uiFlags);

	[DllImport("user32.dll")]
	[return: MarshalAs(UnmanagedType.Bool)]
	public static extern bool GetCursorPos(out POINT pPoint);

	[DllImport("user32.dll")]
	public static extern IntPtr SetParent(IntPtr hWnd, IntPtr hWndParent);

	[DllImport("user32.dll")]
	public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, int uFlags);

	[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
	public static extern bool EnumWindows(EnumThreadDelegate callback, IntPtr extraData);

	[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
	public static extern int GetWindowThreadProcessId(IntPtr handle, out int processId);

	[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
	public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

	#region Enums
	public enum WindowStyles : uint
	{
		WS_CAPTION = 0x00C00000,
		WS_THICKFRAME = 0x00040000,
		WS_CHILD = 0x40000000,
		WS_DLGFRAME = 0x00400000,
		WS_POPUP = 0x80000000,
		WS_SYSMENU = 0x00080000,
		WS_MAXIMIZEBOX = 0x00010000,
		WS_MINIMIZEBOX = 0x00020000
	}
	public enum ExtendedWindowStyles : ulong
	{
		WS_EX_STATICEDGE = 0x00020000,
		WS_EX_CLIENTEDGE = 0x00000200,
		WS_EX_DLGMODALFRAME = 0x00000001,
		WS_EX_TRANSPARENT = 0x20,     //clickthru
		WS_EX_NOACTIVATE = 0x08000000, //don't focus
		WS_EX_TOOLWINDOW = 0x00000080, //don't show in alt-tab
	}

	public enum GWL
	{
		GWL_STYLE = -16,
		GWL_EXSTYLE = -20, //set new exStyle
	}

	public enum WindowsMessages : uint
	{
		WM_CHAR = 0x0102,
		WM_KEYDOWN = 0x0100,
		WM_KEYUP = 0x0101
	}

	public enum CmdShow
	{
		SW_HIDE = 0,
		SW_SHOWNORMAL = 1,
		SW_SHOWMINIMIZED = 2,
		SW_MAXIMIZE = 3,
		SW_SHOWNOACTIVATE = 4,
		SW_SHOW = 5,
		SW_MINIMIZE = 6,
		SW_SHOWMINNOACTIVE = 7,
		SW_SHOWNA = 8,
		SW_RESTORE = 9,
		SW_SHOWDEFAULT = 10,
		SW_FORCEMINIMIZE = 11
	}

	public enum VirtualKeys
	{
		VK_RETURN = 0x0D
	}

	[Flags]
	public enum WindowSizePositionFlags : uint
	{
		SWP_NOZORDER = 0x004,
		SWP_NOACTIVATE = 0x0010,
		SWP_DRAWFRAME = 0x0020
	}

	#endregion

	#region Types
	[StructLayout(LayoutKind.Sequential)]
	public struct RECT
	{
		public readonly int Left;
		public readonly int Top;
		public readonly int Right;
		public readonly int Bottom;
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct POINT
	{
		public int X;
		public int Y;

		public POINT(int x, int y)
		{
			this.X = x;
			this.Y = y;
		}
	}

	public delegate bool EnumThreadDelegate(IntPtr hWnd, IntPtr lParam);

	#endregion
}
