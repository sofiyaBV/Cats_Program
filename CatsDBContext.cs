using DBCats.Tables;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBCats
{
    public class CatsDBContext : DbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<Account> Account { get; set; }
        public DbSet<SaveImage> SaveImage { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=MIPC\\SQLEXPRESS01;Initial Catalog=Cats;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }

    }
}
