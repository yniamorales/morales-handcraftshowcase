using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WFProject.Models.Tables;

namespace WFProject.Models.Maps
{
    public class tbl_artisan_map : EntityTypeConfiguration<tbl_artisan>
    {
        public tbl_artisan_map()
        {
            HasKey(i => i.ArtisanId);
            ToTable("tbl_artisan");
        }
    }
}
