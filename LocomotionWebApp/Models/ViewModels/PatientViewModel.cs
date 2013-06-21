using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataModel.Class;

namespace LocomotionWebApp.Models.ViewModels
{
	public class PatientViewModel
	{
		public long ID { get; set; }
		
		//public int Revision { get; set; }
		
		public String FirstName { get; set; }

		public String LastName { get; set; }
		
		public bool OutOfDate { get; set; }

		public virtual User Therapist { get; set; }

		public DateTime LastUpdate { get; set; } //Currently being treated as LastEdit i.e. Start

		public string ArthritisType { get; set; }

		public string AffectedExtremity { get; set; }

		public string Deformity { get; set; }

		public int ShankLength { get; set; }

		public int ThighLength { get; set; }

		public int Age { get; set; }

		public DateTime Birthday { get; set; }

		public string Gender { get; set; }

		public int Height { get; set; }

		public int Weight { get; set; }

		public DateTime Start { get; set; } //When Therapists adds the patient

		public string PhoneNumber { get; set; }

		public string Email { get; set; }

		public string ContactName { get; set; }

		public string ContactRelation { get; set; }

		public string ContactPhoneNumber { get; set; }

		public string ContactEmail { get; set; }

	}
}