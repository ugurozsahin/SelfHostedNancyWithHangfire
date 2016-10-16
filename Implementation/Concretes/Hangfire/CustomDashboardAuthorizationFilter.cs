using Hangfire.Dashboard;

namespace Implementation.Concretes.Hangfire
{
    public class CustomDashboardAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            return true;
        }
    }
}