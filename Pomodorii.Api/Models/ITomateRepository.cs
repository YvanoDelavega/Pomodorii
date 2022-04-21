using Pomodorii.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pomodorii.Api.Models
{
    /// <summary>
    /// contrat du TomateRepository
    /// </summary>
    public interface ITomateRepository
    {        
        Task<IEnumerable<Tomate>> GetTomates();
        Task<Tomate> GetTomate(int tomateId);
        Task<Tomate> GetTomateByName(string name);
        Task<Tomate> AddTomate(Tomate tomate);
        Task<Tomate> UpdateTomate(Tomate tomate);
        Task<Tomate> DeleteTomate(int tomateId);
    }
}
