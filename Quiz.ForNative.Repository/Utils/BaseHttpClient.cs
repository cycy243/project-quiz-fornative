using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.ForNative.Repository.Utils
{
    public class BaseHttpClient: HttpClient
    {
        public BaseHttpClient(string apiUrl): base() 
        {
            base.BaseAddress = new Uri(apiUrl);
        }

        public override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            try
            {
                return base.SendAsync(request, cancellationToken);
            }
            catch (TaskCanceledException tce)
            {
                // TODO: Throws the right exceptions
                throw new NotImplementedException();
            }
            catch (OperationCanceledException oce)
            {
                // TODO: Throws the right exceptions
                throw new NotImplementedException();
            }
        }
    }
}
