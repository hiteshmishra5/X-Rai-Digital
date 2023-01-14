using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DOTNETCOREEXAMPLE.Models
{
    [Table("registration_type", Schema = "public")]
    public class Class_REGISTRATION_TYPE
    {
        [Key]
        public int registrationtypeid { get; set; }
        public string registrationtypename { get; set; }
        public bool isactive { get; set; }
       // public IEnumerable<SelectListItem> GetRegistrationTypeID { get; set; }
    }
}
