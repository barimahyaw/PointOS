using Microsoft.AspNetCore.Components;
using MudBlazor;
using PointOS.Common.DTO.Request;
using PointOS.Common.Enums;
using PointOS.Services;
using System.Threading.Tasks;

namespace PointOS.Pages.ProductCategory
{
    public partial class Add
    {
        public string FirstName { get; set; }
        public bool FriendSwitch { get; set; } = true;

        public string Message { get; set; }

        [Inject]
        private ISnackbar Snackbar { get; set; }

        [Inject]
        private IDialogService DialogService { get; set; }

        [Inject]
        private IApiEndpointCallService ApiEndpointCallService { get; set; }

        private ProductCategoryRequest ProductCategoryRequest { get; set; } = new ProductCategoryRequest();

        private void OpenDialog(DialogOptions options)
        {
            DialogService.Show<Dialog>("Confirm", options);
            Message = "Are you sure you want add Thinkpad X1 Nano as a Product?";
        }

        protected async Task SaveChanges()
        {
            var response = await ApiEndpointCallService.CallApiService("ProductCategory", ProductCategoryRequest, null, Verb.Post);
            Snackbar.Add(response.Message, Severity.Success, config =>
            {
                config.ShowCloseIcon = false;
            });

        }

        public void ConfirmButtonClicked()
        {

            Snackbar.Add("Product Category saved successfully.", Severity.Success, config =>
            {
                config.ShowCloseIcon = false;
            });

        }


        DialogOptions maxWidth = new DialogOptions() { MaxWidth = MaxWidth.Medium, FullWidth = true };
        DialogOptions closeButton = new DialogOptions() { CloseButton = true/*, MaxWidth = MaxWidth.Small, FullWidth = true*/ };
        DialogOptions noHeader = new DialogOptions() { NoHeader = true };
        DialogOptions disableBackdropClick = new DialogOptions() { DisableBackdropClick = true };
        DialogOptions fullScreen = new DialogOptions() { FullScreen = true, CloseButton = true };


        private void DeleteUser()
        {
            var parameters = new DialogParameters();
            parameters.Add("ContentText", "Do you really want to delete these records? This process cannot be undone.");
            parameters.Add("ButtonText", "Delete");
            parameters.Add("Color", Color.Error);

            var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

            DialogService.Show<Dialog>("Delete", parameters, options);
        }

        private void Confirm()
        {
            var parameters = new DialogParameters();
            parameters.Add("ContentText", "Are you sure you want to remove thisguy@emailz.com from this account?");
            parameters.Add("ButtonText", "Yes");
            parameters.Add("Color", Color.Success);

            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

            DialogService.Show<Dialog>("Confirm", parameters, options);

            //DialogService.ShowMessageBox("", "Are u sure", "Yes", null, "Cancel");
        }

        private void Download()
        {
            var parameters = new DialogParameters();
            parameters.Add("ContentText", "Your computer seems very slow, click the download button to download free RAM.");
            parameters.Add("ButtonText", "Download");
            parameters.Add("Color", Color.Info);

            var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

            DialogService.Show<Dialog>("Slow Computer Detected", parameters, options);
        }
    }
}
