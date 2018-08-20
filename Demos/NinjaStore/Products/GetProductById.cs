
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
    public static class GetProductById
    {
        [FunctionName("GetProductById")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "product/{id}")]HttpRequest req, 
            ILogger log,
            string id)
        {
            log.LogInformation("GetProductById triggered");

            var repository = new ProductRepository();
            var product = repository.GetProductById(id);

            return new OkObjectResult(product);
        }
    }
}
