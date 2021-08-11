using System;

namespace PointOS.Services
{
    public static class ComponentRefreshService
    {
        public static event Action RefreshRequested;
        public static void CallRequestRefresh()
        {
            RefreshRequested?.Invoke();
        }
    }
}
