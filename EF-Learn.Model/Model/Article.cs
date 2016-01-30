using System.ComponentModel.DataAnnotations.Schema;
using EF_Learn.Common.Tools;
using EF_Learn.ModelFactory;

namespace EF_Learn.Model
{
    [Table("Article")]
    public class Article : BaseModel
    {
        public Article()
        {
            this.Description = this.Content.ReplaceHtmlTag();
        }

        /// <summary>
        /// 文章标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 文章内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 文章描述
        /// </summary>
        public string Description { get; set; }

        //todo:类别,标签,作者,评论...
    }
}