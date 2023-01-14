using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DOTNETCOREEXAMPLE.Models
{
    [Table("slot_master", Schema = "public")]
    public class Class_SLOT_MASTER
    {
        [Key]
        public int SLOTID { get; set; }
        public string SLOTNAME { get; set; }
        public bool ISACTIVE { get; set; }
        public int ORDERNO { get; set; }
    }
 

}
