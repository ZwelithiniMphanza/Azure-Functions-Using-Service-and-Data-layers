namespace IntegrationQueueHttpTrigger
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Azure.WebJobs.Extensions.Http;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using TestApp.Service.Interfaces;
    using TestApp.Data.Models;

    public class QueueHttpTrigger
    {
        private readonly IPersonDatabaseService _personDatabaseService;
        public QueueHttpTrigger(IPersonDatabaseService personDatabaseService){
            _personDatabaseService = personDatabaseService;
        }

        [FunctionName("QueueHttpTrigger")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            //log.LogInformation("C# HTTP trigger function processed a request.");

           // string name = req.Query["name"];
            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync().ConfigureAwait(false);
                PersonModel data = JsonConvert.DeserializeObject<PersonModel>(requestBody);
                var response = await _personDatabaseService.InsertOrMergePersonAsync(data).ConfigureAwait(false);
                log.LogDebug(requestBody);
                return new OkObjectResult(new 
                {
                    success = true,
                    data = response
                });
            }catch(Exception e)
            {
                log.LogError(e, e.Message);
                return new BadRequestObjectResult(new
                {
                    success = false,
                    message = e.Message
                });
            }
        }
    }
}
