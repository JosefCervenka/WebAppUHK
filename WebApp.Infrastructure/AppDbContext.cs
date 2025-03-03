using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WebApp.Core.Models;
using WebApp.Core.Models.Common;
using WebApp.Core.Models.Recipe;
using WebApp.Core.Models.Sys;


namespace WebApp.Infrastructure
{
    public class AppDbContext : DbContext
    {
        /// <summary>
        /// Configure connection string and additional setting with database
        /// </summary>
        /// <param name="optionsBuilder"></param>

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=web-app-web-app-uhk.b.aivencloud.com;Port=27459;Database=defaultdb;Username=avnadmin;Password=AVNS_LutJ4R1h5NWcPi-BUx0;SslMode=Require;");
        }


        /// <summary>
        /// Seeding data
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region SysRole
            modelBuilder.Entity<SysRole>().HasData(
                new SysRole
                {
                    Id = (int)Core.Enums.SysRoleEnum.Admin,
                    Name = "Admin",
                }
            );

            modelBuilder.Entity<SysRole>().HasData(
                new SysRole
                {
                    Id = (int)Core.Enums.SysRoleEnum.User,
                    Name = "User",
                }
            );
            #endregion


            base.OnModelCreating(modelBuilder);
        }
        public DbSet<User> User { get; set; }
        public DbSet<SysRole> SysRole { get; set; }
        public DbSet<UserSysRole> UserSysRole { get; set; }
        public DbSet<Image> Image { get; set; }
        public DbSet<Photo> Photo { get; set; }
        public DbSet<Recipe> Recipe { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<Step> Step { get; set; }

    }
}
