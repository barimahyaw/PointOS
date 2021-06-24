using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using PointOS.Common.Helpers.IHelpers;

namespace PointOS.Components
{
    public class CommonComponentBase : ComponentBase
    {
        [Inject]
        protected IRestUtility RestUtility { get; set; }
        [Inject]
        protected NavigationManager NavigationManager { get; set; }
        [Inject]
        protected ISnackbar Snackbar { get; set; }

        [Inject]
        protected ILocalStorageService LocalStorage { get; set; }
    }
}
