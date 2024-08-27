using Newtonsoft.Json;
using Quiz.Dtos;
using Quiz.ForNative.Repository.Exceptions;
using System.Net;

namespace Quiz.ForNative.Middleware.Http
{
    public class BadRequestHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = await base.SendAsync(request, cancellationToken);

            if (response != null && (response.StatusCode == HttpStatusCode.BadRequest))
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                ApiErrorDto error = JsonConvert.DeserializeObject<ApiErrorDto>(responseContent)!;
                throw new ValidationException($"One or more field aren't valid", error.Errors);
            }

            return response;
        }
    }
}
