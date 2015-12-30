using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LBAS.Web.Models;

namespace LBAS.Web.Controllers
{
    public class FranchiseController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Franchise
        public ActionResult Index()
        {
            return View(db.FranchiseViewModels.ToList());
        }

        // GET: Franchise/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FranchiseViewModel franchiseViewModel = db.FranchiseViewModels.Find(id);
            if (franchiseViewModel == null)
            {
                return HttpNotFound();
            }
            return View(franchiseViewModel);
        }

        // GET: Franchise/Create
        public ActionResult Create()
        {
            // Create dummy data for Industry
            List<SelectListItem> industryList = new List<SelectListItem>();
            industryList.Add(new SelectListItem { Value = "1", Text = "Food" });
            industryList.Add(new SelectListItem { Value = "2", Text = "Electronic" });
            industryList.Add(new SelectListItem { Value = "3", Text = "IT" });
            ViewBag.industryList = new SelectList(industryList, "Value", "Text");

            // Create dummy data for State
            List<SelectListItem> stateList = new List<SelectListItem>();
            stateList.Add(new SelectListItem { Value = "1", Text = "Washington" });
            stateList.Add(new SelectListItem { Value = "2", Text = "Los Angeles" });
            stateList.Add(new SelectListItem { Value = "3", Text = "Texas" });
            ViewBag.stateList = new SelectList(stateList, "Value", "Text");

            ViewBag.Message = "";
            ViewBag.MenuItem = 2;
            return View();
        }

        // POST: Franchise/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Industry,Address,City,State,Zipcode")] FranchiseViewModel franchiseViewModel)
        {
            // Create dummy data for Industry
            List<SelectListItem> industryList = new List<SelectListItem>();
            industryList.Add(new SelectListItem { Value = "1", Text = "Food" });
            industryList.Add(new SelectListItem { Value = "2", Text = "Electronic" });
            industryList.Add(new SelectListItem { Value = "3", Text = "IT" });
            ViewBag.industryList = new SelectList(industryList, "Value", "Text");

            // Create dummy data for State
            List<SelectListItem> stateList = new List<SelectListItem>();
            stateList.Add(new SelectListItem { Value = "1", Text = "Washington" });
            stateList.Add(new SelectListItem { Value = "2", Text = "Los Angeles" });
            stateList.Add(new SelectListItem { Value = "3", Text = "Texas" });
            ViewBag.stateList = new SelectList(stateList, "Value", "Text");

            if (ModelState.IsValid)
            {
                //db.FranchiseViewModels.Add(franchiseViewModel);
                //db.SaveChanges();
                ViewBag.Message = "Success !";
                return View();
            }

            return View();
        }

        // GET: Franchise/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FranchiseViewModel franchiseViewModel = db.FranchiseViewModels.Find(id);
            if (franchiseViewModel == null)
            {
                return HttpNotFound();
            }
            return View(franchiseViewModel);
        }

        // POST: Franchise/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Industry,Address,City,State,Zipcode")] FranchiseViewModel franchiseViewModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(franchiseViewModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(franchiseViewModel);
        }

        // GET: Franchise/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FranchiseViewModel franchiseViewModel = db.FranchiseViewModels.Find(id);
            if (franchiseViewModel == null)
            {
                return HttpNotFound();
            }
            return View(franchiseViewModel);
        }

        // POST: Franchise/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FranchiseViewModel franchiseViewModel = db.FranchiseViewModels.Find(id);
            db.FranchiseViewModels.Remove(franchiseViewModel);
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
