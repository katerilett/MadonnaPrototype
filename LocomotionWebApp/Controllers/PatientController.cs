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
	public class PatientController : Controller
	{
		//
		// GET: /Patient/
		[Authorize]
		public ActionResult Index()
		{
			var nvm = new PatientListViewModel();

			using(var c = new DataModelContext())
			{
				nvm.Patients = c.Patients.Include("Therapist").Where(n => n.LastName != null).ToList();
			}

			object alertObject;
			if(TempData.TryGetValue("Alert", out alertObject))
			{
				ViewBag.Alert = alertObject;
			}
			
			return View(nvm);
		}

		[Authorize]
		public ActionResult View(long id)
		{
			var nvm = new PatientViewModel();

			using(var c = new DataModelContext())
			{
				var patient = c.Patients.Find(id);
				if(patient == null)
				{
					TempData["Alert"] = "The selected patient does not exist.";
					return RedirectToAction("Index");
				}								

				nvm.ID = patient.ID;
				nvm.FirstName = patient.FirstName;
				nvm.LastName = patient.LastName;
				nvm.Therapist = patient.Therapist;
				nvm.LastUpdate = patient.LastUpdate;
				nvm.Start = patient.Start;
				nvm.Age = patient.Age;
				nvm.Birthday = patient.Birthday;
				nvm.Gender = patient.Gender;
				nvm.Height = patient.Height;
				nvm.Weight = patient.Weight;
				nvm.ArthritisType = patient.ArthritisType;
				nvm.AffectedExtremity = patient.AffectedExtremity;
				nvm.Deformity = patient.Deformity;
				nvm.ShankLength = patient.ShankLength;
				nvm.ThighLength = patient.ThighLength;
				nvm.Email = patient.Email;
				nvm.PhoneNumber = patient.PhoneNumber;
				nvm.ContactName = patient.ContactName;
				nvm.ContactRelation = patient.ContactRelation;
				nvm.ContactPhoneNumber = patient.ContactPhoneNumber;
				nvm.ContactEmail = patient.ContactEmail;

				nvm.Report = patient.ReportResult;

				//var nameNet = network;
				//while(nameNet.Parent != null && nameNet.Name == null)
				//	nameNet = nameNet.Parent;
				//nvm.Name = nameNet.Name;

				if(patient.ReportResult == null)
				{
					nvm.OutOfDate = false;
					//nvm.Optimized = false;
				}
				else
				{
					nvm.OutOfDate = patient.ReportResult.OutOfDate;
					//nvm.Optimized = true;
				}
			}

			return View("View", nvm);
		}

		[Authorize]
		public ActionResult Report(long id)
		{
			var nvm = new PatientViewModel();

			using (var c = new DataModelContext())
			{
				User currentUser = UserDataEngine.getInstance().GetCurrentUser(c, HttpContext);
				
				var patient = c.Patients.Find(id);
				if (patient == null)
				{
					TempData["Alert"] = "The selected patient does not exist.";
					return RedirectToAction("Index");
				}

				nvm.ID = patient.ID;
				nvm.FirstName = patient.FirstName;
				nvm.LastName = patient.LastName;
				nvm.Therapist = patient.Therapist;
				nvm.LastUpdate = patient.LastUpdate;
				nvm.ArthritisType = patient.ArthritisType;
				nvm.AffectedExtremity = patient.AffectedExtremity;
				nvm.Deformity = patient.Deformity;
				nvm.ShankLength = patient.ShankLength;
				nvm.ThighLength = patient.ThighLength;
				nvm.Report = patient.ReportResult;

				//nvm.ReportResult.AverageGaitSpeed = patient.ReportResult.AverageGaitSpeed;
				//nvm.ReportResult.LeftStrideLength = patient.ReportResult.LeftStrideLength;
				//nvm.ReportResult.RightStrideLength = patient.ReportResult.RightStrideLength;
				//nvm.ReportResult.StancePercent = patient.ReportResult.StancePercent;
				//nvm.ReportResult.SwingPercent = patient.ReportResult.SwingPercent;
				//nvm.ReportResult.SingleLimbStancePercent = patient.ReportResult.SingleLimbStancePercent;
				//nvm.ReportResult.Candence = patient.ReportResult.Candence;

			}
			return View(nvm);

		}


		[Authorize]
		public ActionResult Delete(long id)
		{
			using(var c = new DataModelContext())
			{
				var patient = c.Patients.Find(id);
				patient.LastName = null;
				c.SaveChanges();
			}
			return RedirectToAction("Index");
		}

		//Network History
		//[Authorize]
		//public ActionResult NetworkHistory(long id)
		//{
		//	var nvm = new NetworkListViewModel();
		//	nvm.Networks = new List<Network>();

		//	using(var c = new DataModelContext())
		//	{
		//		var network = c.Networks
		//			.Include("Author")
		//			.Include("Parent")
		//			.SingleOrDefault(n => n.ID == id);
		//		while(true)
		//		{
		//			nvm.Networks.Add(network);
		//			if (network.Parent == null)
		//				break;
		//			network = c.Networks.Include("Author").Include("Parent")
		//				.SingleOrDefault(n => n.ID == network.Parent.ID);
		//		}
		//	}

		//	return View(nvm);
		//}

		[Authorize]
		public ActionResult TempEdit(long id, int clickTab)
		{
			//Patient newPatient;
			int tab = 0;
			tab = clickTab;

			long patientID = id;
			/*
			using(var c = new DataModelContext())
			{
				var originalPatient = c.Patients.Find(id);

				newPatient = (Patient)originalPatient.Clone();
				newPatient.Therapist = UserDataEngine.getInstance().GetCurrentUser(c, HttpContext);
				newPatient.LastUpdate = DateTime.Now;
				//newPatient.Parent = originalNetwork;
				//newPatient.Revision = originalNetwork.Revision + 1;

				c.Patients.Add(newPatient);
				c.SaveChanges();
			}*/

			//ViewBag.ID = newPatient.ID;

			return RedirectToAction("View", new { id = patientID, tab = tab });
		}

		//OptimizationSidebar
		//[Authorize]
		//public ActionResult OptimizationSidebar(long id, int startTab = 1)
		//{
		//	var osvm = new OptimizationSidebarViewModel();
		//	osvm.StartTab = startTab;
		//	using(var c = new DataModelContext())
		//	{
		//		var network = c.Networks
		//			.Include("OptimizationResult")
		//			.Include("OptimizationResult.Nodes")
		//			.Include("OptimizationResult.Nodes.Node")
		//			.Include("OptimizationResult.Links")
		//			.Include("OptimizationResult.Links.Link")
		//			.FirstOrDefault(n => n.ID == id);
		//		osvm.Optimization = network.OptimizationResult;
		//	}
		//	return View(osvm);
		//}

		//[Authorize]
		//public JsonResult GetNetwork(long id)
		//{
			//using(var c = new DataModelContext())
			//{
			//	Network net = c.Networks.Find(id);
			//	Optimization opt = net.OptimizationResult;


			//	if(net == null)
			//	{
			//		return Json(new {
			//				failure=true, failureMessage="The selected network does not exist."
			//			},
			//			JsonRequestBehavior.AllowGet);
			//	}

			//	if(opt == null)
			//	{
			//		return Json(new
			//		{		
			//	MaxCarsPerTrain = net.MaxCarsPerTrain,
			//	NonFuelCostPerMile = net.NonFuelCostPerMile,
			//	FuelCostPerMile = net.FuelCostPerMile,
			//	CarCostPerMile = net.CarCostPerMile,

						//nodes = net.Nodes.ToDictionary(
						//	n => n.ID.ToString(),
						//	n => new
						//	{
						//	id = n.ID,
						//		name = n.Name,
						//		stationcode = n.StationCode,
						//		longitude = n.Location.Longitude,
						//		latitude = n.Location.Latitude,
						//		carCapacity = n.CarCapacity,
						//	}
						//),
					//nodesArray = net.Nodes.Select(n => new
					//{
					//	id = n.ID,
					//	name = n.Name,
					//	stationcode = n.StationCode,
					//	longitude = n.Location.Longitude,
					//	latitude = n.Location.Latitude,
					//	carCapacity = n.CarCapacity,
					//}),
					//	links = net.Links.Select(l => new {
					//		id = l.ID,
					//		fromid = l.From.ID,
					//		toid = l.To.ID,
					//		distance = l.Distance,
					//		maxTrains = l.MaxTrains,
					//		fuelAdjustment = l.FuelAdjustment
					//	}),

					//	orders = net.Orders.Select(l => new
					//	{
					//		id = l.ID,
					//		xmlID = l.XMLOrderID,
					//		fromid = l.Origin.ID,
					//		toid = l.Destination.ID,
					//		cars = l.Cars,
					//		revenue = l.Revenue
					//	})

				//	}, JsonRequestBehavior.AllowGet);
				//}

				//return Json(new
				//{
				//	MaxCarsPerTrain = net.MaxCarsPerTrain,
				//	NonFuelCostPerMile = net.NonFuelCostPerMile,
				//	FuelCostPerMile = net.FuelCostPerMile,
				//	CarCostPerMile = net.CarCostPerMile,

					//nodes = net.Nodes.ToDictionary(
					//	n => n.ID.ToString(),
					//	n => new
					//	{   id = n.ID,
					//		name = n.Name,
					//		stationcode = n.StationCode,
					//		longitude = n.Location.Longitude,
					//		latitude = n.Location.Latitude,
					//		carCapacity = n.CarCapacity,
					//	}
					//),
					//nodesArray = net.Nodes.Select(n => new
					//{   name = n.Name,
					//	id = n.ID,
					//	stationcode = n.StationCode,
					//	longitude = n.Location.Longitude,
					//	latitude = n.Location.Latitude,
					//	carCapacity = n.CarCapacity,
					//}),
					//links = net.Links.Select(l => new {
					//	id = l.ID,
					//	fromid = l.From.ID,
					//	toid = l.To.ID,
					//	distance = l.Distance,
					//	maxTrains = l.MaxTrains,
					//	fuelAdjustment = l.FuelAdjustment
					//}),

					//orders = net.Orders.Select(l => new
					//{
					//	id = l.ID,
					//	xmlID = l.XMLOrderID,
					//	fromid = l.Origin.ID,
					//	toid = l.Destination.ID,
					//	cars = l.Cars,
					//	revenue = l.Revenue
					//}),


					//optimizedNodes = opt.Nodes.ToDictionary(
					//	n => n.Node.ID.ToString(),
					//	n => new
					//	{
					//		inFlow = n.FlowIn,
					//		outFlow = n.FlowOut,
					//	}
					//),
					//optimizedLinks = opt.Links.ToDictionary(
					//	l => l.Link.ID.ToString(),
					//	l => new
					//	{
					//		currentTrains = l.CurrentTrains,
					//		currentFlow = l.Flow,
					//		supscription = l.TrainSubscription()
					//	}
					//)

		//		}, JsonRequestBehavior.AllowGet);
		//	}
		//}

		[Authorize]
		public ActionResult EditPatient (int PatientAge, string PatientGender, int PatientHeight, int PatientWeight, 
			string PatientEmail, string PatientPhoneNumber, PatientViewModel model)
		{
			var pvm = new PatientViewModel();
			long patientID = 0;

			using (var c = new DataModelContext())
			{
				var patient = c.Patients.Find(model.ID);
				patientID = patient.ID;

				patient.Age = PatientAge;
				patient.Gender = PatientGender;
				patient.Height = PatientHeight;
				patient.Weight = PatientWeight;
				patient.Email = PatientEmail;
				patient.PhoneNumber = PatientPhoneNumber;
				
				c.SaveChanges();
				
			}

			ViewBag.Alert = "Profile update successful";
			ViewBag.AlertClass = "alert-success";
			//return View("View", pvm);
			return RedirectToAction("View", new { id = patientID});
		}

		[Authorize]
		public ActionResult EditContact(string PatientContactName, string PatientContactRelation, 
			string PatientContactPhoneNumber, string PatientContactEmail, PatientViewModel model)
		{
			var pvm = new PatientViewModel();
			long patientID = 0;

			using (var c = new DataModelContext())
			{
				var patient = c.Patients.Find(model.ID);
				patientID = patient.ID;
				patient.ContactName = PatientContactName;
				patient.ContactRelation = PatientContactRelation;
				patient.ContactPhoneNumber = PatientContactPhoneNumber;
				patient.ContactEmail = PatientContactEmail;

				c.SaveChanges();

			}

			ViewBag.Alert = "Profile update successful";
			ViewBag.AlertClass = "alert-success";
			//return View("View", pvm);
			return RedirectToAction("View", new { id = patientID });
		}

		[Authorize]
		public ActionResult EditMedical(string PatientArthritisType, string PatientAffectedExtremity,
			string PatientDeformity, PatientViewModel model)
		{
			var pvm = new PatientViewModel();
			long patientID = 0;

			using (var c = new DataModelContext())
			{
				var patient = c.Patients.Find(model.ID);
				patientID = patient.ID;
				patient.ArthritisType = PatientArthritisType;
				patient.AffectedExtremity = PatientAffectedExtremity;
				patient.Deformity = PatientDeformity;

				c.SaveChanges();

			}

			ViewBag.Alert = "Profile update successful";
			ViewBag.AlertClass = "alert-success";
			//return View("View", pvm);
			return RedirectToAction("View", new { id = patientID });
		}

		[Authorize]
		public ActionResult EditRequired(int PatientShankLength, int PatientThighLength, PatientViewModel model)
		{
			var pvm = new PatientViewModel();
			long patientID = 0;

			using (var c = new DataModelContext())
			{
				var patient = c.Patients.Find(model.ID);
				patientID = patient.ID;
				patient.ShankLength = PatientShankLength;
				patient.ThighLength = PatientThighLength;				

				c.SaveChanges();

			}

			ViewBag.Alert = "Profile update successful";
			ViewBag.AlertClass = "alert-success";
			//return View("View", pvm);
			return RedirectToAction("Report", new { id = patientID });
		}

		[Authorize]
		public ActionResult CreateBlank(string PatientFirstName, string PatientLastName)
		{
			var nvm = new PatientListViewModel();

			if(PatientFirstName == "")
			{
				ViewBag.UploadAlert = "Enter the patient's first name";

				using (var c = new DataModelContext())
				{
					nvm.Patients = c.Patients.Include("Therapist").Where(n => n.LastName != null).ToList();
				}
				return View("Index", nvm);
			}

			if (PatientLastName == "")
			{
				ViewBag.UploadAlert = "Enter the patient's last name";

				using (var c = new DataModelContext())
				{
					nvm.Patients = c.Patients.Include("Therapist").Where(n => n.LastName != null).ToList();
				}
				return View("Index", nvm);
			}

			using(var c = new DataModelContext())
			{
				var patient = new Patient();
				patient.ReportResult = ReportEngine.getInstance().GenerateReport(patient);

				patient.FirstName = PatientFirstName;
				patient.LastName = PatientLastName;
				patient.Therapist = UserDataEngine.getInstance().GetCurrentUser(c, HttpContext);
				patient.LastUpdate = DateTime.Now;
				patient.Start = DateTime.Now;
				patient.Birthday = DateTime.Now;
				patient.Age = 0;
				patient.Gender = "Not entered";
				patient.Height = 0;
				patient.Weight = 0;
				patient.ArthritisType = "Not entered";
				patient.AffectedExtremity = "Not entered";
				patient.Deformity = "Not entered";
				patient.ShankLength = 0;
				patient.ThighLength = 0;
				patient.PhoneNumber = "Not entered";
				patient.Email = "Not entered";
				patient.ContactName = "Not entered";
				patient.ContactRelation = "Not entered";
				patient.ContactPhoneNumber = "Not entered";
				patient.ContactEmail = "Not entered";

				//patient.ReportResult.AverageGaitSpeed = 0;
				//patient.ReportResult.LeftStrideLength = 0;
				//patient.ReportResult.RightStrideLength = 0;
				//patient.ReportResult.StancePercent = 0;
				//patient.ReportResult.SwingPercent = 0;
				//patient.ReportResult.SingleLimbStancePercent = 0;
				//patient.ReportResult.Candence = 0;

				c.Patients.Add(patient);

				try
				{
					c.SaveChanges();
				}
				catch(DbEntityValidationException e)
				{
					foreach(var i in e.EntityValidationErrors)
					{
						Console.WriteLine(i.ValidationErrors);
					}
					throw e;
				}
				nvm.Patients = c.Patients.Include("Therapist").Where(n => n.LastName != null).ToList();

				ViewBag.NewPatientID = patient.ID;
			}

			ViewBag.Alert = "Patient upload successful";
			ViewBag.AlertClass = "alert-success";
			return View("Index", nvm);
		}

		[Authorize]
		public ActionResult Upload(IEnumerable<HttpFileCollection> files, PatientViewModel model)
		{			
			var pvm = new PatientViewModel();
			long patientID = 0;

			//HttpFileCollection someFiles = files;
			//IEnumerable<HttpPostedFileBase> someFiles = files;			

			try
			{
				//patientDoc = XDocument.Load(Request.Files["PatientFile"].InputStream);
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				ViewBag.UploadAlert = "You must select a valid file";
			}

			using(var c = new DataModelContext())
			{
				var patient = c.Patients.Find(model.ID);
				patientID = patient.ID;
				HttpPostedFileBase patientDoc;
				patientDoc = Request.Files["PatientFile"];

				string fileNameExtension = System.IO.Path.GetFullPath(patientDoc.FileName);

				//var reportE = new ReportEngine();
				pvm.Report = ReportEngine.getInstance().GenerateReport(patient, fileNameExtension);

				//reportE.GenerateReport(patient, fileNameExtension);

				try
				{
					c.SaveChanges();
				}
				catch(DbEntityValidationException e)
				{
					foreach(var i in e.EntityValidationErrors)
					{
						Console.WriteLine(i.ValidationErrors);
					}
					throw e;
				}
				//nvm.Patients = c.Patients.Include("Therapist").Where(n => n.Name != null).ToList();

				//ViewBag.NewNetworkID = xmlnetwork.ID;
			}

			ViewBag.Alert = "Upload successful";
			ViewBag.AlertClass = "alert-success";
			//return View("Report", nvm);
			return RedirectToAction("Report", new { id = patientID });
		}

		#region URLUpload
		//[Authorize]
		//public ActionResult UploadUrl(string NetworkName, string NetworkUrl)
		//{
		//	var nvm = new NetworkListViewModel();

		//	//Network net;

		//	if(NetworkName == "")
		//	{
		//		ViewBag.UrlUploadAlert = "Enter a network name";

		//		using (var c = new DataModelContext())
		//		{
		//			nvm.Networks = c.Networks.Include("Author").Where(n => n.Name != null).ToList();
		//		}
		//		return View("Index", nvm);
		//	}

		//	try
		//	{
		//		//net = new XmlEngine().UrlDownloadToNetwork(new Uri(NetworkUrl));
		//	}
		//	catch (Exception e)
		//	{
		//		Console.WriteLine(e.Message);
		//		ViewBag.UrlUploadAlert = "Could not find valid XML file at specified URL";

		//		using (var c = new DataModelContext())
		//		{
		//			nvm.Networks = c.Networks.Include("Author").Where(n => n.Name != null).ToList();
		//		}
		//		return View("Index", nvm);
		//	}

		//	using(var c = new DataModelContext())
		//	{
		//		//net.Name = NetworkName;
		//		//net.Author = UserDataEngine.getInstance().GetCurrentUser(c, HttpContext);
		//		//net.LastEdit = DateTime.Now;
		//		//c.Networks.Add(net);

		//		try
		//		{
		//			c.SaveChanges();
		//		}
		//		catch(DbEntityValidationException e)
		//		{
		//			foreach(var i in e.EntityValidationErrors)
		//			{
		//				Console.WriteLine(i.ValidationErrors);
		//			}
		//			throw e;
		//		}
		//		nvm.Networks = c.Networks.Include("Author").Where(n => n.Name != null).ToList();

		//		//ViewBag.NewNetworkID = net.ID;
		//	}

		//	ViewBag.Alert = "Network upload successful";
		//	ViewBag.AlertClass = "alert-success";
		//	return View("Index", nvm);
		//}
		#endregion

		//[Authorize]
		//public ActionResult SaveNetwork(NetworkViewModel model)
		//{
		//	using (var c = new DataModelContext())
		//	{
		//		var net = c.Networks.Find(model.ID);
		//		net.LastEdit = DateTime.Now;

		//		net.Name = net.Parent.Name;
		//		net.Parent.Name = null;

		//		c.SaveChanges();
		//	}
		//	return TempEdit(model.ID, 0);
		//}

		//[Authorize]
		//public ActionResult SaveNetworkAs(NetworkViewModel model)
		//{
		//	using (var c = new DataModelContext())
		//	{
		//		var net = c.Networks.Find(model.ID);
		//		net.Name = model.Name;
		//		net.LastEdit = DateTime.Now;
		//		c.SaveChanges();
		//	}
		//	return TempEdit(model.ID, 0);
		//}

		
	}
}
