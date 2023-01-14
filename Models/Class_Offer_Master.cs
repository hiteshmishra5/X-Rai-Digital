using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DOTNETCOREEXAMPLE.Models
{
    [Table("OFFER_MASTER", Schema = "public")]
    public class Class_Offer_Master
    {
        [Key]
        public int? OFFERID { get; set; }
        public string OFFERNAME { get; set; }
        public int OFFERDISCOUNT { get; set; }
        public bool ISACTIVE { get; set; }
        public int OFFEROPTIONID { get; set; }
        public int USERID { get; set; }
        public int REGISTRATIONTYPEID { get; set; }
        public DateTime EFFECTIVEDATEFROM { get; set; }
        public DateTime EFFECTIVEDATETO { get; set; }
    }
}
