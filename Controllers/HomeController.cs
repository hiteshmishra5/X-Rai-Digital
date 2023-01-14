using DOTNETCOREEXAMPLE.DataContext;
using DOTNETCOREEXAMPLE.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace DOTNETCOREEXAMPLE.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDBContext _context;

       
        private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}
        public HomeController(ApplicationDBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult AboutUs()
        {
            return View();
        }
        public IActionResult Services()
        {
            return View();
        }
        public IActionResult Blog()
        {
            return View();
        }        

        public IActionResult ContactUs()
        {
            return View();
        }
        public IActionResult SendEnquiry(string CONTACTUSNAME, string CONTACTUSEMAIL, string CONTACTUSMESSAGE)
        {
            Class_CONTACT_US_MASTER class_CONTACT_US_MASTER = new Class_CONTACT_US_MASTER();
            class_CONTACT_US_MASTER.CONTACTUSNAME = CONTACTUSNAME;
            class_CONTACT_US_MASTER.CONTACTUSEMAIL = CONTACTUSEMAIL;
            class_CONTACT_US_MASTER.CONTACTUSMESSAGE = CONTACTUSMESSAGE;
           _context.obj_CONTACT_US_MASTER.Add(class_CONTACT_US_MASTER);
            _context.SaveChanges();
            // HttpContext.Session.SetString("fullname", fullname);
            //  TempData["fullname"] = fullname;
            //int userid = _context.obj_PROJECT_USER_MASTER.Where(m => m.mobile == pROJECT_USER_MASTER.mobile).Select(m => m.useridsno).FirstOrDefault();
            
            int port = 25;
            //int port = 587;
            string host = "smtp.gmail.com";
            string username = "saisudhi@gmail.com";
            //string username = "info@xraidigital.com";
            string password = "sarvagnya2015";
            //string password = "Xray@home1234";
            string mailFrom = "saisudhi@gmail.com";
            string mailTo = "info@xraidigital.com";
            string mailTitle = "XRAI Contact Us  - Test";
           //// string mailMessage = "XRAI Contact Us  - Test";

            using (SmtpClient client = new SmtpClient())
            {
                MailAddress from = new MailAddress(mailFrom);
                MailMessage message = new MailMessage
                {
                    From = from
                };
                message.To.Add(mailTo);
                message.Bcc.Add("saisudhi@yahoo.com");
                message.Subject = mailTitle;
                message.Body =  "Message from Customer " +CONTACTUSMESSAGE;
                message.IsBodyHtml = true;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Host = host;
                client.Port = port;
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential
                {
                    UserName = username,
                    Password = password
                };
                //client.Send(message);
            }
            return RedirectToAction("Login", "Account");
            //  return RedirectToAction("Index", "Home");
        }
    }
}
