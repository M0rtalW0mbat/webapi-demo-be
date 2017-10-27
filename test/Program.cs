using model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var ctx = new DBContext())
            {
                var article = new Article() { Title = "Mars invasion", Body = "Its a long story...", CreationDate = DateTime.Now };
                ctx.Articles.Add(article);
                ctx.SaveChanges();
            }
        }
    }
}
