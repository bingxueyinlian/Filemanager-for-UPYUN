using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Filemanager_for_UPYUN
{
    /// <summary>
    /// 加密解密字符串
    /// </summary>
    public class EncryptUtil
    {
        private static string key = "&FS@k_D^";
        private static string iv = "s*~!#)0%";

        /// <summary>
        /// 加密字符串
        /// </summary>
        /// <param name="sourceString">待加密的字符串</param>
        /// <returns>已加密的字符串</returns>
        public static string EncryptString(string sourceString)
        {
            return Encrypt(sourceString, key, iv);
        }

        /// <summary>
        /// 解密字符串
        /// </summary>
        /// <param name="encryptedString">待解密的字符串</param>
        /// <returns>已解密的字符串</returns>
        public static string DecryptString(string encryptedString)
        {
            return Decrypt(encryptedString, key, iv);
        }

        //加密
        private static string Encrypt(string sourceString, string key, string iv)
        {
            try
            {
                byte[] btKey = Encoding.UTF8.GetBytes(key);
                byte[] btIV = Encoding.UTF8.GetBytes(iv);
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                using (MemoryStream ms = new MemoryStream())
                {
                    byte[] inData = Encoding.UTF8.GetBytes(sourceString);
                    try
                    {
                        using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(btKey, btIV), CryptoStreamMode.Write))
                        {
                            cs.Write(inData, 0, inData.Length);
                            cs.FlushFinalBlock();
                        }

                        return Convert.ToBase64String(ms.ToArray());
                    }
                    catch
                    {
                        return sourceString;
                    }
                }
            }
            catch { }

            return String.Empty;
        }

        //解密
        private static string Decrypt(string encryptedString, string key, string iv)
        {
            byte[] btKey = Encoding.UTF8.GetBytes(key);
            byte[] btIV = Encoding.UTF8.GetBytes(iv);
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            using (MemoryStream ms = new MemoryStream())
            {
                byte[] inData = Convert.FromBase64String(encryptedString);
                try
                {
                    using (CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(btKey, btIV), CryptoStreamMode.Write))
                    {
                        cs.Write(inData, 0, inData.Length);
                        cs.FlushFinalBlock();
                    }

                    return Encoding.UTF8.GetString(ms.ToArray());
                }
                catch
                {
                    return encryptedString;
                }
            }
        }

    }
}
