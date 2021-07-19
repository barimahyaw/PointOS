using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace PointOS.Services
{
    public class PasswordVisibilityBase : ComponentBase
    {
        public string Password { get; set; }

        protected internal bool PasswordVisibility;
        public InputType PasswordInput = InputType.Password;        
        public string PasswordInputIcon = Icons.Material.Filled.VisibilityOff;

        public void TogglePasswordVisibility()
        {
            if (PasswordVisibility)
            {
                PasswordVisibility = false;
                PasswordInputIcon = Icons.Material.Filled.VisibilityOff;
                PasswordInput = InputType.Password;
            }
            else
            {
                PasswordVisibility = true;
                PasswordInputIcon = Icons.Material.Filled.Visibility;
                PasswordInput = InputType.Text;
            }
        }
    }
}
