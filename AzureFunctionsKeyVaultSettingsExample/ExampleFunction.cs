using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace AzureFunctionsKeyVaultSettingsExample
{
    public class ExampleFunction
    {
        private readonly ISampleService sampleService;

        public ExampleFunction(ISampleService sampleService)
        {
            this.sampleService = sampleService;
        }

        /* You can use any Key Vault secret just like any other app setting now. In this example, StorageAccountConnectionString and
         * QueueName are both settings from Key Vault.
         * */

        [FunctionName("ProcessQueueItem")]
        public void ProcessQueueItem([QueueTrigger("%QueueName%", Connection = "StorageAccountConnectionString")]string myQueueItem, ILogger log)
        {
            log.LogInformation($"C# Queue trigger function processed: {myQueueItem}");
            var val = sampleService.GetValue();
            log.LogInformation($"The value from the SampleService is: {val}");
        }
    }
}
