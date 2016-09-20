using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRMRecruting.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CRMRecruting.Controllers
{
    public class RolesController : Controller
    {
        private ApplicationDbContext content = new ApplicationDbContext();
        // GET: Roles
        public ActionResult Index()
        {
            return View(content.Roles.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                content.Roles.Add(new IdentityRole()
                {
                    Name = collection["RoleName"]
                });


                content.SaveChanges();
                ViewBag.ResultMessage = "Role created succesfully!";
                return View("Create");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(string RoleName)
        {
            var thisRole = content.Roles.Where(r => r.Name.Equals(RoleName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            content.Roles.Remove(thisRole);
            content.SaveChanges();
            return RedirectToAction("Create");
        }


        public ActionResult Edit(string roleName)
        {
            var thisRole = content.Roles.Where(r => r.Name.Equals(roleName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            return View(thisRole);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(IdentityRole role)
        {
            try
            {
                content.Entry(role).State = System.Data.Entity.EntityState.Modified;
                content.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}