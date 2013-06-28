using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.Class
{
	public class Report : ICloneable
	{
		[Key]
		public long ID { get; set; }

		public bool OutOfDate { get; set; }

		//Gait Characteristis
		public double AverageGaitSpeed { get; set; }
		public double LeftStrideLength { get; set; }
		public double RightStrideLength { get; set; }
		public double StancePercent { get; set; }
		public double SwingPercent { get; set; }
		public double SingleLimbStancePercent { get; set; }
		public double Candence { get; set; }

		//Constructor
		public Report() 
		{
			AverageGaitSpeed = 0;
			LeftStrideLength = 0;
			RightStrideLength = 0;
			StancePercent = 0;
			SwingPercent = 0;
			SingleLimbStancePercent = 0;
			Candence = 0;		
		}
		
		/// <summary>
		/// Note: does not clone Patient reference.
		/// Not used currently. 
		/// </summary>
		public object Clone()
		{
			return new Report()
			{
				OutOfDate = OutOfDate,
				
			};
		}
	}
}
