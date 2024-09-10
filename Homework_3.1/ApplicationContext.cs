using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_3._1
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null;
        
        public ApplicationContext(DbContextOptions options) : base(options)
        {

        }
    }
}
