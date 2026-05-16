using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFProject.Models.Tables
{
    public class tbl_product
    {
        public int ProductId { get; set; }

        public int ArtisanId { get; set; }

        public string ProductName { get; set; }

        public string ProductDesc { get; set; }

        public string ProductImgUrl { get; set; }

        public string ProductStatus { get; set; }
    }
}
