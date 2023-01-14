using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DOTNETCOREEXAMPLE.DataContext;
using DOTNETCOREEXAMPLE.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using CCA.Util;
using Newtonsoft.Json;
using System.Net.Mail;
using System.Net;
using System.Collections;

namespace DOTNETCOREEXAMPLE.Controllers
{
    [SessionTimeout]
    public class SlotBookingMasterController : Controller
    {
        private readonly ApplicationDBContext _context;

        public SlotBookingMasterController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: SlotBookingMaster
        public IActionResult Index()
        {
            HttpContext.Session.GetString("RegistrationTypeId");
            IEnumerable<SelectListItem> SLOTS = _context.obj_SLOT_MASTER.Select(c => new SelectListItem
            {

                Value = c.SLOTID.ToString(),
                Text = c.SLOTNAME

            });
            ViewBag.SLOTS = SLOTS;
            if (HttpContext.Session.GetString("RegistrationTypeId") == "1")
            {

                int intUserId = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
                var viewmodelResult = from p in _context.obj_SLOT_BOOKING_MASTER
                                    //  join s in _context.obj_Slot_Booking_Details on p.SLOTBOOKINGID equals s.SLOTBOOKINGID
                                      join k in _context.obj_SLOT_MASTER on p.SLOTID equals k.SLOTID
                                     // join m in _context.obj_SERVICE_MASTER on s.SERVICETYPEID equals m.SERVICEID
                                      join n in _context.obj_PATIENT_MASTER on p.PATIENTID equals n.PATIENTID
                                    //  join o in _context.obj_SERVICE_GROUP_MASTER on s.SERVICEGROUPID equals o.SERVICEGROUPID
                                      //  join q in _context.obj_Price_Rate_Master on p.PRICERATEID equals q.PRICERATEID
                                      join r in _context.obj_Offer_Master on p.OFFERID equals r.OFFERID
                                      where p.USERID == intUserId & p.ISACTIVE == true
                                      //  group by p.SLOTBOOKINGID
                                      orderby p.SLOTBOOKINGDATETIME descending
                                      select new Models.SlotBookingMasterViewModel { a = p, b = k
                                      //, c = m
                                      , d = n
                                      //, f = o
                                      , g = r
                                      //, h = s
                                      };
                var data = viewmodelResult.ToList();
                return View(data);
                // return View(await _context.obj_SLOT_BOOKING_MASTER.Where(m => m.USERID == intUserId).OrderByDescending(m => m.SLOTBOOKINGID).ToListAsync());
            }
            else if (HttpContext.Session.GetString("RegistrationTypeId") == "2")
            {
                int intUserId = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
                var serviceProvider = _context.obj_SERVICE_PROVIDER_MASTER.Where(s => s.USERID == intUserId).Select(s => s.SERVICEPROVIDERID).FirstOrDefault();
                var viewmodelResult = from p in _context.obj_SLOT_BOOKING_MASTER
                                      join k in _context.obj_SLOT_MASTER on p.SLOTID equals k.SLOTID
                                     // join m in _context.obj_SERVICE_MASTER on p.SERVICETYPEID equals m.SERVICEID
                                      join n in _context.obj_PATIENT_MASTER on p.PATIENTID equals n.PATIENTID
                                    //  join o in _context.obj_SERVICE_GROUP_MASTER on p.SERVICEGROUPID equals o.SERVICEGROUPID
                                     // join q in _context.obj_Price_Rate_Master on p.PRICERATEID equals q.PRICERATEID
                                      join r in _context.obj_Offer_Master on p.OFFERID equals r.OFFERID
                                      where p.SERVICEPROVIDERID == serviceProvider && (p.STATUS == "Alloted" || p.STATUS == "Reached" || p.STATUS == "Files Uploaded" || p.STATUS == "Received") & p.ISACTIVE == true
                                      orderby p.SLOTBOOKINGDATETIME descending
                                      select new Models.SlotBookingMasterViewModel { a = p, b = k
                                      //,  c = m
                                          , d = n
                                         // , e = q
                                          //, f = o
                                          , g = r
                                      };
                var data = viewmodelResult.ToList();
                return View(data);
                // return View(await _context.obj_SLOT_BOOKING_MASTER.OrderByDescending(m => m.SLOTBOOKINGID).ToListAsync());
            }
            else if (HttpContext.Session.GetString("RegistrationTypeId") == "5")
            {
                int intRegistrationTypeId = Convert.ToInt32(HttpContext.Session.GetString("RegistrationTypeId"));
                int intUserId = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
                var viewmodelResult = from p in _context.obj_SLOT_BOOKING_MASTER
                                      join s in _context.obj_Slot_Booking_Details on p.SLOTBOOKINGID equals s.SLOTBOOKINGID
                                      join k in _context.obj_SLOT_MASTER on p.SLOTID equals k.SLOTID
                                      join m in _context.obj_SERVICE_MASTER on s.SERVICETYPEID equals m.SERVICEID
                                      join n in _context.obj_PATIENT_MASTER on p.PATIENTID equals n.PATIENTID
                                      join o in _context.obj_SERVICE_GROUP_MASTER on s.SERVICEGROUPID equals o.SERVICEGROUPID
                                      //  join q in _context.obj_Price_Rate_Master on p.PRICERATEID equals q.PRICERATEID
                                      join r in _context.obj_Offer_Master on p.OFFERID equals r.OFFERID
                                      where p.USERID == intUserId & p.ISACTIVE == true
                                      //  group by p.SLOTBOOKINGID
                                      orderby p.SLOTBOOKINGDATETIME descending
                                      select new Models.SlotBookingMasterViewModel { a = p, b = k, c = m, d = n, f = o, g = r, h = s };
                var data = viewmodelResult.ToList();
                //for promo code
                var offerperuserregistration = _context.obj_Offer_Master.Where(m => m.REGISTRATIONTYPEID == intRegistrationTypeId && m.USERID == intUserId).Select(s => s.OFFERID).ToList();
                var viewmodelResult1 = from p in _context.obj_SLOT_BOOKING_MASTER
                                           //  join s in _context.obj_Slot_Booking_Details on p.SLOTBOOKINGID equals s.SLOTBOOKINGID
                                       join k in _context.obj_SLOT_MASTER on p.SLOTID equals k.SLOTID
                                       // join m in _context.obj_SERVICE_MASTER on s.SERVICETYPEID equals m.SERVICEID
                                       join n in _context.obj_PATIENT_MASTER on p.PATIENTID equals n.PATIENTID
                                       //  join o in _context.obj_SERVICE_GROUP_MASTER on s.SERVICEGROUPID equals o.SERVICEGROUPID
                                       //  join q in _context.obj_Price_Rate_Master on p.PRICERATEID equals q.PRICERATEID
                                       join r in _context.obj_Offer_Master on p.OFFERID equals r.OFFERID
                                       where offerperuserregistration.Contains(p.OFFERID) & p.ISACTIVE == true
                                       //  group by p.SLOTBOOKINGID
                                       orderby p.SLOTBOOKINGDATETIME descending
                                       select new Models.SlotBookingMasterViewModel
                                       {
                                           a = p,
                                           b = k
                                       //, c = m
                                       ,
                                           d = n
                                       //, f = o
                                       ,
                                           g = r
                                           //, h = s
                                       };

                ViewBag.SlotBookingWithPromoCode = viewmodelResult1.ToList();
                //end for promo code
                return View(data);
                // return View(await _context.obj_SLOT_BOOKING_MASTER.Where(m => m.USERID == intUserId).OrderByDescending(m => m.SLOTBOOKINGID).ToListAsync());
            }
            else if (HttpContext.Session.GetString("RegistrationTypeId") == "6")
            {
                int intRegistrationTypeId = Convert.ToInt32(HttpContext.Session.GetString("RegistrationTypeId"));
                int intUserId = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
                var viewmodelResult = from p in _context.obj_SLOT_BOOKING_MASTER
                                          //  join s in _context.obj_Slot_Booking_Details on p.SLOTBOOKINGID equals s.SLOTBOOKINGID
                                      join k in _context.obj_SLOT_MASTER on p.SLOTID equals k.SLOTID
                                      // join m in _context.obj_SERVICE_MASTER on s.SERVICETYPEID equals m.SERVICEID
                                      join n in _context.obj_PATIENT_MASTER on p.PATIENTID equals n.PATIENTID
                                      //  join o in _context.obj_SERVICE_GROUP_MASTER on s.SERVICEGROUPID equals o.SERVICEGROUPID
                                      //  join q in _context.obj_Price_Rate_Master on p.PRICERATEID equals q.PRICERATEID
                                      join r in _context.obj_Offer_Master on p.OFFERID equals r.OFFERID
                                      where p.USERID == intUserId & p.ISACTIVE == true
                                      //  group by p.SLOTBOOKINGID
                                      orderby p.SLOTBOOKINGDATETIME descending
                                      select new Models.SlotBookingMasterViewModel
                                      {
                                          a = p,
                                          b = k
                                      //, c = m
                                      ,
                                          d = n
                                      //, f = o
                                      ,
                                          g = r
                                          //, h = s
                                      };
                var data = viewmodelResult.ToList();
                //for promo code
                var offerperuserregistration = _context.obj_Offer_Master.Where(m => m.REGISTRATIONTYPEID == intRegistrationTypeId && m.USERID== intUserId).Select(s => s.OFFERID).ToList();
                var viewmodelResult1 = from p in _context.obj_SLOT_BOOKING_MASTER
                                          //  join s in _context.obj_Slot_Booking_Details on p.SLOTBOOKINGID equals s.SLOTBOOKINGID
                                      join k in _context.obj_SLOT_MASTER on p.SLOTID equals k.SLOTID
                                      // join m in _context.obj_SERVICE_MASTER on s.SERVICETYPEID equals m.SERVICEID
                                      join n in _context.obj_PATIENT_MASTER on p.PATIENTID equals n.PATIENTID
                                      //  join o in _context.obj_SERVICE_GROUP_MASTER on s.SERVICEGROUPID equals o.SERVICEGROUPID
                                      //  join q in _context.obj_Price_Rate_Master on p.PRICERATEID equals q.PRICERATEID
                                      join r in _context.obj_Offer_Master on p.OFFERID equals r.OFFERID
                                      where offerperuserregistration.Contains(p.OFFERID) & p.ISACTIVE == true
                                       //  group by p.SLOTBOOKINGID
                                       orderby p.SLOTBOOKINGDATETIME descending
                                      select new Models.SlotBookingMasterViewModel
                                      {
                                          a = p,
                                          b = k
                                      //, c = m
                                      ,
                                          d = n
                                      //, f = o
                                      ,
                                          g = r
                                          //, h = s
                                      };
               
                ViewBag.SlotBookingWithPromoCode = viewmodelResult1.ToList();
                //end for promo code

                return View(data);
                // return View(await _context.obj_SLOT_BOOKING_MASTER.Where(m => m.USERID == intUserId).OrderByDescending(m => m.SLOTBOOKINGID).ToListAsync());
            }
            else if (HttpContext.Session.GetString("RegistrationTypeId") == "9")
            {
                int intRegistrationTypeId = Convert.ToInt32(HttpContext.Session.GetString("RegistrationTypeId"));
                int intUserId = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
                var viewmodelResult = from p in _context.obj_SLOT_BOOKING_MASTER
                                          //  join s in _context.obj_Slot_Booking_Details on p.SLOTBOOKINGID equals s.SLOTBOOKINGID
                                      join k in _context.obj_SLOT_MASTER on p.SLOTID equals k.SLOTID
                                      // join m in _context.obj_SERVICE_MASTER on s.SERVICETYPEID equals m.SERVICEID
                                      join n in _context.obj_PATIENT_MASTER on p.PATIENTID equals n.PATIENTID
                                      //  join o in _context.obj_SERVICE_GROUP_MASTER on s.SERVICEGROUPID equals o.SERVICEGROUPID
                                      //  join q in _context.obj_Price_Rate_Master on p.PRICERATEID equals q.PRICERATEID
                                      join r in _context.obj_Offer_Master on p.OFFERID equals r.OFFERID
                                      where p.USERID == intUserId & p.ISACTIVE == true
                                      //  group by p.SLOTBOOKINGID
                                      orderby p.SLOTBOOKINGDATETIME descending
                                      select new Models.SlotBookingMasterViewModel
                                      {
                                          a = p,
                                          b = k
                                      //, c = m
                                      ,
                                          d = n
                                      //, f = o
                                      ,
                                          g = r
                                          //, h = s
                                      };
                var data = viewmodelResult.ToList();
                //for promo code
                var offerperuserregistration = _context.obj_Offer_Master.Where(m => m.REGISTRATIONTYPEID == intRegistrationTypeId && m.USERID == intUserId).Select(s => s.OFFERID).ToList();
                var viewmodelResult1 = from p in _context.obj_SLOT_BOOKING_MASTER
                                           //  join s in _context.obj_Slot_Booking_Details on p.SLOTBOOKINGID equals s.SLOTBOOKINGID
                                       join k in _context.obj_SLOT_MASTER on p.SLOTID equals k.SLOTID
                                       // join m in _context.obj_SERVICE_MASTER on s.SERVICETYPEID equals m.SERVICEID
                                       join n in _context.obj_PATIENT_MASTER on p.PATIENTID equals n.PATIENTID
                                       //  join o in _context.obj_SERVICE_GROUP_MASTER on s.SERVICEGROUPID equals o.SERVICEGROUPID
                                       //  join q in _context.obj_Price_Rate_Master on p.PRICERATEID equals q.PRICERATEID
                                       join r in _context.obj_Offer_Master on p.OFFERID equals r.OFFERID
                                       where offerperuserregistration.Contains(p.OFFERID) & p.ISACTIVE == true
                                       //  group by p.SLOTBOOKINGID
                                       orderby p.SLOTBOOKINGDATETIME descending
                                       select new Models.SlotBookingMasterViewModel
                                       {
                                           a = p,
                                           b = k
                                       //, c = m
                                       ,
                                           d = n
                                       //, f = o
                                       ,
                                           g = r
                                           //, h = s
                                       };

                ViewBag.SlotBookingWithPromoCode = viewmodelResult1.ToList();
                //end for promo code

                return View(data);
                // return View(await _context.obj_SLOT_BOOKING_MASTER.Where(m => m.USERID == intUserId).OrderByDescending(m => m.SLOTBOOKINGID).ToListAsync());
            }
            else
            {
                var viewmodelResult = from p in _context.obj_SLOT_BOOKING_MASTER
                                     // join s in _context.obj_Slot_Booking_Details on p.SLOTBOOKINGID equals s.SLOTBOOKINGID
                                      join k in _context.obj_SLOT_MASTER on p.SLOTID equals k.SLOTID
                                     // join m in _context.obj_SERVICE_MASTER on p.SERVICETYPEID equals m.SERVICEID
                                      join n in _context.obj_PATIENT_MASTER on p.PATIENTID equals n.PATIENTID
                                     // join o in _context.obj_SERVICE_GROUP_MASTER on p.SERVICEGROUPID equals o.SERVICEGROUPID
                                      //join q in _context.obj_Price_Rate_Master on p.PRICERATEID equals q.PRICERATEID
                                      join r in _context.obj_Offer_Master on p.OFFERID equals r.OFFERID
                                      join t in _context.obj_SERVICE_PROVIDER_MASTER on p.SERVICEPROVIDERID equals t.SERVICEPROVIDERID
                                      into dept
                                      from department in dept.DefaultIfEmpty()
                                      where (p.STATUS == "Alloted"  || p.STATUS == "Booked" || p.STATUS == "Received" || p.STATUS == "Files Uploaded")
                                      //|| p.STATUS == null || (p.PAYMENTMETHOD=="ONLINE" && p.PAYMENTSTATUS== "Payment Successful")
                                      orderby p.SLOTBOOKINGDATETIME descending
                                      select new Models.SlotBookingMasterViewModel { a = p, b = k
                                      //, c = m
                                      , d = n
                                      //, e = q
                                      //, f = o
                                      , g = r
                                      ,i=department
                                      //,h=s 
                                      };
                var data = viewmodelResult.ToList();
                return View(data);
                // return View(await _context.obj_SLOT_BOOKING_MASTER.OrderByDescending(m => m.SLOTBOOKINGID).ToListAsync());
            }

        }

