using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRMRecruting.Controllers
{
    public class MembersController : Controller
    {
        [Authorize(Roles="Candidate")]
        public ActionResult Index()
        {
            return View();
        }
    }
}