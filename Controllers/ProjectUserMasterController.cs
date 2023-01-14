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
    public class ProjectUserMasterController : Controller
    {
        private readonly ApplicationDBContext _context;

        public ProjectUserMasterController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: ProjectUserMaster
        public async Task<IActionResult> Index()
        {
            return View(await _context.obj_PROJECT_USER_MASTER.ToListAsync());
        }

        // GET: ProjectUserMaster/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var class_PROJECT_USER_MASTER = await _context.obj_PROJECT_USER_MASTER
                .FirstOrDefaultAsync(m => m.useridsno == id);
            if (class_PROJECT_USER_MASTER == null)
            {
                return NotFound();
            }

            return View(class_PROJECT_USER_MASTER);
        }

        // GET: ProjectUserMaster/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProjectUserMaster/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("useridsno,username,password,email,userid,mobile,address,address1,city,state,pin,registrationtypeid,fullname,gender,mpin")] Class_PROJECT_USER_MASTER class_PROJECT_USER_MASTER)
        {
            if (ModelState.IsValid)
            {
                _context.Add(class_PROJECT_USER_MASTER);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(class_PROJECT_USER_MASTER);
        }

        // GET: ProjectUserMaster/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var class_PROJECT_USER_MASTER = await _context.obj_PROJECT_USER_MASTER.FindAsync(id);
            if (class_PROJECT_USER_MASTER == null)
            {
                return NotFound();
            }
            return View(class_PROJECT_USER_MASTER);
        }

        // POST: ProjectUserMaster/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("useridsno,username,password,email,userid,mobile,address,address1,city,state,pin,registrationtypeid,fullname,gender,mpin")] Class_PROJECT_USER_MASTER class_PROJECT_USER_MASTER)
        {
            if (id != class_PROJECT_USER_MASTER.useridsno)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(class_PROJECT_USER_MASTER);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Class_PROJECT_USER_MASTERExists(class_PROJECT_USER_MASTER.useridsno))
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
            return View(class_PROJECT_USER_MASTER);
        }

        // GET: ProjectUserMaster/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var class_PROJECT_USER_MASTER = await _context.obj_PROJECT_USER_MASTER
                .FirstOrDefaultAsync(m => m.useridsno == id);
            if (class_PROJECT_USER_MASTER == null)
            {
                return NotFound();
            }

            return View(class_PROJECT_USER_MASTER);
        }

        // POST: ProjectUserMaster/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var class_PROJECT_USER_MASTER = await _context.obj_PROJECT_USER_MASTER.FindAsync(id);
            _context.obj_PROJECT_USER_MASTER.Remove(class_PROJECT_USER_MASTER);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Class_PROJECT_USER_MASTERExists(int id)
        {
            return _context.obj_PROJECT_USER_MASTER.Any(e => e.useridsno == id);
        }
        public JsonResult IsActive(int? id)
        {
            string result = "failure";
            Class_PROJECT_USER_MASTER class_PROJECT_USER_MASTER = _context.obj_PROJECT_USER_MASTER.Find(id);
            if (class_PROJECT_USER_MASTER.isactive == true)
            {
                result = "failure";
                class_PROJECT_USER_MASTER.isactive = false;
            }
            else
            {
                result = "success";
                class_PROJECT_USER_MASTER.isactive = true;
            }
            _context.SaveChanges();

            return Json(result);
        }
    }
}
