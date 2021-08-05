using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace PointOS.Pages.ProductCategory
{
    public partial class List
    {
        [Inject]
        private IDialogService DialogService { get; set; }
        public string[] PagerDropDown { get; set; } = { "All", "5", "10", "15", "20" };


        private void OpenDialog()
        {
            DialogService.Show<Add>("Add Product Category");
        }
    }
}
