using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DOTNETCOREEXAMPLE.Models
{
    [Table("SERVICE_MASTER", Schema = "public")]
    public class Class_SERVICE_MASTER
    {
        [Key]
        public int SERVICEID { get; set; }
        public string SERVICENAME { get; set; }
        public int SERVICEGROUPID { get; set; }
        public bool ISACTIVE { get; set; }
    }
}
