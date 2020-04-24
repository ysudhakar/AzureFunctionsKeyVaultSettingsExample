using System;
using System.Collections.Generic;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Azure.Documents;
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

        //[FunctionName("ProcessQueueItem")]
        //public void ProcessQueueItem([QueueTrigger("%QueueName%", Connection = "saCosmosDocumentDbConnectionString")]string myQueueItem, ILogger log)
        //{
        //    log.LogInformation($"C# Queue trigger function processed: {myQueueItem}");
        //    var val = sampleService.GetValue();
        //    log.LogInformation($"The value from the SampleService is: {val}");
        //}
        [FunctionName("ItemChangeFeedListener")]
        public static void Run([CosmosDBTrigger(
            databaseName: "ItemDB",
            collectionName: "Item",
            LeaseCollectionName = "leases",
            LeaseCollectionPrefix = "ItemFeed",
           // ConnectionStringSetting = "saCosmosDbConnString",
             CreateLeaseCollectionIfNotExists = true)] IReadOnlyList<Document> input, ILogger log)
        {
            try
            {
                if (input != null && input.Count > 0)
                {
                    log.LogInformation("Documents modified " + input.Count);
                    log.LogInformation("First document Id " + input[0].Id);
                    throw new Exception("is checkpoint changed to next one OR is the change feed again send the old document!");
                    
                }
            }
            catch (Exception ex) {
                log.LogError("Exception thrown for testing purpose of checkpoint : " + ex.Message);
            }
        }
    }
}
