using EF_Learn.Model;

namespace EF_Learn.IBLL
{
 	public partial interface IArticleBll : IBaseBll<Article> { }
	public partial interface IUserInfoBll : IBaseBll<UserInfo> { }
}