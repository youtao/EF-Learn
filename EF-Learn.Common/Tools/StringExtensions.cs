using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace EF_Learn.Common.Tools
{
    public static class StringExtensions
    {
        /// <summary>
        /// 替换字符串中的HTML标签
        /// </summary>
        /// <param name="html">html</param>
        /// <param name="length">要获取的长度</param>
        /// <returns></returns>
        public static string ReplaceHtmlTag(this string html, int length = 200)
        {
            string temp = Regex.Replace(html, "[<].*?[>]", "");
            temp = Regex.Replace(temp, "&[^;]+;", "");
            if (length > 0 && temp.Length > length)
                return temp.Substring(0, length);
            return temp;
        }

        /// <summary>
        /// 获取字符串的MD5
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetMd5(this string str)
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
    }
}