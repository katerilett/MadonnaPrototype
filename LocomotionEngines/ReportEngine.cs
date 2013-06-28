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
			Report report = new Report();
			//Use ExcelAnalyzer object to get the values.
			ExcelAnalyzer doc = new ExcelAnalyzer();

			//Open Excel
			doc.excel_init(path);
			string A2 = doc.excel_getValue("A2");
			if (A2 != "")
			{
				report.AverageGaitSpeed = Convert.ToDouble(A2);
			}
			else
			{
				report.AverageGaitSpeed = 0;
			}
			string B2 = doc.excel_getValue("B2");
			if (B2 != "")
			{
				report.LeftStrideLength = Convert.ToDouble(B2);
			}
			else
			{
				report.LeftStrideLength = 0;
			}
			string C2 = doc.excel_getValue("C2");
			if (C2 != "")
			{
				report.RightStrideLength = Convert.ToDouble(C2);
			}
			else
			{
				report.RightStrideLength = 0;
			}
			string D2 = doc.excel_getValue("D2");
			if (D2 != "")
			{
				report.StancePercent = Convert.ToDouble(D2);
			}
			else
			{
				report.StancePercent = 0;
			}
			string E2 = doc.excel_getValue("E2");
			if (E2 != "")
			{
				report.SwingPercent = Convert.ToDouble(E2);
			}
			else
			{
				report.SwingPercent = 0;
			}
			string F2 = doc.excel_getValue("F2");
			if (F2 != "")
			{
				report.SingleLimbStancePercent = Convert.ToDouble(F2);
			}
			else
			{
				report.SingleLimbStancePercent = 0;
			}
			string G2 = doc.excel_getValue("G2");
			if (G2 != "")
			{
				report.Candence = Convert.ToDouble(G2);
			}
			else
			{
				report.Candence = 0;
			}

			doc.excel_close();
			return report;
		}
	}
}
