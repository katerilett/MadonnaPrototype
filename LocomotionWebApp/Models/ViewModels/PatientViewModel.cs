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
		
		public String Name { get; set; }
		
		public bool OutOfDate { get; set; }

		public virtual User Therapist { get; set; }

		public DateTime LastUpdate { get; set; } //Currently being treated as LastEdit i.e. Start

		public string ArthritisType { get; set; }

		public string Deformity { get; set; }

		public int Age { get; set; }

		public char Gender { get; set; }

		public int Weight { get; set; }

		public DateTime Start { get; set; } //When Therapists adds the patient

		public string Email { get; set; }

	}
}