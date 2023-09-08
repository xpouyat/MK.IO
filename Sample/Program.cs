using MK.IO;
using MK.IO.Models;
using Microsoft.Extensions.Configuration;

namespace Sample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Sample that operates MK/IO.");


            /* you need to add an appsettings.json file with the following content:
             {
                "MKIOSubscriptionName": "yourMKIOsubscriptionname",
                "MKIOToken": "yourMKIOtoken"
             }
            */


            // Build a config object, using env vars and JSON providers.
            IConfigurationRoot config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();

            Console.WriteLine($"Using '{config["MKIOSubscriptionName"]}' MK/IO subscription.");


            // **********************
            // MK/IO Client creation
            // **********************

            var MKIOClient = new MKIOClient(config["MKIOSubscriptionName"], config["MKIOToken"]);

            var profile = MKIOClient.GetUserInfo();

            // Get subscription stats
            var stats = MKIOClient.GetStats();

            // *******************
            // storage operations
            // *******************

            
            // List
            var storages = MKIOClient.ListStorageAccounts();

            // Creation
            var storage = MKIOClient.CreateStorageAccount(new StorageSpec(config["StorageName"], config["StorageRegion"], new Uri(config["StorageSAS"]), "my description"));

            // Get
            var storage2 = MKIOClient.GetStorageAccount(storage.Metadata.Id);



            // *****************
            // asset operations
            // *****************

            // list assets
            var mkioAssets = MKIOClient.ListAssets();

            // var specc = MKIOClient.ListTracksAndDirListingForAsset("copy-1b510ee166-copy-d32391984a");

            // get streaming locators for asset
            var locatorsAsset = MKIOClient.ListStreamingLocatorsForAsset("copy-1b510ee166-copy-d32391984a");

            // get asset
            var mkasset = MKIOClient.GetAsset("mmyassetname");

            // create asset
            var newasset = MKIOClient.CreateOrUpdateAsset("asset-33adc1873f", new Asset("asset-67c25a02-a672-40cd-a4da-dcc48b89acae", "description of asset", "storagename"));

            // delete asset
            MKIOClient.DeleteAsset("asset-33adc1873f");


            // ******************************
            // Streaming endpoint operations
            // ******************************

            // get streaming endpoint
            var mkse = MKIOClient.GetStreamingEndpoint("streamingendpoint1");

            // list streaming endpoints
            var mkses = MKIOClient.ListStreamingEndpoints();

            // create streaming endpoint
            var newSe = MKIOClient.CreateStreamingEndpoint("streamingendpoint2", new StreamingEndpoint("francecentral", "my description", new StreamingEndpointSku("Standard", 600), 0, false), true);

            // start, stop, delete streaming endpoint
            MKIOClient.StartStreamingEndpoint("streamingendpoint1");
            MKIOClient.StopStreamingEndpoint("streamingendpoint1");
            MKIOClient.DeleteStreamingEndpoint("streamingendpoint2");


            // ******************************
            // Streaming locator operations
            // ******************************

            var mklocators = MKIOClient.ListStreamingLocators();
            var mklocator1 = MKIOClient.GetStreamingLocator("locator-25452");
           
            string uniqueness = Guid.NewGuid().ToString()[..13];
            string locatorName = $"locator-{uniqueness}";
            var mklocator2 = MKIOClient.CreateStreamingLocator(locatorName, new StreamingLocator("copy-9ec48d1bf3-mig", "Predefined_ClearStreamingOnly"));
           
            var pathsl = MKIOClient.ListUrlPathsStreamingLocator("locator-25452");
        }
    }
}