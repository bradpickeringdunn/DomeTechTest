using Airborne;
using Dome.CodeTest.Models;
using System;
using System.Collections.Generic;

namespace Dome.CodeTest.Rules
{
    public static class SnagRules
    {
        public static bool InFailoverMode(List<FailoverEntry> failoverEntries)
        {
            Guard.ArgumentNotNull(failoverEntries, "failoverEntries");

            var failedRequests = 0;

            foreach (var failoverEntry in failoverEntries)
            {
                if (failoverEntry.DateTime > DateTime.Now.AddMinutes(-Config.FailoverTimeCapInMinutes))
                {
                    failedRequests++;
                }
            }

            return failedRequests > Config.NumberOfFailedRequests && Config.IsFailoverModeEnabled;

        }
    }
}
