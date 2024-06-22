using System;
using System.Threading.Tasks;
using MediaInAction.EmbyService.Services;
using MediaInAction.Shared.Hosting.AspNetCore;
using Microsoft.AspNetCore.Http;
using Volo.Abp.DependencyInjection;

namespace MediaInAction.PublicWeb.ServiceProviders
{
    public class UserEmbyProvider : ITransientDependency
    {
        private HttpContext HttpContext => _httpContextAccessor.HttpContext;

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEmbyAppService _embyAppService;

        public UserEmbyProvider(
            IHttpContextAccessor httpContextAccessor,
            IEmbyAppService embyAppService)
        {
            _httpContextAccessor = httpContextAccessor;
            _embyAppService = embyAppService;
        }

        public virtual async Task<EmbyDto> GetEmbyAsync()
        {
            var anonymousUserId = await GetAnonymousUserId();

            return await _embyAppService.GetAsync(Guid.Parse(anonymousUserId));
        }

        // Get anonymous user id from cookie
        private async Task<string> GetAnonymousUserId()
        {
            HttpContext.Request.Cookies.TryGetValue(EShopConstants.AnonymousUserClaimName, out string anonymousUserId);
            // Generate guid for anonymous user id and set to cookie for 14 days
            if (string.IsNullOrEmpty(anonymousUserId))
            {
                anonymousUserId = Guid.NewGuid().ToString();
                HttpContext.Response.Cookies.Append(EShopConstants.AnonymousUserClaimName, anonymousUserId,
                    new CookieOptions
                    {
                        SameSite = SameSiteMode.Lax,
                        Expires = DateTimeOffset.UtcNow.AddDays(14)
                    });
            }

            return await Task.FromResult(anonymousUserId);
        }
    }
}