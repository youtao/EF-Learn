using System.ComponentModel.DataAnnotations.Schema;
using EF_Learn.ModelFactory;

namespace EF_Learn.Model
{
    [Table("UserInfo")]
    public class UserInfo:BaseModel
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
    }
}