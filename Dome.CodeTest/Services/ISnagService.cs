using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dome.CodeTest.Services
{
    public interface ISnagService
    {
        Snag GetSnag(int snagId, bool isSnagArchived);
    }
}
