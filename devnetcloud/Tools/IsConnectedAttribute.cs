using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace devnetcloud.Tools
{
    public class IsConnectedAttribute : TypeFilterAttribute
    {
        public IsConnectedAttribute() : base(typeof(IsConnectedFilter))
        {
        }
    }

    public class IsConnectedFilter : IAuthorizationFilter
    {
        private readonly SessionManager manager;

        public IsConnectedFilter(SessionManager sessionManager)
        {
            manager = sessionManager;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (string.IsNullOrEmpty(manager.Token))
            {
                context.Result = new RedirectToRouteResult(new { action = "Index", Controller = "Home" });
            }
        }
    }
}
