using DOTNETCOREEXAMPLE.DataContext;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using DOTNETCOREEXAMPLE.Controllers;

namespace DOTNETCOREEXAMPLE.Models
{
   
  
     [Table("SLOT_BOOKING_MASTER", Schema = "public")]
    public class Class_SLOT_BOOKING_MASTER
    {
        [Key]
        public int SLOTBOOKINGID { get; set; }
        public int SLOTID { get; set; }
        public int SLOTTYPEID { get; set; }
        public int VISITTYPEID { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime SLOTBOOKINGDATETIME { get; set; }
        public int SERVICETYPEID { get; set; }
        public int SERVICEPROVIDERID { get; set; }
        public string PHONENUMBER { get; set; }

        public string PATIENTNAME { get; set; }
        public int AGE { get; set; }
        public int WEIGHT { get; set; }
        public string GENDER { get; set; }
        public string ADDRESS { get; set; }
        public string ALTERNATEMOBILENUMBER { get; set; }
        public string EMAIL { get; set; }
        public DateTime CREATEDDATE { get; set; }
        public int? SERVICEGROUPID { get; set; }
        public string PAYMENTMETHOD { get; set; }
        public string PRESCRIPTIONFILENAME { get; set; }
        public string EXTENSION { get; set; }
        public int? PATIENTID { get; set; }
        public int? USERID { get; set; }
        public string SERVICEFILENAME { get; set; }
        public string REPORTFILENAME { get; set; }
        public string SERVICEEXTENSION { get; set; }
        public string REPORTEXTENSION { get; set; }
        public string UPLOADORCLICKPHOTO { get; set; }
        public string PAYMENTSTATUS { get; set; }
        public string STATUS { get; set; }

        public int? PRICERATEID { get; set; }
        public int? MACHINEID { get; set; }
        public int? OFFERID { get; set; }
        public int? LOCATIONID { get; set; }
        public bool ISACTIVE { get; set; }

    }
  



}
