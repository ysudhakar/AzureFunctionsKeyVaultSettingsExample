using AzureFunctionsKeyVaultSettingsExample;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.CosmosDB;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Extensions.DependencyInjection;

[assembly: WebJobsStartup(typeof(StartUp))]
namespace AzureFunctionsKeyVaultSettingsExample
{
    public class StartUp : IWebJobsStartup
    {
        public void Configure(IWebJobsBuilder builder)
        {
            // Pass in the name of the setting that contains your Key Vault url 
            // (setting will be read from local.settings.json or Azure app settings)
            var config = builder.AddAzureKeyVaultConfiguration("KeyVaultEndpoint");

            builder.Services.PostConfigure<CosmosDBOptions>(options =>
            {
                options.ConnectionString = config["saCosmosDbConnString"]; // saCosmosDbConnString is a secret in the Key Vault
            });

            // Now config contains all secrets from the Key Vault and you can use them in your DI setup
            builder.Services.AddSingleton<ISampleService>(p => new SampleService(config["SampleValue"]));

            // You can also use any Key Vault secrets in trigger/binding attributes (see ExampleFunction.cs)
        }
    }
}