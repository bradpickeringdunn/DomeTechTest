using Dome.CodeTest.Models;

namespace Dome.CodeTest.DataAccess
{
    public class FailoverSnagDataAccess : IFailoverSnagDataAccess
    {
        public SnagResponse GetSnagById(int id)
        {
            // Retrieve snag from database
            return new SnagResponse();
        }
    }
}