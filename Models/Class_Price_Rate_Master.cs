using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DOTNETCOREEXAMPLE.Models
{
    [Table("Price_Rate_Master", Schema = "public")]
    public class Class_Price_Rate_Master
    {
        [Key]
        public int PRICERATEID { get; set; }
        public int SERVICEGROUPID { get; set; }
        public int SERVICEID { get; set; }
        public int VISITTYPEID { get; set; }

        public DateTime EFFECTIVEFROMDATE { get; set; }
        public DateTime EFFECTIVETODATE { get; set; }
        public int PRICE { get; set; }
    }
}
