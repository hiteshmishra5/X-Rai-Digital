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
    public class RegistrationTypeController : Controller
    {
        private readonly ApplicationDBContext _context;

        public RegistrationTypeController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: RegistrationType
        public async Task<IActionResult> Index()
        {
            return View(await _context.obj_REGISTRATION_TYPE.ToListAsync());
        }

        // GET: RegistrationType/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var class_REGISTRATION_TYPE = await _context.obj_REGISTRATION_TYPE
                .FirstOrDefaultAsync(m => m.registrationtypeid == id);
            if (class_REGISTRATION_TYPE == null)
            {
                return NotFound();
            }

            return View(class_REGISTRATION_TYPE);
        }

        // GET: RegistrationType/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RegistrationType/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("registrationtypeid,registrationtypename,isactive")] Class_REGISTRATION_TYPE class_REGISTRATION_TYPE)
        {
            if (ModelState.IsValid)
            {
                _context.Add(class_REGISTRATION_TYPE);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(class_REGISTRATION_TYPE);
        }

        // GET: RegistrationType/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var class_REGISTRATION_TYPE = await _context.obj_REGISTRATION_TYPE.FindAsync(id);
            if (class_REGISTRATION_TYPE == null)
            {
                return NotFound();
            }
            return View(class_REGISTRATION_TYPE);
        }

        // POST: RegistrationType/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("registrationtypeid,registrationtypename,isactive")] Class_REGISTRATION_TYPE class_REGISTRATION_TYPE)
        {
            if (id != class_REGISTRATION_TYPE.registrationtypeid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(class_REGISTRATION_TYPE);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Class_REGISTRATION_TYPEExists(class_REGISTRATION_TYPE.registrationtypeid))
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
            return View(class_REGISTRATION_TYPE);
        }

        // GET: RegistrationType/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var class_REGISTRATION_TYPE = await _context.obj_REGISTRATION_TYPE
                .FirstOrDefaultAsync(m => m.registrationtypeid == id);
            if (class_REGISTRATION_TYPE == null)
            {
                return NotFound();
            }

            return View(class_REGISTRATION_TYPE);
        }

        // POST: RegistrationType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var class_REGISTRATION_TYPE = await _context.obj_REGISTRATION_TYPE.FindAsync(id);
            _context.obj_REGISTRATION_TYPE.Remove(class_REGISTRATION_TYPE);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Class_REGISTRATION_TYPEExists(int id)
        {
            return _context.obj_REGISTRATION_TYPE.Any(e => e.registrationtypeid == id);
        }
    }
}
