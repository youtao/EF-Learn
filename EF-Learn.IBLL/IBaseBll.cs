using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EF_Learn.Common.Model.QueryParam;
using EF_Learn.ModelFactory;

namespace EF_Learn.IBLL
{
    public interface IBaseBll<T> where T : BaseModel, new()
    {
        DbContext DbContext { get; set; }

        #region 同步

        #region 增

        /// <summary>
        /// 增加
        /// </summary>        
        /// <param name="model"></param>
        /// <returns></returns>
        int Add(T model);

        /// <summary>
        /// 批量增加
        /// </summary>        
        /// <param name="range"></param>
        /// <returns></returns>
        int AddRange(IEnumerable<T> range);

        #endregion

        #region 删

        #region 硬删除

        /// <summary>
        /// 硬删除
        /// </summary>
        /// <param name="id"></param>        
        /// <returns></returns>
        int HardDelete(int id);

        /// <summary>
        /// 硬删除
        /// </summary>
        /// <param name="linq"></param>
        /// <returns></returns>
        int HardDelete(Expression<Func<T, bool>> linq);

        /// <summary>
        /// 硬删除
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int HardDelete(T entity);

        #endregion

        #region 软删除

        /// <summary>
        /// 软删除
        /// </summary>
        /// <returns></returns>
        int SoftDelete(int id);

        /// <summary>
        /// 软删除
        /// 返回-1:序列化失败
        /// 返回0:删除失败
        /// 返回>0:删除成功
        /// </summary>
        /// <param name="ids">json:主键id</param>
        /// <returns></returns>
        int SoftDelete(string ids);

        /// <summary>
        /// 软删除
        /// </summary>
        /// <param name="linq"></param>        
        /// <returns></returns>
        int SoftDelete(Expression<Func<T, bool>> linq);

        /// <summary>
        /// 软删除
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int SoftDelete(T entity);

        #endregion

        #endregion

        #region 更新

        /// <summary>
        /// 更新
        /// </summary>
        /// <returns></returns>
        int Update(T entity);

        /// <summary>
        /// 附加实体到数据库上下文
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int Attach(T entity);

        #endregion

        #region 查

        #region 全部数据

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <returns></returns>
        IQueryable<T> All();

        /// <summary>
        /// 查询满足条件的数据
        /// </summary>
        /// <returns></returns>
        IQueryable<T> Select(Expression<Func<T, bool>> linq);

        /// <summary>
        /// 根据主键id查询第一个元素
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T Single(int? id);

        /// <summary>
        /// 满足条件的第一个元素
        /// </summary>
        /// <param name="linq"></param>
        /// <returns></returns>
        T Single(Expression<Func<T, bool>> linq);


        /// <summary>
        /// 是否满足条件
        /// </summary>
        /// <param name="linq"></param>
        /// <returns></returns>
        bool Any(Expression<Func<T, bool>> linq);


        /// <summary>
        /// 满足条件的元素数量
        /// </summary>
        /// <param name="linq"></param>
        /// <returns></returns>
        int Count(Expression<Func<T, bool>> linq);

        #endregion

        #region 没被软删除的数据

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <returns></returns>
        IQueryable<T> AllNotDelete();

        /// <summary>
        /// 查询满足条件的数据
        /// </summary>
        /// <returns></returns>
        IQueryable<T> SelectNotDelete(Expression<Func<T, bool>> linq);

        /// <summary>
        /// 根据主键id查询第一个元素
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T SingleNotDelete(int? id);

        /// <summary>
        /// 满足条件的第一个元素
        /// </summary>
        /// <param name="linq"></param>
        /// <returns></returns>
        T SingleNotDelete(Expression<Func<T, bool>> linq);


        /// <summary>
        /// 是否满足条件
        /// </summary>
        /// <param name="linq"></param>
        /// <returns></returns>
        bool AnyNotDelete(Expression<Func<T, bool>> linq);


        /// <summary>
        /// 满足条件的元素数量
        /// </summary>
        /// <param name="linq"></param>
        /// <returns></returns>
        int CountNotDelete(Expression<Func<T, bool>> linq);

        #endregion

