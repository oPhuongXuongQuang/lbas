﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LBAS.Web.Controllers
{
    public class SiteController : Controller
    {
        // GET: Site
        public ActionResult Index()
        {
            ViewBag.MenuItem = 4;
            return View();
        }

        // GET: Site/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Site/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Site/Create
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

        // GET: Site/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Site/Edit/5
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

        // GET: Site/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Site/Delete/5
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
