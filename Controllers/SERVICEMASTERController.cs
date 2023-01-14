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
    public class SERVICEMASTERController : Controller
    {
        private readonly ApplicationDBContext _context;

        public SERVICEMASTERController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: SERVICEMASTER
        public async Task<IActionResult> Index()
        {
            return View(await _context.obj_SERVICE_MASTER.ToListAsync());
        }

        // GET: SERVICEMASTER/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var class_SERVICE_MASTER = await _context.obj_SERVICE_MASTER
                .FirstOrDefaultAsync(m => m.SERVICEID == id);
            if (class_SERVICE_MASTER == null)
            {
                return NotFound();
            }

            return View(class_SERVICE_MASTER);
        }

        // GET: SERVICEMASTER/Create
        public IActionResult Create()
        {
            IEnumerable<SelectListItem> GROUPSERVICES = _context.obj_SERVICE_GROUP_MASTER.Select(c => new SelectListItem
            {

                Value = c.SERVICEGROUPID.ToString(),
                Text = c.SERVICEGROUPNAME

            });
            ViewBag.GROUPSERVICES = GROUPSERVICES;
            return View();
        }

        // POST: SERVICEMASTER/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SERVICEID,SERVICENAME,SERVICEGROUPID,ISACTIVE")] Class_SERVICE_MASTER class_SERVICE_MASTER)
        {
            if (ModelState.IsValid)
            {
                _context.Add(class_SERVICE_MASTER);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(class_SERVICE_MASTER);
        }

        // GET: SERVICEMASTER/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var class_SERVICE_MASTER = await _context.obj_SERVICE_MASTER.FindAsync(id);
            if (class_SERVICE_MASTER == null)
            {
                return NotFound();
            }
            return View(class_SERVICE_MASTER);
        }

        // POST: SERVICEMASTER/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SERVICEID,SERVICENAME,SERVICEGROUPID,ISACTIVE")] Class_SERVICE_MASTER class_SERVICE_MASTER)
        {
            if (id != class_SERVICE_MASTER.SERVICEID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(class_SERVICE_MASTER);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Class_SERVICE_MASTERExists(class_SERVICE_MASTER.SERVICEID))
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
            return View(class_SERVICE_MASTER);
        }

        // GET: SERVICEMASTER/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var class_SERVICE_MASTER = await _context.obj_SERVICE_MASTER
                .FirstOrDefaultAsync(m => m.SERVICEID == id);
            if (class_SERVICE_MASTER == null)
            {
                return NotFound();
            }

            return View(class_SERVICE_MASTER);
        }

        // POST: SERVICEMASTER/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var class_SERVICE_MASTER = await _context.obj_SERVICE_MASTER.FindAsync(id);
            _context.obj_SERVICE_MASTER.Remove(class_SERVICE_MASTER);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Class_SERVICE_MASTERExists(int id)
        {
            return _context.obj_SERVICE_MASTER.Any(e => e.SERVICEID == id);
        }
    }
}
