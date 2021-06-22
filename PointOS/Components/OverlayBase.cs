using Microsoft.AspNetCore.Components;

namespace PointOS.Components
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
