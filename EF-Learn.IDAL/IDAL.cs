using EF_Learn.Model;

namespace EF_Learn.IDAL
{
	public partial interface IArticleDal : IBaseDal<Article> { }
	public partial interface IUserInfoDal : IBaseDal<UserInfo> { }
}