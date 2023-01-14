using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DOTNETCOREEXAMPLE.DataContext;
using DOTNETCOREEXAMPLE.Models;

namespace DOTNETCOREEXAMPLE.Controllers
{
    public class OfferMasterController : Controller
    {
        private readonly ApplicationDBContext _context;

        public OfferMasterController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: OfferMaster
        public async Task<IActionResult> Index()
        {
          //  var optValue = new Random().Next(100000, 999999);
         //   var random = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789".Random(6);
            return View(await _context.obj_Offer_Master.ToListAsync());
        }

        // GET: OfferMaster/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var class_Offer_Master = await _context.obj_Offer_Master
                .FirstOrDefaultAsync(m => m.OFFERID == id);
            if (class_Offer_Master == null)
            {
                return NotFound();
            }

            return View(class_Offer_Master);
        }

        // GET: OfferMaster/Create
        public IActionResult Create()
        {
            IEnumerable<SelectListItem> OFFEROPTIONS = _context.obj_Offer_Option_Master.Select(c => new SelectListItem
            {

                Value = c.OFFEROPTIONID.ToString(),
                Text = c.OFFEROPTION

            });

            ViewBag.OFFEROPTIONS = OFFEROPTIONS;

            IEnumerable<SelectListItem> REGISTRATIONTYPES = _context.obj_REGISTRATION_TYPE.Select(c => new SelectListItem
            {

                Value = c.registrationtypeid.ToString(),
                Text = c.registrationtypename

            });

            ViewBag.REGISTRATIONTYPES = REGISTRATIONTYPES;
            IEnumerable<SelectListItem> users = _context.obj_PROJECT_USER_MASTER.Select(c => new SelectListItem
            {

                Value = c.useridsno.ToString(),
                Text = c.username

            });

            ViewBag.Users = users;
            return View();
        }

        // POST: OfferMaster/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OFFERID,OFFERNAME,OFFERDISCOUNT,ISACTIVE,USERID,REGISTRATIONTYPEID,EFFECTIVEDATEFROM,EFFECTIVEDATETO")] Class_Offer_Master class_Offer_Master)
        {
            if (ModelState.IsValid)
            {
                _context.Add(class_Offer_Master);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(class_Offer_Master);
        }

        // GET: OfferMaster/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var class_Offer_Master = await _context.obj_Offer_Master.FindAsync(id);
            if (class_Offer_Master == null)
            {
                return NotFound();
            }
            return View(class_Offer_Master);
        }

        // POST: OfferMaster/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OFFERID,OFFERNAME,OFFERDISCOUNT,ISACTIVE")] Class_Offer_Master class_Offer_Master)
        {
            if (id != class_Offer_Master.OFFERID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(class_Offer_Master);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    //if (!Class_Offer_MasterExists(class_Offer_Master.OFFERID))
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
            return View(class_Offer_Master);
        }

        // GET: OfferMaster/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var class_Offer_Master = await _context.obj_Offer_Master
                .FirstOrDefaultAsync(m => m.OFFERID == id);
            if (class_Offer_Master == null)
            {
                return NotFound();
            }

            return View(class_Offer_Master);
        }

        // POST: OfferMaster/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var class_Offer_Master = await _context.obj_Offer_Master.FindAsync(id);
            _context.obj_Offer_Master.Remove(class_Offer_Master);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Class_Offer_MasterExists(int id)
        {
            return _context.obj_Offer_Master.Any(e => e.OFFERID == id);
        }
        [HttpGet]
        public JsonResult GetUsersByRegistrationType(int? registrationtypeid)
        {// I expect this call to be filtered
         // so I'll leave this to you on how you want this filtered
         // DateTime dtSLOTBOOKINGDATETIME = Convert.ToDateTime(dt);
            //int RegistrationTypeId = Convert.ToInt32(HttpContext.Session.GetString("RegistrationTypeId"));
           // int UserID = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
            //var projects;
            
                var projects = _context.obj_PROJECT_USER_MASTER.Where(m => m.registrationtypeid == registrationtypeid).ToList();
                return Json(new SelectList(projects, "useridsno", "username"));
            
            // at this point, projects should already be filtered with "customerId"
            //  var shifts = db.obj_SLOT_MASTER.Where(p => !projects.Contains(p.SLOTID)).ToList();



        }
        public JsonResult OfferCodeExists(string Prefix)
        {
            var cntUser = _context.obj_Offer_Master.Where(s => s.OFFERNAME == Prefix).Select(s => s.OFFERID).ToList().Count();
            string strStatus = "";
            //bool boolResult=false;
            if (cntUser == 0)
            {
                strStatus = "success";

            }
            else
            {
                strStatus = "failure";
            }
            return Json(strStatus);
        }
    }
}
