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
    public class ServiceProviderMasterController : Controller
    {
        private readonly ApplicationDBContext _context;

        public ServiceProviderMasterController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: ServiceProviderMaster
        public async Task<IActionResult> Index()
        {
            return View(await _context.obj_SERVICE_PROVIDER_MASTER.ToListAsync());
        }

        // GET: ServiceProviderMaster/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var class_SERVICE_PROVIDER_MASTER = await _context.obj_SERVICE_PROVIDER_MASTER
                .FirstOrDefaultAsync(m => m.SERVICEPROVIDERID == id);
            if (class_SERVICE_PROVIDER_MASTER == null)
            {
                return NotFound();
            }

            return View(class_SERVICE_PROVIDER_MASTER);
        }

        // GET: ServiceProviderMaster/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ServiceProviderMaster/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SERVICEPROVIDERID,SERVICEPROVIDERNAME,ISACTIVE,USERID")] Class_SERVICE_PROVIDER_MASTER class_SERVICE_PROVIDER_MASTER)
        {
            if (ModelState.IsValid)
            {
                _context.Add(class_SERVICE_PROVIDER_MASTER);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(class_SERVICE_PROVIDER_MASTER);
        }

        // GET: ServiceProviderMaster/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var class_SERVICE_PROVIDER_MASTER = await _context.obj_SERVICE_PROVIDER_MASTER.FindAsync(id);
            if (class_SERVICE_PROVIDER_MASTER == null)
            {
                return NotFound();
            }
            return View(class_SERVICE_PROVIDER_MASTER);
        }

        // POST: ServiceProviderMaster/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SERVICEPROVIDERID,SERVICEPROVIDERNAME,ISACTIVE,USERID")] Class_SERVICE_PROVIDER_MASTER class_SERVICE_PROVIDER_MASTER)
        {
            if (id != class_SERVICE_PROVIDER_MASTER.SERVICEPROVIDERID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(class_SERVICE_PROVIDER_MASTER);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Class_SERVICE_PROVIDER_MASTERExists(class_SERVICE_PROVIDER_MASTER.SERVICEPROVIDERID))
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
            return View(class_SERVICE_PROVIDER_MASTER);
        }

        // GET: ServiceProviderMaster/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var class_SERVICE_PROVIDER_MASTER = await _context.obj_SERVICE_PROVIDER_MASTER
                .FirstOrDefaultAsync(m => m.SERVICEPROVIDERID == id);
            if (class_SERVICE_PROVIDER_MASTER == null)
            {
                return NotFound();
            }

            return View(class_SERVICE_PROVIDER_MASTER);
        }

        // POST: ServiceProviderMaster/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var class_SERVICE_PROVIDER_MASTER = await _context.obj_SERVICE_PROVIDER_MASTER.FindAsync(id);
            _context.obj_SERVICE_PROVIDER_MASTER.Remove(class_SERVICE_PROVIDER_MASTER);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Class_SERVICE_PROVIDER_MASTERExists(int id)
        {
            return _context.obj_SERVICE_PROVIDER_MASTER.Any(e => e.SERVICEPROVIDERID == id);
        }
    }
}
