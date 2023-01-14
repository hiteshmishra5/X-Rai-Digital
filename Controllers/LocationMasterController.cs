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
    public class LocationMasterController : Controller
    {
        private readonly ApplicationDBContext _context;

        public LocationMasterController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: LocationMaster
        public async Task<IActionResult> Index()
        {
            return View(await _context.obj_Location_Master.ToListAsync());
        }

        // GET: LocationMaster/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var class_Location_Master = await _context.obj_Location_Master
                .FirstOrDefaultAsync(m => m.LOCATIONID == id);
            if (class_Location_Master == null)
            {
                return NotFound();
            }

            return View(class_Location_Master);
        }

        // GET: LocationMaster/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LocationMaster/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LOCATIONID,LOCATIONNAME,ISACTIVE")] Class_Location_Master class_Location_Master)
        {
            if (ModelState.IsValid)
            {
                _context.Add(class_Location_Master);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(class_Location_Master);
        }

        // GET: LocationMaster/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var class_Location_Master = await _context.obj_Location_Master.FindAsync(id);
            if (class_Location_Master == null)
            {
                return NotFound();
            }
            return View(class_Location_Master);
        }

        // POST: LocationMaster/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LOCATIONID,LOCATIONNAME,ISACTIVE")] Class_Location_Master class_Location_Master)
        {
            if (id != class_Location_Master.LOCATIONID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(class_Location_Master);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Class_Location_MasterExists(class_Location_Master.LOCATIONID))
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
            return View(class_Location_Master);
        }

        // GET: LocationMaster/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var class_Location_Master = await _context.obj_Location_Master
                .FirstOrDefaultAsync(m => m.LOCATIONID == id);
            if (class_Location_Master == null)
            {
                return NotFound();
            }

            return View(class_Location_Master);
        }

        // POST: LocationMaster/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var class_Location_Master = await _context.obj_Location_Master.FindAsync(id);
            _context.obj_Location_Master.Remove(class_Location_Master);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Class_Location_MasterExists(int id)
        {
            return _context.obj_Location_Master.Any(e => e.LOCATIONID == id);
        }
    }
}
