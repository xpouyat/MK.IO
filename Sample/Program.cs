// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Microsoft.Extensions.Configuration;
using MK.IO;
using MK.IO.Models;
using System.Security.Cryptography;

namespace Sample
{
    internal class Program
    {
        static void Main()
        {
            // MainAsync().Wait();
            // or, if you want to avoid exceptions being wrapped into AggregateException:
            MainAsync().GetAwaiter().GetResult();
        }


        static async Task MainAsync()
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
            try
            {
                // ck = await MKIOClient.GetContentKeyPolicyAsync("testpolcreate");
            }

            catch
            {

            }

            // var cks = MKIOClient.ListContentKeyPolicies();

            try
            {
                await MKIOClient.DeleteContentKeyPolicyAsync("testpolcreate");
            }

            catch
            {

            }

            /*
            var key = GenerateSymKeyAsBase64();

            var newpol = MKIOClient.CreateContentKeyPolicy(
                "testpolcreate",
                new ContentKeyPolicy("My description", new List<ContentKeyPolicyOption>()
                {
                    new ContentKeyPolicyOption(
                        "option1",
                        new ContentKeyPolicyConfigurationWidevine("{}"),
                        new ContentKeyPolicyTokenRestriction(
                            "issuer",
                            "audience",
                            "Jwt",
                            new ContentKeyPolicySymmetricTokenKey(key)
                            )
                        )
                })
                );
            */

            // *******************
            // storage operations
            // *******************

            // Creation

            /*
            var storage = MKIOClient.CreateStorageAccount(new StorageRequestSchema
            {
                Spec = new StorageSchema
                {
                    Name = config["StorageName"],
                    Location = config["StorageRegion"],
                    Description = "my description",
                    AzureStorageConfiguration = new BlobStorageAzureProperties
                    {
                        Url = config["StorageSAS"]
                    }
                }
            }
            );
            */

            // List
            var storages = MKIOClient.ListStorageAccounts();

            // Get
            var storage2 = MKIOClient.GetStorageAccount((Guid)storages.First().Metadata.Id);


            var creds = MKIOClient.ListStorageAccountCredentials((Guid)storages.First().Metadata.Id);

            var cred = MKIOClient.GetStorageAccountCredential((Guid)storages.First().Metadata.Id, (Guid)creds.First().Metadata.Id);


            // Delete
            // MKIOClient.DeleteStorageAccount(storages.First().Metadata.Id);


            // *****************
            // asset operations
            // *****************

            // list assets
            var mkioAssets = MKIOClient.ListAssets();

            // var specc = MKIOClient.ListTracksAndDirListingForAsset("copy-1b510ee166-copy-d32391984a");

            // get streaming locators for asset
            var locatorsAsset = MKIOClient.ListStreamingLocatorsForAsset("copy-1b510ee166-copy-d32391984a");

            // get asset
            var mkasset = MKIOClient.GetAsset("copy-152b839997");

            // create asset
            // var newasset = MKIOClient.CreateOrUpdateAsset("copy-ef2058b692-copy", "asset-2346d605-b4d6-4958-a80b-b4943b602ea8", "amsxpfrstorage", "description of asset copy");

            // delete asset
            //MKIOClient.DeleteAsset("asset-33adc1873f");


            // ******************************
            // Streaming endpoint operations
            // ******************************

            // get streaming endpoint
            var mkse = MKIOClient.GetStreamingEndpoint("xpouyatse1");

            // list streaming endpoints
            var mkses = MKIOClient.ListStreamingEndpoints();

            // create streaming endpoint

            /*
            var newSe = MKIOClient.CreateStreamingEndpoint("streamingendpoint2", "francecentral", new Dictionary<string, string>(), new StreamingEndpointProperties
            {
                Description = "my description",
                ScaleUnits = 0,
                CdnEnabled = false,
                Sku = new StreamingEndpointsCurrentSku
                {
                    Name = "Standard",
                    Capacity = 600
                }
            });
            */

            // start, stop, delete streaming endpoint
            //MKIOClient.StartStreamingEndpoint("streamingendpoint1");
            //MKIOClient.StopStreamingEndpoint("streamingendpoint1");
            //MKIOClient.DeleteStreamingEndpoint("streamingendpoint2");


            // ******************************
            // Streaming locator operations
            // ******************************

            var mklocators = MKIOClient.ListStreamingLocators();

            //var mklocator1 = MKIOClient.GetStreamingLocator("locator-25452");

            string uniqueness = Guid.NewGuid().ToString()[..13];
            string locatorName = $"locator-{uniqueness}";
            //var mklocator2 = MKIOClient.CreateStreamingLocator(locatorName, new StreamingLocator("copy-9ec48d1bf3-mig", "Predefined_ClearStreamingOnly"));
            var mklocator2 = MKIOClient.CreateStreamingLocator(locatorName, new StreamingLocatorProperties { AssetName = "copy-ef2058b692-copy", StreamingPolicyName = "Predefined_ClearStreamingOnly" });

            var pathsl = MKIOClient.ListUrlPathsStreamingLocator(mklocator2.Name);

            // MKIOClient.DeleteStreamingLocator("locator-25452");

        }


        private static string GenerateSymKeyAsBase64()
        {
            byte[] TokenSigningKey = new byte[40];
            var rng = RandomNumberGenerator.Create();
            rng.GetBytes(TokenSigningKey);
            return Convert.ToBase64String(TokenSigningKey);
        }
    }
}