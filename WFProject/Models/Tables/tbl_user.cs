using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFProject.Models.Tables
{
    public class tbl_user
    {
        public int UserId {get; set;}

        public string username { get; set; }

        public string password { get; set; }

        public string name { get; set; }

        public string user_role { get; set; }

        public string status { get; set; }

        public DateTime created_at { get; set; }
    }
}
