using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel.Class;
using System.IO;

namespace LocomotionEngines
{
	//Previously public sealed class
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

		public Report GenerateReport(Patient net)
		{
			Report report = new Report();

			return report;
		}


		public Report GenerateReport(Patient net, string path)
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
				report.AverageGaitSpeed = 999999;
			}
			
			//report.ReportedNetwork = net;
			//report.UnoptimizedReport = new UnoptimizedSection();

			//if(optimal != null)
			//{
				//var optimizedReport = new OptimizedSection()
				//{
					//TotalCost = optimal.TotalCost,
					//LinkCosts = new Dictionary<Link, LinkCost>(),
					//RawOptimization = optimal
				//};

				//foreach(var olink in optimal.Links)
				//{
				//	var link = olink.Link;
				//	LinkCost lcost = new LinkCost();

				//	lcost.CarFlowCost = olink.Flow * link.Distance * net.CarCostPerMile;

				//	lcost.LocomotiveCost = olink.CurrentTrains * link.Distance
				//		* (net.FuelCostPerMile + net.NonFuelCostPerMile);

				//	optimizedReport.LinkCosts.Add(link, lcost);
				//}

				//report.OptimizedReport = optimizedReport;
			//}

			return report;
		}
	}
}
