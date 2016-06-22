using Airborne;
using Dome.CodeTest.DataAccess;
using Dome.CodeTest.Rules;

namespace Dome.CodeTest.Services
{
    public class SnagService : ISnagService
    {
        private readonly IFailoverSnagDataAccess failoverSnagDataAccess;

        private readonly ISnagDataAccess dataAccess;

        private readonly IArchivedDataService archivedDataService;

        private readonly IFailoverRepository failoverRepository;


        public SnagService(IFailoverSnagDataAccess failoverSnagDataAccess, ISnagDataAccess snagDataAccess, IArchivedDataService archivedDataService, IFailoverRepository failoverRepository)
        {
            Guard.ArgumentNotNull(failoverSnagDataAccess, "failoverSnagDataAccess");
            Guard.ArgumentNotNull(snagDataAccess, "snagDataAccess");
            Guard.ArgumentNotNull(archivedDataService, "archivedDataService");
            Guard.ArgumentNotNull(failoverRepository, "failoverRepository");

            this.failoverSnagDataAccess = failoverSnagDataAccess;
            this.dataAccess = snagDataAccess;
            this.archivedDataService = archivedDataService;
            this.failoverRepository = failoverRepository;
        }

        public Snag GetSnag(int snagId, bool isSnagArchived)
        {
            Snag snag = null;

            if (isSnagArchived)
            {
                snag = archivedDataService.GetArchivedSnag(snagId);
            }
            else
            {
                snag = GetSnag(snagId);
            }

            return snag;
        }

        private Snag GetSnag(int snagId)
        {
            SnagResponse snagResponse = null;

            var failoverEntries = failoverRepository.GetFailOverEntries();
            
            if (SnagRules.InFailoverMode(failoverEntries))
            {
                snagResponse = failoverSnagDataAccess.GetSnagById(snagId);
            }
            else
            {
                snagResponse = dataAccess.LoadSnag(snagId);
            }

            Snag snag = null;

            if (snagResponse.IsArchived)
            {
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
