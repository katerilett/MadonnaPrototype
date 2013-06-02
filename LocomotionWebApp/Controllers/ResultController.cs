using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataModel.Class;
using LocomotionEngines;
using LocomotionWebApp.Models.ViewModels;
using System.Xml;
using System.IO;
using System.Xml.Linq;
using System.Data.Entity.Validation;

namespace LocomotionWebApp.Controllers
{
    public class ResultController : Controller
    {
        //
        // GET: /Result/

        public ActionResult Index()
        {
			var rvm = new ResultViewModel();

			using (var c = new DataModelContext())
			{
				User currentUser = UserDataEngine.getInstance().GetCurrentUser(c, HttpContext);

				//rvm.Networks = c.Patients.Include("Therapist").Where(n =>
				//	n.Name != null &&
				//	n.Therapist.Username == currentUser.Username
				//	).ToList();
			}

			return View(rvm);
            
        }

    }
}
