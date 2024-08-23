using Quiz.ForNative.Repository.Exceptions;
using System.Net;

namespace Quiz.ForNative.Middleware.Http
{
    public class ConflictHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = await base.SendAsync(request, cancellationToken);

            if (response != null && (response.StatusCode == HttpStatusCode.Conflict))
            {
                throw new RessourceAlreadyExistsException("User hasn't the right to consult the ressource.");
            }

            return response;
        }
    }
}
