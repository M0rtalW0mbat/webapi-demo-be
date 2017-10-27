using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace model
{
    public class DBContext : DbContext
    {
        public DBContext(): base("MyArticleAppDB") 
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<DBContext>());
        }

        public DbSet<Article> Articles { get; set; }
    }
}
