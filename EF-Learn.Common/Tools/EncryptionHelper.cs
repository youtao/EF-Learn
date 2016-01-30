using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace EF_Learn.Common.Tools
{
    public class EncryptionHelper
    {
        #region MD5Helper
        /// <summary>
        /// 输入字符串返回该字符串的Md5值
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetMd5(string str)
        {
            using (MD5 md5 = new MD5CryptoServiceProvider())
            {
                byte[] buffers = Encoding.Default.GetBytes(str);
                byte[] md5Buffers = md5.ComputeHash(buffers);
                StringBuilder sbMd5 = new StringBuilder();
                foreach (byte b in md5Buffers)
                {
                    sbMd5.Append(b.ToString("x2"));
                }
                return sbMd5.ToString();
            }
        }
        /// <summary>
        /// 计算文件的MD5值
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static string GetMd5(Stream stream)
        {
            string result = "";
            string hash = "";
            MD5CryptoServiceProvider provider = new MD5CryptoServiceProvider();
            var bytes = provider.ComputeHash(stream);
            //由以连字符分隔的十六进制对构成的String，其中每一对表示value 中对应的元素；例如“F-2C-4A”
            hash = BitConverter.ToString(bytes);
            //替换-
            hash = hash.Replace("-", "");
            result = hash;
            return result;
        }
        #endregion

        #region 非法字符Helper

        /// <summary>
        /// 判断是否是非法字符
        /// </summary>
        /// <param name="str">要判断的字符</param>
        /// <returns></returns>
        public static Boolean IsLegalNumber(string str)
        {
            char[] arry = str.ToLower().ToCharArray();
            for (int i = 0; i < arry.Length; i++)
            {
                int num = Convert.ToInt32(arry[i]);
                if (!(IsChineseLetter(num) || (num >= 48 && num <= 57) || (num >= 97 && num <= 123) || (num >= 65 && num <= 90) || num == 45))
                {
                    return false;
                }
            }
            foreach (var item in arry)
            {
                int num = Convert.ToInt32(item);
                if ((num >= 48 && num <= 57) || (num >= 97 && num <= 123) || (num >= 65 && num <= 90) || num == 45)
                {

                }
            }
            return true;
        }


        /// <summary>
        /// 判断字符的Unicode值是否是汉字
        /// </summary>
        /// <param name="code">字符的Unicode</param>
        /// <returns></returns>
        protected static bool IsChineseLetter(int code)
        {
            int chfrom = Convert.ToInt32("4e00", 16);    //范围（0x4e00～0x9fff）转换成int（chfrom～chend）
            int chend = Convert.ToInt32("9fff", 16);

            if (code >= chfrom && code <= chend)
            {
                return true;     //当code在中文范围内返回true

            }
            else
            {
                return false;    //当code不在中文范围内返回false
            }
        }

        #endregion
    }
}