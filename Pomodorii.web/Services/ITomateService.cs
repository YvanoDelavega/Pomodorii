using Pomodorii.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pomodorii.Web.Services
{
    public interface ITomateService
    {
        Task<IEnumerable<Tomate>> GetTomates();
        Task<Tomate> GetTomate(int id);
        Task<Tomate> UpdateTomate(Tomate updatedTomate);
        Task<int> CreateTomate(Tomate newTomate);
        Task DeleteTomate(int id);
    }
}
