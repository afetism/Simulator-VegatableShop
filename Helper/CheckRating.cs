using f_1.DB;
using f_1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace f_1.Helper
{
    internal static class CheckRating
    {

        public static int  CustomerRating()
        {

            MarketDB<DTO1> weeklyReport = new("weeklyReport.json");
            List<DTO1> days = weeklyReport.GetReports;
            if (days.Any())
                if (days[^1].CustomerCount>=days[^2].CustomerCount) return days[^1].CustomerCount+5; else return days[^1].CustomerCount-5;
            else throw new Exception("Heftelik report yoxdu");
        }

        public static bool VegatableRating(VegetableName vegetableName)
        {
            MarketDB<DTO1> weeklyReport = new("weeklyReport.json");
            List<DTO1> days = weeklyReport.GetReports;
            if (days.Any())
            {
                if (days[^1].VegRating[vegetableName]>days[^2].VegRating[vegetableName]) return true; else return false;

            }
            else
                throw new Exception("Heftelik report yoxdu");



        }


    }
}
