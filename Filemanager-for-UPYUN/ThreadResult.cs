using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Filemanager_for_UPYUN
{
    public class ThreadResult
    {
        /// <summary>
        /// 结果数据
        /// </summary>
        public List<FolderItem> DataList { get; set; }

        /// <summary>
        /// 线程参数
        /// </summary>
        public ThreadArgument ThreadArg { get; set; }

        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsSuccess { get; set; }
    }
}
