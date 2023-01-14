using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DOTNETCOREEXAMPLE.Models
{
    [Table("OFFER_OPTION_MASTER", Schema = "public")]
    public class Class_Offer_Option_Master
    {
        [Key]
        public int OFFEROPTIONID { get; set; }
        public string OFFEROPTION { get; set; }
       
        public bool ISACTIVE { get; set; }
    }
}
