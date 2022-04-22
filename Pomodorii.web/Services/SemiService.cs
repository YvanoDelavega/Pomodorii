using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Pomodorii.Models;
using Microsoft.AspNetCore.Components;

namespace Pomodorii.Web.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class SemiService : ISemiService
    {
        private readonly HttpClient httpClient;

        public SemiService(HttpClient httpClient, NavigationManager navigationManager)
        {
            this.httpClient = httpClient;
            // initialise l'adresse de base du client avec celle du serveur pour qu'il sache où envoyer les requêtes
            httpClient.BaseAddress = new Uri(navigationManager.BaseUri);
        }

        public async Task<IEnumerable<Semi>> GetSemis()
        {
            return await httpClient.GetFromJsonAsync<Semi[]>("api/semis");
        }
    }
}
