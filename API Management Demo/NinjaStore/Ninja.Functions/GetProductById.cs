using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using NinjaStore.Common.Models;
using NinjaStore.Common.Repositories;

namespace Ninja.Functions
{
    public static class GetProductById
    {
        [FunctionName("GetProductById")]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "product/{id}")]HttpRequestMessage req, 
            string id,
            TraceWriter log)
        {
            log.Info("GetProductById triggered");

            var repository = new ProductRepository();
            var product = repository.GetProductById(id);

            return product == null
                ? req.CreateResponse(HttpStatusCode.NotFound)
                : req.CreateResponse(HttpStatusCode.OK, product);
        }
    }
}
