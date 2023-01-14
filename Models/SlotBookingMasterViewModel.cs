using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DOTNETCOREEXAMPLE.Models
{
    public class SlotBookingMasterViewModel
    {
        [Key]
        public Class_SLOT_BOOKING_MASTER a { get; set; }
        public Class_SLOT_MASTER b { get; set; }
        public Class_SERVICE_MASTER c { get; set; }
        public Class_PATIENT_MASTER d { get; set; }
        public Class_Price_Rate_Master e { get; set; }
        public Class_SERVICE_GROUP_MASTER f { get; set; }    
        public Class_Offer_Master g { get; set; }
        public Class_Slot_Booking_Details h { get; set; }
        public Class_SERVICE_PROVIDER_MASTER i { get; set; }

    }
}
