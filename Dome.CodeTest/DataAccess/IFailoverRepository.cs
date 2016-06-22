using Dome.CodeTest.Models;
using System.Collections.Generic;

namespace Dome.CodeTest.DataAccess
{
    public interface IFailoverRepository
    {
        List<FailoverEntry> GetFailOverEntries();
    }
}
