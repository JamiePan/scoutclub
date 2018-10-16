using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FIT5032_assi.Models;

namespace FIT5032_assi.Controllers
{
    public class ClassArrangementsController : Controller
    {
        private C_LevelEntities db = new C_LevelEntities();

        // GET: ClassArrangements
        public ActionResult Index()
        {
            var classArrangements = db.ClassArrangements.Include(c => c.Activity).Include(c => c.Client).Include(c => c.Staff);
            return View(classArrangements.ToList());
        }

        // GET: ClassArrangements/Details/5
        
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClassArrangement classArrangement = db.ClassArrangements.Find(id);
            if (classArrangement == null)
            {
                return HttpNotFound();
            }
            return View(classArrangement);
        }

        // GET: ClassArrangements/Create
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            ViewBag.ActivityID = new SelectList(db.Activities, "ActivityID", "Description");
            ViewBag.Id = new SelectList(db.Clients, "Id", "LastName");
            ViewBag.StaffID = new SelectList(db.Staffs, "StaffID", "LastName");
            return View();
        }

        // POST: ClassArrangements/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create([Bind(Include = "ClassID,Available,ActivityID,StaffID,Id")] ClassArrangement classArrangement)
        {
            if (ModelState.IsValid)
            {
                db.ClassArrangements.Add(classArrangement);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ActivityID = new SelectList(db.Activities, "ActivityID", "Description", classArrangement.ActivityID);
            ViewBag.Id = new SelectList(db.Clients, "Id", "LastName", classArrangement.Id);
            ViewBag.StaffID = new SelectList(db.Staffs, "StaffID", "LastName", classArrangement.StaffID);
            return View(classArrangement);
        }

        // GET: ClassArrangements/Edit/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClassArrangement classArrangement = db.ClassArrangements.Find(id);
            if (classArrangement == null)
            {
                return HttpNotFound();
            }
            ViewBag.ActivityID = new SelectList(db.Activities, "ActivityID", "Description", classArrangement.ActivityID);
            ViewBag.Id = new SelectList(db.Clients, "Id", "LastName", classArrangement.Id);
            ViewBag.StaffID = new SelectList(db.Staffs, "StaffID", "LastName", classArrangement.StaffID);
            return View(classArrangement);
        }

        // POST: ClassArrangements/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit([Bind(Include = "ClassID,Available,ActivityID,StaffID,Id")] ClassArrangement classArrangement)
        {
            if (ModelState.IsValid)
            {
                db.Entry(classArrangement).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ActivityID = new SelectList(db.Activities, "ActivityID", "Description", classArrangement.ActivityID);
            ViewBag.Id = new SelectList(db.Clients, "Id", "LastName", classArrangement.Id);
            ViewBag.StaffID = new SelectList(db.Staffs, "StaffID", "LastName", classArrangement.StaffID);
            return View(classArrangement);
        }

        // GET: ClassArrangements/Delete/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClassArrangement classArrangement = db.ClassArrangements.Find(id);
            if (classArrangement == null)
            {
                return HttpNotFound();
            }
            return View(classArrangement);
        }

        // POST: ClassArrangements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteConfirmed(int id)
        {
            ClassArrangement classArrangement = db.ClassArrangements.Find(id);
            db.ClassArrangements.Remove(classArrangement);
            db.SaveChanges();
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
