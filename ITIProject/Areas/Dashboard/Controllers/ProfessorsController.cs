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
    public class ProfessorsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Dashboard/Professors
        public async Task<ActionResult> Index()
        {
            var professors = db.Professors.Include(p => p.Department);
            return View(await professors.ToListAsync());
        }

        // GET: Dashboard/Professors/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Professor professor = await db.Professors.FindAsync(id);
            if (professor == null)
            {
                return HttpNotFound();
            }
            return View(professor);
        }

        // GET: Dashboard/Professors/Create
        public ActionResult Create()
        {
            ViewBag.Professor_Department_ID = new SelectList(db.Deparments, "ID", "Name");
            return View();
        }

        // POST: Dashboard/Professors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Name,City,Salary,Professor_Department_ID")] Professor professor)
        {
            if (ModelState.IsValid)
            {
                db.Professors.Add(professor);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Professor_Department_ID = new SelectList(db.Deparments, "ID", "Name", professor.Professor_Department_ID);
            return View(professor);
        }

        // GET: Dashboard/Professors/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Professor professor = await db.Professors.FindAsync(id);
            if (professor == null)
            {
                return HttpNotFound();
            }
            ViewBag.Professor_Department_ID = new SelectList(db.Deparments, "ID", "Name", professor.Professor_Department_ID);
            return View(professor);
        }

        // POST: Dashboard/Professors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Name,City,Salary,Professor_Department_ID")] Professor professor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(professor).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Professor_Department_ID = new SelectList(db.Deparments, "ID", "Name", professor.Professor_Department_ID);
            return View(professor);
        }

        // GET: Dashboard/Professors/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Professor professor = await db.Professors.FindAsync(id);
            if (professor == null)
            {
                return HttpNotFound();
            }
            return View(professor);
        }

        // POST: Dashboard/Professors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Professor professor = await db.Professors.FindAsync(id);
            db.Professors.Remove(professor);
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
