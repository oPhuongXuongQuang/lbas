using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LBAS.Web.Models;

namespace LBAS.Web.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Index()
        {
            // Create dummy data for Corporation
            List<SelectListItem> corporationList = new List<SelectListItem>();
            corporationList.Add(new SelectListItem { Value = "1", Text = "McDonalds" });
            corporationList.Add(new SelectListItem { Value = "2", Text = "KFC" });
            corporationList.Add(new SelectListItem { Value = "3", Text = "Jolibee" });
            ViewBag.corporationList = new SelectList(corporationList, "Value", "Text");

            // Create dummy data for Franchise
            List<SelectListItem> franchiseList = new List<SelectListItem>();
            franchiseList.Add(new SelectListItem { Value = "1", Text = "Franchise Vietnam" });
            franchiseList.Add(new SelectListItem { Value = "2", Text = "Franchise USA" });
            franchiseList.Add(new SelectListItem { Value = "3", Text = "Franchise EU" });
            ViewBag.franchiseList = new SelectList(franchiseList, "Value", "Text");

            //Mock system return
            var system = new SystemViewModel();
            system.PinCode = "123456";
            system.SerialNo = "123456";
            system.SystemName = "Flexy™ EC Rooftop";
            system.SystemAddress = "Floor 2 - 2140 Lake Park Blvd. Richardson";
            system.City = "Dallas";
            system.State = "Texas";
            system.ZipCode = "75080";

            ViewBag.system = system;
            ViewBag.MenuItem = 6;
            return View();
        }

        // GET: Dashboard/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Dashboard/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Dashboard/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Dashboard/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Dashboard/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Dashboard/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Dashboard/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
