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

namespace DOTNETCOREEXAMPLE.Controllers
{
    [SessionTimeout]
    public class PatientMasterController : Controller
    {
        private readonly ApplicationDBContext _context;

        public PatientMasterController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: PatientMaster
        public async Task<IActionResult> Index(int id)
        {
            if (HttpContext.Session.GetString("RegistrationTypeId") == "1")
            { 
                int cntPatient = _context.obj_PATIENT_MASTER.Where(s => s.USERID == id).Count();
            // return View(_context.obj_PATIENT_MASTER.Where(s => s.USERID == userid).ToList());
            return View(await _context.obj_PATIENT_MASTER.Where(s => s.USERID == id).ToListAsync());
        }
        else   if (HttpContext.Session.GetString("RegistrationTypeId") == "2")
            {
                return View(await _context.obj_PATIENT_MASTER.ToListAsync());
            }
            else if (HttpContext.Session.GetString("RegistrationTypeId") == "5")
            {
                int cntPatient = _context.obj_PATIENT_MASTER.Where(s => s.USERID == id).Count();
                // return View(_context.obj_PATIENT_MASTER.Where(s => s.USERID == userid).ToList());
                return View(await _context.obj_PATIENT_MASTER.Where(s => s.USERID == id).ToListAsync());
            }
           else if (HttpContext.Session.GetString("RegistrationTypeId") == "6")
            {
                int cntPatient = _context.obj_PATIENT_MASTER.Where(s => s.USERID == id).Count();
                // return View(_context.obj_PATIENT_MASTER.Where(s => s.USERID == userid).ToList());
                return View(await _context.obj_PATIENT_MASTER.Where(s => s.USERID == id).ToListAsync());
            }
            else if (HttpContext.Session.GetString("RegistrationTypeId") == "9")
            {
                int cntPatient = _context.obj_PATIENT_MASTER.Where(s => s.USERID == id).Count();
                // return View(_context.obj_PATIENT_MASTER.Where(s => s.USERID == userid).ToList());
                return View(await _context.obj_PATIENT_MASTER.Where(s => s.USERID == id).ToListAsync());
            }
            else
            {

                return View(await _context.obj_PATIENT_MASTER.ToListAsync());
            }

        }

        // GET: PatientMaster/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var class_PATIENT_MASTER = await _context.obj_PATIENT_MASTER
                .FirstOrDefaultAsync(m => m.PATIENTID == id);
            if (class_PATIENT_MASTER == null)
            {
                return NotFound();
            }

            return View(class_PATIENT_MASTER);
        }

        // GET: PatientMaster/Create
        public IActionResult Create()
        {

            int intUserId = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
            ViewBag.UserId= intUserId ;
            var mobile = _context.obj_PROJECT_USER_MASTER.Where(m => m.useridsno == intUserId).Select(m => m.mobile).FirstOrDefault();
            ViewBag.mobile = mobile;
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

            ViewBag.GENDER = selectGenderList;
            return View();
        }

        // POST: PatientMaster/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Class_PATIENT_MASTER class_PATIENT_MASTER)
        {
            // string UserId = HttpContext.Session.GetString("UserId"); ;
            //  if (ModelState.IsValid)
            //{
            double heightInMeterSqr =Math.Pow(Convert.ToDouble(class_PATIENT_MASTER.HEIGHT) / 100,2);
            decimal _bmi = Convert.ToDecimal(class_PATIENT_MASTER.WEIGHT / heightInMeterSqr);
            string strBmr = _bmi.ToString("0.##");
            class_PATIENT_MASTER.BMI = Convert.ToDouble(strBmr);
            _context.Add(class_PATIENT_MASTER);
                await _context.SaveChangesAsync();
              
           //   }
           // return RedirectToAction(nameof(Index));
            return RedirectToAction("Index", "PatientMaster", new { id = class_PATIENT_MASTER.USERID });
        }

        // GET: PatientMaster/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
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

            var class_PATIENT_MASTER = await _context.obj_PATIENT_MASTER.FindAsync(id);
            var mobile = _context.obj_PROJECT_USER_MASTER.Where(m => m.useridsno == class_PATIENT_MASTER.USERID).Select(m => m.mobile).FirstOrDefault();
            ViewBag.mobile = mobile;
            if (class_PATIENT_MASTER == null)
            {
                return NotFound();
            }
            return View(class_PATIENT_MASTER);
        }

        // POST: PatientMaster/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Class_PATIENT_MASTER class_PATIENT_MASTER, IFormFile file)
        {
            string filename="";
            // string filenamewithext = "";
            string extension = "";
            if (file == null || file.Length == 0)
            {

            }
            else
            {
                ///return Content("file not selected");

                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot//patientfiles", file.FileName);
                //if (path != null)
                //    return Content("path not selected" + path);
                using (var stream = new FileStream(path, FileMode.Create))
                {

                    await file.CopyToAsync(stream);
                }
                filename = Path.GetFileNameWithoutExtension(path).ToLowerInvariant();
                extension = Path.GetExtension(path).ToLowerInvariant();

            }
                if (ModelState.IsValid)
                {
                    try
                    {
                        class_PATIENT_MASTER.PATIENTFILENAME = filename;
                        class_PATIENT_MASTER.PATIENTFILEEXTENSION = extension;
                        _context.Update(class_PATIENT_MASTER);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!Class_PATIENT_MASTERExists(class_PATIENT_MASTER.PATIENTID))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                return RedirectToAction("Index", "PatientMaster", new { id = class_PATIENT_MASTER.USERID });
              //  return RedirectToAction(nameof(Index));
                }
            
            return View(class_PATIENT_MASTER);
        }

        // GET: PatientMaster/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var class_PATIENT_MASTER = await _context.obj_PATIENT_MASTER
                .FirstOrDefaultAsync(m => m.PATIENTID == id);
            if (class_PATIENT_MASTER == null)
            {
                return NotFound();
            }

            return View(class_PATIENT_MASTER);
        }

        // POST: PatientMaster/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var class_PATIENT_MASTER = await _context.obj_PATIENT_MASTER.FindAsync(id);
            _context.obj_PATIENT_MASTER.Remove(class_PATIENT_MASTER);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Class_PATIENT_MASTERExists(int id)
        {
            return _context.obj_PATIENT_MASTER.Any(e => e.PATIENTID == id);
        }
        public string GetPatientName(int PATIENTID)
        {
            var strPatientName = _context.obj_PATIENT_MASTER.Where(s => s.PATIENTID == PATIENTID).Select(s => s.PATIENTNAME).FirstOrDefault();
            return strPatientName;
        }
    }
}
