using System;
using System.Linq;
using System.Linq.Expressions;
using EF_Learn.Common.Model.QueryParam;
using EF_Learn.IBLL;
using EF_Learn.IDAL;
using EF_Learn.Model;
using IIC.BLL;

namespace EF_Learn.BLL
{
 	public partial class ArticleBll : BaseBll<Article>, IArticleBll
    {
		private readonly IArticleDal CurrentDal;
        public ArticleBll(IArticleDal dal)
        {
            this.BaseDal = dal;
			this.CurrentDal = dal;
			this.DbContext = dal.DbContext;
        }
    }
	public partial class UserInfoBll : BaseBll<UserInfo>, IUserInfoBll
    {
		private readonly IUserInfoDal CurrentDal;
        public UserInfoBll(IUserInfoDal dal)
        {
            this.BaseDal = dal;
			this.CurrentDal = dal;
			this.DbContext = dal.DbContext;
        }
    }
}