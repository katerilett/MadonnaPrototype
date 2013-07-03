using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.Class
{
	public class MedProfile : ICloneable
	{
		[Key]
		public long ID { get; set; }

		//Past Conditions
		public bool HeartDisease { get; set; }
		public bool Diabetes { get; set; }
		public bool Cancer { get; set; }
		public bool HighBloodPressure { get; set; }

		//Constructor
		public MedProfile() 
		{
			HeartDisease = false;
			Diabetes = false;
			Cancer = false;
			HighBloodPressure = false;
		}
		
		/// <summary>
		/// Note: does not clone Patient reference.
		/// Not used currently. 
		/// </summary>
		public object Clone()
		{
			return new MedProfile()
			{
				
				
			};
		}
	}
}
