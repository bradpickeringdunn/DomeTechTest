using Dome.CodeTest.Models;

namespace Dome.CodeTest.Services
{
    public interface IArchivedDataService
    {
        Snag GetArchivedSnag(int snagId);
    }
}
