using System;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Dome.CodeTest
{
    public class SnagService
    {
        public Snag GetSnag(int snagId, bool isSnagArchived)
        {
            Snag archivedSnag = null;

            if (isSnagArchived)
            {
                var archivedDataService = new ArchivedDataService();
                archivedSnag = archivedDataService.GetArchivedSnag(snagId);

                return archivedSnag;
            }
            else
            {
                var failoverRespository = new FailoverRepository();
                var failoverEntries = failoverRespository.GetFailOverEntries();

                var failedRequests = 0;

                foreach (var failoverEntry in failoverEntries)
                {
                    if (failoverEntry.DateTime > DateTime.Now.AddMinutes(-10))
                    {
                        failedRequests++;
                    }
                }

                SnagResponse snagResponse = null;
                Snag snag = null;

                if (failedRequests > 100 && (ConfigurationManager.AppSettings["IsFailoverModeEnabled"] == "true" || ConfigurationManager.AppSettings["IsFailoverModeEnabled"] == "True"))
                {
                    snagResponse = FailoverSnagDataAccess.GetSnagById(snagId);
                }
                else
                {
                    var dataAccess = new SnagDataAccess();
                    snagResponse = dataAccess.LoadSnag(snagId);
                }

                if (snagResponse.IsArchived)
                {
                    var archivedDataService = new ArchivedDataService();
                    snag = archivedDataService.GetArchivedSnag(snagId);
                }
                else
                {
                    snag = snagResponse.Snag;
                }

                return snag;
            }
        }
    }
}
