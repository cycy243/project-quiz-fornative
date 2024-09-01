using Newtonsoft.Json;
using Quiz.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.ForNative.Repository.Http
{
    public static class HttpExtensions
    {
        public static async Task<T> GetContentAsync<T>(this HttpResponseMessage responseMessage)
        {
            string responseContent = await responseMessage.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(responseContent)!;
        }
    }
}
