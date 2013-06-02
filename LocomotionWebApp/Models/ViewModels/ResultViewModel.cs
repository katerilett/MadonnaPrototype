using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataModel.Class;
using LocomotionEngines;

namespace LocomotionWebApp.Models.ViewModels
{
	public class ResultViewModel
	{
		public long ID { get; set; }

		public string Name { get; set; }

		public Report Report { get; set; }
	}
}