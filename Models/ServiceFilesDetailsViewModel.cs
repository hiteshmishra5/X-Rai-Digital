using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DOTNETCOREEXAMPLE.Models
{
    public class ServiceFilesDetailsViewModel
    {
        [Key]
        public Class_Slot_Booking_Details a { get; set; }
        public Class_SERVICE_FILES_DETAILS b { get; set; }
        
    }
}
