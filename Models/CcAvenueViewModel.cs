using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DOTNETCOREEXAMPLE.Models
{
    public class CcAvenueViewModel
    {
        public string AccessCode { get; set; }
        public string EncryptionRequest { get; set; }
        public string CheckoutUrl { get; set; }
        public CcAvenueViewModel(string EncryptionRequest1, string AccessCode1, string CheckoutUrl1)
        {
            AccessCode = AccessCode1;

            EncryptionRequest = EncryptionRequest1;

            CheckoutUrl = CheckoutUrl1;

        }


    }
}