using Quiz.ForNative.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Quiz.ForNative.Repository.Http
{
    public class QuizHttpClient: IQuizHttpClient
    {
        HttpClient _httpClient;

        public QuizHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<HttpResponseMessage> PostAsJsonAsync<T>(string requestUri, T data, CancellationToken cancellationToken = default)
        {
            try
            {
                return await _httpClient.PostAsJsonAsync(requestUri, data, cancellationToken);
            }
            catch (TaskCanceledException ex)
            {
                if (ex.CancellationToken == cancellationToken)
                {
                    throw new TimeoutException("The request timed out.", ex);
                }

                throw;
            }
        }

        public async Task<HttpResponseMessage> PostAsync<T>(string requestUri, HttpContent data, CancellationToken cancellationToken = default)
        {
            try
            {
                return await _httpClient.PostAsync(requestUri, data, cancellationToken);
            }
            catch (TaskCanceledException ex)
            {
                if (ex.CancellationToken == cancellationToken)
                {
                    throw new TimeoutException("The request timed out.", ex);
                }

                throw;
            }
        }
    }
}
