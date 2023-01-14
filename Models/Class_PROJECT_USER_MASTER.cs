using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DOTNETCOREEXAMPLE.Models
{
    [Table("project_user_master", Schema = "public")]
    public class Class_PROJECT_USER_MASTER
    {
        [Key]
        public int useridsno { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string userid { get; set; }
        public string mobile { get; set; }
        public string address { get; set; }
        public string address1 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public int pin { get; set; }
        public int registrationtypeid { get; set; }

        public string fullname { get; set; }
        public string gender { get; set; }
        public string mpin { get; set; }
        public bool isactive { get; set; }

    }
}
