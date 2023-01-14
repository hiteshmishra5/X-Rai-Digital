using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DOTNETCOREEXAMPLE.Models
{
    [Table("SERVICE_GROUP_MASTER", Schema = "public")]
    public class Class_SERVICE_GROUP_MASTER
    {
        [Key]
        public int SERVICEGROUPID { get; set; }
        public string SERVICEGROUPNAME { get; set; }
        public bool ISACTIVE { get; set; }
    }
}
