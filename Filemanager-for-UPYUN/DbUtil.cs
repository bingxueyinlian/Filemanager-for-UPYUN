using System;
using System.Data.SQLite;
using System.IO;

namespace Filemanager_for_UPYUN
{
    public class DbUtil
    {
        /// <summary>
        /// 数据库文件名
        /// </summary>
        public static string DbFileName
        {
            get
            {
                return "fm.db";
            }
        }
        private static string connString = string.Concat("data source=", Path.GetFullPath(DbUtil.DbFileName));
        private static string password = "z@d3*2h4f%j3s";//密码
        /// <summary>
        /// 创建数据库
        /// </summary>
        public static void CreateDB()
        {
            SQLiteConnection.CreateFile(DbUtil.DbFileName);
            //加密
            using (SQLiteConnection cnn = new SQLiteConnection(connString))
            {
                cnn.Open();
                cnn.ChangePassword(password);
                CreateTable();
            }
        }
        /// <summary>
        /// 创建表
        /// </summary>
        private static void CreateTable()
        {
            string sql = @"CREATE TABLE [FileMng] (
                              [ID] INTEGER PRIMARY KEY AUTOINCREMENT, 
                              [ServerName] VARCHAR2(1000), 
                              [LocalName] VARCHAR2(1000), 
                              [ServerPath] VARCHAR2(1000), 
                              [LocalPath] VARCHAR2(1000));
                            CREATE INDEX [FileName_Index] ON [FileMng] ([LocalName]);";

            using (SQLiteConnection cnn = new SQLiteConnection(connString))
            {
                cnn.SetPassword(password);
                cnn.Open();
                SQLiteCommand cmm = new SQLiteCommand(sql, cnn);
                cmm.ExecuteNonQuery();
            }
        }
        /// <summary>
        /// 添加文件
        /// </summary>
        /// <param name="fileDto"></param>
        /// <returns></returns>
        public static bool Add(FileDto fileDto)
        {
            string sql = @"INSERT INTO FILEMNG(SERVERNAME,SERVERPATH,LOCALNAME,LOCALPATH)
                            VALUES(:SERVERNAME,:SERVERPATH,:LOCALNAME,:LOCALPATH)";
            SQLiteParameter sp1 = new SQLiteParameter("SERVERNAME", fileDto.ServerName);
            SQLiteParameter sp2 = new SQLiteParameter("SERVERPATH", fileDto.ServerPath);
            SQLiteParameter sp3 = new SQLiteParameter("LOCALNAME", fileDto.LocalName);
            SQLiteParameter sp4 = new SQLiteParameter("LOCALPATH", fileDto.LocalPath);

            using (SQLiteConnection cnn = new SQLiteConnection(connString))
            {
                cnn.SetPassword(password);
                cnn.Open();
                SQLiteCommand cmm = new SQLiteCommand(sql, cnn);
                cmm.Parameters.AddRange(new SQLiteParameter[] { sp1, sp2, sp3, sp4 });
                return cmm.ExecuteNonQuery() > 0;
            }
        }
        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="fileDto"></param>
        /// <returns></returns>
        public static bool Delete(FileDto fileDto)
        {
            string sql = "DELETE FROM FILEMNG WHERE SERVERPATH=:SERVERPATH AND SERVERNAME=:SERVERNAME";
            SQLiteParameter sp1 = new SQLiteParameter("SERVERNAME", fileDto.ServerName);
            SQLiteParameter sp2 = new SQLiteParameter("SERVERPATH", fileDto.ServerPath);
            using (SQLiteConnection cnn = new SQLiteConnection(connString))
            {
                cnn.SetPassword(password);
                cnn.Open();
                SQLiteCommand cmm = new SQLiteCommand(sql, cnn);
                cmm.Parameters.AddRange(new SQLiteParameter[] { sp1, sp2 });
                return cmm.ExecuteNonQuery() > 0;
            }
        }
        public static bool Update(FileDto fileDto)
        {
            string sql = @"UPDATE FILEMNG
                            SET LOCALNAME=:LOCALNAME,LOCALPATH=:LOCALPATH
                            WHERE SERVERNAME=:SERVERNAME AND SERVERPATH=:SERVERPATH";
            SQLiteParameter sp1 = new SQLiteParameter("LOCALNAME", fileDto.LocalName);
            SQLiteParameter sp2 = new SQLiteParameter("LOCALPATH", fileDto.LocalPath);
            SQLiteParameter sp3 = new SQLiteParameter("SERVERNAME", fileDto.ServerName);
            SQLiteParameter sp4 = new SQLiteParameter("SERVERPATH", fileDto.ServerPath);


            using (SQLiteConnection cnn = new SQLiteConnection(connString))
            {
                cnn.SetPassword(password);
                cnn.Open();
                SQLiteCommand cmm = new SQLiteCommand(sql, cnn);
                cmm.Parameters.AddRange(new SQLiteParameter[] { sp1, sp2, sp3, sp4 });
                return cmm.ExecuteNonQuery() > 0;
            }
        }

        public static void Save(FileDto fileDto)
        {
            string sql = "SELECT COUNT(1) FROM FILEMNG WHERE SERVERPATH=:SERVERPATH AND SERVERNAME=:SERVERNAME";
            SQLiteParameter sp1 = new SQLiteParameter("SERVERNAME", fileDto.ServerName);
            SQLiteParameter sp2 = new SQLiteParameter("SERVERPATH", fileDto.ServerPath);
            using (SQLiteConnection cnn = new SQLiteConnection(connString))
            {
                cnn.SetPassword(password);
                cnn.Open();
                SQLiteCommand cmm = new SQLiteCommand(sql, cnn);
                cmm.Parameters.AddRange(new SQLiteParameter[] { sp1, sp2 });
                int count = Convert.ToInt32(cmm.ExecuteScalar().ToString());
                if (count == 0)
                {
                    Add(fileDto);
                }
                else
                {
                    Update(fileDto);
                }
            }
        }
    }
}
