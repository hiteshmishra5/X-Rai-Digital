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


namespace DOTNETCOREEXAMPLE.Controllers
{
    [SessionTimeout]
    public class UserHomeController : Controller
    {

        
        private readonly ApplicationDBContext _context;

        public UserHomeController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: UserHome
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("RegistrationTypeId") == "1")

            {
                int intUserId = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
                int strSlotName = _context.obj_SLOT_BOOKING_MASTER.Where(s => s.USERID == intUserId).Count();
                int strPatients = _context.obj_PATIENT_MASTER.Where(s => s.USERID == intUserId).Count();
                ViewBag.CntSlotBookings = strSlotName;
                ViewBag.CntPatients = strPatients;
            }
            else if (HttpContext.Session.GetString("RegistrationTypeId") == "5")

            {
                int intUserId = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
                int strSlotName = _context.obj_SLOT_BOOKING_MASTER.Where(s => s.USERID == intUserId).Count();
                int strPatients = _context.obj_PATIENT_MASTER.Where(s => s.USERID == intUserId).Count();
                ViewBag.CntSlotBookings = strSlotName;
                ViewBag.CntPatients = strPatients;
            }
            else
            {
                int strSlotName = _context.obj_SLOT_BOOKING_MASTER.Count();
                int strPatients = _context.obj_PATIENT_MASTER.Count();
                ViewBag.CntSlotBookings = strSlotName;
                ViewBag.CntPatients = strPatients;
            }

                return View(await _context.obj_PROJECT_USER_MASTER.ToListAsync());
        }

        // GET: UserHome/Details/5
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

        // GET: UserHome/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UserHome/Create
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

        // GET: UserHome/Edit/5
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

        // POST: UserHome/Edit/5
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

        // GET: UserHome/Delete/5
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

        // POST: UserHome/Delete/5
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
    }
}
