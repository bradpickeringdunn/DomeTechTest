using Dome.CodeTest.Models;

namespace Dome.CodeTest.Services
{
    public interface ISnagService
    {
        Snag GetSnag(int snagId, bool isSnagArchived);
    }
}
