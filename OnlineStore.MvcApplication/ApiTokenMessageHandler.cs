using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace OnlineStore.MvcApplication
{
    public class ApiTokenMessageHandler : DelegatingHandler
    {
        /// <summary>
        /// HttpContextAccessor.
        /// </summary>
        private readonly IHttpContextAccessor _accessor;

        public ApiTokenMessageHandler(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = _accessor.HttpContext.Request.Cookies["token"];
            request.Headers.Add("Authorization", "Bearer " + token);
            var response = await base.SendAsync(request, cancellationToken);
            return response;
        }
    }
}
