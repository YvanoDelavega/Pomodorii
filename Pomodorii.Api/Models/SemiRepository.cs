using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pomodorii.Models;
using Microsoft.EntityFrameworkCore;

namespace Pomodorii.Api.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class SemiRepository : ISemiRepository
    {
        /// <summary>
        /// Le DbContext d'entityFramework
        /// </summary>
        private readonly AppDbContext appDbContext;

        public SemiRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        /// <summary>
        /// renvoi la liste des semis
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Semi>> GetSemis()
        {
            // on inclus les tomates associés au semi
            return await appDbContext.Semis.Include(t => t.Tomate).ToListAsync();
        }
    }
}
