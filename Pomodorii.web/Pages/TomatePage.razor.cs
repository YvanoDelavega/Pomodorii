using Pomodorii.Models;
using Pomodorii.Web.Services;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Pomodorii.web.Pages
{
    public class TomatePageBase : ComponentBase
    {
        [Inject]
        public ITomateService TomateService { get; set; }

        [Inject]
        NavigationManager NavigationManager { get; set; }

        /// <summary>
        /// id de la tomate à afficher passé en paramètre de la page
        /// </summary>
        [Parameter]
        public int? TomateId { get; set; }

        /// <summary>
        ///  tomate à afficher
        /// </summary>
        public Tomate? Tomate { get; set; }

        // est ce que la tomate a été trouvée ?       
        protected bool notFound = false;
        // gestion du busy indicator
        protected bool isBusy = true;

        /// <summary>
        /// image d'une tomate par défaut
        /// </summary>
        protected string ImgTomate { get { return Tomate != null ? Tomate.ImageUrlDefaut : Constants.IMG_DEFAULT; } }

        /// <summary>
        /// chargement de la page
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            try
            {
                // cherche la tomate
                Tomate = await TomateService.GetTomate(TomateId.Value);
            }
            catch (Exception ex)
            {
                // permettra de faire un affichage personalisé si la tomate n'est pas trouvée
                notFound = true;
                Console.Error.WriteLine(ex);
            }
            finally
            {
                // on enleve le busy indicator dans tous les cas
                isBusy = false;
            }
        }

        /// <summary>
        /// suppression d'une tomate
        /// </summary>
        public async void DeleteTomate()
        {
            if (Tomate != null)
            {
                await TomateService.DeleteTomate(Tomate.Id);
                NavigationManager.NavigateTo("/mestomates");
            }
        }
    }
}
