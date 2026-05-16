using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFProject.Models.Tables
{
    public class tbl_artisan
    {
        public int ArtisanId {  get; set; }
        public int UserId {  get; set; }
        public string artisanBio { get; set; }

        public string contactNum { get; set; }

        public string artisanStatus { get; set; }

        public string artisanName { get; set; }
    }
}
