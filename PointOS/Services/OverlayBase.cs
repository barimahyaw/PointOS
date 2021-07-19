using Microsoft.AspNetCore.Components;

namespace PointOS.Services
{
    public class OverlayBase : ComponentBase
    {
        [Parameter]
        public bool IsOverlayVisible { get; set; }

        public void ToggleOverlay(bool value)
        {
            IsOverlayVisible = value;
        }
    }
}
