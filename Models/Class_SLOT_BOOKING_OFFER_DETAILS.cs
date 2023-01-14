using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DOTNETCOREEXAMPLE.Models
{
    [Table("SLOT_BOOKING_OFFER_DETAILS", Schema = "public")]
    public class Class_SLOT_BOOKING_OFFER_DETAILS
    {
        [Key]
        public int SLOTBOOKINGOFFERID { get; set; }
        public int SLOTBOOKINGID { get; set; }
        public int OFFERID { get; set; }
        public DateTime CREATEDDATE { get; set; }
        public int CREATEDBY { get; set; }
        
    }
}
