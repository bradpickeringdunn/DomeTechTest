using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dome.CodeTest.DataAccess
{
    public interface ISnagDataAccess
    {
        SnagResponse LoadSnag(int snagId);
    }
}
