using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DOTNETCOREEXAMPLE.Models
{
    [Table("REPORT_FILES_DETAILS", Schema = "public")]
    public class Class_REPORT_FILES_DETAILS
    {
        [Key]
        public int REPORTFILEDETID { get; set; }
        public int SLOTBOOKINGDETID { get; set; }

        public string REPORTFILENAME { get; set; }
        public string REPORTEXTENSION { get; set; }
        public bool ISACTIVE { get; set; }
        public int SLOTBOOKINGID { get; set; }
    }
}
