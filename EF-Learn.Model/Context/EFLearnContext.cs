using System.Data.Entity;

namespace EF_Learn.Model
{
    public class EFLearnContext : DbContext
    {
        public EFLearnContext() : base("EFLearnContext")
        {

        }

        public virtual DbSet<UserInfo> UserInfo { get; set; }
        public virtual DbSet<Article> Article { get; set; }
    }
}