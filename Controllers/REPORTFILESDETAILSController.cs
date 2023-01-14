using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DOTNETCOREEXAMPLE.DataContext;
using DOTNETCOREEXAMPLE.Models;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace DOTNETCOREEXAMPLE.Controllers
{
    public class REPORTFILESDETAILSController : Controller
    {
        private readonly ApplicationDBContext _context;

        public REPORTFILESDETAILSController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: REPORTFILESDETAILS
        public async Task<IActionResult> Index(int id, int slotbookingid)
        {
            return View(await _context.obj_REPORT_FILES_DETAILS.Where(m => m.SLOTBOOKINGDETID == id && m.SLOTBOOKINGID == slotbookingid).ToListAsync());
            
        }
        public async Task<IActionResult> Index1(int id)
        {
            return View(await _context.obj_REPORT_FILES_DETAILS.Where(m => m.SLOTBOOKINGID == id).ToListAsync());
        }

        // GET: REPORTFILESDETAILS/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var class_REPORT_FILES_DETAILS = await _context.obj_REPORT_FILES_DETAILS
                .FirstOrDefaultAsync(m => m.REPORTFILEDETID == id);
            if (class_REPORT_FILES_DETAILS == null)
            {
                return NotFound();
            }

            return View(class_REPORT_FILES_DETAILS);
        }

        // GET: REPORTFILESDETAILS/Create
        public IActionResult Create(int id, int SLOTBOOKINGID)
        {
            ViewBag.SLOTBOOKINGDETID = id;
            ViewBag.SLOTBOOKINGID = SLOTBOOKINGID;
            return View();
        }

        // POST: REPORTFILESDETAILS/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SLOTBOOKINGDETID,REPORTFILENAME,REPORTEXTENSION,ISACTIVE,SLOTBOOKINGID")] Class_REPORT_FILES_DETAILS class_REPORT_FILES_DETAILS, IFormFile reportfile)
        {
            if (ModelState.IsValid)
            {
                string reportfilename = "";

                //string servicefilenamewithext = "";
                string reportextension = "";
                if (reportfile == null || reportfile.Length == 0)
                { }
                else
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot//reportfiles", reportfile.FileName);
                    //if (path != "") { return Content(path); }
                    using (var stream = new FileStream(path, FileMode.Create))
                    {

                        await reportfile.CopyToAsync(stream);
                    }

                    reportfilename = Path.GetFileNameWithoutExtension(path);
                    reportextension = Path.GetExtension(path);
                }
                class_REPORT_FILES_DETAILS.REPORTFILENAME = reportfilename;
                class_REPORT_FILES_DETAILS.REPORTEXTENSION = reportextension;
                class_REPORT_FILES_DETAILS.ISACTIVE = true;
                _context.Add(class_REPORT_FILES_DETAILS);

                _context.Add(class_REPORT_FILES_DETAILS);
                await _context.SaveChangesAsync();

                return RedirectToAction("Edit", "SlotBookingMaster", new { id = class_REPORT_FILES_DETAILS.SLOTBOOKINGID });
            }
            // return View(class_REPORT_FILES_DETAILS);
            return RedirectToAction("Login", "Account");
        }

        // GET: REPORTFILESDETAILS/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var class_REPORT_FILES_DETAILS = await _context.obj_REPORT_FILES_DETAILS.FindAsync(id);
            if (class_REPORT_FILES_DETAILS == null)
            {
                return NotFound();
            }
            return View(class_REPORT_FILES_DETAILS);
        }

        // POST: REPORTFILESDETAILS/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("REPORTFILEDETID,SLOTBOOKINGDETID,REPORTFILENAME,REPORTEXTENSION,ISACTIVE")] Class_REPORT_FILES_DETAILS class_REPORT_FILES_DETAILS)
        {
            if (id != class_REPORT_FILES_DETAILS.REPORTFILEDETID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(class_REPORT_FILES_DETAILS);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Class_REPORT_FILES_DETAILSExists(class_REPORT_FILES_DETAILS.REPORTFILEDETID))
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
            return View(class_REPORT_FILES_DETAILS);
        }

        // GET: REPORTFILESDETAILS/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var class_REPORT_FILES_DETAILS = await _context.obj_REPORT_FILES_DETAILS
                .FirstOrDefaultAsync(m => m.REPORTFILEDETID == id);
            if (class_REPORT_FILES_DETAILS == null)
            {
                return NotFound();
            }

            return View(class_REPORT_FILES_DETAILS);
        }

        // POST: REPORTFILESDETAILS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var class_REPORT_FILES_DETAILS = await _context.obj_REPORT_FILES_DETAILS.FindAsync(id);
            _context.obj_REPORT_FILES_DETAILS.Remove(class_REPORT_FILES_DETAILS);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Class_REPORT_FILES_DETAILSExists(int id)
        {
            return _context.obj_REPORT_FILES_DETAILS.Any(e => e.REPORTFILEDETID == id);
        }
    }
}
