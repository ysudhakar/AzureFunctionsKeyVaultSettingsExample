using System;
using System.Collections.Generic;
using System.Text;

namespace AzureFunctionsKeyVaultSettingsExample
{
    public interface ISampleService
    {
        string GetValue();
    }

    public class SampleService : ISampleService
    {
        private readonly string value;

        public SampleService(string value)
        {
            this.value = value;
        }

        public string GetValue()
        {
            return value;
        }
    }
}
