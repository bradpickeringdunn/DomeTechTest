using Dome.CodeTest.Models;

namespace Dome.CodeTest.DataAccess
{
    public interface IFailoverSnagDataAccess
    {
        SnagResponse GetSnagById(int id);
    }
}
