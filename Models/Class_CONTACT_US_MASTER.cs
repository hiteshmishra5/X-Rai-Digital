using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DOTNETCOREEXAMPLE.Models
{
    [Table("CONTACT_US_MASTER", Schema = "public")]
    public class Class_CONTACT_US_MASTER
    {
        [Key]
        public int CONTACTUSID { get; set; }
        public string CONTACTUSNAME { get; set; }
        public string CONTACTUSEMAIL { get; set; }
        public string CONTACTUSMESSAGE { get; set; }

        public DateTime CREATEDDATE { get; set; }

    }
}
