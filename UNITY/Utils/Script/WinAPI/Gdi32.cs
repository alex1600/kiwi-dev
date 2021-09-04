using System;
using System.Runtime.InteropServices;

namespace Nostrum.WinAPI
{
    public static class Gdi32
    {
        /// <summary>
        /// A raster-operation code. These codes define how the color data for the source rectangle is to be combined with the color data for the destination rectangle to achieve the final color in the <see cref="Gdi32.BitBlt"/> function.
        /// </summary>
        public enum RasterOpcodes
        {
            SRCCOPY = 0x00CC0020
        }

        /// <summary>
        /// The DeleteObject function deletes a logical pen, brush, font, bitmap, region, or palette, freeing all system resources associated with the object. After the object is deleted, the specified handle is no longer valid.
        /// </summary>
        /// <param name="o">A handle to a logical pen, brush, font, bitmap, region, or palette.</param>
        /// <returns>nonzero if it succeeds</returns>
        [DllImport("gdi32.dll")]
        public static extern int DeleteObject(IntPtr o);

        /// <summary>
        /// The BitBlt function performs a bit-block transfer of the color data corresponding to a rectangle of pixels from the specified source device context into a destination device context.
        /// </summary>
        /// <param name="hObject">A handle to the destination device context.</param>
        /// <param name="nXDest">The x-coordinate, in logical units, of the upper-left corner of the destination rectangle.</param>
        /// <param name="nYDest">The y-coordinate, in logical units, of the upper-left corner of the destination rectangle.</param>
        /// <param name="nWidth">The width, in logical units, of the source and destination rectangles.</param>
        /// <param name="nHeight">The height, in logical units, of the source and the destination rectangles.</param>
        /// <param name="hObjectSource">A handle to the source device context.</param>
        /// <param name="nXSrc">The x-coordinate, in logical units, of the upper-left corner of the source rectangle.</param>
        /// <param name="nYSrc">The y-coordinate, in logical units, of the upper-left corner of the source rectangle.</param>
        /// <param name="dwRop">A raster-operation code. These codes define how the color data for the source rectangle is to be combined with the color data for the destination rectangle to achieve the final color.</param>
        /// <returns></returns>
        [DllImport("gdi32.dll")]
        public static extern bool BitBlt(IntPtr hObject, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hObjectSource, int nXSrc, int nYSrc, int dwRop);

        /// <summary>
        /// The CreateCompatibleBitmap function creates a bitmap compatible with the device that is associated with the specified device context.
        /// </summary>
        /// <param name="hDC">A handle to a device context.</param>
        /// <param name="nWidth">The bitmap width, in pixels.</param>
        /// <param name="nHeight">The bitmap height, in pixels.</param>
        /// <returns>If the function succeeds, the return value is a handle to the compatible bitmap (DDB). If the function fails, the return value is NULL.</returns>
        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateCompatibleBitmap(IntPtr hDC, int nWidth, int nHeight);

        /// <summary>
        /// The CreateCompatibleDC function creates a memory device context (DC) compatible with the specified device.
        /// </summary>
        /// <param name="hDC">A handle to an existing DC. If this handle is NULL, the function creates a memory DC compatible with the application's current screen.</param>
        /// <returns>If the function succeeds, the return value is the handle to a memory DC. If the function fails, the return value is NULL.</returns>
        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateCompatibleDC(IntPtr hDC);

        /// <summary>
        /// The DeleteDC function deletes the specified device context (DC).
        /// </summary>
        /// <param name="hDC">A handle to the device context.</param>
        /// <returns>If the function succeeds, the return value is nonzero. If the function fails, the return value is zero.</returns>
        [DllImport("gdi32.dll")]
        public static extern bool DeleteDC(IntPtr hDC);

        /// <summary>
        /// The SelectObject function selects an object into the specified device context (DC). The new object replaces the previous object of the same type.
        /// </summary>
        /// <param name="hDC">A handle to the DC.</param>
        /// <param name="hObject">A handle to the object to be selected.</param>
        /// <returns>see https://docs.microsoft.com/en-us/windows/win32/api/wingdi/nf-wingdi-selectobject#return-value</returns>
        [DllImport("gdi32.dll")]
        public static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);

    }
}