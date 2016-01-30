using EF_Learn.IDAL;
using EF_Learn.Model;

namespace EF_Learn.DAL
{
 	public partial class ArticleDal : BaseDal<Article>, IArticleDal { }
	public partial class UserInfoDal : BaseDal<UserInfo>, IUserInfoDal { }
}