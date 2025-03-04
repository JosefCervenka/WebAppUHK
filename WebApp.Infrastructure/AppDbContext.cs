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
            modelBuilder.Entity<Unit>().HasData(
                new Unit { Id = 1, Name = "ks" },
                new Unit { Id = 2, Name = "g" },
                new Unit { Id = 3, Name = "kg" },
                new Unit { Id = 4, Name = "ml" },
                new Unit { Id = 5, Name = "dl" },
                new Unit { Id = 6, Name = "l" },
                new Unit { Id = 7, Name = "balení" },
                new Unit { Id = 8, Name = "balíček" },
                new Unit { Id = 9, Name = "hrnek" },
                new Unit { Id = 10, Name = "hrst" },
                new Unit { Id = 11, Name = "kalíšek" },
                new Unit { Id = 12, Name = "kelímek" },
                new Unit { Id = 13, Name = "kostka" },
                new Unit { Id = 14, Name = "lahev" },
                new Unit { Id = 15, Name = "lžička" },
                new Unit { Id = 16, Name = "lžíce" },
                new Unit { Id = 17, Name = "miska" },
                new Unit { Id = 18, Name = "plátek" },
                new Unit { Id = 19, Name = "plechovka" },
                new Unit { Id = 20, Name = "sklenice" },
                new Unit { Id = 21, Name = "snítka" },
                new Unit { Id = 22, Name = "stroužek" },
                new Unit { Id = 23, Name = "svazek" },
                new Unit { Id = 24, Name = "šálek" },
                new Unit { Id = 25, Name = "špetka" },
                new Unit { Id = 26, Name = "krajíc" },
                new Unit { Id = 27, Name = "stonek" }
            );
            #region unit

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
        public DbSet<Ingredient> Ingredient { get; set; }
        public DbSet<Unit> Unit { get; set; }

    }
}
