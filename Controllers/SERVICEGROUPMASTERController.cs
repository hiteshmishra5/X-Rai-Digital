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
    public class SERVICEGROUPMASTERController : Controller
    {
        private readonly ApplicationDBContext _context;

        public SERVICEGROUPMASTERController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: SERVICEGROUPMASTER
        public async Task<IActionResult> Index()
        {
            return View(await _context.obj_SERVICE_GROUP_MASTER.ToListAsync());
        }

        // GET: SERVICEGROUPMASTER/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var class_SERVICE_GROUP_MASTER = await _context.obj_SERVICE_GROUP_MASTER
                .FirstOrDefaultAsync(m => m.SERVICEGROUPID == id);
            if (class_SERVICE_GROUP_MASTER == null)
            {
                return NotFound();
            }

            return View(class_SERVICE_GROUP_MASTER);
        }

        // GET: SERVICEGROUPMASTER/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SERVICEGROUPMASTER/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SERVICEGROUPID,SERVICEGROUPNAME,ISACTIVE")] Class_SERVICE_GROUP_MASTER class_SERVICE_GROUP_MASTER)
        {
            if (ModelState.IsValid)
            {
                _context.Add(class_SERVICE_GROUP_MASTER);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(class_SERVICE_GROUP_MASTER);
        }

        // GET: SERVICEGROUPMASTER/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var class_SERVICE_GROUP_MASTER = await _context.obj_SERVICE_GROUP_MASTER.FindAsync(id);
            if (class_SERVICE_GROUP_MASTER == null)
            {
                return NotFound();
            }
            return View(class_SERVICE_GROUP_MASTER);
        }

        // POST: SERVICEGROUPMASTER/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SERVICEGROUPID,SERVICEGROUPNAME,ISACTIVE")] Class_SERVICE_GROUP_MASTER class_SERVICE_GROUP_MASTER)
        {
            if (id != class_SERVICE_GROUP_MASTER.SERVICEGROUPID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(class_SERVICE_GROUP_MASTER);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Class_SERVICE_GROUP_MASTERExists(class_SERVICE_GROUP_MASTER.SERVICEGROUPID))
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
            return View(class_SERVICE_GROUP_MASTER);
        }

        // GET: SERVICEGROUPMASTER/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var class_SERVICE_GROUP_MASTER = await _context.obj_SERVICE_GROUP_MASTER
                .FirstOrDefaultAsync(m => m.SERVICEGROUPID == id);
            if (class_SERVICE_GROUP_MASTER == null)
            {
                return NotFound();
            }

            return View(class_SERVICE_GROUP_MASTER);
        }

        // POST: SERVICEGROUPMASTER/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var class_SERVICE_GROUP_MASTER = await _context.obj_SERVICE_GROUP_MASTER.FindAsync(id);
            _context.obj_SERVICE_GROUP_MASTER.Remove(class_SERVICE_GROUP_MASTER);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Class_SERVICE_GROUP_MASTERExists(int id)
        {
            return _context.obj_SERVICE_GROUP_MASTER.Any(e => e.SERVICEGROUPID == id);
        }
    }
}
