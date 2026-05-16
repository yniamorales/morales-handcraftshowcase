using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WFProject.Models.Tables;

namespace WFProject.Models.Maps
{
    public class tbl_user_map : EntityTypeConfiguration<tbl_user>
    {
        public tbl_user_map()
        {
            HasKey(i => i.UserId);
            ToTable("tbl_user");
        }
    }
}
