using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ITIProject.Models;
using ITIProject.Models.DBFiles;

namespace ITIProject.Areas.Dashboard.Controllers
{
    public class CoursesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Dashboard/Courses
        public async Task<ActionResult> Index()
        {
            var courses = db.Courses.Include(c => c.Department).Include(c => c.Professor);
            return View(await courses.ToListAsync());
        }

        // GET: Dashboard/Courses/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cource cource = await db.Courses.FindAsync(id);
            if (cource == null)
            {
                return HttpNotFound();
            }
            return View(cource);
        }

        // GET: Dashboard/Courses/Create
        public ActionResult Create()
        {
            ViewBag.Course_Department_ID = new SelectList(db.Deparments, "ID", "Name");
            ViewBag.Course_Professor_ID = new SelectList(db.Professors, "ID", "Name");
            return View();
        }

        // POST: Dashboard/Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Name,Free,Cost,Hours,Degree,MinDegree,Course_Department_ID,Course_Professor_ID")] Cource cource)
        {
            if (ModelState.IsValid)
            {
                db.Courses.Add(cource);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Course_Department_ID = new SelectList(db.Deparments, "ID", "Name", cource.Course_Department_ID);
            ViewBag.Course_Professor_ID = new SelectList(db.Professors, "ID", "Name", cource.Course_Professor_ID);
            return View(cource);
        }

        // GET: Dashboard/Courses/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cource cource = await db.Courses.FindAsync(id);
            if (cource == null)
            {
                return HttpNotFound();
            }
            ViewBag.Course_Department_ID = new SelectList(db.Deparments, "ID", "Name", cource.Course_Department_ID);
            ViewBag.Course_Professor_ID = new SelectList(db.Professors, "ID", "Name", cource.Course_Professor_ID);
            return View(cource);
        }

        // POST: Dashboard/Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Name,Free,Cost,Hours,Degree,MinDegree,Course_Department_ID,Course_Professor_ID")] Cource cource)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cource).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Course_Department_ID = new SelectList(db.Deparments, "ID", "Name", cource.Course_Department_ID);
            ViewBag.Course_Professor_ID = new SelectList(db.Professors, "ID", "Name", cource.Course_Professor_ID);
            return View(cource);
        }

        // GET: Dashboard/Courses/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cource cource = await db.Courses.FindAsync(id);
            if (cource == null)
            {
                return HttpNotFound();
            }
            return View(cource);
        }

        // POST: Dashboard/Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Cource cource = await db.Courses.FindAsync(id);
            db.Courses.Remove(cource);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
