using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.Class
{
	
	public class Patient
	{
		[Key]
		public long ID { get; set; }

		// Change tracking variables
		public string FirstName { get; set; }

		public string LastName { get; set; }

		public virtual User Therapist { get; set; }

		public DateTime LastUpdate { get; set; } //Currently being treated as LastEdit i.e. Start

		public string ArthritisType { get; set; }

		public string AffectedExtremity { get; set; }

		public string Deformity { get; set; }

		public int Age { get; set; }

		public string Gender { get; set; }

		public int Height { get; set; }

		public int Weight { get; set; }

		public DateTime Start { get; set; } //When Therapists adds the patient

		public string Email { get; set; }

		/// <summary>
		/// If a summary has been calculated, it is referenced here.
		/// </summary>
		public virtual Report ReportResult { get; set; }

		/// <summary>
		/// Clones this patient
		/// Does NOT copy the ID
		/// </summary>
		public object Clone()
		{
			Patient net = new Patient()
			{
				Therapist = Therapist,
			};

			return net;
		}
	}
}
