using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DOTNETCOREEXAMPLE.Models
{
    [Table("SLOT_BOOKING_DETAILS", Schema = "public")]
    public class Class_Slot_Booking_Details
    {
        [Key]
        public int SLOTBOOKINGDETID { get; set; }
        public int SLOTBOOKINGID { get; set; }
        public int SERVICEGROUPID { get; set; }
        public int SERVICETYPEID { get; set; }
        public int PRICE { get; set; }

    }
}
