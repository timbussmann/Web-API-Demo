namespace DemoApi.Filters
{
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http.Controllers;
    using System.Web.Http.Filters;

    public class NotNullAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (actionContext.ActionArguments.ContainsValue(null))
            {
                actionContext.Response = actionContext.Request.CreateResponse(
                    HttpStatusCode.BadRequest,
                    string.Format(
                        "The argument cannot be null: {0}",
                        actionContext.ActionArguments
                            .Where(kvp => kvp.Value == null)
                            .Select(kvp => kvp.Key)
                            .FirstOrDefault()));
            }
        }
    }
}