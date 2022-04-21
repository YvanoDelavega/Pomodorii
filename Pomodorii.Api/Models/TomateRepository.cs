using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pomodorii.Models;
using Microsoft.EntityFrameworkCore;

namespace Pomodorii.Api.Models
{
    /// <summary>
    /// Le repository pattern permet d'accéder aux données de la BD tout en isolant la couche d'accès aux données de la couche métier. 
    /// On expose donc les CRUD (Create, Read, Update, Delete) qui seront appelés par la couche métier
    /// </summary>
    public class TomateRepository : ITomateRepository
    {
        /// <summary>
        /// Le DbContext d'entityFramework
        /// </summary>
        private readonly AppDbContext appDbContext;

        public TomateRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        /// <summary>
        /// renvoi la liste des tomates
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Tomate>> GetTomates()
        {
            return await appDbContext.Tomates.ToListAsync();
        }

        /// <summary>
        /// renvoi une tomate
        /// </summary>
        /// <param name="tomateId"></param>
        /// <returns></returns>
        public async Task<Tomate> GetTomate(int tomateId)
        {
            return await appDbContext.Tomates
                .FirstOrDefaultAsync(e => e.Id == tomateId);
        }

        /// <summary>
        /// renvoi une tomate grâce à son nom
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<Tomate> GetTomateByName(string name)
        {
            return await appDbContext.Tomates
                .FirstOrDefaultAsync(e => e.Nom == name);
        }

        /// <summary>
        /// ajouter une tomate
        /// </summary>
        /// <param name="tomate"></param>
        /// <returns></returns>
        public async Task<Tomate> AddTomate(Tomate tomate)
        {
            var result = await appDbContext.Tomates.AddAsync(tomate);
            await appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        /// <summary>
        /// maj d'une tomate
        /// </summary>
        /// <param name="tomate"></param>
        /// <returns></returns>
        public async Task<Tomate> UpdateTomate(Tomate tomate)
        {
            var result = await appDbContext.Tomates
                .FirstOrDefaultAsync(e => e.Id == tomate.Id);

            if (result != null)
            {
                result.Nom = tomate.Nom;
                result.Description = tomate.Description;
                result.ImageUrl = tomate.ImageUrl;

                await appDbContext.SaveChangesAsync();

                return result;
            }

            return null;
        }

        /// <summary>
        /// suppression d'une tomate
        /// </summary>
        /// <param name="tomateId"></param>
        /// <returns></returns>
        public async Task<Tomate> DeleteTomate(int tomateId)
        {
            var result = await appDbContext.Tomates
                .FirstOrDefaultAsync(e => e.Id == tomateId);
            if (result != null)
            {
                appDbContext.Tomates.Remove(result);
                await appDbContext.SaveChangesAsync();
                return result;
            }

            return null;
        }
    }
}
