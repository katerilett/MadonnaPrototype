using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel.Class;
using System.IO;

namespace LocomotionEngines
{
	
	public sealed class ReportEngine
	{
		//Implement as a singleton. (Should refactor other engines like this.)
		private static ReportEngine instance;
		public static ReportEngine getInstance()
		{
			if(instance == null)
				instance = new ReportEngine();
			return instance;
		}
		private ReportEngine() {}

		public Report GenerateReport(Patient pat)
		{
			Report report = new Report();

			report.AverageGaitSpeed = 0;
			report.LeftStrideLength = 0;
			report.RightStrideLength = 0;
			report.StancePercent = 0;
			report.SwingPercent = 0;
			report.SingleLimbStancePercent = 0;
			report.Candence = 0;

			return report;
		}


		public Report GenerateReport(Patient pat, string path)
		{
			//Report optimal = net.ReportResult;

			Report report = new Report();
			ExcelAnalyzer doc = new ExcelAnalyzer();

			doc.excel_init(path);
			string A1 = doc.excel_getValue("A1");
			if (A1 != "")
			{
				report.AverageGaitSpeed = Convert.ToDouble(A1);
			}
			else
			{
				report.AverageGaitSpeed = 0;
			}

			return report;
		}
	}
}
