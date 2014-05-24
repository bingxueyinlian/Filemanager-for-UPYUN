using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Filemanager_for_UPYUN
{
    /// <summary>
    /// 线程运行时传递的参数
    /// </summary>
    public class ThreadArgument
    {
        /// <summary>
        /// 线程操作类型
        /// </summary>
        public WorkType WorkType { get; set; }
        /// <summary>
        /// 数据
        /// </summary>
        public object Data { get; set; }

    }
}
