using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace Filemanager_for_UPYUN
{
    /// <summary>
    /// 引用自:http://www.csharpwin.com/csharpspace/10907r150.shtml
    /// </summary>
    public class IconUtils
    {
        /// <summary>
        /// Used to access system folder icons.
        /// </summary>
        /// <param name="largeIcon">Specify large or small icons.</param>
        /// <param name="openFolder">Specify open or closed FolderType.</param>
        /// <returns>The requested Icon.</returns>
        public static Icon GetIconForFolder(Boolean largeIcon, Boolean openFolder)
        {
            SHFILEINFO shellFileInfo = new SHFILEINFO();
            try
            {
                uint flags = SHGFI_ICON | SHGFI_USEFILEATTRIBUTES;
                flags |= openFolder ? SHGFI_OPENICON : 0;
                flags |= largeIcon ? SHGFI_LARGEICON : SHGFI_SMALLICON;

                SHGetFileInfo(@"C:\Windows",//null,
                    FILE_ATTRIBUTE_DIRECTORY,
                    ref shellFileInfo,
                    (uint)Marshal.SizeOf(shellFileInfo),
                    flags);

                return (Icon)Icon.FromHandle(shellFileInfo.hIcon).Clone();
            }
            finally
            {
                DestroyIcon(shellFileInfo.hIcon); // Cleanup
            }
        }

        /// <summary>
        /// Used to access file icons for a given extension.
        /// </summary>
        /// <param name="extension">The file extension to get the icon for.</param>
        /// <param name="largeIcon">Specify large or small icons.</param>
        /// <param name="linkOverlay">Specify link overlay on the icon.</param>
        /// <returns>The requested Icon</returns>
        public static Icon GetIconForFileExtension(String extension, Boolean largeIcon, Boolean linkOverlay)
        {
            if (!extension.StartsWith("."))
                extension = "." + extension;

            SHFILEINFO shellFileInfo = new SHFILEINFO();
            try
            {
                uint flags = SHGFI_ICON | SHGFI_USEFILEATTRIBUTES;
                flags |= linkOverlay ? SHGFI_LINKOVERLAY : 0;
                flags |= largeIcon ? SHGFI_LARGEICON : SHGFI_SMALLICON;

                SHGetFileInfo(extension,
                    FILE_ATTRIBUTE_NORMAL,
                    ref shellFileInfo,
                    (uint)Marshal.SizeOf(shellFileInfo),
                    flags);

                return (Icon)Icon.FromHandle(shellFileInfo.hIcon).Clone();
            }
            finally
            {
                DestroyIcon(shellFileInfo.hIcon);
            }
        }

        #region P/Invoke
        private const int MAX_PATH = 256;

        private const uint SHGFI_ICON = 0x000000100;
        private const uint SHGFI_LINKOVERLAY = 0x000008000;
        private const uint SHGFI_LARGEICON = 0x000000000;
        private const uint SHGFI_SMALLICON = 0x000000001;
        private const uint SHGFI_OPENICON = 0x000000002;
        private const uint SHGFI_USEFILEATTRIBUTES = 0x000000010;

        private const uint FILE_ATTRIBUTE_DIRECTORY = 0x00000010;
        private const uint FILE_ATTRIBUTE_NORMAL = 0x00000080;

        [StructLayout(LayoutKind.Sequential)]
        private struct SHFILEINFO
        {
            public const int NAMESIZE = 80;
            public IntPtr hIcon;
            public int iIcon;
            public uint dwAttributes;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_PATH)]
            public string szDisplayName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = NAMESIZE)]
            public string szTypeName;
        };

        [DllImport("Shell32.dll")]
        private static extern IntPtr SHGetFileInfo(
            string pszPath,
            uint dwFileAttributes,
            ref SHFILEINFO psfi,
            uint cbFileInfo,
            uint uFlags
            );

        [DllImport("User32.dll")]
        private static extern int DestroyIcon(IntPtr hIcon);
        #endregion

    }
}
