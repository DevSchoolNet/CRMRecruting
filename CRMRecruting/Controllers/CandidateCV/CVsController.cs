using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CRMRecruting.Models;

namespace CRMRecruting.Controllers.CandidateCV
{
    public class CVsController : Controller
    {
        private Entities1 db = new Entities1();

        // GET: CVs
        public ActionResult Index()
        {
            
            //var cVs = db.CVs.Include(c => c.Candidate);
            return View();
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
