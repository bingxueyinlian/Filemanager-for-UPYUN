using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Filemanager_for_UPYUN
{
    /// <summary>
    /// 右键菜单类型
    /// </summary>
    enum MenuType
    {
        /// <summary>
        /// 文件菜单
        /// </summary>
        File,
        /// <summary>
        /// 目录菜单
        /// </summary>
        Dir,
        /// <summary>
        /// 默认菜单
        /// </summary>
        Default
    }

    /// <summary>
    /// 文件类型
    /// </summary>
    struct FileType
    {
        /// <summary>
        /// 目录:F
        /// </summary>
        public const string Dir = "F";
        /// <summary>
        /// 文件:N
        /// </summary>
        public const string File = "N";

    }
}
