
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using NinjaStore.Common.Models;
using NinjaStore.Common.Repositories;

namespace Products
{
    public static class CreateProduct
    {
        [FunctionName("CreateProduct")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "product")]HttpRequest req, ILogger log)
        {
            log.LogInformation("CreateProduct triggered");

            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var input = JsonConvert.DeserializeObject<Product>(requestBody);

            var repository = new ProductRepository();
            var result = repository.CreateProduct(input);

            return new OkObjectResult(result);
        }
    }
}
