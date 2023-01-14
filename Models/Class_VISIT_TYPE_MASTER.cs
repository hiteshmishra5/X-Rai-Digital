using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DOTNETCOREEXAMPLE.Models
{
    [Table("VISIT_TYPE_MASTER", Schema = "public")]
    public class Class_VISIT_TYPE_MASTER
    {
        [Key]
        public int VISITTYPEID { get; set; }
        public string VISITTYPENAME { get; set; }
        public bool ISACTIVE { get; set; }
    }
}
