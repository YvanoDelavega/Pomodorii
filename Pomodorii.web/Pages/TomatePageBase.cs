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

        public Tomate? Tomate { get; set; }
        [Parameter]
        public int? tomateId { get; set; }
        protected bool notFound = false;
        protected bool isBusy = true;

        protected string ImgTomate { get { return Tomate != null ? Tomate.ImageUrlDefaut : Constants.IMG_DEFAULT; } }

        protected override async Task OnInitializedAsync()
        {            
            try
            {
                Tomate = await TomateService.GetTomate(tomateId.Value);
            }
            catch (Exception ex)
            {
                notFound = true;
                Console.Error.WriteLine(ex);
            }
            finally
            {
                isBusy = false;
            }
        }


        

        
    }
}
