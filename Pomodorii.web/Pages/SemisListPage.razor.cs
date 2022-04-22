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
    public class SemisListPageBase : ComponentBase
    {
        [Inject]
        public ISemiService SemiService { get; set; }

        public IEnumerable<Semi> Semis { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Semis = (await SemiService.GetSemis()).ToList();
        }
    }
}
