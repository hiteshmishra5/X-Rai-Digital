using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DOTNETCOREEXAMPLE.DataContext;
using DOTNETCOREEXAMPLE.Models;
using System.Web;
using System.Net;
using Newtonsoft.Json.Linq;
using System.Collections.Specialized;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Http;

namespace DOTNETCOREEXAMPLE.Controllers
{
    
    public class AccountController : Controller
    {
        private readonly ApplicationDBContext _context;

        public AccountController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: Account
        public async Task<IActionResult> Index()
        {
            return View(await _context.obj_PROJECT_USER_MASTER.ToListAsync());
        }

        // GET: Account/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var class_PROJECT_USER_MASTER = await _context.obj_PROJECT_USER_MASTER
                .FirstOrDefaultAsync(m => m.useridsno == id);
            if (class_PROJECT_USER_MASTER == null)
            {
                return NotFound();
            }

            return View(class_PROJECT_USER_MASTER);
        }

        // GET: Account/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Account/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("useridsno,username,password,email,userid,mobile,address,address1,city,state,pin,registrationtypeid,fullname,gender")] Class_PROJECT_USER_MASTER class_PROJECT_USER_MASTER)
        {
            if (ModelState.IsValid)
            {
                _context.Add(class_PROJECT_USER_MASTER);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(class_PROJECT_USER_MASTER);
        }

        // GET: Account/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var class_PROJECT_USER_MASTER = await _context.obj_PROJECT_USER_MASTER.FindAsync(id);
            if (class_PROJECT_USER_MASTER == null)
            {
                return NotFound();
            }
            return View(class_PROJECT_USER_MASTER);
        }

