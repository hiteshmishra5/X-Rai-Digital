using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DOTNETCOREEXAMPLE.Models
{
    [Table("SERVICE_PROVIDER_MASTER", Schema = "public")]
    public class Class_SERVICE_PROVIDER_MASTER
    {
        [Key]
        public int SERVICEPROVIDERID { get; set; }
        public string SERVICEPROVIDERNAME { get; set; }
        public bool ISACTIVE { get; set; }
        public int USERID { get; set; }
    }
}
