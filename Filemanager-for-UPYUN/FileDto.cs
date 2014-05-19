
namespace Filemanager_for_UPYUN
{
    public class FileDto
    {
        /// <summary>
        /// 数据库自增ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 服务器实际文件名
        /// </summary>
        public string ServerName { get; set; }
        /// <summary>
        /// 服务器实际路径
        /// </summary>
        public string ServerPath { get; set; }
        /// <summary>
        /// 本地文件名
        /// </summary>
        public string LocalName { get; set; }
        /// <summary>
        /// 本地显示路径
        /// </summary>
        public string LocalPath { get; set; }
    }
}
