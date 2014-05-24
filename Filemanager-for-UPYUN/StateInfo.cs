using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Filemanager_for_UPYUN
{
    /// <summary>
    /// 状态栏信息
    /// </summary>
    public class StateInfo
    {
        /// <summary>
        /// 文件名称
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 操作类型
        /// </summary>
        public WorkType WorkType { get; set; }

        /// <summary>
        /// 是否完成
        /// </summary>
        public bool IsComplete { get; set; }
    }
}
