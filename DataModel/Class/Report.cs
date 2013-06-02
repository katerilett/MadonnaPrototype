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

		/// <summary>
		/// Note: does not clone Patient reference.
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
