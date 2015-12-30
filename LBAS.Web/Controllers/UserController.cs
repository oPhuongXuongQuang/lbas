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
    public class UserController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: User
        public ActionResult Index()
        {
            return View(db.UserViewModels.ToList());
        }

        // GET: User/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserViewModel userViewModel = db.UserViewModels.Find(id);
            if (userViewModel == null)
            {
                return HttpNotFound();
            }
            return View(userViewModel);
        }

        // GET: User/Create
        public ActionResult Create()
        {
            // Create dummy data for Corporation
            List<SelectListItem> corporationList = new List<SelectListItem>();
            corporationList.Add(new SelectListItem { Value = "1", Text = "McDonalds" });
            corporationList.Add(new SelectListItem { Value = "2", Text = "KFC" });
            corporationList.Add(new SelectListItem { Value = "3", Text = "Jolibee" });
            ViewBag.corporationList = new SelectList(corporationList, "Value", "Text");

            // Create dummy data for UserRole
            List<SelectListItem> userRoleList = new List<SelectListItem>();
            userRoleList.Add(new SelectListItem { Value = "1", Text = "LennoxAdmin" });
            userRoleList.Add(new SelectListItem { Value = "2", Text = "Manager" });
            userRoleList.Add(new SelectListItem { Value = "3", Text = "Admin" });
            ViewBag.userRoleList = new SelectList(userRoleList, "Value", "Text");

            ViewBag.Message = "";
            ViewBag.MenuItem = 3;
            return View();
        }

        // POST: User/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Username,Password,FirstName,LastName,EmailAddress,PhoneNumber,Corporation,UserRole")] UserViewModel userViewModel)
        {
            // Create dummy data for Corporation
            List<SelectListItem> corporationList = new List<SelectListItem>();
            corporationList.Add(new SelectListItem { Value = "1", Text = "McDonalds" });
            corporationList.Add(new SelectListItem { Value = "2", Text = "KFC" });
            corporationList.Add(new SelectListItem { Value = "3", Text = "Jolibee" });
            ViewBag.corporationList = new SelectList(corporationList, "Value", "Text");

            // Create dummy data for UserRole
            List<SelectListItem> userRoleList = new List<SelectListItem>();
            userRoleList.Add(new SelectListItem { Value = "1", Text = "LennoxAdmin" });
            userRoleList.Add(new SelectListItem { Value = "2", Text = "Manager" });
            userRoleList.Add(new SelectListItem { Value = "3", Text = "Admin" });
            ViewBag.userRoleList = new SelectList(userRoleList, "Value", "Text");

            if (ModelState.IsValid)
            {
                //db.UserViewModels.Add(userViewModel);
                //db.SaveChanges();
                ViewBag.Message = "Success !";
                return View();
            }

            return View();
        }

        // GET: User/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserViewModel userViewModel = db.UserViewModels.Find(id);
            if (userViewModel == null)
            {
                return HttpNotFound();
            }
            return View(userViewModel);
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Username,Password,FirstName,LastName,EmailAddress,PhoneNumber,Corporation,UserRole")] UserViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userViewModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userViewModel);
        }

        // GET: User/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserViewModel userViewModel = db.UserViewModels.Find(id);
            if (userViewModel == null)
            {
                return HttpNotFound();
            }
            return View(userViewModel);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserViewModel userViewModel = db.UserViewModels.Find(id);
            db.UserViewModels.Remove(userViewModel);
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
