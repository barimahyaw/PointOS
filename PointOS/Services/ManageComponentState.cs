using System;

namespace PointOS.Services
{
    public static class ManageComponentState
    {
        public static event Action RefreshRequested;
        public static void CallRequestRefresh()
        {
            RefreshRequested?.Invoke();
        }
    }
}
