using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WFProject.Models.Maps;
using WFProject.Models.Tables;

namespace WFProject.Models.Context
{
    public class CraftContext : DbContext
    {
        static CraftContext()
        {
            Database.SetInitializer<CraftContext>(null);
        }

        public CraftContext() : base("Name=CraftContext") { }

        public virtual DbSet<tbl_user> tbl_user { get; set; }
        public virtual DbSet<tbl_artisan> tbl_artisan { get; set; }
        public virtual DbSet<tbl_product> tbl_product { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.Add(new tbl_user_map());
            modelBuilder.Configurations.Add(new tbl_artisan_map());
            modelBuilder.Configurations.Add(new tbl_product_map());
        }
    }
}
