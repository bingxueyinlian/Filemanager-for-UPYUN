using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Filemanager_for_UPYUN
{
    /// <summary>
    /// 线程工作类型
    /// </summary>
    public enum WorkType
    {
        /// <summary>
        /// 默认，根据URL查询列表结果
        /// </summary>
        Default,
        /// <summary>
        /// 上传
        /// </summary>
        Upload,
        /// <summary>
        /// 下载
        /// </summary>
        Download,
        /// <summary>
        /// 新建文件夹
        /// </summary>
        NewDir,
        /// <summary>
        /// 删除
        /// </summary>
        Delete,
        /// <summary>
        /// 查询
        /// </summary>
        Search
    }
}
