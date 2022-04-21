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
        [Inject]
        public ITomateService TomateService { get; set; }

        public IEnumerable<Tomate> Tomates { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Tomates = (await TomateService.GetTomates()).ToList();
        }
    }
}
