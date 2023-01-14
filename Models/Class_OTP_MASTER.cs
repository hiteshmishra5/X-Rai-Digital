using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DOTNETCOREEXAMPLE.Models
{
    [Table("OTP_MASTER", Schema = "public")]
    public class Class_OTP_MASTER
    {
        [Key]
        public int OTPID { get; set; }
        public string OTP { get; set; }
        public DateTime OTPDATETIME { get; set; }
        public string MOBILE { get; set; }
    }
}
