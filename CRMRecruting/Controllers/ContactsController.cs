using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CRMRecruting.Models;

namespace CRMRecruting.Controllers
{
    public class ContactsController : Controller
    {
        private Entities1 db = new Entities1();

        // GET: Contacts
        public ActionResult Index()
        {
            var contacts = db.Contacts.Include(c => c.AspNetUser);
            return View(contacts.ToList());
        }


        // GET: Contacts/Create
        public ActionResult Create()
        {
            ViewBag.Id = new SelectList(db.AspNetUsers, "Id", "Email");
            return View();
        }

        // POST: Contacts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,Phone")] Contact contact)
        {
            string userIdentity = User.Identity.Name;
            contact.Email = db.AspNetUsers.Where(x => x.UserName.Equals(userIdentity)).Select(x => x.Email).FirstOrDefault();
            contact.Id = db.AspNetUsers.Where(x => x.UserName.Equals(userIdentity)).Select(x => x.Id).FirstOrDefault();
            
           
            //string currentRoleId = db.AspNetUsers.Where(x => ).Select(x => x.Name).FirstOrDefault();

            if (ModelState.IsValid)
            {

                db.Contacts.Add(contact);
                db.SaveChanges();
                if(User.IsInRole("Candidate"))
                {
                    return RedirectToAction("Create", "Candidates");
                }
                else
                {
                    return RedirectToAction("Create", "Companies");
                }
                
            }

            ViewBag.Id = new SelectList(db.AspNetUsers, "Id", "Email", contact.Id);
            return View(contact);
        }

        // GET: Contacts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = db.Contacts.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id = new SelectList(db.AspNetUsers, "Id", "Email", contact.Id);
            return View(contact);
        }

        // POST: Contacts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ContactId,Name,Email,Phone,Id")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contact).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id = new SelectList(db.AspNetUsers, "Id", "Email", contact.Id);
            return View(contact);
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
