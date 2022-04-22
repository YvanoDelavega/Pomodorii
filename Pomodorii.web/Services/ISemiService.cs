using Pomodorii.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pomodorii.Web.Services
{
    public interface ISemiService
    {
        Task<IEnumerable<Semi>> GetSemis();
    }
}
