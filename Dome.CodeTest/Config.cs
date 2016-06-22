using Airborne;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Dome.CodeTest
{
    public static class Config
    {
        public static bool IsFailoverModeEnabled
        {
            get
            {
                var IsFailoverModeEnabled = false;
                bool.TryParse(ConfigurationManager.AppSettings["IsFailoverModeEnabled"], out IsFailoverModeEnabled);

                return IsFailoverModeEnabled;
            }
        }
        
        public static int FailoverTimeCapInMinutes
        {
            get
            {
                var timecap = 0;

                int.TryParse(ConfigurationManager.AppSettings["FailoverTimeCapInMinutes"], out timecap);

                Guard.IsTrue(timecap < 0, new Exception("FailoverTimeCapInMinutes not valid."));

                return timecap;
            }
        }

        public static int NumberOfFailedRequests
        {
            get
            {
                var total = 0;

                int.TryParse(ConfigurationManager.AppSettings["NumberOfFailedRequests"], out total);

                Guard.IsTrue(total < 0, new Exception("NumberOfFailedRequests not valid."));

                return total;
            }
        }
    }
}
