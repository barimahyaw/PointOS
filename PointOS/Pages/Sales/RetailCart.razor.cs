using Microsoft.AspNetCore.Components;
using MudBlazor;
using PointOS.Common.DTO.Response;
using System.Collections.Generic;

namespace PointOS.Pages.Sales
{
    public partial class RetailCart
    {
        [Inject]
        private IDialogService DialogService { get; set; }
        public string[] PagerDropDown { get; set; } = { "All", "5", "10", "15", "20" };

        public bool IsCartEmpty { get; set; }
        public double SubTotal { get; set; }
        public double Tax { get; set; } = 0.0;

        [Parameter]
        public IList<ProductResponse> Products { get; set; } = new List<ProductResponse>();

        /// <summary>
        /// Generic wrapper to call the dialog component
        /// </summary>
        /// <param name="header"></param>
        /// <param name="content"></param>
        public void Dialog(string header, string content)
        {
            var parameters = new DialogParameters { { "ContentText", content } };

            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

            DialogService.Show<Utilities.Dialog>(header, parameters, options);
        }

        public void Save() { }

        private void Checkout()
        {
            if (Products.Count <= 0)
            {
                Dialog("Cart Empty", "There is no product in the Cart");
            }
            else
            {
                var parameters = new DialogParameters();
                parameters.Add("Products", Products);
                DialogService.Show<Checkout>("Summary", parameters);
            }
        }

        public void ResetCart() => Products = new List<ProductResponse>();

        public void RefreshState() => StateHasChanged();
    }
}
