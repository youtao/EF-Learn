namespace EF_Learn.Common.Model.QueryParam
{
    /// <summary>
    /// 分页查询基本参数
    /// </summary>
    public class BasePaging
    {
        public BasePaging()
        {
            this.PageIndex = 1;
            this.IsDeleted = false;
        }

        /// <summary>
        /// 页索引
        /// </summary>
        public int? PageIndex { get; set; }

        /// <summary>
        /// 页大小
        /// </summary>
        public int? PageSize { get; set; }

        /// <summary>
        /// 总记录条数
        /// </summary>
        public int RecordTotal { get; set; }

        /// <summary>
        /// 是否软删除
        /// </summary>
        public bool IsDeleted { get; set; }

    }
}