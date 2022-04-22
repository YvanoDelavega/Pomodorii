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
    /// on pourrait appeler l'api directement depuis les composants blazor mais pour plus de clarté,
    /// on gérer les appels depuis une classe "service" qui va appeler les différentes route de notre API
    /// </summary>
    public class TomateService : ITomateService
    {
        private readonly HttpClient httpClient;

        public TomateService(HttpClient httpClient, NavigationManager navigationManager)
        {
            this.httpClient = httpClient;
            // initialise l'adresse de base du client avec celle du serveur pour qu'il sache où envoyer les requêtes
            httpClient.BaseAddress = new Uri(navigationManager.BaseUri);
        }

        public async Task<int> CreateTomate(Tomate newTomate)
        {
            var res = await httpClient.PostAsJsonAsync<Tomate>("api/tomates", newTomate);

            if (res.IsSuccessStatusCode)
            {
                var resTomate = res.Content.ReadFromJsonAsync<Tomate>();
                if (resTomate != null && resTomate.Result != null) return resTomate.Result.Id;
            }

            return -1;
        }

        public async Task DeleteTomate(int id)
        {
            await httpClient.DeleteAsync($"api/tomates/{id}");
        }

        public async Task<Tomate> GetTomate(int id)
        {
            var tomate = await httpClient.GetFromJsonAsync<Tomate>($"api/tomates/{id}");
            if (tomate is null) throw new AggregateException("The tomate could not be de-serialized into json");
            return tomate;
        }

        public async Task<IEnumerable<Tomate>> GetTomates()
        {
            return await httpClient.GetFromJsonAsync<Tomate[]>("api/tomates");
        }

        public async Task<Tomate> UpdateTomate(Tomate updatedTomate)
        {
            var res = await httpClient.PutAsJsonAsync<Tomate>("api/tomates", updatedTomate);
            var t = await res.Content.ReadFromJsonAsync<Tomate>();
            return t;
        }
    }
}
