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
        protected private EditContext editContext;

        protected override void OnInitialized()
        {
            tomate = new Tomate();
            // on crée le EditContext par défaut ici sinon ça va planter plus tard
            editContext = new(tomate);
            editContext.OnFieldChanged += HandleFieldChanged;
        }

        protected override async Task OnInitializedAsync()
        {
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
                // on libere le 1er editContext et on en recrée un
                Dispose();
                editContext = new(tomate);
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



        public async void DeleteTomate()
        {
            if (tomate != null)
            {
                await TomateService.DeleteTomate(tomate.Id);
                NavigationManager.NavigateTo("/");
            }
        }



        public void HandleFieldChanged(object sender, FieldChangedEventArgs e)
        {
            isError = !editContext.Validate();
            StateHasChanged();
        }

        public void Dispose()
        {
            if (editContext != null) editContext.OnFieldChanged -= HandleFieldChanged;
        }

    }
}
