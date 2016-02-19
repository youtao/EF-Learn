using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using EF_Learn.Common.Model.QueryParam;
using EF_Learn.Common.Tools;
using EF_Learn.IDAL;
using EF_Learn.Model;
using EF_Learn.ModelFactory;
using Newtonsoft.Json;

namespace IIC.BLL
{
    public abstract class BaseBll<T> where T : BaseModel, new()
    {
        protected IBaseDal<T> BaseDal;

        public DbContext DbContext { get; set; }

        #region 同步

        #region 增

        /// <summary>
        /// 增加
        /// </summary>        
        /// <param name="model"></param>
        /// <returns></returns>
        public int Add(T model)
        {
            this.BaseDal.Add(model);
            return this.DbContext.SaveChanges();
        }

        /// <summary>
        /// 批量增加
        /// </summary>        
        /// <param name="range"></param>
        /// <returns></returns>
        public int AddRange(IEnumerable<T> range)
        {
            this.BaseDal.AddRange(range);
            return this.DbContext.SaveChanges();
        }

        #endregion

        #region 删

        #region 硬删除

        /// <summary>
        /// 硬删除
        /// </summary>
        /// <param name="id"></param>        
        /// <returns></returns>
        public int HardDelete(int id)
        {
            this.BaseDal.HardDelete(entity => entity.Id == id);
            return this.DbContext.SaveChanges();
        }

        /// <summary>
        /// 硬删除
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        public int HardDelete(Expression<Func<T, bool>> whereLambda)
        {
            this.BaseDal.HardDelete(whereLambda);
            return this.DbContext.SaveChanges();
        }

        /// <summary>
        /// 硬删除
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int HardDelete(T entity)
        {
            this.BaseDal.HardDelete(entity);
            return this.DbContext.SaveChanges();
        }

        #endregion

        #region 软删除

        /// <summary>
        /// 软删除
        /// </summary>
        /// <returns></returns>
        public int SoftDelete(int id)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("update");
            sb.AppendLine(" @Table");
            sb.AppendLine("set");
            sb.AppendLine(" IsDeleted = 1");
            sb.AppendLine("where");
            sb.AppendLine(" Id = @Id");
            sb.AppendLine(";");
            return this.DbContext.Database.ExecuteSqlCommand(sb.ToString(),
                new SqlParameter("@Table", GetTableName()),
                new SqlParameter("@Id", id));
        }

        /// <summary>
        /// 软删除
        /// </summary>
        /// <param name="id">json:主键id</param>
        /// <returns></returns>
        public int SoftDelete(string id)
        {
            id = id.Replace("\"", "").Replace("[", "(").Replace("]", ")");
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("update");
            sb.AppendLine(" @Table");
            sb.AppendLine("set");
            sb.AppendLine(" IsDeleted = 1");
            sb.AppendLine("where");
            sb.AppendLine(" Id in @Id");
            sb.AppendLine(";");
            return this.DbContext.Database.ExecuteSqlCommand(sb.ToString(),
                new SqlParameter("@Table", GetTableName()),
                new SqlParameter("@Id", id));
        }

        /// <summary>
        /// 软删除
        /// </summary>
        /// <param name="whereLambda"></param>        
        /// <returns></returns>
        public int SoftDelete(Expression<Func<T, bool>> whereLambda)//todo:
        {
            var temp = this.Select(whereLambda);            
            foreach (var item in temp)
            {
                item.IsDeleted = true;
            }
            return this.DbContext.SaveChanges();
        }

