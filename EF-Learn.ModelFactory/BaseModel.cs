using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EF_Learn.ModelFactory
{
    /// <summary>
    /// 所有Model的基础类
    /// </summary>
    [Serializable]
    public class BaseModel
    {
        public BaseModel()
        {
            this.CreateDate = DateTime.Now;
            this.IsDeleted = false;
        }

        /// <summary>
        /// 主键Id
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// 创建时间(默认当前时间)
        /// </summary>
        [Display(Name = "创建时间")]
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// 是否软删除(默认没有)
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}