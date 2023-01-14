using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DOTNETCOREEXAMPLE.Models
{
    [Table("PATIENT_MASTER", Schema = "public")]
    public class Class_PATIENT_MASTER
    {
        [Key]
        public int PATIENTID { get; set; }
        public string PATIENTNAME { get; set; }
        [Range(1, 120, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int AGE { get; set; }
        public int WEIGHT { get; set; }
        public string GENDER { get; set; }
        public string ADDRESS { get; set; }
        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Invalid Mobile Number.")]
        public string ALTERNATEMOBILENUMBER { get; set; }
        public string EMAIL { get; set; }
        public int USERID { get; set; }
        public string PATIENTFILENAME { get; set; }
        public string PATIENTFILEEXTENSION { get; set; }
        public string PIN { get; set; }
        public string REFNO { get; set; }
        public double HEIGHT { get; set; }
        public double BMI { get; set; }
        public string BP { get; set; }

    }
}
