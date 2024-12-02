using Microsoft.AspNetCore.Authorization;

namespace Api.Middlewares
{
    public class RolesRequirement : IAuthorizationRequirement
    {
        public IEnumerable<Guid> AllowedRoles { get; }

        public RolesRequirement(params Guid[] allowedRoles)
        {
            AllowedRoles = allowedRoles;
        }
    }
}
