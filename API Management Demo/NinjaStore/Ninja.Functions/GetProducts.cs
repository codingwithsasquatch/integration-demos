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
    public static class GetProducts
    {
        [FunctionName("GetProducts")]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "product")]HttpRequestMessage req, 
            TraceWriter log)
        {
            var repository = new ProductRepository();
            var products = repository.GetAllProducts();

            return req.CreateResponse(HttpStatusCode.OK, products);
        }
    }
}