        // GET: SlotBookingMaster/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            ViewBag.SLOTBOOKINGID = id;
            if (HttpContext.Session.GetString("fullname") == null)
            {

                // TempData["PaymentDone"] = "Payment successfull";
                return RedirectToAction("Details", "SlotBookingMaster", new { id = id });
            }
            else
            {

                var class_SLOT_BOOKING_MASTER = await _context.obj_SLOT_BOOKING_MASTER
                   .FirstOrDefaultAsync(m => m.SLOTBOOKINGID == id);

                var PatientList = _context.obj_PATIENT_MASTER.Where(s => s.PATIENTID == class_SLOT_BOOKING_MASTER.PATIENTID).FirstOrDefault();
                ViewBag.PATIENTNAME = PatientList.PATIENTNAME;
                ViewBag.PATIENTID = PatientList.PATIENTID;
                // ViewBag.PATIENTNAME = PatientList.PATIENTNAME;
                ViewBag.AGE = PatientList.AGE;
                ViewBag.WEIGHT = PatientList.WEIGHT;
                ViewBag.ADDRESS = PatientList.ADDRESS;
                ViewBag.GENDER = PatientList.GENDER;
                ViewBag.ALTERNATEMOBILENUMBER = PatientList.ALTERNATEMOBILENUMBER;
                ViewBag.EMAIL = PatientList.EMAIL;
                ViewBag.SLOTNAME = _context.obj_SLOT_MASTER.Where(s => s.SLOTID == class_SLOT_BOOKING_MASTER.SLOTID).Select(s => s.SLOTNAME).FirstOrDefault();
                ViewBag.SERVICENAME = _context.obj_SERVICE_MASTER.Where(s => s.SERVICEID == class_SLOT_BOOKING_MASTER.SERVICETYPEID).Select(s => s.SERVICENAME).FirstOrDefault();
                ViewBag.SERVICEGROUPNAME = _context.obj_SERVICE_GROUP_MASTER.Where(s => s.SERVICEGROUPID == class_SLOT_BOOKING_MASTER.SERVICEGROUPID).Select(s => s.SERVICEGROUPNAME).FirstOrDefault();
                ViewBag.MyURL = "\\prescriptionfiles\\" + class_SLOT_BOOKING_MASTER.PRESCRIPTIONFILENAME + class_SLOT_BOOKING_MASTER.EXTENSION;
                ViewBag.MyReportURL = "\\reportfiles\\" + class_SLOT_BOOKING_MASTER.REPORTFILENAME + class_SLOT_BOOKING_MASTER.REPORTEXTENSION;
                ViewBag.MyServiceURL = "\\servicefiles\\" + class_SLOT_BOOKING_MASTER.SERVICEFILENAME + class_SLOT_BOOKING_MASTER.SERVICEEXTENSION;
                ViewBag.GetPaymentMethod = GetPaymentMethod(id);
                ViewBag.RedirectURL = "https://xraidigital.com/SlotBookingMaster/Details/" + id;
                ViewBag.OrderID = "XRAI/" + class_SLOT_BOOKING_MASTER.SLOTBOOKINGDATETIME.Month + "/" + id;
                ViewBag.FileName = class_SLOT_BOOKING_MASTER.PRESCRIPTIONFILENAME + class_SLOT_BOOKING_MASTER.EXTENSION;
                //   TempData["PaymentDone"]="Payment successfull";
                if (id == null)
                {
                    return NotFound();
                }


                if (class_SLOT_BOOKING_MASTER == null)
                {
                    return NotFound();
                }
                IEnumerable<SelectListItem> SERVICES = _context.obj_SERVICE_MASTER.Select(c => new SelectListItem
                {

                    Value = c.SERVICEID.ToString(),
                    Text = c.SERVICENAME

                });
                ViewBag.SERVICES = SERVICES;
                IEnumerable<SelectListItem> GROUPSERVICES = _context.obj_SERVICE_GROUP_MASTER.Select(c => new SelectListItem
                {

                    Value = c.SERVICEGROUPID.ToString(),
                    Text = c.SERVICEGROUPNAME

                });
                ViewBag.GROUPSERVICES = GROUPSERVICES;

                var viewmodelResult = from p in _context.obj_SLOT_BOOKING_MASTER
                                      join s in _context.obj_Slot_Booking_Details on p.SLOTBOOKINGID equals s.SLOTBOOKINGID
                                     // join k in _context.obj_SLOT_MASTER on p.SLOTID equals k.SLOTID
                                      join m in _context.obj_SERVICE_MASTER on s.SERVICETYPEID equals m.SERVICEID
                                     // join n in _context.obj_PATIENT_MASTER on p.PATIENTID equals n.PATIENTID
                                      join o in _context.obj_SERVICE_GROUP_MASTER on s.SERVICEGROUPID equals o.SERVICEGROUPID
                                      //join q in _context.obj_Price_Rate_Master on s.PRICERATEID equals q.PRICERATEID
                                      //join r in _context.obj_Offer_Master on p.OFFERID equals r.OFFERID

                                      where p.SLOTBOOKINGID == id
                                      orderby p.SLOTBOOKINGID descending
                                      select new Models.SlotBookingMasterViewModel { a = p
                                      , c = m
                                      ,  f = o
                                      , h = s
                                          /*, b = k, d = n,g = r*/
                                      };
                var data = viewmodelResult.ToList();
                int? AMOUNTPAYABLE = 0;
                int? OFFERAMOUNT = 0;
                int TOTAL = _context.obj_Slot_Booking_Details.Where(m => m.SLOTBOOKINGID == id).Sum(m => m.PRICE);
                int? OFFERID = _context.obj_SLOT_BOOKING_MASTER.Where(m => m.SLOTBOOKINGID == id).Select(m => m.OFFERID).FirstOrDefault();
                int? OFFERDISCOUNT = _context.obj_Offer_Master.Where(m => m.OFFERID == OFFERID).Select(m => m.OFFERDISCOUNT).FirstOrDefault();
                OFFERAMOUNT = TOTAL * OFFERDISCOUNT / 100;

                AMOUNTPAYABLE = TOTAL - (TOTAL * OFFERDISCOUNT / 100);
                ViewBag.OFFERAMOUNT = OFFERAMOUNT;
                ViewBag.TOTAL = TOTAL;
                ViewBag.AMOUNTPAYABLE = AMOUNTPAYABLE;
                ViewBag.OFFERDISCOUNT = OFFERDISCOUNT;
                ViewBag.SLOTBOOKINGDETAILS = data;
                var viewmodelResult1 = from p in _context.obj_Slot_Booking_Details
                                      join s in _context.obj_SERVICE_FILES_DETAILS on p.SLOTBOOKINGDETID equals s.SLOTBOOKINGDETID
                                     

                                      where p.SLOTBOOKINGDETID == id
                                      orderby p.SLOTBOOKINGID descending
                                      select new Models.ServiceFilesDetailsViewModel { a = p, b = s };
                var dataServiceFiles = viewmodelResult1.ToList();
                ViewBag.SERVICESLOTBOOKINGDETAILS = dataServiceFiles;
                return View(class_SLOT_BOOKING_MASTER);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Details(int id, [Bind("SLOTBOOKINGID,SLOTID,SLOTTYPEID,VISITTYPEID,SLOTBOOKINGDATETIME,SERVICETYPEID,SERVICEPROVIDERID,PHONENUMBER,PATIENTNAME,AGE,WEIGHT,GENDER,ADDRESS,ALTERNATEMOBILENUMBER,EMAIL,CREATEDDATE,SERVICEGROUPID,PAYMENTMETHOD,PRESCRIPTIONFILENAME,EXTENSION,PATIENTID,USERID,SERVICEFILENAME,REPORTFILENAME,SERVICEEXTENSION,REPORTEXTENSION,PRICERATEID")] Class_SLOT_BOOKING_MASTER class_SLOT_BOOKING_MASTER)
        {
            //   return View();
            var PRICERATEID = _context.obj_SLOT_BOOKING_MASTER.Where(m => m.SLOTBOOKINGID == id).Select(m => m.PRICERATEID).FirstOrDefault();
            var PRICE = _context.obj_Price_Rate_Master.Where(m => m.PRICERATEID == PRICERATEID).Select(m => m.PRICE).FirstOrDefault();
            var queryParameter = new CCACrypto();
            TempData["PaymentSuccessFull"] = "Payment Successful";
            return View("CcAvenue", new CcAvenueViewModel(queryParameter.Encrypt(BuildCcAvenueRequestParameters(id.ToString(), "1"), "E78BC2C9FDDD81E137CB8D0A5137F1F4"), "AVMC08ID14CF41CMFC", "https://secure.ccavenue.com/transaction.do?command=initiateTransaction"));
            //return View("CcAvenue", new CcAvenueViewModel(queryParameter.Encrypt(BuildCcAvenueRequestParameters("123456","1"), "E78BC2C9FDDD81E137CB8D0A5137F1F4"), "AVMC08ID14CF41CMFC", "https://test.ccavenue.com/transaction/transaction.do?command=initiateTransaction"));
        }

        // GET: SlotBookingMaster/Create
        public IActionResult Create(int id, int UserId)
        {
            ViewBag.UserId = HttpContext.Session.GetString("UserId");
            ViewBag.RegistrationTypeId = HttpContext.Session.GetString("RegistrationTypeId");

            var phonenumber = _context.obj_PROJECT_USER_MASTER.Where(m => m.useridsno == UserId).Select(m => m.mobile).FirstOrDefault();
            ViewBag.phonenumber = phonenumber;
            //ViewBag.CountPATIENTNAMESFORPHONENUMBER = db.obj_SLOT_BOOKING_MASTER.Where(s => s.PHONENUMBER == phonenumber).Count();
            //IEnumerable<SelectListItem> PATIENTNAMESFORPHONENUMBER = db.obj_SLOT_BOOKING_MASTER.Where(s=>s.PHONENUMBER==phonenumber).Select(c => new SelectListItem
            //{

            //    Value = c.PATIENTNAME,
            //    Text = c.PATIENTNAME

            //});
            //ViewBag.PATIENTNAMESFORPHONENUMBER = PATIENTNAMESFORPHONENUMBER;
            var PatientList = _context.obj_PATIENT_MASTER.Where(s => s.PATIENTID == id).FirstOrDefault();
            ViewBag.obj = new Class_PATIENT_MASTER() { PATIENTNAME = PatientList.PATIENTNAME };
            ViewBag.PATIENTNAME = PatientList.PATIENTNAME;
            ViewBag.PATIENTID = PatientList.PATIENTID;
            // ViewBag.PATIENTNAME = PatientList.PATIENTNAME;
            ViewBag.AGE = PatientList.AGE;
            ViewBag.WEIGHT = PatientList.WEIGHT;
            ViewBag.ADDRESS = PatientList.ADDRESS;
            ViewBag.PIN = PatientList.PIN;
            ViewBag.GENDER = PatientList.GENDER;
            ViewBag.ALTERNATEMOBILENUMBER = PatientList.ALTERNATEMOBILENUMBER;
            ViewBag.EMAIL = PatientList.EMAIL;



            var selectGenderList = new List<SelectListItem>();
            selectGenderList.Add(new SelectListItem
            {

                Text = "MALE",
                Value = "MALE"

            }
                );
            selectGenderList.Add(new SelectListItem
            {

                Text = "FEMALE",
                Value = "FEMALE"

            }
                );
            selectGenderList.Add(new SelectListItem
            {

                Text = "OTHERS",
                Value = "OTHERS"

            }
    );

            ViewBag.GENDERS = selectGenderList;

            var selectPaymentMethod = new List<SelectListItem>();
            selectPaymentMethod.Add(new SelectListItem
            {

                Text = "ONLINE",
                Value = "ONLINE"

            }
                );
            selectPaymentMethod.Add(new SelectListItem
            {

                Text = "PAY AT HOME",
                Value = "HOME PAYMENT"

            }
                );

            if (HttpContext.Session.GetString("RegistrationTypeId") == "5") 
            {
                selectPaymentMethod.Add(new SelectListItem
                {

                    Text = "PAY TO CORPORATE",
                    Value = "PAY TO CORPORATE"

                }
                );
            }

            ViewBag.PAYMENTMETHOD = selectPaymentMethod;
            IEnumerable<SelectListItem> SLOTS = _context.obj_SLOT_MASTER.Select(c => new SelectListItem
            {

                Value = c.SLOTID.ToString(),
                Text = c.SLOTNAME

            });
            ViewBag.SLOTS = SLOTS;
            IEnumerable<SelectListItem> VISITTYPES = _context.obj_VISIT_TYPE_MASTER.Select(c => new SelectListItem
            {

                Value = c.VISITTYPEID.ToString(),
                Text = c.VISITTYPENAME

            });
            ViewBag.VISITTYPES = VISITTYPES;
            IEnumerable<SelectListItem> SERVICES = _context.obj_SERVICE_MASTER.Select(c => new SelectListItem
            {

                Value = c.SERVICEID.ToString(),
                Text = c.SERVICENAME

            });
            ViewBag.SERVICES = SERVICES;
            IEnumerable<SelectListItem> GROUPSERVICES = _context.obj_SERVICE_GROUP_MASTER.Select(c => new SelectListItem
            {

                Value = c.SERVICEGROUPID.ToString(),
                Text = c.SERVICEGROUPNAME

            });
            ViewBag.GROUPSERVICES = GROUPSERVICES;
            //IEnumerable<SelectListItem> SLOTTYPES = _context.obj_SLOT_TYPE_MASTER.Select(c => new SelectListItem
            //{

            //    Value = c.SLOTTYPEID.ToString(),
            //    Text = c.SLOTTYPENAME

            //});
            //ViewBag.SLOTTYPES = SLOTTYPES;

            //Fordate slots
            //IEnumerable<SelectListItem> SLOTS1 = db.obj_SLOT_BOOKING_MASTER.Select(c => new SelectListItem
            //{

            //    Value = c.SLOTID.ToString(),
            //    Text = c.SLOTNAME

            //});
            ViewBag.SLOTS = SLOTS;

            //fordate slots
            IEnumerable<SelectListItem> VISITORTYPES = _context.obj_VISIT_TYPE_MASTER.Select(c => new SelectListItem
            {

                Value = c.VISITTYPEID.ToString(),
                Text = c.VISITTYPENAME

            });
            ViewBag.VISITORTYPES = VISITORTYPES;
            IEnumerable<SelectListItem> OFFERS = _context.obj_Offer_Master.Where(s => s.ISACTIVE == true).Select(c => new SelectListItem
            {

                Value = c.OFFERID.ToString(),
                Text = c.OFFERNAME

            });
            ViewBag.OFFERS = OFFERS;

            IEnumerable<SelectListItem> LOCATIONS = _context.obj_Location_Master.Select(c => new SelectListItem
            {

                Value = c.LOCATIONID.ToString(),
                Text = c.LOCATIONNAME

            });
            ViewBag.LOCATIONS = LOCATIONS;
            IEnumerable<SelectListItem> OFFEROPTIONS = _context.obj_Offer_Option_Master.Select(c => new SelectListItem
            {

                Value = c.OFFEROPTIONID.ToString(),
                Text = c.OFFEROPTION

            });
            ViewBag.OFFEROPTIONS = OFFEROPTIONS;
            return View();
        }

        // POST: SlotBookingMaster/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Class_SLOT_BOOKING_MASTER class_SLOT_BOOKING_MASTER, IFormFile file, IFormCollection iFormCollection)
        {

            string filename;
            // string filenamewithext = "";
            string dataImage = iFormCollection["dataImage"];
            string txtslotbookingdetails = iFormCollection["txtslotbookingdetails"];
            dynamic instagramDataList = JsonConvert.DeserializeObject(txtslotbookingdetails);
            
            string extension = "";
            if (file == null || file.Length == 0)
            {
                if (class_SLOT_BOOKING_MASTER.UPLOADORCLICKPHOTO == "CLICKPHOTO")
                {
                    filename = DateTime.Now.ToString("dd-MM-yy-hh-mm-ss");
                    byte[] imageBytes = Convert.FromBase64String(dataImage.Split(',')[1]);

                    extension = ".jpg";
                    //Save the Byte Array as Image File.
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot//prescriptionfiles", filename + extension);
                    //  string filePath = Server.MapPath(string.Format("~/prescriptionfiles/{0}.jpg", fileName));
                    System.IO.File.WriteAllBytes(filePath, imageBytes);

                    // filename = HttpContext.Session.GetString("FileName");
                    //                    (string)TempData["FileName"];
                    // extension = HttpContext.Session.GetString("Extension");

                    //(string)TempData["Extension"];
                }
                else
                {
                    filename = "";
                    extension = "";
                }
            }
            else
            {



                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot//prescriptionfiles", file.FileName);
                //if (path != null)
                //    return Content("path not selected" + path);
                using (var stream = new FileStream(path, FileMode.Create))
                {

                    await file.CopyToAsync(stream);
                }
                filename = Path.GetFileNameWithoutExtension(path);
                extension = Path.GetExtension(path);
            }

            class_SLOT_BOOKING_MASTER.PRESCRIPTIONFILENAME = filename;
            class_SLOT_BOOKING_MASTER.EXTENSION = extension;
            if (class_SLOT_BOOKING_MASTER.PAYMENTMETHOD == "ONLINE") { }
            else
            {
                class_SLOT_BOOKING_MASTER.STATUS = "Booked";
            }
            class_SLOT_BOOKING_MASTER.CREATEDDATE = DateTime.Now;
            if (class_SLOT_BOOKING_MASTER.OFFERID == null)
            {
                class_SLOT_BOOKING_MASTER.OFFERID = 2;

            }
            class_SLOT_BOOKING_MASTER.ISACTIVE = true;
            _context.obj_SLOT_BOOKING_MASTER.Add(class_SLOT_BOOKING_MASTER);
            _context.SaveChanges();
            if (class_SLOT_BOOKING_MASTER.PAYMENTMETHOD == "ONLINE")
            {
                TempData["MailSentAfterBooking"] = "You have selected online Please proceed to MAKE PAYMENT";
            }
            else
            {
                TempData["MailSentAfterBooking"] = "Booking is Confirmed";
            }
            await _context.SaveChangesAsync();
            int? OFFERID = _context.obj_SLOT_BOOKING_MASTER.Where(m => m.SLOTBOOKINGID == class_SLOT_BOOKING_MASTER.SLOTBOOKINGID).Select(m => m.OFFERID).FirstOrDefault();
            foreach (var data in instagramDataList)
            {
                int? servicegroupid = 0;
                if (data.SERVICEGROUPID == "") { }

                else
                {
                    servicegroupid = data.SERVICEGROUPID;
                }
                int? servicetypeid = 0;
                servicetypeid = data.SERVICETYPEID;
                int? PRICE = 0;
                PRICE = data.PRICE;
                Class_Slot_Booking_Details class_SLOT_BOOKING_DETAILS = new Class_Slot_Booking_Details();
                class_SLOT_BOOKING_DETAILS.SLOTBOOKINGID = Convert.ToInt32(class_SLOT_BOOKING_MASTER.SLOTBOOKINGID);
                // class_SLOT_BOOKING_DETAILS.SLOTBOOKINGID = class_SLOT_BOOKING_MASTER.SLOTID;
                //class_SLOT_BOOKING_DETAILS.SERVICEGROUPID = Convert.ToInt32(class_SLOT_BOOKING_MASTER.SERVICEGROUPID);
                class_SLOT_BOOKING_DETAILS.SERVICEGROUPID = Convert.ToInt32(servicegroupid);
                //class_SLOT_BOOKING_DETAILS.SERVICETYPEID = Convert.ToInt32(class_SLOT_BOOKING_MASTER.SERVICETYPEID);
                class_SLOT_BOOKING_DETAILS.SERVICETYPEID = Convert.ToInt32(servicetypeid);
                class_SLOT_BOOKING_DETAILS.PRICE = Convert.ToInt32(PRICE);
                _context.obj_Slot_Booking_Details.Add(class_SLOT_BOOKING_DETAILS);
                _context.SaveChanges();
            }
            return RedirectToAction("Details", "SlotBookingMaster", new { id = class_SLOT_BOOKING_MASTER.SLOTBOOKINGID });

            // return View(class_SLOT_BOOKING_MASTER);
        }

        // GET: SlotBookingMaster/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var UserId = _context.obj_SLOT_BOOKING_MASTER.Where(m => m.SLOTBOOKINGID == id).Select(m => m.USERID).FirstOrDefault();
            var PatientId = _context.obj_SLOT_BOOKING_MASTER.Where(m => m.SLOTBOOKINGID == id).Select(m => m.PATIENTID).FirstOrDefault();
            var PAYMENTMET = _context.obj_SLOT_BOOKING_MASTER.Where(m => m.SLOTBOOKINGID == id).Select(m => m.PAYMENTMETHOD).FirstOrDefault();
            var phonenumber = _context.obj_PROJECT_USER_MASTER.Where(m => m.useridsno == UserId).Select(m => m.mobile).FirstOrDefault();
            ViewBag.PAYMENTMET = PAYMENTMET;
            ViewBag.phonenumber = phonenumber;
            var PatientList = _context.obj_PATIENT_MASTER.Where(s => s.PATIENTID == PatientId).FirstOrDefault();
            ViewBag.PATIENTNAME = PatientList.PATIENTNAME;
            ViewBag.PATIENTID = PatientList.PATIENTID;
            // ViewBag.PATIENTNAME = PatientList.PATIENTNAME;
            ViewBag.AGE = PatientList.AGE;
            ViewBag.WEIGHT = PatientList.WEIGHT;
            ViewBag.ADDRESS = PatientList.ADDRESS;
            ViewBag.GENDER = PatientList.GENDER;
            ViewBag.ALTERNATEMOBILENUMBER = PatientList.ALTERNATEMOBILENUMBER;
            ViewBag.EMAIL = PatientList.EMAIL;
            var PRESCRIPTIONFILENAME = _context.obj_SLOT_BOOKING_MASTER.Where(m => m.SLOTBOOKINGID == id).Select(m => m.PRESCRIPTIONFILENAME).FirstOrDefault();
            var EXTENSION = _context.obj_SLOT_BOOKING_MASTER.Where(m => m.SLOTBOOKINGID == id).Select(m => m.EXTENSION).FirstOrDefault();

            ViewBag.MyURL = "\\prescriptionfiles\\" + PRESCRIPTIONFILENAME + EXTENSION;
            var selectGenderList = new List<SelectListItem>();
            selectGenderList.Add(new SelectListItem
            {

                Text = "MALE",
                Value = "MALE"

            }
                );
            selectGenderList.Add(new SelectListItem
            {

                Text = "FEMALE",
                Value = "FEMALE"

            }
                );

            ViewBag.GENDERS = selectGenderList;

            var selectPaymentMethod = new List<SelectListItem>();
            selectPaymentMethod.Add(new SelectListItem
            {

                Text = "ONLINE",
                Value = "ONLINE"

            }
                );
            selectPaymentMethod.Add(new SelectListItem
            {

                Text = "HOME PAYMENT",
                Value = "HOME PAYMENT"

            }
                );

            ViewBag.PAYMENTMETHOD = selectPaymentMethod;
            IEnumerable<SelectListItem> SLOTS = _context.obj_SLOT_MASTER.Select(c => new SelectListItem
            {

                Value = c.SLOTID.ToString(),
                Text = c.SLOTNAME

            });
            ViewBag.SLOTS = SLOTS;
            IEnumerable<SelectListItem> VISITTYPES = _context.obj_VISIT_TYPE_MASTER.Select(c => new SelectListItem
            {

                Value = c.VISITTYPEID.ToString(),
                Text = c.VISITTYPENAME

            });
            ViewBag.VISITTYPES = VISITTYPES;
            IEnumerable<SelectListItem> SERVICES = _context.obj_SERVICE_MASTER.Select(c => new SelectListItem
            {

                Value = c.SERVICEID.ToString(),
                Text = c.SERVICENAME

            });
            ViewBag.SERVICES = SERVICES;
            IEnumerable<SelectListItem> GROUPSERVICES = _context.obj_SERVICE_GROUP_MASTER.Select(c => new SelectListItem
            {

                Value = c.SERVICEGROUPID.ToString(),
                Text = c.SERVICEGROUPNAME

            });
            ViewBag.GROUPSERVICES = GROUPSERVICES;
            var projects = _context.obj_SLOT_BOOKING_MASTER.Where(m => m.STATUS == "Alloted" || m.STATUS == "Reached" || m.STATUS == "Received").Select(s => s.SERVICEPROVIDERID).ToList();
            // at this point, projects should already be filtered with "customerId"
            IEnumerable<SelectListItem> SERVICEPROVIDERS = _context.obj_SERVICE_PROVIDER_MASTER.Select(c => new SelectListItem
            //IEnumerable <SelectListItem> SERVICEPROVIDERS  = _context.obj_SERVICE_PROVIDER_MASTER.Where(p => !projects.Contains(p.SERVICEPROVIDERID)).Select(c => new SelectListItem
            {

                Value = c.SERVICEPROVIDERID.ToString(),
                Text = c.SERVICEPROVIDERNAME

            });
            //IEnumerable<SelectListItem> SERVICEPROVIDERS = _context.obj_SERVICE_PROVIDER_MASTER.Select(c => new SelectListItem
            //{

            //    Value = c.SERVICEPROVIDERID.ToString(),
            //    Text = c.SERVICEPROVIDERNAME

            //});
            //ViewBag.SERVICEPROVIDERS = SERVICEPROVIDERS;
            ViewBag.SERVICEPROVIDERS = SERVICEPROVIDERS;

            var projectsMachines = _context.obj_SLOT_BOOKING_MASTER.Where(m => m.STATUS == "Alloted" || m.STATUS == "Reached" || m.STATUS == "Received").Select(s => s.MACHINEID).ToList();
            // at this point, projects should already be filtered with "customerId"
            IEnumerable<SelectListItem> MACHINES = _context.obj_Machine_Master.Where(p => !projectsMachines.Contains(p.MACHINEID)).Select(c => new SelectListItem
            {

                Value = c.MACHINEID.ToString(),
                Text = c.MACHINENAME

            });
            ViewBag.MACHINES = MACHINES;
            //IEnumerable<SelectListItem> SLOTTYPES = _context.obj_SLOT_TYPE_MASTER.Select(c => new SelectListItem
            //{

            //    Value = c.SLOTTYPEID.ToString(),
            //    Text = c.SLOTTYPENAME

            //});
            //ViewBag.SLOTTYPES = SLOTTYPES;

            //Fordate slots
            //IEnumerable<SelectListItem> SLOTS1 = db.obj_SLOT_BOOKING_MASTER.Select(c => new SelectListItem
            //{

            //    Value = c.SLOTID.ToString(),
            //    Text = c.SLOTNAME

            //});
            ViewBag.SLOTS = SLOTS;


            if (id == null)
            {
                return NotFound();
            }

            var class_SLOT_BOOKING_MASTER = await _context.obj_SLOT_BOOKING_MASTER.FindAsync(id);
            if (class_SLOT_BOOKING_MASTER == null)
            {
                return NotFound();
            }
            var PAYMENTMETHOD = _context.obj_SLOT_BOOKING_MASTER.Where(m => m.SLOTBOOKINGID == id).Select(m => m.PAYMENTMETHOD).FirstOrDefault();
            ViewBag.PAYMENTMETHODS = PAYMENTMETHOD;
            var SLOTBOOKINGDATETIME = _context.obj_SLOT_BOOKING_MASTER.Where(m => m.SLOTBOOKINGID == id).Select(m => m.SLOTBOOKINGDATETIME).FirstOrDefault();
            ViewBag.SLOTBOOKINGDATETIME = SLOTBOOKINGDATETIME;
            var viewmodelResult = from p in _context.obj_SLOT_BOOKING_MASTER
                                  join s in _context.obj_Slot_Booking_Details on p.SLOTBOOKINGID equals s.SLOTBOOKINGID
                                  join k in _context.obj_SLOT_MASTER on p.SLOTID equals k.SLOTID
                                  join m in _context.obj_SERVICE_MASTER on s.SERVICETYPEID equals m.SERVICEID
                                  join n in _context.obj_PATIENT_MASTER on p.PATIENTID equals n.PATIENTID
                                  join o in _context.obj_SERVICE_GROUP_MASTER on s.SERVICEGROUPID equals o.SERVICEGROUPID
                                  //join q in _context.obj_Price_Rate_Master on s.PRICERATEID equals q.PRICERATEID
                                  join r in _context.obj_Offer_Master on p.OFFERID equals r.OFFERID

                                  where p.SLOTBOOKINGID == id
                                  orderby p.SLOTBOOKINGID descending
                                  select new Models.SlotBookingMasterViewModel { a = p, b = k, c = m, d = n, f = o, g = r, h = s };
            var data = viewmodelResult.ToList();
            ViewBag.SLOTBOOKINGDETAILS = data;
            return View(class_SLOT_BOOKING_MASTER);
        }

