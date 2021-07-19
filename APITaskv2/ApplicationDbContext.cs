using Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace APITaskv2
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext():base(@"Data Source=DESKTOP-8JKEUH5\SQLEXPRESS;Initial Catalog=TASKS;Integrated Security=True")
        {

        }

        public DbSet<User> User { get; set; }
        public DbSet<ToDo> ToDo { get; set; }
    }
}