        #region 分页

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        IQueryable<T> SelectForPaging(BasePaging param);

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="linq"></param>        
        /// <param name="param"></param>
        /// <returns></returns>
        IQueryable<T> SelectForPaging(Expression<Func<T, bool>> linq, BasePaging param);

        #endregion

        #endregion

        #endregion

        #region 异步

        #region 增

        /// <summary>
        /// 异步增加
        /// </summary>        
        /// <param name="model"></param>
        /// <returns></returns>
        Task<int> AddAsync(T model);

        /// <summary>
        /// 异步批量增加
        /// </summary>        
        /// <param name="range"></param>
        /// <returns></returns>
        Task<int> AddRangeAsync(IEnumerable<T> range);

        #endregion

        #region 删

        #region 硬删除

        /// <summary>
        /// 异步硬删除
        /// </summary>
        /// <param name="id"></param>        
        /// <returns></returns>
        Task<int> HardDeleteAsync(int id);

        /// <summary>
        /// 异步硬删除
        /// </summary>
        /// <param name="linq"></param>
        /// <returns></returns>
        Task<int> HardDeleteAsync(Expression<Func<T, bool>> linq);



        /// <summary>
        /// 异步硬删除
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<int> HardDeleteAsync(T entity);

        #endregion

        #region 软删除

        /// <summary>
        /// 异步软删除
        /// </summary>
        /// <returns></returns>
        Task<int> SoftDeleteAsync(int id);

        /// <summary>
        /// 异步软删除
        /// 返回-1:序列化失败
        /// 返回0:删除失败
        /// 返回>0:删除成功
        /// </summary>
        /// <param name="ids">json:主键id</param>
        /// <returns></returns>
        Task<int> SoftDeleteAsync(string ids);

        /// <summary>
        /// 异步软删除
        /// </summary>
        /// <param name="linq"></param>        
        /// <returns></returns>
        Task<int> SoftDeleteAsync(Expression<Func<T, bool>> linq);

        /// <summary>
        /// 异步软删除
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<int> SoftDeleteAsync(T entity);

        #endregion

        #endregion

        #region 更新

        /// <summary>
        /// 异步更新
        /// </summary>
        /// <returns></returns>
        Task<int> UpdateAsync(T entity);

        /// <summary>
        /// 异步附加实体到数据库上下文
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<int> AttachAsync(T entity);

        #endregion

        #region 查

        #region 全部数据

        /// <summary>
        /// 异步根据主键id查询第一个元素
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> SingleAsync(int? id);

        /// <summary>
        /// 异步满足条件的第一个元素
        /// </summary>
        /// <param name="linq"></param>
        /// <returns></returns>
        Task<T> SingleAsync(Expression<Func<T, bool>> linq);

        /// <summary>
        /// 异步满足条件的数量
        /// </summary>
        /// <param name="linq"></param>
        /// <returns></returns>
        Task<int> CountAsync(Expression<Func<T, bool>> linq);

        /// <summary>
        /// 异步是否满足条件
        /// </summary>
        /// <param name="linq"></param>
        /// <returns></returns>
        Task<bool> AnyAsync(Expression<Func<T, bool>> linq);

        #endregion

        #region 没被删除的数据

        /// <summary>
        /// 异步根据主键id查询第一个元素(在没被软删除的数据中)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> SingleNotDeleteAsync(int? id);

        /// <summary>
        /// 异步满足条件的第一个元素(在没被软删除的数据中)
        /// </summary>
        /// <param name="linq"></param>
        /// <returns></returns>
        Task<T> SingleNotDeleteAsync(Expression<Func<T, bool>> linq);

        /// <summary>
        /// 异步满足条件数量(在没被软删除的数据中)
        /// </summary>
        /// <param name="linq"></param>
        /// <returns></returns>
        Task<int> CountNotDeleteAsync(Expression<Func<T, bool>> linq);

        /// <summary>
        /// 异步是否满足条件(在没被软删除的数据中)
        /// </summary>
        /// <param name="linq"></param>
        /// <returns></returns>
        Task<bool> AnyNotDeleteAsync(Expression<Func<T, bool>> linq);

        #endregion

        #endregion

        #endregion
    }
}