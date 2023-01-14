using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DOTNETCOREEXAMPLE.Models
{
    [Table("MACHINE_MASTER", Schema = "public")]
    public class Class_Machine_Master
    {
        [Key]
        public int MACHINEID { get; set; }
        public int SERVICEGROUPID { get; set; }
        public string MACHINENAME  { get; set; }
        public bool ISACTIVE { get; set; }
        public int LOCATIONID { get; set; }
        
    }
}
