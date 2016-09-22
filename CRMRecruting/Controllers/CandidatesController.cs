using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CRMRecruting.Models;
using Microsoft.Reporting.WebForms;
using System.IO;

namespace CRMRecruting.Controllers
{

    public class CandidatesController : Controller
    {
        private Entities1 db = new Entities1();

        // GET: Candidates
        public ActionResult Index()
        {
            var candidates = db.Candidates.Include(c => c.CandidateBill).Include(c => c.Contact);
            return View(candidates.ToList());
        }

        // GET: Candidates/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Candidate candidate = db.Candidates.Find(id);
            if (candidate == null)
            {
                return HttpNotFound();
            }
            return View(candidate);
        }

        public ActionResult CandidatesReport(string id)
        {
            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/CandidatesReport"), "CandidatesReport.rdlc");
            if(System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("Index");
            }

            List<Candidate> listOfCandidates = new List<Candidate>();
            using (Entities1 db = new Entities1())
            {
                listOfCandidates = db.Candidates.ToList();
            }

            ReportDataSource rds = new ReportDataSource("CandidatesDataSet", listOfCandidates);
            lr.DataSources.Add(rds);
            string reportType = id;
            string mimeType;
            string encoding;
            string fileExtension;


            string deviceInfo =
                "<DeviceInfo>" +
                "<OutputFormat>" + id + "</OutputFormat>" +
                //" <PageWidth>8.5inc</PageWidth>" +
                // " <PageHeight>11inc</PageHeight>" +
                // "<MarginTop>0.5inc</MarginTop" +
                // "<MarginLeft>0.5inc</MarginLeft" +
                // "<MarginRight>0.5inc</MarginRight>"+
                // "<MarginBottom>0.5inc</MarginBottom"+
                 "</DeviceInfo>";
            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;

            renderedBytes = lr.Render(reportType, deviceInfo,
                out mimeType, out encoding, out fileExtension,
                out streams, out warnings);
            return File(renderedBytes, mimeType);
        }

        // GET: Candidates/Create
        public ActionResult Create()
        {
            ViewBag.BillId = new SelectList(db.CandidateBills, "Id", "Id");
            ViewBag.ContactId = new SelectList(db.Contacts, "ContactId", "Name");
            return View();
        }

        // POST: Candidates/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,University,PersonalStatement,ContactId,BillId")] Candidate candidate)
        {
            if (ModelState.IsValid)
            {
                db.Candidates.Add(candidate);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BillId = new SelectList(db.CandidateBills, "Id", "Id", candidate.BillId);
            ViewBag.ContactId = new SelectList(db.Contacts, "ContactId", "Name", candidate.ContactId);
            return View(candidate);
        }

        // GET: Candidates/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Candidate candidate = db.Candidates.Find(id);
            if (candidate == null)
            {
                return HttpNotFound();
            }
            ViewBag.BillId = new SelectList(db.CandidateBills, "Id", "Id", candidate.BillId);
            ViewBag.ContactId = new SelectList(db.Contacts, "ContactId", "Name", candidate.ContactId);
            return View(candidate);
        }

        // POST: Candidates/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,University,PersonalStatement,ContactId,BillId")] Candidate candidate)
        {
            if (ModelState.IsValid)
            {
                db.Entry(candidate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BillId = new SelectList(db.CandidateBills, "Id", "Id", candidate.BillId);
            ViewBag.ContactId = new SelectList(db.Contacts, "ContactId", "Name", candidate.ContactId);
            return View(candidate);
        }

        // GET: Candidates/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Candidate candidate = db.Candidates.Find(id);
            if (candidate == null)
            {
                return HttpNotFound();
            }
            return View(candidate);
        }

        // POST: Candidates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Candidate candidate = db.Candidates.Find(id);
            db.Candidates.Remove(candidate);
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