        /// <summary>
        /// 软删除
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int SoftDelete(T entity)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("update");
            sb.AppendLine(" @Table");
            sb.AppendLine("set");
            sb.AppendLine(" IsDeleted = 1");
            sb.AppendLine("where");
            sb.AppendLine(" Id = @Id");
            sb.AppendLine(";");
            return this.DbContext.Database.ExecuteSqlCommand(sb.ToString(),
                new SqlParameter("@Table", GetTableName()),
                new SqlParameter("@Id", entity.Id));
        }

        #endregion

        #endregion

        #region 更新

        /// <summary>
        /// 更新
        /// </summary>
        /// <returns></returns>
        public int Update(T entity)
        {
            this.BaseDal.Update(entity);
            return this.DbContext.SaveChanges();
        }

        /// <summary>
        /// 附加实体到数据库上下文
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Attach(T entity)
        {
            this.BaseDal.Attach(entity);
            return this.DbContext.SaveChanges();
        }

        #endregion

        #region 查

        #region 全部数据
        /// <summary>
        /// 查询全部
        /// </summary>
        /// <returns></returns>
        public IQueryable<T> All()
        {
            return this.BaseDal.SelectAll();
        }

        /// <summary>
        /// 查询满足条件的数据
        /// </summary>
        /// <returns></returns>
        public IQueryable<T> Select(Expression<Func<T, bool>> whereLambda)
        {
            return this.BaseDal.SelectAll().Where(whereLambda);
        }

        /// <summary>
        /// 根据主键id查询第一个元素
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T Single(int? id)
        {
            return BaseDal.Single(entity => entity.Id == id);
        }

        /// <summary>
        /// 满足条件的第一个元素
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        public T Single(Expression<Func<T, bool>> whereLambda)
        {
            return BaseDal.Single(whereLambda);
        }


        /// <summary>
        /// 是否满足条件
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        public bool Any(Expression<Func<T, bool>> whereLambda)
        {
            return BaseDal.Any(whereLambda);
        }


        /// <summary>
        /// 满足条件的元素数量
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        public int Count(Expression<Func<T, bool>> whereLambda)
        {
            return this.BaseDal.Count(whereLambda);
        }
        #endregion

        #region 没被软删除的数据

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <returns></returns>
        public IQueryable<T> AllNotDelete()
        {
            return this.BaseDal.SelectNotDelete();
        }

        /// <summary>
        /// 查询满足条件的数据
        /// </summary>
        /// <returns></returns>
        public IQueryable<T> SelectNotDelete(Expression<Func<T, bool>> whereLambda)
        {
            return this.BaseDal.SelectNotDelete().Where(whereLambda);
        }

        /// <summary>
        /// 根据主键id查询第一个元素
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T SingleNotDelete(int? id)
        {
            return this.BaseDal.SingleNotDelete(entity => entity.Id == id);
        }

        /// <summary>
        /// 满足条件的第一个元素
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        public T SingleNotDelete(Expression<Func<T, bool>> whereLambda)
        {
            return this.BaseDal.SingleNotDelete(whereLambda);
        }


        /// <summary>
        /// 是否满足条件
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        public bool AnyNotDelete(Expression<Func<T, bool>> whereLambda)
        {
            return this.BaseDal.AnyNotDelete(whereLambda);
        }


        /// <summary>
        /// 满足条件的元素数量
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        public int CountNotDelete(Expression<Func<T, bool>> whereLambda)
        {
            return this.BaseDal.CountNotDelete(whereLambda);
        }

        #endregion

        #region 分页

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public IQueryable<T> SelectForPaging(BasePaging param)
        {
            this.InitPagingQueryParam(param);
            var temp = this.Select(e => e.IsDeleted == param.IsDeleted);
            param.RecordTotal = temp.Count();
            temp = temp.OrderByDescending(t => t.Id);
            temp = temp.Skip(param.PageSize.Value * (param.PageIndex.Value - 1)).Take(param.PageSize.Value);
            return temp;
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="param"></param>
        /// <param name="linq"></param>        
        /// <returns></returns>
        public IQueryable<T> SelectForPaging(Expression<Func<T, bool>> linq, BasePaging param)
        {
            this.InitPagingQueryParam(param);
            var temp = this.Select(e => e.IsDeleted == param.IsDeleted).Where(linq);
            param.RecordTotal = temp.Count();
            temp = temp.OrderByDescending(t => t.Id);
            temp = temp.Skip(param.PageSize.Value * (param.PageIndex.Value - 1)).Take(param.PageSize.Value);
            return temp;
        }

        #endregion

        #endregion

        #endregion

        #region 异步

        #region 增

        /// <summary>
        /// 异步增加
        /// </summary>        
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<int> AddAsync(T entity)
        {
            this.BaseDal.Add(entity);
            return await this.DbContext.SaveChangesAsync();
        }

        /// <summary>
        /// 异步批量增加
        /// </summary>        
        /// <param name="range"></param>
        /// <returns></returns>
        public async Task<int> AddRangeAsync(IEnumerable<T> range)
        {
            this.BaseDal.AddRange(range);
            return await this.DbContext.SaveChangesAsync();
        }

        #endregion

        #region 删

        #region 硬删除

        /// <summary>
        /// 异步硬删除
        /// </summary>
        /// <param name="id"></param>        
        /// <returns></returns>
        public async Task<int> HardDeleteAsync(int id)
        {
            this.BaseDal.HardDelete(entity => entity.Id == id);
            return await this.DbContext.SaveChangesAsync();
        }

        /// <summary>
        /// 异步硬删除
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        public async Task<int> HardDeleteAsync(Expression<Func<T, bool>> whereLambda)
        {
            this.BaseDal.HardDelete(whereLambda);
            return await this.DbContext.SaveChangesAsync();
        }


        /// <summary>
        /// 异步硬删除
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<int> HardDeleteAsync(T entity)
        {
            this.BaseDal.HardDelete(entity);
            return await this.DbContext.SaveChangesAsync();
        }

        #endregion

        #region 软删除

        /// <summary>
        /// 异步软删除
        /// </summary>
        /// <returns></returns>
        public async Task<int> SoftDeleteAsync(int id)
        {
            var entity = await this.BaseDal.SingleAsync(e => e.Id == id);
            entity.IsDeleted = true;
            this.BaseDal.Update(entity);
            return await this.DbContext.SaveChangesAsync();
        }

        /// <summary>
        /// 异步软删除
        /// 返回-1:序列化失败
        /// 返回0:删除失败
        /// 返回>0:删除成功
        /// </summary>
        /// <param name="ids">json:主键id</param>
        /// <returns></returns>
        public async Task<int> SoftDeleteAsync(string ids)
        {
            List<int> list;
            try
            {
                list = JsonConvert.DeserializeObject<List<int>>(ids);
            }
            catch (Exception)
            {
                return -1;
            }
            if (list.Count <= 0)
            {
                return -1;
            }
            var temp = this.Select(e => list.Contains(e.Id));
            foreach (var item in temp)
            {
                item.IsDeleted = true;
            }
            return await this.DbContext.SaveChangesAsync();
        }

        /// <summary>
        /// 异步软删除
        /// </summary>
        /// <param name="whereLambda"></param>        
        /// <returns></returns>
        public async Task<int> SoftDeleteAsync(Expression<Func<T, bool>> whereLambda)
        {
            var temp = this.Select(whereLambda);
            foreach (var item in temp)
            {
                item.IsDeleted = true;
            }
            return await this.DbContext.SaveChangesAsync();
        }

        /// <summary>
        /// 异步软删除
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<int> SoftDeleteAsync(T entity)
        {
            entity.IsDeleted = true;
            BaseDal.Update(entity);
            return await BaseDal.DbContext.SaveChangesAsync();
        }

        #endregion

        #endregion

        #region 更新

        /// <summary>
        /// 异步更新
        /// </summary>
        /// <returns></returns>
        public async Task<int> UpdateAsync(T entity)
        {
            this.BaseDal.Update(entity);
            return await this.BaseDal.DbContext.SaveChangesAsync();
        }

        /// <summary>
        /// 异步附加实体到数据库上下文
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<int> AttachAsync(T entity)
        {
            this.BaseDal.Attach(entity);
            return await this.DbContext.SaveChangesAsync();
        }

        #endregion

        #region 查

        #region 全部数据
        /// <summary>
        /// 异步根据主键id查询第一个元素
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<T> SingleAsync(int? id)
        {
            return await this.BaseDal.SingleAsync(entity => entity.Id == id);
        }

        /// <summary>
        /// 异步满足条件的第一个元素
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        public async Task<T> SingleAsync(Expression<Func<T, bool>> whereLambda)
        {
            return await this.BaseDal.SingleAsync(whereLambda);
        }

        /// <summary>
        /// 异步满足条件的数量
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        public async Task<int> CountAsync(Expression<Func<T, bool>> whereLambda)
        {
            return await this.BaseDal.CountAsync(whereLambda);
        }

        /// <summary>
        /// 异步是否满足条件
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        public async Task<bool> AnyAsync(Expression<Func<T, bool>> whereLambda)
        {
            return await this.BaseDal.AnyAsync(whereLambda);
        }
        #endregion

        #region 没被删除的数据

        /// <summary>
        /// 异步根据主键id查询第一个元素(在没被软删除的数据中)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<T> SingleNotDeleteAsync(int? id)
        {
            return await this.BaseDal.SingleNotDeleteAsync(entity => entity.Id == id);
        }

        /// <summary>
        /// 异步满足条件的第一个元素(在没被软删除的数据中)
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        public async Task<T> SingleNotDeleteAsync(Expression<Func<T, bool>> whereLambda)
        {
            return await this.BaseDal.SingleNotDeleteAsync(whereLambda);
        }

        /// <summary>
        /// 异步满足条件数量(在没被软删除的数据中)
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        public async Task<int> CountNotDeleteAsync(Expression<Func<T, bool>> whereLambda)
        {
            return await this.BaseDal.CountNotDeleteAsync(whereLambda);
        }

        /// <summary>
        /// 异步是否满足条件(在没被软删除的数据中)
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        public async Task<bool> AnyNotDeleteAsync(Expression<Func<T, bool>> whereLambda)
        {
            return await this.BaseDal.AnyNotDeleteAsync(whereLambda);
        }
        #endregion

        #endregion

        #endregion

        #region private

        /// <summary>
        /// 初始化分页查询参数
        /// </summary>
        /// <param name="param"></param>
        private void InitPagingQueryParam(BasePaging param)
        {
            if (param.PageIndex == null || param.PageIndex.Value <= 0)
            {
                param.PageIndex = 1;
            }
            if (param.PageSize == null || param.PageSize.Value <= 0)
            {

                var size = ConfigurationManager.AppSettings["PageSize"];
                param.PageSize = Convert.ToInt32(size);
            }
            param.PageSize = param.PageSize ?? 12;
        }

        /// <summary>
        /// 获取表名
        /// </summary>
        /// <returns></returns>
        private string GetTableName()
        {
            return this.DbContext.GetTableName<T>();
        }

        public void PP()
        {
           
        }

        #endregion
    }
}