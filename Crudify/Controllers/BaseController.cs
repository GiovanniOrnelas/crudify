using Crudify.Commons.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Crudify.Controllers
{
    public class BaseController : Controller
    {
        internal long UserTenant()
        {
            if (User != null || User.Identity.Name != null)
            {
                var tenant = User.Claims?.FirstOrDefault(d => d.Type.Equals("tenantId"))?.Value;
                if (long.TryParse(tenant, out long tenantId))
                    return tenantId;
            }

            throw HttpErrorException.BadRequest("UserNotAllowed");
        }

        internal long UserIdentity(long? id = null)
        {
            if (User == null || User.Identity.Name == null || !long.TryParse(User.Identity.Name, out long userId) || (id.HasValue && id.Value != userId))
                throw HttpErrorException.BadRequest("UserNotAllowed");

            return userId;
        }
    }
}