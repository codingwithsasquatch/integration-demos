using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using NinjaStore.Common.Repositories;

namespace Ninja.Functions
{
    public static class DeleteProduct
    {
        [FunctionName("DeleteProduct")]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "product/{id}")]HttpRequestMessage req, 
            string id,
            TraceWriter log)
        {
            log.Info("DeleteProduct triggered");

            var repository = new ProductRepository();
            var product = repository.DeleteProduct(id);

            return req.CreateResponse(HttpStatusCode.Accepted);
        }
    }
}

