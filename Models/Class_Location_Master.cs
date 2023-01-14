using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DOTNETCOREEXAMPLE.Models
{
    [Table("LOCATION_MASTER", Schema = "public")]
    public class Class_Location_Master
    {
        [Key]
        public int LOCATIONID { get; set; }
        
        public string LOCATIONNAME { get; set; }
        public bool ISACTIVE { get; set; }
    }
}
