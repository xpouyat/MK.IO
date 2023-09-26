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

            var client = new MKIOClient(config["MKIOSubscriptionName"], config["MKIOToken"]);

            MKIOClient.GenerateUniqueName("asset");

            //var profile = client.Subscription.GetUserInfo();

            // Get subscription stats
            //var stats = client.Subscription.GetStats();

            // **************************
            // account filter operations
            // **************************

            var acfilters = client.AccountFilters.List();

            var filter = client.AccountFilters.CreateOrUpdate("filter7", new MediaFilterProperties
            {
                PresentationTimeRange = new PresentationTimeRange
                {
                    Timescale = 10000000,
                },
                Tracks = new List<FilterTrackSelection>()
                {

                    new FilterTrackSelection
                    {
                        //TrackType = "Audio",
                        TrackSelections = new List<FilterTrackPropertyCondition>()
                        {
                            new FilterTrackPropertyCondition
                            {
                                Property = "Language",
                                Operation = "Equal",
                                Value = "eng"
                            },
                            new FilterTrackPropertyCondition
                            {
                                Property = "Type",
                                Operation = "Equal",
                                Value = "Audio"
                            }
                        }
                    }
                }
            });

            client.AccountFilters.Delete("filter4");

            // **********************
            // live event operations
            // **********************

            var les = client.LiveEvents.List();

            // client.LiveEvents.Delete("liveevent4");

            var le = client.LiveEvents.Create(MKIOClient.GenerateUniqueName("liveEvent"), "francecentral", new LiveEventProperties
            {
                Input = new LiveEventInput { StreamingProtocol = "RTMP" },
                StreamOptions = new List<string> { "Default" },
                Encoding = new LiveEventEncoding { EncodingType = "PassthroughBasic" }
            });

            // **********************
            // live output operations
            // **********************

            // create live output asset
            var nameasset = MKIOClient.GenerateUniqueName("liveoutput");
            var loasset = client.Assets.CreateOrUpdate(nameasset, "asset-" + nameasset, config["StorageName"], "live output asset");

            var lo = client.LiveOutputs.Create(le.Name, MKIOClient.GenerateUniqueName("liveOutput"), new LiveOutputProperties
            {
                ArchiveWindowLength = "PT5M",
                AssetName = nameasset
            });

            // live outputs listing
            var los = client.LiveOutputs.List(le.Name);

            if (los.Count == 1)
            {
                var looo = client.LiveOutputs.Get(le.Name, los.First().Name);
            }

            // *******************
            // storage operations
            // *******************

            // Creation

            /*
            var storage = client.CreateStorageAccount(new StorageRequestSchema
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
            var storages = client.StorageAccounts.List();

            // Get
            var storage2 = client.StorageAccounts.Get((Guid)storages.First().Metadata.Id);


            var creds = client.StorageAccounts.ListCredentials((Guid)storages.First().Metadata.Id);

            var cred = client.StorageAccounts.GetCredential((Guid)storages.First().Metadata.Id, (Guid)creds.First().Metadata.Id);

            // Delete
            // client.StorageAccounts.Delete(storages.First().Metadata.Id);




            // *****************
            // asset operations
            // *****************

            // list assets
            //var mkioAssets = client.Assets.List("name desc", 4);


            var mkioAssetsResult = client.Assets.ListAsPage("name desc", 4);
            do
            {
                mkioAssetsResult = client.Assets.ListAsPageNext(mkioAssetsResult.NextPageLink);
            } while (mkioAssetsResult.NextPageLink != null);


            var specc = client.Assets.ListTracksAndDirListing("copy-ef2058b692-copy");

            // get streaming locators for asset
            var locatorsAsset = client.Assets.ListStreamingLocators("copy-1b510ee166-copy-d32391984a");

            // get asset
            var mkasset = client.Assets.Get("copy-152b839997");

            // create asset
            // var newasset = client.Assets.CreateOrUpdate("copy-ef2058b692-copy", "asset-2346d605-b4d6-4958-a80b-b4943b602ea8", "amsxpfrstorage", "description of asset copy");

            // delete asset
            //client.Assets.Delete("asset-33adc1873f");

            // *********************
            // transform operations
            // *********************


            var tranform = client.Transforms.CreateOrUpdate("mytesttranf", new TransformProperties
            {
                Description = "desc",
                Outputs = new List<TransformOutput>() {
                    new TransformOutput {
                        Preset = new BuiltInStandardEncoderPreset(EncoderNamedPreset.H264SingleBitrate720P),
                        RelativePriority = "Normal" } }
            });



            // ***************
            // job operations
            // ***************

            var jobs = client.Jobs.ListAll();

            //var job = client.Jobs.Get("simple", "testjob1");

            /*
            var outputAsset = client.Assets.CreateOrUpdate("outputasset-012", "asset-outputasset-012", config["StorageName"], "output asset for job");

            client.Jobs.Create("simple", "testjob2", new JobProperties
            {
                Description = "My job",
                Priority = "Normal",
                Input = new JobInputAsset(
                    "copy-ef2058b692-copy",
                    new List<string> {
                        "switch_1920x1080_AACAudio_3677.mp4"
                    }),
                Outputs = new List<JobOutputAsset>()
                {
                    new JobOutputAsset()
                    {
                        AssetName="outputasset-012"
                    }
                }
            }
            );
            */

            var outputAsset = client.Assets.CreateOrUpdate("outputasset-014", "asset-outputasset-014", config["StorageName"], "output asset for job");

            var jobHttp = client.Jobs.Create("simple", MKIOClient.GenerateUniqueName("job"), new JobProperties
            {
                Description = "My job",
                Priority = "Normal",
                Input = new JobInputHttp(
                    null,
                    new List<string> {
                        "http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/ForBiggerEscapes.mp4"
                    }),
                Outputs = new List<JobOutputAsset>()
                {
                    new JobOutputAsset()
                    {
                        AssetName=MKIOClient.GenerateUniqueName("outputasset")
                    }
                }
            }
            );

            client.Jobs.Cancel("simple", "testjob2");
            //client.Jobs.Delete("simple", "testjob1");

            // ******************************
            // content key policy operations
            // ******************************

            try
            {
                // ck = await client.ContentKeyPolicies.Get("testpolcreate");
            }

            catch
            {

            }

            // var cks = client.ContentKeyPolicies.List();

            try
            {
                await client.ContentKeyPolicies.DeleteAsync("testpolcreate");
            }

            catch
            {

            }

            /*
            var key = GenerateSymKeyAsBase64();

            var newpol = client.ContentKeyPolicies.Create(
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




            // ******************************
            // Streaming endpoint operations
            // ******************************

            // get streaming endpoint
            var mkse = client.StreamingEndpoints.Get("xpouyatse1");

            // list streaming endpoints
            var mkses = client.StreamingEndpoints.List();

            // create streaming endpoint

            /*
            var newSe = client.StreamingEndpoints.Create("streamingendpoint2", "francecentral", new StreamingEndpointProperties
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
            //client.StreamingEndpoints.Start("streamingendpoint1");
            //client.StreamingEndpoints.Stop("streamingendpoint1");
            //client.StreamingEndpoints.Delete("streamingendpoint2");


            // ******************************
            // Streaming locator operations
            // ******************************

            var mklocators = client.StreamingLocators.List();

            //var mklocator1 = client.StreamingLocators.Get("locator-25452");

            var mklocator2 = client.StreamingLocators.Create(
               MKIOClient.GenerateUniqueName("locator"),
               new StreamingLocatorProperties
               {
                   AssetName = "copy-ef2058b692-copy",
                   StreamingPolicyName = "Predefined_ClearStreamingOnly"
               });

            var pathsl = client.StreamingLocators.ListUrlPaths(mklocator2.Name);

            // client.StreamingLocators.Delete("locator-25452");

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