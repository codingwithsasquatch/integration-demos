
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using Products.Repositories;

namespace Products
{
    public static class Init
    {
        [FunctionName("init")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)]HttpRequest req, 
            ILogger log)
        {
            log.LogInformation("Init function triggered");

            var productRepository = new ProductRepository();
            await productRepository.Initialize();

            return new OkObjectResult("");
        }
    }
}