        // POST: SlotBookingMaster/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SLOTBOOKINGID,SLOTID,SLOTTYPEID,VISITTYPEID,SLOTBOOKINGDATETIME,SERVICETYPEID,SERVICEPROVIDERID,PHONENUMBER,PATIENTNAME,AGE,WEIGHT,GENDER,ADDRESS,ALTERNATEMOBILENUMBER,EMAIL,CREATEDDATE,SERVICEGROUPID,PAYMENTMETHOD,PRESCRIPTIONFILENAME,EXTENSION,PATIENTID,USERID,SERVICEFILENAME,REPORTFILENAME,SERVICEEXTENSION,REPORTEXTENSION,ISACTIVE")] Class_SLOT_BOOKING_MASTER class_SLOT_BOOKING_MASTER, IFormFile servicefile, IFormFile reportfile, IFormFile prescriptionfile)
        {
            string prescriptionfilename = "";
            string prescriptionextension = "";
            if (HttpContext.Session.GetString("RegistrationTypeId") == "2")
            {
                if (id != class_SLOT_BOOKING_MASTER.SLOTBOOKINGID)
                {
                    return NotFound();
                }
                int? SERVICEGROUPID = _context.obj_SLOT_BOOKING_MASTER.Where(m => m.SLOTBOOKINGID == id).Select(m => m.SERVICEGROUPID).FirstOrDefault();
                string SERVICEGROUPNAME = _context.obj_SERVICE_GROUP_MASTER.Where(m => m.SERVICEGROUPID == id).Select(m => m.SERVICEGROUPNAME).FirstOrDefault();
                ViewBag.SERVICEGROUPNAME = SERVICEGROUPNAME;
                string servicefilename = "";

                //string servicefilenamewithext = "";
                string serviceextension = "";
                string reportfilename = "";
                //string reportfilenamewithext = "";
                string reportextension = "";
                //if (servicefile == null || servicefile.Length == 0)
                //    return Content("file not selected");
                if (prescriptionfile == null || prescriptionfile.Length == 0)
                { }
                else
                {
                    var prescriptionpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot//prescriptionfiles", prescriptionfile.FileName);
                    //if (path != "") { return Content(path); }
                    using (var stream = new FileStream(prescriptionpath, FileMode.Create))
                    {

                        await prescriptionfile.CopyToAsync(stream);
                    }

                    prescriptionfilename = Path.GetFileNameWithoutExtension(prescriptionpath);
                    prescriptionextension = Path.GetExtension(prescriptionpath);
                }
                if (servicefile == null || servicefile.Length == 0)
                { }
                else
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot//servicefiles", servicefile.FileName);
                    //if (path != "") { return Content(path); }
                    using (var stream = new FileStream(path, FileMode.Create))
                    {

                        await servicefile.CopyToAsync(stream);
                    }

                    servicefilename = Path.GetFileNameWithoutExtension(path);
                    serviceextension = Path.GetExtension(path);
                }
                //if (reportfile == null || reportfile.Length == 0)
                //    return Content("file not selected");
                if (reportfile == null || reportfile.Length == 0)
                { }
                else
                {
                    var reportpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot//reportfiles", reportfile.FileName);
                    using (var stream = new FileStream(reportpath, FileMode.Create))
                    {

                        await reportfile.CopyToAsync(stream);
                    }
                    reportfilename = Path.GetFileNameWithoutExtension(reportpath);
                    reportextension = Path.GetExtension(reportpath);
                }

                class_SLOT_BOOKING_MASTER.SERVICEFILENAME = servicefilename;
                class_SLOT_BOOKING_MASTER.SERVICEEXTENSION = serviceextension;
                class_SLOT_BOOKING_MASTER.REPORTFILENAME = reportfilename;
                class_SLOT_BOOKING_MASTER.REPORTEXTENSION = reportextension;
                if (reportfile == null || servicefile == null || reportfile.Length == 0 || servicefile.Length == 0) { }
                else
                {
                    class_SLOT_BOOKING_MASTER.STATUS = "Files Uploaded";
                }

            }
            if (HttpContext.Session.GetString("RegistrationTypeId") == "3")
            {
                //  class_SLOT_BOOKING_MASTER.SERVICEPROVIDERID = class_SLOT_BOOKING_MASTER.SERVICEPROVIDERID;
                string PRESCRIPTIONFILENAME = _context.obj_SLOT_BOOKING_MASTER.Where(m => m.SLOTBOOKINGID == id).Select(m => m.PRESCRIPTIONFILENAME).FirstOrDefault();
                int? PRICERATEID = _context.obj_SLOT_BOOKING_MASTER.Where(m => m.SLOTBOOKINGID == id).Select(m => m.PRICERATEID).FirstOrDefault();
                int? OFFERID = _context.obj_SLOT_BOOKING_MASTER.Where(m => m.SLOTBOOKINGID == id).Select(m => m.OFFERID).FirstOrDefault();
                DateTime CREATEDDATE = _context.obj_SLOT_BOOKING_MASTER.Where(m => m.SLOTBOOKINGID == id).Select(m => m.CREATEDDATE).FirstOrDefault();
                string EXTENSION = _context.obj_SLOT_BOOKING_MASTER.Where(m => m.SLOTBOOKINGID == id).Select(m => m.EXTENSION).FirstOrDefault();
                class_SLOT_BOOKING_MASTER.PRESCRIPTIONFILENAME = PRESCRIPTIONFILENAME;
                class_SLOT_BOOKING_MASTER.CREATEDDATE = CREATEDDATE;
                class_SLOT_BOOKING_MASTER.PRICERATEID = PRICERATEID;
                class_SLOT_BOOKING_MASTER.OFFERID = OFFERID;
                class_SLOT_BOOKING_MASTER.EXTENSION = EXTENSION;
                string PAYMENTSTATUS = _context.obj_SLOT_BOOKING_MASTER.Where(m => m.SLOTBOOKINGID == id).Select(m => m.PAYMENTSTATUS).FirstOrDefault();
                int? LOCATIONID = _context.obj_SLOT_BOOKING_MASTER.Where(m => m.SLOTBOOKINGID == id).Select(m => m.LOCATIONID).FirstOrDefault();
                class_SLOT_BOOKING_MASTER.PAYMENTSTATUS = PAYMENTSTATUS;
                class_SLOT_BOOKING_MASTER.STATUS = "Alloted";
                class_SLOT_BOOKING_MASTER.LOCATIONID = LOCATIONID;

            }
            else if (HttpContext.Session.GetString("RegistrationTypeId") == "2")
            {

                string PRESCRIPTIONFILENAME = _context.obj_SLOT_BOOKING_MASTER.Where(m => m.SLOTBOOKINGID == id).Select(m => m.PRESCRIPTIONFILENAME).FirstOrDefault();
                int? PRICERATEID = _context.obj_SLOT_BOOKING_MASTER.Where(m => m.SLOTBOOKINGID == id).Select(m => m.PRICERATEID).FirstOrDefault();
                int? OFFERID = _context.obj_SLOT_BOOKING_MASTER.Where(m => m.SLOTBOOKINGID == id).Select(m => m.OFFERID).FirstOrDefault();
                DateTime CREATEDDATE = _context.obj_SLOT_BOOKING_MASTER.Where(m => m.SLOTBOOKINGID == id).Select(m => m.CREATEDDATE).FirstOrDefault();
                string EXTENSION = _context.obj_SLOT_BOOKING_MASTER.Where(m => m.SLOTBOOKINGID == id).Select(m => m.EXTENSION).FirstOrDefault();
                string PAYMENTSTATUS = _context.obj_SLOT_BOOKING_MASTER.Where(m => m.SLOTBOOKINGID == id).Select(m => m.PAYMENTSTATUS).FirstOrDefault();
                string STATUS = _context.obj_SLOT_BOOKING_MASTER.Where(m => m.SLOTBOOKINGID == id).Select(m => m.STATUS).FirstOrDefault();
                int? LOCATIONID = _context.obj_SLOT_BOOKING_MASTER.Where(m => m.SLOTBOOKINGID == id).Select(m => m.LOCATIONID).FirstOrDefault();
                if (PRESCRIPTIONFILENAME == "")
                {
                    class_SLOT_BOOKING_MASTER.PRESCRIPTIONFILENAME = prescriptionfilename;
                    class_SLOT_BOOKING_MASTER.EXTENSION = prescriptionextension;
                    class_SLOT_BOOKING_MASTER.STATUS = STATUS;

                }
                else
                {
                    class_SLOT_BOOKING_MASTER.PRESCRIPTIONFILENAME = PRESCRIPTIONFILENAME;
                    class_SLOT_BOOKING_MASTER.EXTENSION = EXTENSION;
                    class_SLOT_BOOKING_MASTER.STATUS = STATUS;


                }
                if (reportfile == null || servicefile == null || reportfile.Length == 0 || servicefile.Length == 0)
                {
                    class_SLOT_BOOKING_MASTER.STATUS = STATUS;
                }
                else
                {
                    class_SLOT_BOOKING_MASTER.STATUS = "Files Uploaded";
                }

                class_SLOT_BOOKING_MASTER.CREATEDDATE = CREATEDDATE;
                class_SLOT_BOOKING_MASTER.PRICERATEID = PRICERATEID;
                class_SLOT_BOOKING_MASTER.OFFERID = OFFERID;
                class_SLOT_BOOKING_MASTER.LOCATIONID = LOCATIONID;

                class_SLOT_BOOKING_MASTER.PAYMENTSTATUS = PAYMENTSTATUS;


            }
            else if (HttpContext.Session.GetString("RegistrationTypeId") == "1") {
                string PRESCRIPTIONFILENAME = _context.obj_SLOT_BOOKING_MASTER.Where(m => m.SLOTBOOKINGID == id).Select(m => m.PRESCRIPTIONFILENAME).FirstOrDefault();
                int? PRICERATEID = _context.obj_SLOT_BOOKING_MASTER.Where(m => m.SLOTBOOKINGID == id).Select(m => m.PRICERATEID).FirstOrDefault();
                int? OFFERID = _context.obj_SLOT_BOOKING_MASTER.Where(m => m.SLOTBOOKINGID == id).Select(m => m.OFFERID).FirstOrDefault();
                DateTime CREATEDDATE = _context.obj_SLOT_BOOKING_MASTER.Where(m => m.SLOTBOOKINGID == id).Select(m => m.CREATEDDATE).FirstOrDefault();
                string EXTENSION = _context.obj_SLOT_BOOKING_MASTER.Where(m => m.SLOTBOOKINGID == id).Select(m => m.EXTENSION).FirstOrDefault();
                string PAYMENTSTATUS = _context.obj_SLOT_BOOKING_MASTER.Where(m => m.SLOTBOOKINGID == id).Select(m => m.PAYMENTSTATUS).FirstOrDefault();
                string STATUS = _context.obj_SLOT_BOOKING_MASTER.Where(m => m.SLOTBOOKINGID == id).Select(m => m.STATUS).FirstOrDefault();
                int? LOCATIONID = _context.obj_SLOT_BOOKING_MASTER.Where(m => m.SLOTBOOKINGID == id).Select(m => m.LOCATIONID).FirstOrDefault();
                if (PRESCRIPTIONFILENAME == "")
                {
                    class_SLOT_BOOKING_MASTER.PRESCRIPTIONFILENAME = prescriptionfilename;
                    class_SLOT_BOOKING_MASTER.EXTENSION = prescriptionextension;
                    class_SLOT_BOOKING_MASTER.STATUS = STATUS;

                }
                else
                {
                    class_SLOT_BOOKING_MASTER.PRESCRIPTIONFILENAME = PRESCRIPTIONFILENAME;
                    class_SLOT_BOOKING_MASTER.EXTENSION = EXTENSION;
                    class_SLOT_BOOKING_MASTER.STATUS = STATUS;


                }
                if (reportfile == null || servicefile == null || reportfile.Length == 0 || servicefile.Length == 0)
                {
                    class_SLOT_BOOKING_MASTER.STATUS = STATUS;
                }
                else
                {
                   // class_SLOT_BOOKING_MASTER.STATUS = "Files Uploaded";
                }

                class_SLOT_BOOKING_MASTER.CREATEDDATE = CREATEDDATE;
                class_SLOT_BOOKING_MASTER.PRICERATEID = PRICERATEID;
                class_SLOT_BOOKING_MASTER.OFFERID = OFFERID;
                class_SLOT_BOOKING_MASTER.LOCATIONID = LOCATIONID;

                class_SLOT_BOOKING_MASTER.PAYMENTSTATUS = PAYMENTSTATUS;
            }

