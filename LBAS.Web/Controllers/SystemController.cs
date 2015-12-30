using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LBAS.Web.Models;

namespace LBAS.Web.Controllers
{
    public class SystemController : Controller
    {
        // GET: System
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
            ViewBag.MenuItem = 5;
            return View();
        }

        // GET: System/Details/5
        public ActionResult Details(int id)
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

            // Create dummy data for Dealer
            List<SelectListItem> dealerList = new List<SelectListItem>();
            dealerList.Add(new SelectListItem { Value = "1", Text = "Dealer001" });
            dealerList.Add(new SelectListItem { Value = "2", Text = "Dealer002" });
            dealerList.Add(new SelectListItem { Value = "3", Text = "Dealer003" });
            ViewBag.dealerList = new SelectList(dealerList, "Value", "Text");

            // Create dummy data for Building
            List<SelectListItem> buildingList = new List<SelectListItem>();
            buildingList.Add(new SelectListItem { Value = "1", Text = "Building001" });
            buildingList.Add(new SelectListItem { Value = "2", Text = "Building002" });
            buildingList.Add(new SelectListItem { Value = "3", Text = "Building003" });
            ViewBag.buildingList = new SelectList(buildingList, "Value", "Text");

            //Mock system return
            var system = new SystemViewModel();
            ViewBag.PinCode = "123456";
            ViewBag.SerialNo = "1234567890";
            ViewBag.SystemName = "Flexy™ EC Rooftop";
            ViewBag.SystemAddress = "Floor 2 - 2140 Lake Park Blvd. Richardson";
            ViewBag.SiteAddress = "2140 Lake Park Blvd. Richardson, TX 75080";
            ViewBag.City = "Dallas";

            List<SelectListItem> stateList = new List<SelectListItem>();
            stateList.Add(new SelectListItem { Value = "1", Text = "Texas" });
            ViewBag.stateList = new SelectList(stateList, "Value", "Text");

            ViewBag.ZipCode = "75080";

            return View();
        }

        public ActionResult SystemsOfSite(int siteId)
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
            return View();
        }

        public ActionResult ConnectSystem(int id)
        {
            // Dummy data
            // IsRegister = true: go to next screen -> send request to update status from server every 3s.
            // IsRegister = false: register fail -> show message.
            var jsonResult = new { IsRegister = true, Message = "OK" };
            return Json(jsonResult, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EnableConnection(int id)
        {
            // Dummy data
            // 'System to Cloud connection' and 'Cloud to System connection' have 3 status type
            // Status 0: Success
            // Status 1: Fail
            // Status 2: In-progress
            var jsonResult = new { system = 0, cloud = 0, Message = "Success: Congratulation, your system is now registered to cloud hub and you are ready to monitor it!" };
            System.Threading.Thread.Sleep(2500);
            return Json(jsonResult, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AbortConnection(int id)
        {
            return RedirectToAction("Index");
        }

        public ActionResult Update(int Id)
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

            // Create dummy data for Command
            List<SelectListItem> commandList = new List<SelectListItem>();
            commandList.Add(new SelectListItem { Value = "1", Text = "Temperature" });
            commandList.Add(new SelectListItem { Value = "2", Text = "Humidity" });
            ViewBag.commandList = new SelectList(commandList, "Value", "Text");

            return View();
        }

        public ActionResult AddNewCommand()
        {
            // Create dummy data for Command
            List<SelectListItem> commandList = new List<SelectListItem>();
            commandList.Add(new SelectListItem { Value = "1", Text = "Temperature" });
            commandList.Add(new SelectListItem { Value = "2", Text = "Humidity" });
            ViewBag.commandList = new SelectList(commandList, "Value", "Text");
            CommandViewModel command = new CommandViewModel();
            command.IsExtensionCommand = true;
            command.CommandList = new SelectList(commandList, "Value", "Text");
            return PartialView("_Command", command);
        }

        public ActionResult SendCommand()
        {
            return PartialView("_CommandHistoryLine");
        }

        public ActionResult RegisterSystem()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterSystem([Bind(Include = "PinCode,SerialNo")] SystemViewModel systemViewModel)
        {
            if (ModelState.IsValid)
            {
                //db.SystemViewModels.Add(systemViewModel);
                //db.SaveChanges();
                return RedirectToAction("VerifyPINCode");
            }
            return View();
        }

        public ActionResult VerifyPINCode()
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
            return View();
        }

        public ActionResult AddMoreInfo()
        {
            // Create dummy data for Corporation
            List<SelectListItem> corporationList = new List<SelectListItem>();
            corporationList.Add(new SelectListItem { Value = "1", Text = "McDonalds" });
            corporationList.Add(new SelectListItem { Value = "2", Text = "KFC" });
            corporationList.Add(new SelectListItem { Value = "3", Text = "Jolibee" });
            ViewBag.corporationList = new SelectList(corporationList, "Value", "Text");

            // Create dummy data for Dealer
            List<SelectListItem> dealerList = new List<SelectListItem>();
            dealerList.Add(new SelectListItem { Value = "1", Text = "Dealer001" });
            dealerList.Add(new SelectListItem { Value = "2", Text = "Dealer002" });
            dealerList.Add(new SelectListItem { Value = "3", Text = "Dealer003" });
            ViewBag.dealerList = new SelectList(dealerList, "Value", "Text");

            // Create dummy data for Building
            List<SelectListItem> buildingList = new List<SelectListItem>();
            buildingList.Add(new SelectListItem { Value = "1", Text = "Building001" });
            buildingList.Add(new SelectListItem { Value = "2", Text = "Building002" });
            buildingList.Add(new SelectListItem { Value = "3", Text = "Building003" });
            ViewBag.buildingList = new SelectList(buildingList, "Value", "Text");

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

            return View();
        }

        public ActionResult SaveSystem()
        {
            return RedirectToAction("Index");
        }

        // GET: System/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: System/Create
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

        // GET: System/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: System/Edit/5
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

        // GET: System/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: System/Delete/5
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
