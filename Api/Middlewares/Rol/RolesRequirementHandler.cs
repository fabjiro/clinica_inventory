using Application.Specifications.User;
using Domain.Entities;
using Domain.Interface;
using Domain.Repository;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Api.Middlewares
{
    public class RolesRequirementHandler : AuthorizationHandler<RolesRequirement>
    {
        private readonly IAsyncRepository<UserEntity> _userRepository;

        public RolesRequirementHandler(IAsyncRepository<UserEntity> userRepository)
        {
            _userRepository = userRepository;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, RolesRequirement requirement)
        {
            var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                context.Fail();
                return;
            }

            var user = await _userRepository.FirstOrDefaultAsync(
                new GetUserByIdIncludesSpecifications(Guid.Parse(userId))
            );


            if(user == null)
            {
                context.Fail();
                return;
            }

            if ( requirement.AllowedRoles.Contains(user!.Rol!.RolId))
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }
        }
    }
}