        // POST: Account/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("useridsno,username,password,email,userid,mobile,address,address1,city,state,pin,registrationtypeid,fullname,gender")] Class_PROJECT_USER_MASTER class_PROJECT_USER_MASTER)
        {
            if (id != class_PROJECT_USER_MASTER.useridsno)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(class_PROJECT_USER_MASTER);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Class_PROJECT_USER_MASTERExists(class_PROJECT_USER_MASTER.useridsno))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(class_PROJECT_USER_MASTER);
        }

        // GET: Account/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var class_PROJECT_USER_MASTER = await _context.obj_PROJECT_USER_MASTER
                .FirstOrDefaultAsync(m => m.useridsno == id);
            if (class_PROJECT_USER_MASTER == null)
            {
                return NotFound();
            }

            return View(class_PROJECT_USER_MASTER);
        }

        // POST: Account/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var class_PROJECT_USER_MASTER = await _context.obj_PROJECT_USER_MASTER.FindAsync(id);
            _context.obj_PROJECT_USER_MASTER.Remove(class_PROJECT_USER_MASTER);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Class_PROJECT_USER_MASTERExists(int id)
        {
            return _context.obj_PROJECT_USER_MASTER.Any(e => e.useridsno == id);
        }
        public IActionResult Login()
        {
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
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Class_PROJECT_USER_MASTER pROJECT_USER_MASTER, Microsoft.AspNetCore.Http.IFormCollection formCollection)
        {
            string enterotp = formCollection["enterotp"];
            //SMS.APIType = SMSGateway.Site2SMS;
            //SMS.MashapeKey = "b651348e9a3f5fd44ac04a6ce50c3d87c89982947724c0dd2aa9559bd4049671";
            //SMS.Username = "saisudhi@yahoo.com";
            //SMS.Password = "Sarvagnya2015";
            //if (txtRecipientNumber.Text.Trim().IndexOf(",") == -1)
            //{
            //Single SMS
            ////  SMS.SendSms("9160655444", "Sai");
            // }
            //else
            //{
            //Multiple SMS
            //List<string> numbers = txtRecipientNumber.Text.Trim().Split(',').ToList();
            //SMS.SendSms(numbers, txtMessage.Text.Trim());
            //}
            //var obj = db.obj_PROJECT_USER_MASTER.Where(a => a.username.Equals(pROJECT_USER_MASTER.username) && a.password.Equals(pROJECT_USER_MASTER.password)).FirstOrDefault();
            //Session["UserId"] = obj.userid.ToString();
            //Session["UserName"] = obj.username.ToString();
            //return RedirectToAction("Index", "UserHome");
            //var usercount =db.obj_PROJECT_USER_MASTER.Where(m => m.mobile == pROJECT_USER_MASTER.mobile).Count();
            //if (usercount == 0)
            //{
            //    pROJECT_USER_MASTER.registrationtypeid = 1;
            //    db.obj_PROJECT_USER_MASTER.Add(pROJECT_USER_MASTER);
            //    db.SaveChanges();
            //    //return RedirectToAction("Register", "Account");
            //}
            var userid = _context.obj_PROJECT_USER_MASTER.Where(m => m.mobile == pROJECT_USER_MASTER.mobile).Select(m => m.useridsno).FirstOrDefault();
            var registrationtypeid = _context.obj_PROJECT_USER_MASTER.Where(m => m.mobile == pROJECT_USER_MASTER.mobile).Select(m => m.registrationtypeid).FirstOrDefault();
            HttpContext.Session.SetString("UserId", userid.ToString());
            //TempData["UserId"] = userid;
            HttpContext.Session.SetString("mobile", pROJECT_USER_MASTER.mobile);
            HttpContext.Session.SetString("RegistrationTypeId", registrationtypeid.ToString());
            string FullName = "";
             FullName = _context.obj_PROJECT_USER_MASTER.Where(s => s.useridsno == userid).Select(s => s.fullname).FirstOrDefault();
            if (FullName == "") {
                HttpContext.Session.SetString("fullname", FullName);
            }
            else
            {
                HttpContext.Session.SetString("fullname", FullName.ToUpper());
            }
            //  TempData["mobile"] = pROJECT_USER_MASTER.mobile;
            //  TempData["RegistrationTypeId"] = registrationtypeid;


            //  TempData["fullname"] = FullName;
            //return RedirectToAction("Create", "SlotBookingMaster",new { phonenumber=pROJECT_USER_MASTER.mobile});

            //  string currentObject = HttpContext.Session.GetString("CurrentObject");
            string OTPFROMDATABASE = _context.obj_OTP_MASTER.Where(s => s.MOBILE == pROJECT_USER_MASTER.mobile).OrderByDescending(s => s.OTPID).Select(m => m.OTP).FirstOrDefault();
            string MPINFROMDATABASE = _context.obj_PROJECT_USER_MASTER.Where(s => s.mobile == pROJECT_USER_MASTER.mobile).Select(m => m.mpin).FirstOrDefault();
            string mpin = formCollection["mmpin"];
            if (enterotp == OTPFROMDATABASE || MPINFROMDATABASE==mpin)
            {
                 FullName = _context.obj_PROJECT_USER_MASTER.Where(s => s.useridsno == userid).Select(s => s.fullname).FirstOrDefault();
              //  HttpContext.Session.SetString("fullname", FullName);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "PatientMaster", new { id = userid });
            }
            else
            {
                HttpContext.Session.SetString("CurrentObject", "");
                
               // TempData["CurrentObject"] = null;
                TempData["OTPMESSAGE"] = "Entered a wrong OTP Or not entered  Or not enteredMPin";
                await _context.SaveChangesAsync();
                return RedirectToAction("Login", "Account");
            }
            
        }

        public JsonResult SendOTP(string mobile)
        {
            int optValue = new Random().Next(100000, 999999);
            var status = "";
            try
            {
                string recipient = mobile;
                //string recipient = "916302463841";
                //   string recipient = "918700534682";

                string APIKey = "NzRjNjg4NTEyMDM1YTRjMDBiNTFlNmU2NTk3MzE4ODI =";

                string message = "Dear customer, your OTP to login at XRAI digital is " + optValue + ". Thank you!";
                //   string message = "Welcome to Xrai. Your OTP for mobile verification is " + optValue;
                //  string message = "Hi there, thank you for sending your first test message from Textlocal.Get 20 % off today with our code: "+optValue;
                // string message = "Hi there, thank you for sending your first test message from Textlocal. See how you can send effective SMS campaigns here: https://tx.gl/r/2nGVj/";
                string encodedMessage = HttpUtility.UrlEncode(message);
                using (var webClient = new WebClient())
                {

                    byte[] response = webClient.UploadValues("https://api.textlocal.in/send/", new NameValueCollection()
                    {
                        {"username","parthadey@u4rad.com" },
                        {"hash","5b773b47380712054b80507334bd59ee5ddd4db60f36efc231175454e66e700e" },
                        {"apikey" ,APIKey},
                        {"numbers",recipient },
                        {"message",encodedMessage },
                        {"sender","XRAIDG" }

                    });

                    string result = System.Text.Encoding.UTF8.GetString(response);
                    var jsonObject = JObject.Parse(result);
                    status = jsonObject["status"].ToString();
                    Class_OTP_MASTER class_OTP_MASTER = new Class_OTP_MASTER();
                    class_OTP_MASTER.OTP = optValue.ToString();
                    class_OTP_MASTER.MOBILE = mobile;
                    class_OTP_MASTER.OTPDATETIME = DateTime.Now;
                    _context.obj_OTP_MASTER.Add(class_OTP_MASTER);
                    _context.SaveChanges();
                    //  HttpContext.Session.SetString("CurrentObject", optValue.ToString());

                    // TempData["CurrentObject"] = optValue;
                }
                return Json(status);

            }
            catch (Exception ex)
            {
                throw (ex);
                //TXTLCL
            }
        }

        public JsonResult UserExists(string Prefix)
                {
            var cntUser = _context.obj_PROJECT_USER_MASTER.Where(s => s.mobile == Prefix).Select(s=>s.mobile).ToList().Count();
            string strStatus = "";
            //bool boolResult=false;
            if (cntUser == 0)
            {
                strStatus = "failure";

            }
            else
            {
                strStatus = "success";
            }
            return Json(strStatus);
        }

        public JsonResult VerifyOTP(string otp, string mobile,string mpin)
        {
            string result = "";
            string OTPFROMDATABASE = _context.obj_OTP_MASTER.Where(s => s.MOBILE == mobile).OrderByDescending(s => s.OTPID).Select(m => m.OTP).FirstOrDefault();
            string MPINFROMDATABASE = _context.obj_PROJECT_USER_MASTER.Where(s => s.mobile == mobile).Select(m => m.mpin).FirstOrDefault();
            //string sessionOTP = HttpContext.Session.GetString("CurrentObject"); ;
            // if (otp == sessionOTP)
            //   if (otp == "123456")
            if (otp == OTPFROMDATABASE || MPINFROMDATABASE==mpin )
            {
                result = "success";

            }
            else {
                result = "failure";
            }

            return Json(result);
        }
        
        public IActionResult RegisterVisitor(string mobile, string fullname, string gender, string email,string mpin)
        {
            Class_PROJECT_USER_MASTER pROJECT_USER_MASTER = new Class_PROJECT_USER_MASTER();
            pROJECT_USER_MASTER.mobile = mobile;
            pROJECT_USER_MASTER.fullname = fullname;
            pROJECT_USER_MASTER.gender = gender;
            pROJECT_USER_MASTER.email = email;
            pROJECT_USER_MASTER.mpin = mpin;
            pROJECT_USER_MASTER.registrationtypeid = 1;
            _context.obj_PROJECT_USER_MASTER.Add(pROJECT_USER_MASTER);
            _context.SaveChanges();
            HttpContext.Session.SetString("fullname", fullname);
          //  TempData["fullname"] = fullname;
            int userid = _context.obj_PROJECT_USER_MASTER.Where(m => m.mobile == pROJECT_USER_MASTER.mobile).Select(m => m.useridsno).FirstOrDefault();
            
            return RedirectToAction("Login", "Account");
            //  return RedirectToAction("Index", "Home");
        }

        public IActionResult Register()
        {
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
            IEnumerable<SelectListItem> items = _context.obj_REGISTRATION_TYPE.Select(c => new SelectListItem
            {

                Value = c.registrationtypeid.ToString(),
                Text = c.registrationtypename

            });
            Class_REGISTRATION_TYPE class_REGISTRATION_TYPE = new Class_REGISTRATION_TYPE();
            ViewBag.items = items;
          //  class_REGISTRATION_TYPE.GetRegistrationTypeID = items;
            return View();
        }

        [HttpPost]
        
        public async Task<IActionResult> Register(Class_PROJECT_USER_MASTER pROJECT_USER_MASTER)
        {

            _context.obj_PROJECT_USER_MASTER.Add(pROJECT_USER_MASTER);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "ProjectUserMaster");
        }

        [HttpPost]
        public JsonResult LoginAdmin(string username,string password)
        {
            string result = "failure";
            //string username = formCollection["username"];
            //string password = formCollection["password"];
            int userid = _context.obj_PROJECT_USER_MASTER.Where(m => m.username == username && m.password==password).Count();

            if (userid == 1)
            {
                var FullName = _context.obj_PROJECT_USER_MASTER.Where(s => s.username == username).Select(s => s.fullname).FirstOrDefault();

                HttpContext.Session.SetString("fullname", FullName);
             //   Session["fullname"] = FullName;
                var registrationtypeid = _context.obj_PROJECT_USER_MASTER.Where(m => m.username == username).Select(m => m.registrationtypeid).FirstOrDefault();
                HttpContext.Session.SetString("RegistrationTypeId", registrationtypeid.ToString());
                var useridsno = _context.obj_PROJECT_USER_MASTER.Where(m => m.username == username).Select(m => m.useridsno).FirstOrDefault();
                HttpContext.Session.SetString("UserId", useridsno.ToString());
                //               Session["RegistrationTypeId"] = registrationtypeid;
                result = "success";
               

            }
            else
            {
                result = "failure";
                
            }

            return Json(result);

        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            //HttpContext.Session.RemoveAll();
           
            HttpContext.Session.Remove("fullname");
            HttpContext.Session.Remove("UserId");
            HttpContext.Session.Remove("registrationtypeid");

            //            HttpContext.Session.Remove("UserId");
            return RedirectToAction("Login", "Account");
        }

        public JsonResult UpdateMPIN(string id,string mpin)
        {
            //Class_SLOT_BOOKING_MASTER class_SLOT_BOOKING_MASTER = new Class_SLOT_BOOKING_MASTER();
            string result = "failure";
            Class_PROJECT_USER_MASTER class_PROJECT_USER_MASTER = _context.obj_PROJECT_USER_MASTER.Where(s=>s.mobile==id).FirstOrDefault();
            //var projects = _context.obj_SLOT_BOOKING_MASTER.Where(s => s.SERVICEPROVIDERID > 0).Select(s => s.SERVICEPROVIDERID).ToList();
            // at this point, projects should already be filtered with "customerId"
            //int shifts = _context.obj_SERVICE_PROVIDER_MASTER.Where(p => !projects.Contains(p.SERVICEPROVIDERID)).Select(p => p.SERVICEPROVIDERID).FirstOrDefault();



            //if (shifts != 0)
            //{
            class_PROJECT_USER_MASTER.mpin = mpin;
            
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
        [HttpGet]
        public IActionResult ForgetPassword()
        {


            return View();
        }

        [HttpPost]

        public async Task<IActionResult> ForgetPassword(Class_PROJECT_USER_MASTER pROJECT_USER_MASTER)
        {

            if (ModelState.IsValid)
            {
                try
                {
                  //  _context.Update(class_PROJECT_USER_MASTER);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    //if (!Class_PROJECT_USER_MASTERExists(class_PROJECT_USER_MASTER.useridsno))
                    //{
                    //    return NotFound();
                    //}
                    //else
                    //{
                    //    throw;
                    //}
                }
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction("Login", "Account");
        }
        public JsonResult VerifyOTPForForgetPassword(string otp, string mobile, string mpin)
        {
            string result = "";
            string OTPFROMDATABASE = _context.obj_OTP_MASTER.Where(s => s.MOBILE == mobile).OrderByDescending(s => s.OTPID).Select(m => m.OTP).FirstOrDefault();
            string MPINFROMDATABASE = _context.obj_PROJECT_USER_MASTER.Where(s => s.mobile == mobile).Select(m => m.mpin).FirstOrDefault();
            //string sessionOTP = HttpContext.Session.GetString("CurrentObject"); ;
            // if (otp == sessionOTP)
            //   if (otp == "123456")
            if (otp == OTPFROMDATABASE || MPINFROMDATABASE == mpin)
            {
                result = "success";

            }
            else
            {
                result = "failure";
            }

            return Json(result);
        }
    }
    }
