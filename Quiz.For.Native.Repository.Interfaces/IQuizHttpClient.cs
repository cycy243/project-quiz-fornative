﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.ForNative.Repository.Interfaces
{
    public interface IQuizHttpClient
    {
        /// <summary>
        /// Make post request with data as json.
        /// </summary>
        /// <typeparam name="T">Type of the data</typeparam>
        /// <param name="requestUri">request uri</param>
        /// <param name="data">data to put in the request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>
        /// An HttpResponseMessage containing the request result.
        /// </returns>
        Task<HttpResponseMessage> PostAsJsonAsync<T>(string requestUri, T data, CancellationToken cancellationToken = default);

        Task<HttpResponseMessage> PostAsync<T>(string requestUri, HttpContent data, CancellationToken cancellationToken = default);
    }
}
