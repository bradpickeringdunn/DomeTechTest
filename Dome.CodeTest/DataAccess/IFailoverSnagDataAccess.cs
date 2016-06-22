using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dome.CodeTest.DataAccess
{
    public interface IFailoverSnagDataAccess
    {
        SnagResponse GetSnagById(int id);
    }
}