            //  if (ModelState.IsValid)
            //{

            try
            {
                _context.Update(class_SLOT_BOOKING_MASTER);
                await _context.SaveChangesAsync();
                if (HttpContext.Session.GetString("RegistrationTypeId") == "3")
                {

                    int SERVICEPROVIDERID = _context.obj_SLOT_BOOKING_MASTER.Where(m => m.SLOTBOOKINGID == id).Select(m => m.SERVICEPROVIDERID).FirstOrDefault();
                    int USERID = _context.obj_SERVICE_PROVIDER_MASTER.Where(m => m.SERVICEPROVIDERID == SERVICEPROVIDERID).Select(m => m.USERID).FirstOrDefault();
                    string FULLNAME = _context.obj_SERVICE_PROVIDER_MASTER.Where(m => m.SERVICEPROVIDERID == SERVICEPROVIDERID).Select(m => m.SERVICEPROVIDERNAME).FirstOrDefault();
                    string EMAILID = _context.obj_PROJECT_USER_MASTER.Where(m => m.useridsno == USERID).Select(m => m.email).FirstOrDefault();

                    int port = 587;
                    string host = "smtp.gmail.com";
                    string username = "saisudhakar.paluri@gmail.com";
                    //string username = "info@xraidigital.com";
                    string password = "saisudhakar";
                    //string password = "Xray@home1234";
                    string mailFrom = "saisudhakar.paluri@gmail.com";
                    string mailTo = "info@xraidigital.com";
                    string mailTitle = "XRAI Alloted an X-RAY to perform - Reg";
                    //// string mailMessage = "XRAI Contact Us  - Test";

                    using (SmtpClient client = new SmtpClient())
                    {
                        MailAddress from = new MailAddress(mailFrom);
                        MailMessage message = new MailMessage
                        {
                            From = from
                        };
                        message.To.Add(EMAILID);
                        message.Bcc.Add("saisudhi@yahoo.com");
                        message.Subject = mailTitle;
                        message.Body = "Dear " + FULLNAME + "<br><br>You are Alloted a service. Please login to the <a href='xraidigital.com'>xraidigital.com</a> <br><br>Regards,<br>Admin";
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
                }

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Class_SLOT_BOOKING_MASTERExists(class_SLOT_BOOKING_MASTER.SLOTBOOKINGID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction("Details", "SlotBookingMaster", new { id = id });
            //}
            // return View(class_SLOT_BOOKING_MASTER);
        }

        // GET: SlotBookingMaster/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var class_SLOT_BOOKING_MASTER = await _context.obj_SLOT_BOOKING_MASTER
                .FirstOrDefaultAsync(m => m.SLOTBOOKINGID == id);
            if (class_SLOT_BOOKING_MASTER == null)
            {
                return NotFound();
            }

            return View(class_SLOT_BOOKING_MASTER);
        }

        // POST: SlotBookingMaster/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var class_SLOT_BOOKING_MASTER = await _context.obj_SLOT_BOOKING_MASTER.FindAsync(id);
            _context.obj_SLOT_BOOKING_MASTER.Remove(class_SLOT_BOOKING_MASTER);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Class_SLOT_BOOKING_MASTERExists(int id)
        {
            return _context.obj_SLOT_BOOKING_MASTER.Any(e => e.SLOTBOOKINGID == id);
        }

        public string GetSlotName(int SLOTID)
        {
            string SLOTNAME = _context.obj_SLOT_MASTER.Where(s => s.SLOTID == SLOTID).Select(s => s.SLOTNAME).FirstOrDefault();
            return SLOTNAME;

        }
        public string GetPaymentMethod(int? SLOTBOOKINGID)
        {
            string PaymentMethod = _context.obj_SLOT_BOOKING_MASTER.Where(s => s.SLOTBOOKINGID == SLOTBOOKINGID).Select(s => s.PAYMENTMETHOD).FirstOrDefault();
            return PaymentMethod;

        }
        public ActionResult GetPatientDetailsForPatient(string valueOfDropDown)
        {
            Class_SLOT_BOOKING_MASTER class_SLOT_BOOKING_MASTER = new Class_SLOT_BOOKING_MASTER();
            class_SLOT_BOOKING_MASTER = _context.obj_SLOT_BOOKING_MASTER.Where(s => s.PATIENTNAME == valueOfDropDown).FirstOrDefault();
            return Json(new
            {
                success = true,
                value1 = class_SLOT_BOOKING_MASTER.AGE,
                value2 = class_SLOT_BOOKING_MASTER.WEIGHT,
                value3 = class_SLOT_BOOKING_MASTER.GENDER,
                value4 = class_SLOT_BOOKING_MASTER.ADDRESS,
                value5 = class_SLOT_BOOKING_MASTER.ALTERNATEMOBILENUMBER,
                value6 = class_SLOT_BOOKING_MASTER.EMAIL



            });
        }
        private string BuildCcAvenueRequestParameters(string orderId, string amount)
        {
            int intorderid = Convert.ToInt32(orderId);
            int? PATIENTID = _context.obj_SLOT_BOOKING_MASTER.Where(m => m.SLOTBOOKINGID == intorderid).Select(m => m.PATIENTID).FirstOrDefault();
            string PATIENTNAME = _context.obj_PATIENT_MASTER.Where(m => m.PATIENTID == PATIENTID).Select(m => m.PATIENTNAME).FirstOrDefault();
            string PATIENTADDRESS = _context.obj_PATIENT_MASTER.Where(m => m.PATIENTID == PATIENTID).Select(m => m.ADDRESS).FirstOrDefault();
            string ALTERNATEMOBILENUMBER = _context.obj_PATIENT_MASTER.Where(m => m.PATIENTID == PATIENTID).Select(m => m.ALTERNATEMOBILENUMBER).FirstOrDefault();
            string EMAIL = _context.obj_PATIENT_MASTER.Where(m => m.PATIENTID == PATIENTID).Select(m => m.EMAIL).FirstOrDefault();
            string PIN = _context.obj_PATIENT_MASTER.Where(m => m.PATIENTID == PATIENTID).Select(m => m.PIN).FirstOrDefault();
            int? PRICERATEID = _context.obj_SLOT_BOOKING_MASTER.Where(m => m.SLOTBOOKINGID == intorderid).Select(m => m.PRICERATEID).FirstOrDefault();
            int? OFFERID = _context.obj_SLOT_BOOKING_MASTER.Where(m => m.SLOTBOOKINGID == intorderid).Select(m => m.OFFERID).FirstOrDefault();
            //int? PRICE = _context.obj_Price_Rate_Master.Where(m => m.PRICERATEID == PRICERATEID).Select(m => m.PRICE).FirstOrDefault();
            int? OFFERDISCOUNT = _context.obj_Offer_Master.Where(m => m.OFFERID == OFFERID).Select(m => m.OFFERDISCOUNT).FirstOrDefault();
            int? PRICE = _context.obj_Slot_Booking_Details.Where(m => m.SLOTBOOKINGID == intorderid).Sum(m=>m.PRICE);
            int? Amount = PRICE - ((PRICE * OFFERDISCOUNT) / 100);

            var queryParameters = new Dictionary<string, string>
{
{"order_id", orderId},
{"merchant_id", "384342"},
{"amount", Amount.ToString()},
{"currency","INR" },
{"tid",orderId },
{"redirect_url","https://xraidigital.com/SlotBookingMaster/PaymentSuccessful?id=" + intorderid },
{"cancel_url","https://xraidigital.com/SlotBookingMaster/PaymentFailed?id="+ intorderid},
{"request_type","JSON" },
{"response_type","JSON" },
{"version","1.1" },
{"billing_name",PATIENTNAME },
{"billing_address",PATIENTADDRESS },
                {"billing_tel",ALTERNATEMOBILENUMBER },
                {"billing_email",EMAIL },
                 {"billing_zip",PIN }
                
                //add other key value pairs here.
}.Select(item => string.Format("{0}={1}", item.Key, item.Value));
            return string.Join("&", queryParameters);

        }
        public ActionResult ccAvenue()
        {

            return View();
        }
        [HttpPost]
        //public ActionResult PaymentSuccessful(string encResp)
        //{
        //    /*Do not change the encResp.If you change it you will not get any response.
        //    Decode the encResp*/
        //    var decryption = new CCACrypto();
        //    var decryptedParameters = decryption.Decrypt(encResp, "CcAvenueWorkingKey");
        //    /*split the decryptedParameters by & and then by = and save your values*/
        //    return View();
        //}

        public ActionResult PaymentFailed(int? id)
        {
            ViewBag.ID = id;
            return View();
        }
        public ActionResult GetSlotByDate(string dt)
        {// I expect this call to be filtered
         // so I'll leave this to you on how you want this filtered
            DateTime dtSLOTBOOKINGDATETIME = Convert.ToDateTime(dt);
            var projects = _context.obj_SLOT_BOOKING_MASTER.Where(m => m.SLOTBOOKINGDATETIME == dtSLOTBOOKINGDATETIME).Select(s => s.SLOTID).ToList();
            // at this point, projects should already be filtered with "customerId"
            if (HttpContext.Session.GetString("RegistrationTypeId") == "3")
            {
                var shifts = _context.obj_SLOT_MASTER.OrderBy(s => s.ORDERNO).ToList();
                return Json(new SelectList(shifts, "SLOTID", "SLOTNAME"));
            }
            else
            {
                var shifts = _context.obj_SLOT_MASTER.Where(p => !projects.Contains(p.SLOTID)).OrderBy(s => s.ORDERNO).ToList();
                return Json(new SelectList(shifts, "SLOTID", "SLOTNAME"));
            }
               
            //return Json(new SelectList(shifts, "SLOTID", "SLOTNAME"));


        }
        [HttpGet]
        public JsonResult GetServicesByServiceGroup(int dt)
        {// I expect this call to be filtered
         // so I'll leave this to you on how you want this filtered
         // DateTime dtSLOTBOOKINGDATETIME = Convert.ToDateTime(dt);
            var projects = _context.obj_SERVICE_MASTER.Where(m => m.SERVICEGROUPID == dt).ToList();
            // at this point, projects should already be filtered with "customerId"
            //  var shifts = db.obj_SLOT_MASTER.Where(p => !projects.Contains(p.SLOTID)).ToList();
            return Json(new SelectList(projects, "SERVICEID", "SERVICENAME"));


        }

        public ContentResult SaveCapture(string id)
        {
            string fileName = DateTime.Now.ToString("dd-MM-yy hh-mm-ss");
            if (id == "") { }
            else
            {
                //Convert Base64 Encoded string to Byte Array.
                byte[] imageBytes = Convert.FromBase64String(id.Split(',')[1]);


                //Save the Byte Array as Image File.
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot//prescriptionfiles", fileName + ".jpg");
                //  string filePath = Server.MapPath(string.Format("~/prescriptionfiles/{0}.jpg", fileName));
                System.IO.File.WriteAllBytes(filePath, imageBytes);
                HttpContext.Session.SetString("FileName", fileName);
                HttpContext.Session.SetString("Extension", ".jpg");
                // TempData["FileName"] = fileName;
                //TempData["Extension"] = ".jpg";
            }
            return Content("true");
        }

        public ActionResult CaptureImage()
        {
            return View();
        }

        public ActionResult PaymentSuccessful(int? id)
        {
            var class_SLOT_BOOKING_MASTER = new Class_SLOT_BOOKING_MASTER();
            //  int SlotBookingid = Convert.ToInt32(encResp);
            int? SlotBookingid = id;
            class_SLOT_BOOKING_MASTER = _context.obj_SLOT_BOOKING_MASTER.Where(m => m.SLOTBOOKINGID == SlotBookingid).FirstOrDefault();
            class_SLOT_BOOKING_MASTER.PAYMENTSTATUS = "Payment Successful";
            class_SLOT_BOOKING_MASTER.STATUS = "Booked";
            //  class_SLOT_BOOKING_MASTER.STATUS = "Received";
            _context.SaveChanges();
            /*Do not change the encResp.If you change it you will not get any response.
            Decode the encResp*/
            //var decryption = new CCACrypto();
            //var decryptedParameters = decryption.Decrypt(encResp,
            //"your-working-key-from-web.config");

            /*split the decryptedParameters by & and then by = and save your values*/
            ViewBag.ID = id;
            return View();
            // return RedirectToAction("Details", "SlotBookingMaster", new { id = SlotBookingid });
        }
        [HttpPost]
        public IActionResult IndexView()
        {
            HttpContext.Session.GetString("RegistrationTypeId");

            if (HttpContext.Session.GetString("RegistrationTypeId") == "1")
            {

                int intUserId = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
                // recordsTotal = _context.obj_SLOT_BOOKING_MASTER.Where(m => m.USERID == intUserId).OrderByDescending(m => m.SLOTBOOKINGID).Count();
                // recordsTotal = _context.obj_SLOT_BOOKING_MASTER.Where(m => m.USERID == intUserId).OrderByDescending(m => m.SLOTBOOKINGID).Count();
                var customerData = _context.obj_SLOT_BOOKING_MASTER.Where(m => m.USERID == intUserId).OrderByDescending(m => m.SLOTBOOKINGID).ToListAsync();

                //  return View(_context.obj_SLOT_BOOKING_MASTER.Where(m => m.USERID == intUserId).OrderByDescending(m => m.SLOTBOOKINGID).ToListAsync());
                return Json(JsonConvert.SerializeObject(customerData));
            }
            else
            {
                //  recordsTotal = _context.obj_SLOT_BOOKING_MASTER.OrderByDescending(m => m.SLOTBOOKINGID).Count();
                List<Class_SLOT_MASTER> customerData = (from customer in _context.obj_SLOT_MASTER
                                                        select customer).ToList();
                // List<Class_SLOT_BOOKING_MASTER> customerData = _context.obj_SLOT_BOOKING_MASTER.OrderByDescending(m => m.SLOTBOOKINGID).ToList();
                //var data = customerData.Skip(skip).Take(pageSize).ToList();
                // return View(_context.obj_SLOT_BOOKING_MASTER.OrderByDescending(m => m.SLOTBOOKINGID).ToListAsync());
                return Json(JsonConvert.SerializeObject(customerData));
            }

        }

        public JsonResult Allocate(int? id)
        {
            //Class_SLOT_BOOKING_MASTER class_SLOT_BOOKING_MASTER = new Class_SLOT_BOOKING_MASTER();
            string result = "failure";
            Class_SLOT_BOOKING_MASTER class_SLOT_BOOKING_MASTER = _context.obj_SLOT_BOOKING_MASTER.Find(id);
            var projects = _context.obj_SLOT_BOOKING_MASTER.Where(s => s.SERVICEPROVIDERID > 0 && s.STATUS == "Alloted").Select(s => s.SERVICEPROVIDERID).ToList();
            // at this point, projects should already be filtered with "customerId"
            int shifts = _context.obj_SERVICE_PROVIDER_MASTER.Where(p => !projects.Contains(p.SERVICEPROVIDERID)).Select(p => p.SERVICEPROVIDERID).FirstOrDefault();



            if (shifts != 0)
            {
                class_SLOT_BOOKING_MASTER.SERVICEPROVIDERID = shifts;
                class_SLOT_BOOKING_MASTER.STATUS = "Alloted";
                _context.SaveChanges();
                result = "success";
            }
            else
            {
                result = "failure";
            }
            return Json(result);
        }
        public JsonResult Reach(int? id)
        {
            //Class_SLOT_BOOKING_MASTER class_SLOT_BOOKING_MASTER = new Class_SLOT_BOOKING_MASTER();
            string result = "failure";
            Class_SLOT_BOOKING_MASTER class_SLOT_BOOKING_MASTER = _context.obj_SLOT_BOOKING_MASTER.Find(id);
            //var projects = _context.obj_SLOT_BOOKING_MASTER.Where(s => s.SERVICEPROVIDERID > 0).Select(s => s.SERVICEPROVIDERID).ToList();
            // at this point, projects should already be filtered with "customerId"
            //int shifts = _context.obj_SERVICE_PROVIDER_MASTER.Where(p => !projects.Contains(p.SERVICEPROVIDERID)).Select(p => p.SERVICEPROVIDERID).FirstOrDefault();



            //if (shifts != 0)
            //{
            class_SLOT_BOOKING_MASTER.PAYMENTSTATUS = "Payment Successful";
            class_SLOT_BOOKING_MASTER.STATUS = "Received";
            //TempData["MailSentAfterBooking"] = "Booking is Confirmed";
            //// class_SLOT_BOOKING_MASTER.STATUS = "Reached";
            //class_SLOT_BOOKING_MASTER.STATUS = "Alloted";
            _context.SaveChanges();
            result = "success";
            //}
            //else
            //{
            //  result = "failure";
            //}
            // return RedirectToAction("Index", "SlotBookingMaster", new { id = class_SLOT_BOOKING_MASTER.SLOTBOOKINGID });
            return Json(result);
        }
        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformatsofficedocument.spreadsheetml.sheet"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"}
            };
        }
        public async Task<IActionResult> Download(string filename)
        {
            if (filename == null)
                return Content("filename not present");

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "prescriptionfiles", filename);

            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.OpenOrCreate))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, GetContentType(path), Path.GetFileName(path));
        }
        public FileResult DownloadFile(int SLOTBOOKINGID)
        {
            //Build the File Path.
            string fileName = _context.obj_SLOT_BOOKING_MASTER.Where(s => s.SLOTBOOKINGID == SLOTBOOKINGID).Select(s => s.PRESCRIPTIONFILENAME + s.EXTENSION).FirstOrDefault();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\prescriptionfiles", fileName);

            //Read the File data into Byte Array.
            byte[] bytes = System.IO.File.ReadAllBytes(path);

            //Send the File to Download.
            return File(bytes, "application/octet-stream", fileName);
        }
        public IActionResult Invoice(int? id)
        {

            var viewmodelResult = from p in _context.obj_SLOT_BOOKING_MASTER
                                  join k in _context.obj_SLOT_MASTER on p.SLOTID equals k.SLOTID
                                  join n in _context.obj_PATIENT_MASTER on p.PATIENTID equals n.PATIENTID
                                  join r in _context.obj_Offer_Master on p.OFFERID equals r.OFFERID
                                  where p.SLOTBOOKINGID == id
                                  orderby p.SLOTBOOKINGID
                                  select new Models.SlotBookingMasterViewModel { a = p, b = k, d = n, g = r };

            var data = viewmodelResult.ToList();
            //  join m in _context.obj_SERVICE_MASTER on p.SERVICETYPEID equals m.SERVICEID
            //c = m,
           
            //
            //e = q,
            //join q in _context.obj_Price_Rate_Master on p.PRICERATEID equals q.PRICERATEID
            var viewmodelResult1 = from p in _context.obj_SLOT_BOOKING_MASTER
                                  join s in _context.obj_Slot_Booking_Details on p.SLOTBOOKINGID equals s.SLOTBOOKINGID
                                  join k in _context.obj_SLOT_MASTER on p.SLOTID equals k.SLOTID
                                  join m in _context.obj_SERVICE_MASTER on s.SERVICETYPEID equals m.SERVICEID
                                  join n in _context.obj_PATIENT_MASTER on p.PATIENTID equals n.PATIENTID
                                  join o in _context.obj_SERVICE_GROUP_MASTER on s.SERVICEGROUPID equals o.SERVICEGROUPID
                                  //join q in _context.obj_Price_Rate_Master on s.PRICERATEID equals q.PRICERATEID
                                  join r in _context.obj_Offer_Master on p.OFFERID equals r.OFFERID

                                  where p.SLOTBOOKINGID == id
                                  orderby p.SLOTBOOKINGID descending
                                  select new Models.SlotBookingMasterViewModel { a = p, b = k, c = m, d = n, f = o, g = r, h = s };
            var data1 = viewmodelResult1.ToList();
            ViewBag.SLOTBOOKINGDETAILS = data1;
            int? PRICE = _context.obj_Slot_Booking_Details.Where(m => m.SLOTBOOKINGID == id).Sum(m => m.PRICE);
            ViewBag.Price = PRICE;
           // int PRICE = viewmodelResult.Select(m => m.e.PRICE).FirstOrDefault();
            int OFFERDISCOUNT = viewmodelResult.Select(m => m.g.OFFERDISCOUNT).FirstOrDefault();
            ViewBag.OFFERDISCOUNT = OFFERDISCOUNT;
            ViewBag.OFFERDISCOUNTAMT = PRICE * OFFERDISCOUNT / 100;
            double NumToWord = Convert.ToDouble(PRICE - (PRICE * OFFERDISCOUNT / 100));
            ViewBag.AmountAfterDiscount = NumToWord;
            string strNumToWord = ConvertAmount(NumToWord);
            ViewBag.strNumToWord = strNumToWord;
            return View(data);
        }
        public IActionResult SendEmailAfterSlotBooking(string CONTACTUSNAME, string CONTACTUSEMAIL, string CONTACTUSMESSAGE)
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
            int port = 587;
            string host = "smtp.gmail.com";
            string username = "saisudhakar.paluri@gmail.com";
            //string username = "info@xraidigital.com";
            string password = "saisudhakar";
            //string password = "Xray@home1234";
            string mailFrom = "saisudhakar.paluri@gmail.com";
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
                message.Body = "Message from Customer " + CONTACTUSMESSAGE;
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
                client.Send(message);
            }
            return RedirectToAction("Login", "Account");
            //  return RedirectToAction("Index", "Home");
        }
        public ActionResult GetServiceProvidersByFree()
        {// I expect this call to be filtered
         // so I'll leave this to you on how you want this filtered
         //DateTime dtSLOTBOOKINGDATETIME = Convert.ToDateTime(dt);
            var projects = _context.obj_SLOT_BOOKING_MASTER.Where(m => m.STATUS == "Booked" || m.STATUS == "Alloted" || m.STATUS == "Reached" || m.STATUS == "Received").Select(s => s.SERVICEPROVIDERID).ToList();
            // at this point, projects should already be filtered with "customerId"
            var shifts = _context.obj_SERVICE_PROVIDER_MASTER.Where(p => !projects.Contains(p.SERVICEPROVIDERID)).ToList();
            return Json(new SelectList(shifts, "SERVICEPROVIDERID", "SERVICEPROVIDERNAME"));


        }

        public ActionResult GetMachinesByFree(int? id)
        {// I expect this call to be filtered
         // so I'll leave this to you on how you want this filtered
         //DateTime dtSLOTBOOKINGDATETIME = Convert.ToDateTime(dt);
            var projects = _context.obj_SLOT_BOOKING_MASTER.Where(m => (m.STATUS == "Booked" || m.STATUS == "Alloted" || m.STATUS == "Reached" || m.STATUS == "Received") && m.SERVICEGROUPID == id).Select(s => s.MACHINEID).ToList();
            // at this point, projects should already be filtered with "customerId"
            var shifts = _context.obj_Machine_Master.Where(p => !projects.Contains(p.MACHINEID)).ToList();
            return Json(new SelectList(shifts, "MACHINEID", "MACHINENAME"));


        }

        public int GetOfferPriceOnOffer(int? id)
        {// I expect this call to be filtered
         // so I'll leave this to you on how you want this filtered
         //DateTime dtSLOTBOOKINGDATETIME = Convert.ToDateTime(dt);
            int projects = _context.obj_Offer_Master.Where(m => m.OFFERID == id).Select(s => s.OFFERDISCOUNT).FirstOrDefault();
            // at this point, projects should already be filtered with "customerId"
            //           var shifts = _context.obj_Machine_Master.Where(p => !projects.Contains(p.MACHINEID)).ToList();
            return projects;


        }

        //public string GetTens(string tenstext)
        //{
        //    string GT;
        //    GT = ""; // null out the temporary function value
        //    if (Conversion.Val(Strings.Left(tenstext, 1)) == 1)
        //    {
        //        switch (Conversion.Val(tenstext))
        //        {
        //            case 10:
        //                {
        //                    GT = "Ten"; // 
        //                    break;
        //                }

        //            case 11:
        //                {
        //                    GT = "Eleven"; // 
        //                    break;
        //                }

        //            case 12:
        //                {
        //                    GT = "Twelve"; // 
        //                    break;
        //                }

        //            case 13:
        //                {
        //                    GT = "Thirteen"; // Retrieve numeric word
        //                    break;
        //                }

        //            case 14:
        //                {
        //                    GT = "Fourteen"; // value if between ten and
        //                    break;
        //                }

        //            case 15:
        //                {
        //                    GT = "Fifteen"; // nineteen inclusive.
        //                    break;
        //                }

        //            case 16:
        //                {
        //                    GT = "Sixteen"; // 
        //                    break;
        //                }

        //            case 17:
        //                {
        //                    GT = "Seventeen"; // 
        //                    break;
        //                }

        //            case 18:
        //                {
        //                    GT = "Eighteen"; // 
        //                    break;
        //                }

        //            case 19:
        //                {
        //                    GT = "Nineteen"; // 
        //                    break;
        //                }

        //            default:
        //                {
        //                    break;
        //                }
        //        }
        //    }
        //    else
        //    {
        //        switch (Conversion.Val(Strings.Left(tenstext, 1)))
        //        {
        //            case 2:
        //                {
        //                    GT = "Twenty "; // 
        //                    break;
        //                }

        //            case 3:
        //                {
        //                    GT = "Thirty "; // 
        //                    break;
        //                }

        //            case 4:
        //                {
        //                    GT = "Forty "; // 
        //                    break;
        //                }

        //            case 5:
        //                {
        //                    GT = "Fifty "; // Retrieve value if it is
        //                    break;
        //                }

        //            case 6:
        //                {
        //                    GT = "Sixty "; // divisible by ten
        //                    break;
        //                }

        //            case 7:
        //                {
        //                    GT = "Seventy "; // excluding the value ten.
        //                    break;
        //                }

        //            case 8:
        //                {
        //                    GT = "Eighty "; // 
        //                    break;
        //                }

        //            case 9:
        //                {
        //                    GT = "Ninety "; // 
        //                    break;
        //                }

        //            default:
        //                {
        //                    break;
        //                }
        //        }
        //        GT = GT + GetDigit(Strings.Right(tenstext, 1)); // Retrieve ones place
        //    }

        //    return (GT); // Assign function return value.
        //}
        // =======================================
        // The following function converts a number from 0 to 999 to text
        // =======================================
        //public string GetWord(string NumText)
        //{
        //    string GW;
        //    int x;
        //    GW = ""; // null out temporary function value
        //    if (Conversion.Val(NumText) > 0)
        //    {
        //        for (x = 1; x <= Strings.Len(NumText); x++) // loop the length of NumText times
        //        {
        //            switch (Strings.Len(NumText))
        //            {
        //                case 3:
        //                    {
        //                        if (Conversion.Val(NumText) > 99)
        //                            GW = GetDigit(Strings.Left(NumText, 1)) + " Hundred ";
        //                        NumText = Strings.Right(NumText, 2);
        //                        break;
        //                    }

        //                case 2:
        //                    {
        //                        GW = GW + GetTens(NumText);
        //                        NumText = "";
        //                        break;
        //                    }

        //                case 1:
        //                    {
        //                        GW = GetDigit(NumText);
        //                        break;
        //                    }

        //                default:
        //                    {
        //                        break;
        //                    }
        //            }
        //        }
        //    }
        //    return (GW); // assign function return value
        //}

        //public string GetDigit(string Digit)
        //{
        //    switch (Conversion.Val(Digit))
        //    {
        //        case 1:
        //            {
        //                return ("One"); // 
        //            }

        //        case 2:
        //            {
        //                return ("Two"); // 
        //            }

        //        case 3:
        //            {
        //                return ("Three"); // 
        //            }

        //        case 4:
        //            {
        //                return ("Four"); // Assign a numeric word value
        //            }

        //        case 5:
        //            {
        //                return ("Five"); // based on a single digit.
        //            }

        //        case 6:
        //            {
        //                return ("Six"); // 
        //            }

        //        case 7:
        //            {
        //                return ("Seven"); // 
        //            }

        //        case 8:
        //            {
        //                return ("Eight"); // 
        //            }

        //        case 9:
        //            {
        //                return ("Nine"); // 
        //            }

        //        default:
        //            {
        //                return (""); // 
        //            }
        //    }
        //} // End function GetDigit - return to calling program

        private static String[] units = { "Zero", "One", "Two", "Three",
    "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven",
    "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen",
    "Seventeen", "Eighteen", "Nineteen" };
        private static String[] tens = { "", "", "Twenty", "Thirty", "Forty",
    "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };
        public static String ConvertAmount(double amount)
        {
            try
            {
                Int64 amount_int = (Int64)amount;
                Int64 amount_dec = (Int64)Math.Round((amount - (double)(amount_int)) * 100);
                if (amount_dec == 0)
                {
                    return ConvertNum(amount_int) + " Only.";
                }
                else
                {
                    return ConvertNum(amount_int) + " Point " + ConvertNum(amount_dec) + " Only.";
                }
            }
            catch (Exception e)
            {
                // TODO: handle exception  
            }
            return "";
        }

        public static String ConvertNum(Int64 i)
        {
            if (i < 20)
            {
                return units[i];
            }
            if (i < 100)
            {
                return tens[i / 10] + ((i % 10 > 0) ? " " + ConvertNum(i % 10) : "");
            }
            if (i < 1000)
            {
                return units[i / 100] + " Hundred"
                        + ((i % 100 > 0) ? " And " + ConvertNum(i % 100) : "");
            }
            if (i < 100000)
            {
                return ConvertNum(i / 1000) + " Thousand "
                + ((i % 1000 > 0) ? " " + ConvertNum(i % 1000) : "");
            }
            if (i < 10000000)
            {
                return ConvertNum(i / 100000) + " Lakh "
                        + ((i % 100000 > 0) ? " " + ConvertNum(i % 100000) : "");
            }
            if (i < 1000000000)
            {
                return ConvertNum(i / 10000000) + " Crore "
                        + ((i % 10000000 > 0) ? " " + ConvertNum(i % 10000000) : "");
            }
            return ConvertNum(i / 1000000000) + " Arab "
                    + ((i % 1000000000 > 0) ? " " + ConvertNum(i % 1000000000) : "");
        }

        public IActionResult AddService(int SLOTBOOKINGID, int SERVICEGROUPID, int SERVICETYPEID)
        {
            Class_Slot_Booking_Details class_Slot_Booking_Details = new Class_Slot_Booking_Details();
            class_Slot_Booking_Details.SLOTBOOKINGID = SLOTBOOKINGID;
            class_Slot_Booking_Details.SERVICEGROUPID = SERVICEGROUPID;
            class_Slot_Booking_Details.SERVICETYPEID = SERVICETYPEID;
            int? AMOUNTPAYABLE = 0;
            int servicescount = _context.obj_Slot_Booking_Details.Where(m => m.SLOTBOOKINGID == SLOTBOOKINGID && m.SERVICEGROUPID == SERVICEGROUPID).Count();
            int? PRICE = _context.obj_Price_Rate_Master.Where(m => m.SERVICEGROUPID == SERVICEGROUPID && m.SERVICEID == SERVICETYPEID).Select(m => m.PRICE).FirstOrDefault();
            if (SERVICEGROUPID == 1)
            {

                if (servicescount == 1)
                {
                    PRICE = PRICE - (PRICE * 50 / 100);

                }
                else if (servicescount >= 2)

                {
                    PRICE = PRICE - (PRICE * 75 / 100);
                }

            }
            else
            {

            }


            class_Slot_Booking_Details.PRICE = Convert.ToInt32(PRICE);

            _context.obj_Slot_Booking_Details.Add(class_Slot_Booking_Details);
            _context.SaveChanges();
            // HttpContext.Session.SetString("fullname", fullname);
            //  TempData["fullname"] = fullname;
            //   int userid = _context.obj_PROJECT_USER_MASTER.Where(m => m.mobile == pROJECT_USER_MASTER.mobile).Select(m => m.useridsno).FirstOrDefault();

            return RedirectToAction("Details", "SlotBookingMaster", new { id = SLOTBOOKINGID });
            //  return RedirectToAction("Index", "Home");
        }




        //public string NumToWord(string numval)
        //{
        //    string NTW, NText, dollars, cents, NWord, totalcents;
        //    int decplace, TotalSets, cnt, LDollHold;
        //    string[] NumParts = new string[10]; // Array for Amount (sets of three)
        //    string[] Place = new string[10]; // Array containing place holders
        //    int LDoll; // Length of the Dollars Text Amount
        //    Place[2] = " Thousand "; // 
        //    Place[3] = " Lakh "; // Place holder names for money
        //    Place[4] = " Crore "; // amounts
        //    Place[5] = " Trillion "; // 
        //    NTW = ""; // Temp value for the function
        //    NText = numval; // Roundup the cents to eliminate cents gr 2
        //    NText = Strings.Trim(Conversion.Str(NText)); // String representation of amount
        //    decplace = Strings.InStr(Strings.Trim(NText), "."); // Position of decimal 0 if none
        //    dollars = Strings.Trim(Strings.Left(NText, Interaction.IIf(decplace == 0, Strings.Len(numval), decplace - 1)));
        //    LDoll = Strings.Len(dollars);
        //    cents = Strings.Trim(Strings.Right(NText, Interaction.IIf(decplace == 0, 0, Math.Abs(decplace - Strings.Len(NText)))));

        //    if (Strings.Len(cents) == 1)
        //        cents = cents + "0";
        //    if (LDoll > 3)
        //    {
        //        TotalSets = 1;
        //        if ((LDoll - 3 % 2) == 0)
        //            TotalSets = TotalSets + ((LDoll - 3) / 2);
        //        else
        //            TotalSets = TotalSets + ((LDoll - 3) / 2) + 1;
        //    }
        //    else
        //        TotalSets = 1;

        //    cnt = 1;
        //    LDollHold = LDoll;
        //    while (LDoll > 0)
        //    {
        //        if (cnt == 1)
        //        {
        //            NumParts[cnt] = Interaction.IIf(LDoll > 3, Strings.Right(dollars, 3), Strings.Trim(dollars));
        //            dollars = Interaction.IIf(LDoll > 3, Strings.Left(dollars, (Interaction.IIf(LDoll < 3, 3, LDoll)) - 3), "");
        //            LDoll = Strings.Len(dollars);
        //            cnt = cnt + 1;
        //        }
        //        else
        //        {
        //            NumParts[cnt] = Interaction.IIf(LDoll > 2, Strings.Right(dollars, 2), Strings.Trim(dollars));
        //            dollars = Interaction.IIf(LDoll > 2, Strings.Left(dollars, (Interaction.IIf(LDoll < 2, 2, LDoll)) - 2), "");
        //            LDoll = Strings.Len(dollars);
        //            cnt = cnt + 1;
        //        }
        //    }
        //    for (cnt = TotalSets; cnt >= 1; cnt += -1) // step through NumParts array
        //    {
        //        NWord = GetWord(NumParts[cnt]); // convert 1 element of NumParts
        //        NTW = NTW + NWord; // concatenate it to temp variable
        //        if (NWord != "")
        //            NTW = NTW + Place[cnt];
        //    } // loop through
        //    if (LDollHold > 0)
        //    {
        //        if (Strings.Len(cents) > 0)
        //            NTW = NTW + " and "; // concatenate text
        //        else
        //            NTW = NTW;// concatenate text
        //    }
        //    else
        //        NTW = NTW;// concatenate text
        //    totalcents = GetTens(cents); // Convert cents part to word
        //    if (totalcents == "")
        //    {
        //    }
        //    else
        //        NTW = NTW + totalcents;// Concat Dollars and Cents
        //    return (NTW); // Assign word value to    function
        //}

        public JsonResult InsertCustomers(string customers)
        {
            string result = "";

            return Json(result);
        }
        public ActionResult GetOffersByOption(int? offeroptionid)
        {// I expect this call to be filtered
         // so I'll leave this to you on how you want this filtered
         // DateTime dtSLOTBOOKINGDATETIME = Convert.ToDateTime(dt);
            int RegistrationTypeId = Convert.ToInt32(HttpContext.Session.GetString("RegistrationTypeId"));
            int UserID = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
            //var projects;
            if (RegistrationTypeId == 1) {
                var projects = _context.obj_Offer_Master.Where(m => m.OFFEROPTIONID == offeroptionid  && m.REGISTRATIONTYPEID == RegistrationTypeId).ToList();
                return Json(new SelectList(projects, "OFFERID", "OFFERNAME"));
            }
            else { 
         var projects = _context.obj_Offer_Master.Where(m => m.OFFEROPTIONID == offeroptionid && m.USERID== UserID && m.REGISTRATIONTYPEID== RegistrationTypeId).ToList();
                return Json(new SelectList(projects, "OFFERID", "OFFERNAME"));
            }
            // at this point, projects should already be filtered with "customerId"
            //  var shifts = db.obj_SLOT_MASTER.Where(p => !projects.Contains(p.SLOTID)).ToList();
            


        }

        public ActionResult GetOfferIdPercentage(string OFFERCODE)
        {// I expect this call to be filtered
         // so I'll leave this to you on how you want this filtered
         // DateTime dtSLOTBOOKINGDATETIME = Convert.ToDateTime(dt);
            int RegistrationTypeId = Convert.ToInt32(HttpContext.Session.GetString("RegistrationTypeId"));
            int UserID = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
            //var projects;
            if (RegistrationTypeId == 1 || RegistrationTypeId == 3 )
            {
                var projects = _context.obj_Offer_Master.Where(m => m.OFFERNAME == OFFERCODE && DateTime.Now.Date >= m.EFFECTIVEDATEFROM.Date && DateTime.Now.Date <= m.EFFECTIVEDATETO.Date).ToList();
                return Json(new SelectList(projects, "OFFERID", "OFFERDISCOUNT"));
            }
            else
            {
                var projects = _context.obj_Offer_Master.Where(m => m.OFFERNAME == OFFERCODE && m.USERID==UserID && m.REGISTRATIONTYPEID == RegistrationTypeId  && DateTime.Now.Date>=m.EFFECTIVEDATEFROM.Date && DateTime.Now.Date<=m.EFFECTIVEDATETO.Date).ToList();
                return Json(new SelectList(projects, "OFFERID", "OFFERDISCOUNT"));

            }
            // at this point, projects should already be filtered with "customerId"
            //  var shifts = db.obj_SLOT_MASTER.Where(p => !projects.Contains(p.SLOTID)).ToList();



        }
        public ActionResult ViewFilesAndInvoice(int slotbookingid)
        {


            var servicefiles = _context.obj_SERVICE_FILES_DETAILS.Where(m => m.SLOTBOOKINGID == slotbookingid).ToList();
            ViewBag.ServiceFiles = servicefiles;
            var reportfiles = _context.obj_REPORT_FILES_DETAILS.Where(m => m.SLOTBOOKINGID == slotbookingid).ToList();
            ViewBag.ReportFiles = reportfiles;
            var prescriptionfile = _context.obj_SLOT_BOOKING_MASTER.Where(m => m.SLOTBOOKINGID == slotbookingid).ToList();
            ViewBag.PrescriptionFile = prescriptionfile;
            //var prescriptionfile = _context.obj_SLOT_BOOKING_MASTER.Where(m => m.SLOTBOOKINGID == slotbookingid).se();
            ViewBag.SlotBookingId = slotbookingid;


            return View();
        }
        public JsonResult IsActive(int? id)
        {
            string result = "failure";
            Class_SLOT_BOOKING_MASTER class_SLOT_BOOKING_MASTER = _context.obj_SLOT_BOOKING_MASTER.Find(id);
            if (class_SLOT_BOOKING_MASTER.ISACTIVE == true)
            {
                result = "failure";
                class_SLOT_BOOKING_MASTER.ISACTIVE = false;
            }
            else
            {
                result = "success";
                class_SLOT_BOOKING_MASTER.ISACTIVE = true;
            }
            _context.SaveChanges();
            
            return Json(result);
        }
    }
}
