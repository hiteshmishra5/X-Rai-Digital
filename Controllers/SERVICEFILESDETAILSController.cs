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
    public class SERVICEFILESDETAILSController : Controller
    {
        private readonly ApplicationDBContext _context;

        public SERVICEFILESDETAILSController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: SERVICEFILESDETAILS
        public async Task<IActionResult> Index(int id,int slotbookingid)
        {
            return View(await _context.Class_SERVICE_FILES_DETAILS.Where(m=>m.SLOTBOOKINGDETID==id && m.SLOTBOOKINGID== slotbookingid).ToListAsync());
        }
        public async Task<IActionResult> Index1( int slotbookingid)
        {
            return View(await _context.Class_SERVICE_FILES_DETAILS.Where(m=>m.SLOTBOOKINGID == slotbookingid).ToListAsync());
        }

        // GET: SERVICEFILESDETAILS/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var class_SERVICE_FILES_DETAILS = await _context.Class_SERVICE_FILES_DETAILS
                .FirstOrDefaultAsync(m => m.SERVICEFILEDETID == id);
            if (class_SERVICE_FILES_DETAILS == null)
            {
                return NotFound();
            }

            return View(class_SERVICE_FILES_DETAILS);
        }

        // GET: SERVICEFILESDETAILS/Create
        public IActionResult Create(int id,int SLOTBOOKINGID)
        {
            ViewBag.SLOTBOOKINGDETID = id;
            ViewBag.SLOTBOOKINGID = SLOTBOOKINGID;
            return View();
        }

        // POST: SERVICEFILESDETAILS/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SLOTBOOKINGDETID,SERVICEFILENAME,SERVICEEXTENSION,ISACTIVE,SLOTBOOKINGID")] Class_SERVICE_FILES_DETAILS class_SERVICE_FILES_DETAILS, IFormFile servicefile)
        {
            if (ModelState.IsValid)
            {
                string servicefilename = "";

                //string servicefilenamewithext = "";
                string serviceextension = "";
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
                class_SERVICE_FILES_DETAILS.SERVICEFILENAME = servicefilename;
                class_SERVICE_FILES_DETAILS.SERVICEEXTENSION = serviceextension;
                class_SERVICE_FILES_DETAILS.ISACTIVE = true;
                _context.Add(class_SERVICE_FILES_DETAILS);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("Edit","SlotBookingMaster",new { id = class_SERVICE_FILES_DETAILS.SLOTBOOKINGID });
            }
            // return View(class_SERVICE_FILES_DETAILS);
            return RedirectToAction("Login", "Account");
        }

        // GET: SERVICEFILESDETAILS/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var class_SERVICE_FILES_DETAILS = await _context.Class_SERVICE_FILES_DETAILS.FindAsync(id);
            if (class_SERVICE_FILES_DETAILS == null)
            {
                return NotFound();
            }
            return View(class_SERVICE_FILES_DETAILS);
        }

        // POST: SERVICEFILESDETAILS/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SERVICEFIEDETID,SLOTBOOKINGDETID,SERVICEFILENAME,SERVICEEXTENSION,ISACTIVE")] Class_SERVICE_FILES_DETAILS class_SERVICE_FILES_DETAILS)
        {
            if (id != class_SERVICE_FILES_DETAILS.SERVICEFILEDETID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(class_SERVICE_FILES_DETAILS);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Class_SERVICE_FILES_DETAILSExists(class_SERVICE_FILES_DETAILS.SERVICEFILEDETID))
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
            return View(class_SERVICE_FILES_DETAILS);
        }

        // GET: SERVICEFILESDETAILS/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var class_SERVICE_FILES_DETAILS = await _context.Class_SERVICE_FILES_DETAILS
                .FirstOrDefaultAsync(m => m.SERVICEFILEDETID == id);
            if (class_SERVICE_FILES_DETAILS == null)
            {
                return NotFound();
            }

            return View(class_SERVICE_FILES_DETAILS);
        }

        // POST: SERVICEFILESDETAILS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var class_SERVICE_FILES_DETAILS = await _context.Class_SERVICE_FILES_DETAILS.FindAsync(id);
            _context.Class_SERVICE_FILES_DETAILS.Remove(class_SERVICE_FILES_DETAILS);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Class_SERVICE_FILES_DETAILSExists(int id)
        {
            return _context.Class_SERVICE_FILES_DETAILS.Any(e => e.SERVICEFILEDETID == id);
        }
    }
}
