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
    public class PriceRateMasterController : Controller
    {
        private readonly ApplicationDBContext _context;

        public PriceRateMasterController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: PriceRateMasterController
        public async Task<IActionResult> Index()
        {
            return View(await _context.Class_Price_Rate_Master.ToListAsync());
        }

        // GET: PriceRateMasterController/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var class_Price_Rate_Master = await _context.Class_Price_Rate_Master
                .FirstOrDefaultAsync(m => m.PRICERATEID == id);
            if (class_Price_Rate_Master == null)
            {
                return NotFound();
            }

            return View(class_Price_Rate_Master);
        }

        // GET: PriceRateMasterController/Create
        public IActionResult Create()
        {
            IEnumerable<SelectListItem> VISITTYPES = _context.obj_VISIT_TYPE_MASTER.Select(c => new SelectListItem
            {

                Value = c.VISITTYPEID.ToString(),
                Text = c.VISITTYPENAME

            });
            ViewBag.VISITTYPES = VISITTYPES;
            IEnumerable<SelectListItem> SERVICES = _context.obj_SERVICE_MASTER.Select(c => new SelectListItem
            {

                Value = c.SERVICEID.ToString(),
                Text = c.SERVICENAME

            });
            ViewBag.SERVICES = SERVICES;
            IEnumerable<SelectListItem> GROUPSERVICES = _context.obj_SERVICE_GROUP_MASTER.Select(c => new SelectListItem
            {

                Value = c.SERVICEGROUPID.ToString(),
                Text = c.SERVICEGROUPNAME

            });
            ViewBag.GROUPSERVICES = GROUPSERVICES;
            return View();
        }

        // POST: PriceRateMasterController/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PRICERATEID,SERVICEGROUPID,SERVICEID,VISITTYPEID,EFFECTIVEFROMDATE,EFFECTIVETODATE,PRICE")] Class_Price_Rate_Master class_Price_Rate_Master)
        {
            if (ModelState.IsValid)
            {
                _context.Add(class_Price_Rate_Master);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(class_Price_Rate_Master);
        }

        // GET: PriceRateMasterController/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var class_Price_Rate_Master = await _context.Class_Price_Rate_Master.FindAsync(id);
            if (class_Price_Rate_Master == null)
            {
                return NotFound();
            }
            return View(class_Price_Rate_Master);
        }

        // POST: PriceRateMasterController/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PRICERATEID,SERVICEGROUPID,SERVICEID,VISITTYPEID,EFFECTIVEFROMDATE,EFFECTIVETODATE,PRICE")] Class_Price_Rate_Master class_Price_Rate_Master)
        {
            if (id != class_Price_Rate_Master.PRICERATEID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(class_Price_Rate_Master);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Class_Price_Rate_MasterExists(class_Price_Rate_Master.PRICERATEID))
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
            return View(class_Price_Rate_Master);
        }

        // GET: PriceRateMasterController/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var class_Price_Rate_Master = await _context.Class_Price_Rate_Master
                .FirstOrDefaultAsync(m => m.PRICERATEID == id);
            if (class_Price_Rate_Master == null)
            {
                return NotFound();
            }

            return View(class_Price_Rate_Master);
        }

        // POST: PriceRateMasterController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var class_Price_Rate_Master = await _context.Class_Price_Rate_Master.FindAsync(id);
            _context.Class_Price_Rate_Master.Remove(class_Price_Rate_Master);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Class_Price_Rate_MasterExists(int id)
        {
            return _context.Class_Price_Rate_Master.Any(e => e.PRICERATEID == id);
        }
        public JsonResult GetPriceServiceGroupVisitWise(int SERVICEID, int SERVICEGROUPID, int VISITTYPEID)
        {
            var projects = _context.obj_Price_Rate_Master.Where(m => m.SERVICEGROUPID == SERVICEGROUPID && m.SERVICEID == SERVICEID && m.VISITTYPEID == VISITTYPEID).ToList();
            return Json(new SelectList(projects, "PRICERATEID", "PRICE"));
        }
    }
}
