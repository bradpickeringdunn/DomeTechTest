using Dome.CodeTest.Models;

namespace Dome.CodeTest.DataAccess
{
    public class SnagDataAccess : ISnagDataAccess
    {
        public SnagResponse LoadSnag(int snagId)
        {
            // Retrieve snag from 3rd party webservice
            return new SnagResponse();
        }
    }
}