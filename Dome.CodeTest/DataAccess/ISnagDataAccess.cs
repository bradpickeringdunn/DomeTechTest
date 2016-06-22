using Dome.CodeTest.Models;

namespace Dome.CodeTest.DataAccess
{
    public interface ISnagDataAccess
    {
        SnagResponse LoadSnag(int snagId);
    }
}
