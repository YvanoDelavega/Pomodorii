using Pomodorii.Models;
using Pomodorii.Web.Services;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Forms;

namespace Pomodorii.web.Pages
{
    public class TomateEditPageBase : ComponentBase, IDisposable
    {
        [Inject]
        public ITomateService TomateService { get; set; }

        [Inject]
        HttpClient HttpClient { get; set; }

        [Inject]
        NavigationManager NavigationManager { get; set; }

        [Parameter]
        public int? tomateId { get; set; }

        protected Tomate tomate { get; set; }

        // mode modification ou ajout d'une tomate ?
        protected private bool modeAjout { get { return !tomateId.HasValue; } }

        protected bool isError = true;

        /// <summary>
        /// permet de gérer les erreurs dans le formulaire
        /// </summary>
        protected private EditContext editContext;

        protected override void OnInitialized()
        {
            tomate = new Tomate();
            // on crée un EditContext par défaut ici sinon ça va planter plus tard
            editContext = new(tomate);
            editContext.OnFieldChanged += HandleFieldChanged;
        }

        protected override async Task OnInitializedAsync()
        {
            // charge la tomate avec son id existe
            if (tomateId.HasValue)
                try
                {
                    tomate = await TomateService.GetTomate(tomateId.Value);
                    isError = tomate == null;
                }
                catch (Exception ex)
                {
                    isError = true;
                    Console.Error.WriteLine(ex);
                }


            if (!isError)
            {
                // on libere le 1er editContext et on en recrée un avec notre tomate fraichement chargées
                Dispose();
                editContext = new(tomate);
                // lorsqu'un champ est modifié, on déclenche un evenement pour controler la validité du formulaire
                editContext.OnFieldChanged += HandleFieldChanged;
            }
        }

        /// <summary>
        /// validation du formulaire : creation ou update d'une tomate
        /// </summary>
        /// <returns></returns>
        protected async Task OnSubmit()
        {
            if (modeAjout)
            {
                var newId = await TomateService.CreateTomate(tomate);
                if (newId > 0) NavigationManager.NavigateTo($"/tomate/{newId}");
            }
            else
            {
                await TomateService.UpdateTomate(tomate);
                NavigationManager.NavigateTo($"/tomate/{tomate.Id}");
            }
        }

        /// <summary>
        /// lorsqu'un champ perd le focus, on valide ou non l'état du formulaire
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void HandleFieldChanged(object sender, FieldChangedEventArgs e)
        {
            isError = !editContext.Validate();
            StateHasChanged();
        }

        /// <summary>
        /// lors du déchargement de la page
        /// </summary>
        public void Dispose()
        {
            // on désabonne l'evenement
            if (editContext != null) editContext.OnFieldChanged -= HandleFieldChanged;
        }

    }
}
