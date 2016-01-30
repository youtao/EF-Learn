using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EF_Learn.IDAL
{
    public interface IBaseDal<T> where T : new()
    {
        DbContext DbContext { get; set; }

        #region 同步

        #region 增

        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="model">要增加的模型</param>
        /// <returns></returns>
        void Add(T model);


        /// <summary>
        /// 批量增加
        /// </summary>
        /// <param name="range">模型集合</param>
        /// <returns></returns>
        void AddRange(IEnumerable<T> range);

        #endregion

        #region 硬删除

        /// <summary>
        /// 硬删除数据
        /// </summary>
        /// <param name="linq">查询表达式</param>
        /// <returns></returns>
        void HardDelete(Expression<Func<T, bool>> linq);

        /// <summary>
        /// 硬删除数据
        /// </summary>
        /// <param name="entity">要删除的实体</param>
        void HardDelete(T entity);

        #endregion

        #region 更新

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity">要更新的实体</param>
        void Update(T entity);

        /// <summary>
        /// 附加实体到数据库上下文
        /// </summary>
        /// <param name="entity">要附加的实体</param>
        void Attach(T entity);

        #endregion

        #region 查

        #region 全部数据

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <returns></returns>
        IQueryable<T> SelectAll();

        /// <summary>
        /// 满足条件的第一个元素
        /// </summary>
        /// <param name="linq">查询表达式</param>
        /// <returns></returns>
        T Single(Expression<Func<T, bool>> linq);

        /// <summary>
        /// 是否满足条件
        /// </summary>
        /// <param name="linq">查询表达式</param>
        /// <returns></returns>
        bool Any(Expression<Func<T, bool>> linq);

        /// <summary>
        /// 满足条件的元素数量
        /// </summary>
        /// <param name="linq">查询表达式</param>
        /// <returns></returns>
        int Count(Expression<Func<T, bool>> linq);


        #endregion

        #region 在没被软删除的数据中查询

        /// <summary>
        /// 查询全部(在没被软删除的数据中查询)
        /// </summary>
        /// <returns></returns>
        IQueryable<T> SelectNotDelete();


        /// <summary>
        /// 满足条件的第一个元素(在没被软删除的数据中查询)
        /// </summary>
        /// <param name="linq">查询表达式</param>
        /// <returns></returns>
        T SingleNotDelete(Expression<Func<T, bool>> linq);


        /// <summary>
        /// 是否满足条件(在没被软删除的数据中查询)
        /// </summary>
        /// <param name="linq">查询表达式</param>
        /// <returns></returns>
        bool AnyNotDelete(Expression<Func<T, bool>> linq);

        /// <summary>
        /// 满足条件的元素数量(在没被软删除的数据中查询)
        /// </summary>
        /// <param name="linq">查询表达式</param>
        /// <returns></returns>
        int CountNotDelete(Expression<Func<T, bool>> linq);


        #endregion

        #endregion

        #endregion

        #region 异步

        #region 查询

        #region 全部数据

        /// <summary>
        /// 满足条件的第一个元素
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        Task<T> SingleAsync(Expression<Func<T, bool>> whereLambda);

        /// <summary>
        /// 是否满足条件
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        Task<bool> AnyAsync(Expression<Func<T, bool>> whereLambda);

        /// <summary>
        /// 满足条件的元素数量
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        Task<int> CountAsync(Expression<Func<T, bool>> whereLambda);

        #endregion

        #region 没被软删除的数据

        /// <summary>
        /// 满足条件的第一个元素(在没被软删除的数据中查询)
        /// </summary>
        /// <param name="linq">查询表达式</param>
        /// <returns></returns>
        Task<T> SingleNotDeleteAsync(Expression<Func<T, bool>> linq);

        /// <summary>
        /// 是否满足条件(在没被软删除的数据中查询)
        /// </summary>
        /// <param name="linq">查询表达式</param>
        /// <returns></returns>
        Task<bool> AnyNotDeleteAsync(Expression<Func<T, bool>> linq);

        /// <summary>
        /// 满足条件的元素数量(在没被软删除的数据中查询)
        /// </summary>
        /// <param name="linq">查询表达式</param>
        /// <returns></returns>
        Task<int> CountNotDeleteAsync(Expression<Func<T, bool>> linq);


        #endregion

        #endregion

        #endregion
    }
}
