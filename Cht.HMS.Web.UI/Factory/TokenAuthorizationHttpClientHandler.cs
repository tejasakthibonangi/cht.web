using Cht.HMS.Web.UI.Models;
using Microsoft.Extensions.Options;

namespace Cht.HMS.Web.UI.Factory
{
    public class TokenAuthorizationHttpClientHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ChtsConfig _chtsConfigConfig;


        public TokenAuthorizationHttpClientHandler(IHttpContextAccessor httpContextAccessor, IOptions<ChtsConfig> chtsConfigConfig)
        {
            _httpContextAccessor = httpContextAccessor;
            _chtsConfigConfig = chtsConfigConfig.Value;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {

            var accessToken = _httpContextAccessor.HttpContext.Session.GetString("AccessToken");

            if (!string.IsNullOrEmpty(accessToken))
                request.Headers.Add("Authorization", accessToken);


            return await base.SendAsync(request, cancellationToken);
        }
    }
}
