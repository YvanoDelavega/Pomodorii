using Pomodorii.Models;
using Pomodorii.Web.Services;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Pomodorii.web.Pages
{
    /// <summary>
    /// on déporte le code c# dans une "classe de base" pour plus de clarté
    /// </summary>
    public class TomatesListPageBase : ComponentBase
    {
        /// <summary>
        /// service pour obtenir les tomates
        /// </summary>
        [Inject]
        public ITomateService TomateService { get; set; }

        /// <summary>
        /// liste des tomates à afficher
        /// </summary>
        public IEnumerable<Tomate> Tomates { get; set; }

        /// <summary>
        /// initialisation de la page
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            // recherche la liste des tomates
            Tomates = (await TomateService.GetTomates()).ToList();
        }
    }
}
