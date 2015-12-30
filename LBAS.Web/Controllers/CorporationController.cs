using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LBAS.Web.Models;
using System.Threading.Tasks;
using Microsoft.Azure.ActiveDirectory.GraphClient;
using LBAS.Web.BusinessLogic;
using LBAS.Web.Utils;
using Microsoft.Owin.Security.OpenIdConnect;

namespace LBAS.Web.Controllers
{
    public class CorporationController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Corporation
        public ActionResult Index()
        {
            return View(db.CorporationViewModels.ToList());
        }

        // GET: Corporation/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CorporationViewModel corporationViewModel = db.CorporationViewModels.Find(id);
            if (corporationViewModel == null)
            {
                return HttpNotFound();
            }
            return View(corporationViewModel);
        }

        // GET: Corporation/Create
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
            ViewBag.MenuItem = 1;
            return View();
        }

        // POST: Corporation/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<ActionResult> Create
            ([Bind(Include = "DisplayName,JobTitle,StreetAddress,City,State,PostalCode")] User corp)
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

            PasswordProfile pp = new PasswordProfile();
            pp.ForceChangePasswordNextLogin = false;
            pp.Password = "Abcd1234";

            corp.UserPrincipalName = Helper.RemoveSpace(corp.DisplayName) + LBAS.Web.Utils.Constants.Email;
            corp.AccountEnabled = true;
            corp.PasswordProfile = pp;
            corp.MailNickname = Helper.RemoveSpace(corp.DisplayName);
            corp.UserType = LBAS.Web.Utils.Constants.VirtualUser;
            corp.Department = LBAS.Web.Utils.Constants.Corporation;

            OrganizationBLL orgBLL = new OrganizationBLL();
            try
            {
                await orgBLL.CreateCorporation(corp);
                ViewBag.Message = "New corporation was saved";
                return View();
            }
            catch (Exception exception)
            {
                ModelState.AddModelError("", exception.Message);
                return View();
            }
        }

        // GET: Corporation/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CorporationViewModel corporationViewModel = db.CorporationViewModels.Find(id);
            if (corporationViewModel == null)
            {
                return HttpNotFound();
            }
            return View(corporationViewModel);
        }

        // POST: Corporation/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Industry,Address,City,State,Zipcode")] CorporationViewModel corporationViewModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(corporationViewModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(corporationViewModel);
        }

        // GET: Corporation/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CorporationViewModel corporationViewModel = db.CorporationViewModels.Find(id);
            if (corporationViewModel == null)
            {
                return HttpNotFound();
            }
            return View(corporationViewModel);
        }

        // POST: Corporation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CorporationViewModel corporationViewModel = db.CorporationViewModels.Find(id);
            db.CorporationViewModels.Remove(corporationViewModel);
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
