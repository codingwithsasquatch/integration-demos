using System.IO;
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
    public static class CreateProduct
    {
        [FunctionName("CreateProduct")]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "product")]HttpRequestMessage req, 
            TraceWriter log)
        {
            log.Info("CreateProduct triggered");

            var requestBody = await req.Content.ReadAsStringAsync();
            var input = JsonConvert.DeserializeObject<Product>(requestBody);

            var repository = new ProductRepository();
            await repository.CreateProduct(input);

            return req.CreateResponse(HttpStatusCode.Accepted);
        }
    }
}
