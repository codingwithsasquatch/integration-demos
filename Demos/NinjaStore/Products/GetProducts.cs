
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using NinjaStore.Common.Repositories;

namespace Products
{
    public static class GetProducts
    {
        [FunctionName("GetProducts")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "product")]HttpRequest req,
            ILogger log)
        {
            log.LogInformation("GetProducts triggered");

            var repository = new ProductRepository();
            var products = repository.GetAllProducts();

            return new OkObjectResult(products);
        }
    }
}
