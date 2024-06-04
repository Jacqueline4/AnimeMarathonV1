using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AnimeMarathon.Web.Pages
{
    public class SessionModel : PageModel
    {
        protected const string SessionKeyName = "_Name";
        protected const string SessionKeyId = "_Id";

        public string CurrentUserName => HttpContext.Session.GetString(SessionKeyName);
        public string CurrentUserId => HttpContext.Session.GetString(SessionKeyId);

        public bool IsUserLoggedIn => !string.IsNullOrEmpty(CurrentUserId);

        public override void OnPageHandlerExecuting(PageHandlerExecutingContext context)
        {
            if (!IsUserLoggedIn)
            {
                context.Result = new RedirectToPageResult("/Login");
            }
        }
    }
}
