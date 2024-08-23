using System.Net;
using Exception = Quiz.ForNative.Repository.Exceptions;

namespace Quiz.ForNative.Middleware.Http
{
    public class UnAuthorizedAccessHandler: DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = await base.SendAsync(request, cancellationToken);

            if (response != null && (response.StatusCode == HttpStatusCode.Unauthorized || response.StatusCode == HttpStatusCode.Forbidden))
            {
                throw new Exception.UnAuthorizedAccessException("User hasn't the right to consult the ressource.");
            }

            return response;
        }
    }
}
