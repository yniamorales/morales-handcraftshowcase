using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WFProject.Models.Tables;

namespace WFProject.Models.Maps
{
    public class tbl_product_map : EntityTypeConfiguration<tbl_product>
    {
        public tbl_product_map()
        {
            HasKey(i => i.ProductId);
            ToTable("tbl_product");
        }
    }
}
