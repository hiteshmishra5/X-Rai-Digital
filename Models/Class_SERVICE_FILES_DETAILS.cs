using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DOTNETCOREEXAMPLE.Models
{
    [Table("SERVICE_FILES_DETAILS", Schema = "public")]
    public class Class_SERVICE_FILES_DETAILS
    {
        [Key]
        public int SERVICEFILEDETID { get; set; }
        public int SLOTBOOKINGDETID { get; set; }
       
        public string SERVICEFILENAME { get; set; }
        public string SERVICEEXTENSION { get; set; }
        public bool ISACTIVE { get; set; }
        public int SLOTBOOKINGID { get; set; }
    }
}
