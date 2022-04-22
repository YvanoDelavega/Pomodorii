using Pomodorii.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pomodorii.Api.Models
{
    /// <summary>
    /// contrat du SemiRepository
    /// </summary>
    public interface ISemiRepository
    {        
        Task<IEnumerable<Semi>> GetSemis();
    }
}
