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
    public class MachineMasterController : Controller
    {
        private readonly ApplicationDBContext _context;

        public MachineMasterController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: MachineMaster
        public async Task<IActionResult> Index()
        {
            return View(await _context.obj_Machine_Master.ToListAsync());
        }

        // GET: MachineMaster/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var class_Machine_Master = await _context.obj_Machine_Master
                .FirstOrDefaultAsync(m => m.MACHINEID == id);
            if (class_Machine_Master == null)
            {
                return NotFound();
            }

            return View(class_Machine_Master);
        }

        // GET: MachineMaster/Create
        public IActionResult Create()
        {
            IEnumerable<SelectListItem> GROUPSERVICES = _context.obj_SERVICE_GROUP_MASTER.Select(c => new SelectListItem
            {

                Value = c.SERVICEGROUPID.ToString(),
                Text = c.SERVICEGROUPNAME

            });
            ViewBag.GROUPSERVICES = GROUPSERVICES;
            IEnumerable<SelectListItem> LOCATIONS = _context.obj_Location_Master.Select(c => new SelectListItem
            {

                Value = c.LOCATIONID.ToString(),
                Text = c.LOCATIONNAME

            });
            ViewBag.LOCATIONS = LOCATIONS;
            return View();
        }

        // POST: MachineMaster/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MACHINEID,SERVICEGROUPID,MACHINENAME,ISACTIVE,LOCATIONID")] Class_Machine_Master class_Machine_Master)
        {
            if (ModelState.IsValid)
            {
                _context.Add(class_Machine_Master);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(class_Machine_Master);
        }

        // GET: MachineMaster/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var class_Machine_Master = await _context.obj_Machine_Master.FindAsync(id);
            if (class_Machine_Master == null)
            {
                return NotFound();
            }
            return View(class_Machine_Master);
        }

        // POST: MachineMaster/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MACHINEID,SERVICEGROUPID,MACHINENAME,ISACTIVE,LOCATIONID")] Class_Machine_Master class_Machine_Master)
        {
            if (id != class_Machine_Master.MACHINEID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(class_Machine_Master);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Class_Machine_MasterExists(class_Machine_Master.MACHINEID))
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
            return View(class_Machine_Master);
        }

        // GET: MachineMaster/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var class_Machine_Master = await _context.obj_Machine_Master
                .FirstOrDefaultAsync(m => m.MACHINEID == id);
            if (class_Machine_Master == null)
            {
                return NotFound();
            }

            return View(class_Machine_Master);
        }

        // POST: MachineMaster/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var class_Machine_Master = await _context.obj_Machine_Master.FindAsync(id);
            _context.obj_Machine_Master.Remove(class_Machine_Master);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Class_Machine_MasterExists(int id)
        {
            return _context.obj_Machine_Master.Any(e => e.MACHINEID == id);
        }
    }
}
