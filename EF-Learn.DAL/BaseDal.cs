using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using EF_Learn.ModelFactory;

namespace EF_Learn.DAL
{
    public class BaseDal<T> where T : BaseModel, new()
    {
        public DbContext DbContext { get; set; }
        public BaseDal()
        {
            var assembly = Assembly.GetAssembly(typeof(T));//获取当前类的程序集
            var assemblyName = assembly.GetName().Name.Replace("-","_");
            IDbContextFactory factory = assembly.CreateInstance(assemblyName + ".DbContextFactory", true) as IDbContextFactory;
            if (factory != null) this.DbContext = factory.GetContext(assemblyName);
        }

        #region 同步

        #region 增

        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="model">要增加的模型</param>
        /// <returns></returns>
        public void Add(T model)
        {
            this.DbContext.Set<T>().Add(model);            
        }

        /// <summary>
        /// 批量增加
        /// </summary>
        /// <param name="range">模型集合</param>
        /// <returns></returns>
        public void AddRange(IEnumerable<T> range)
        {
            this.DbContext.Set<T>().AddRange(range);
        }

        #endregion

        #region 硬删除

        /// <summary>
        /// 硬删除数据
        /// </summary>
        /// <param name="linq">查询表达式</param>
        /// <returns></returns>
        public void HardDelete(Expression<Func<T, bool>> linq)
        {
            var temp = this.DbContext.Set<T>().Where(linq);
            this.DbContext.Set<T>().RemoveRange(temp);
        }


        /// <summary>
        /// 硬删除数据
        /// </summary>
        /// <param name="entity">要删除的实体</param>
        public void HardDelete(T entity)
        {
            this.DbContext.Entry(entity).State = EntityState.Deleted;
        }

        #endregion

        #region 更新

        /// <summary>
        /// 更新
        /// </summary>
        /// <returns>要更新的实体</returns>
        public void Update(T entity)
        {
            this.DbContext.Entry(entity).State = EntityState.Modified;
        }


        /// <summary>
        /// 附加实体到数据库上下文
        /// </summary>
        /// <param name="entity">要附加的实体</param>
        public void Attach(T entity)
        {
            this.DbContext.Set<T>().Attach(entity);
        }

        #endregion

        #region 查

        #region 全部数据

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <returns></returns>
        public IQueryable<T> SelectAll()
        {
            return this.DbContext.Set<T>();
        }

        /// <summary>
        /// 满足条件的第一个元素
        /// </summary>
        /// <param name="linq">查询表达式</param>
        /// <returns></returns>
        public T Single(Expression<Func<T, bool>> linq)
        {
            return this.DbContext.Set<T>().FirstOrDefault(linq);
        }

        /// <summary>
        /// 是否满足条件
        /// </summary>
        /// <param name="linq">查询表达式</param>
        /// <returns></returns>
        public bool Any(Expression<Func<T, bool>> linq)
        {
            return this.DbContext.Set<T>().Any(linq);
        }

        /// <summary>
        /// 满足条件的元素数量
        /// </summary>
        /// <param name="linq">查询表达式</param>
        /// <returns></returns>
        public int Count(Expression<Func<T, bool>> linq)
        {
            return this.DbContext.Set<T>().Count(linq);
        }


        #endregion

        #region 在没被软删除的数据中查询

        /// <summary>
        /// 查询全部(在没被软删除的数据中查询)
        /// </summary>
        /// <returns></returns>
        public IQueryable<T> SelectNotDelete()
        {
            return this.DbContext.Set<T>().Where(t => t.IsDeleted == false);
        }


        /// <summary>
        /// 满足条件的第一个元素(在没被软删除的数据中查询)
        /// </summary>
        /// <param name="linq">查询表达式</param>
        /// <returns></returns>
        public T SingleNotDelete(Expression<Func<T, bool>> linq)
        {
            return this.DbContext.Set<T>().Where(t => t.IsDeleted == false).FirstOrDefault(linq);
        }


        /// <summary>
        /// 是否满足条件(在没被软删除的数据中查询)
        /// </summary>
        /// <param name="linq">查询表达式</param>
        /// <returns></returns>
        public bool AnyNotDelete(Expression<Func<T, bool>> linq)
        {
            return this.DbContext.Set<T>().Where(t => t.IsDeleted == false).Any(linq);
        }


        /// <summary>
        /// 满足条件的元素数量(在没被软删除的数据中查询)
        /// </summary>
        /// <param name="linq">查询表达式</param>
        /// <returns></returns>
        public int CountNotDelete(Expression<Func<T, bool>> linq)
        {
            return this.DbContext.Set<T>().Where(t => t.IsDeleted == false).Count(linq);
        }



        #endregion

        #endregion

        #endregion

        #region 异步

        #region 查询

        #region 全部数据
        /// <summary>
        /// 满足条件的第一个元素
        /// </summary>
        /// <param name="linq">查询表达式</param>
        /// <returns></returns>
        public async Task<T> SingleAsync(Expression<Func<T, bool>> linq)
        {
            return await this.DbContext.Set<T>().FirstOrDefaultAsync(linq);
        }

        /// <summary>
        /// 是否满足条件
        /// </summary>
        /// <param name="linq">查询表达式</param>
        /// <returns></returns>
        public async Task<bool> AnyAsync(Expression<Func<T, bool>> linq)
        {
            return await this.DbContext.Set<T>().AnyAsync(linq);
        }

        /// <summary>
        /// 满足条件的元素数量
        /// </summary>
        /// <param name="linq">查询表达式</param>
        /// <returns></returns>
        public async Task<int> CountAsync(Expression<Func<T, bool>> linq)
        {
            return await this.DbContext.Set<T>().CountAsync(linq);
        }

        #endregion

        #region 没被软删除的数据

        /// <summary>
        /// 满足条件的第一个元素(在没被软删除的数据中查询)
        /// </summary>
        /// <param name="linq">查询表达式</param>
        /// <returns></returns>
        public async Task<T> SingleNotDeleteAsync(Expression<Func<T, bool>> linq)
        {
            return await this.DbContext.Set<T>().Where(t => t.IsDeleted == false).FirstOrDefaultAsync(linq);
        }

        /// <summary>
        /// 是否满足条件(在没被软删除的数据中查询)
        /// </summary>
        /// <param name="linq">查询表达式</param>
        /// <returns></returns>
        public async Task<bool> AnyNotDeleteAsync(Expression<Func<T, bool>> linq)
        {
            return await this.DbContext.Set<T>().Where(t => t.IsDeleted == false).AnyAsync(linq);
        }

        /// <summary>
        /// 满足条件的元素数量(在没被软删除的数据中查询)
        /// </summary>
        /// <param name="linq">查询表达式</param>
        /// <returns></returns>
        public async Task<int> CountNotDeleteAsync(Expression<Func<T, bool>> linq)
        {
            return await this.DbContext.Set<T>().Where(t => t.IsDeleted == false).CountAsync(linq);
        }


        #endregion

        #endregion

        #endregion
    }
}
