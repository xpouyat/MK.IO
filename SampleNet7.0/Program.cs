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

            var client = new MKIOClient(config["MKIOSubscriptionName"]!, config["MKIOToken"]!);

            MKIOClient.GenerateUniqueName("asset");

            UserInfo profile;
            try
            {
                profile = client.Account.GetUserProfile();
            }
            catch (ApiException ex)
            {
                Console.WriteLine(ex.Message);
                Environment.Exit(0);
            }

            // Get subscription stats
            var stats = client.Account.GetSubscriptionStats();

            var subs = await client.Account.ListAllSubscriptionsAsync();
            var sub = await client.Account.GetSubscriptionAsync();
            var locs = await client.Account.ListAllLocationsAsync();

            // *****************
            // asset operations
            // *****************

            // list assets

            var mkioAssetsResult = client.Assets.ListAsPage("properties/created desc", 10);
            while (true)
            {
                foreach (var a in mkioAssetsResult.Results)
                {
                    Console.WriteLine(a.Name);
                }
                if (mkioAssetsResult.NextPageLink == null) break;

                mkioAssetsResult = client.Assets.ListAsPageNext(mkioAssetsResult.NextPageLink);
            }

            var specc = client.Assets.ListTracksAndDirListing("copy-ef2058b692-copy");

            // get streaming locators for asset
            try
            {
                var locatorsAsset = client.Assets.ListStreamingLocators("uploaded-c9c6146a98-CustomPreset-AutoFit-57653ac7b8-autofit");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


            // get asset
            var mkasset = client.Assets.Get("copy-152b839997");

            // create asset
            // var newasset = client.Assets.CreateOrUpdate("copy-ef2058b692-copy", "asset-2346d605-b4d6-4958-a80b-b4943b602ea8", "amsxpfrstorage", "description of asset copy");

            // delete asset
            // client.Assets.Delete("asset-33adc1873f");


            // ************************
            // asset filter operations
            // ************************

            var assetFilters = client.AssetFilters.List("copy-ef2058b692-copy");

            assetFilters.ForEach(af => client.AssetFilters.Delete("copy-ef2058b692-copy", af.Name));
            //var assetFilter1 = client.AssetFilters.Get("liveoutput-c4debfe5", assetFilters.First().Name);

            // asset filter creation
            // Typically, you will want to select a matching Type, such as Video, and then select additional filters.
            // For instance, to include all audio tracks with mp4a, and all video tracks that are between 0 and 1 Mbps, you would provide these FilterTrackSelection objects:

            var assetFilter = client.AssetFilters.CreateOrUpdate("copy-ef2058b692-copy", MKIOClient.GenerateUniqueName("filter"), new MediaFilterProperties
            {
                PresentationTimeRange = new PresentationTimeRange
                {
                    Timescale = 10000000,
                },
                Tracks = new List<FilterTrackSelection>()
                {
                    new() {
                        TrackSelections = new List<FilterTrackPropertyCondition>()
                        {
                            new() {
                                Property = FilterTrackPropertyType.Type,
                                Operation = FilterTrackPropertyCompareOperation.Equal,
                                Value = FilterPropertyTypeValue.Video
                            },
                            new() {
                                Property = FilterTrackPropertyType.Bitrate,
                                Operation = FilterTrackPropertyCompareOperation.Equal,
                                Value = "0-1048576"
                            }
                        },
                    },
                    new() {
                        TrackSelections = new List<FilterTrackPropertyCondition>()
                        {
                            new() {
                                Property = FilterTrackPropertyType.Type,
                                Operation = FilterTrackPropertyCompareOperation.Equal,
                                Value = FilterPropertyTypeValue.Audio
                            },
                            new() {
                                Property = FilterTrackPropertyType.FourCC,
                                Operation = FilterTrackPropertyCompareOperation.Equal,
                                Value = "mp4a"
                            }
                        }
                    }
                }
            });

            client.AssetFilters.Delete("liveoutput-c4debfe5", assetFilter.Name);

            // **************************
            // account filter operations
            // **************************

            var acfilters = client.AccountFilters.List();

            // account filter creation
            // Typically, you will want to select a matching Type, such as Video, and then select additional filters.
            // For instance, to include all audio tracks with mp4a, and all video tracks that are between 0 and 1 Mbps, you would provide these FilterTrackSelection objects:

            var filter = client.AccountFilters.CreateOrUpdate(MKIOClient.GenerateUniqueName("acc-filter"), new MediaFilterProperties
            {
                PresentationTimeRange = new PresentationTimeRange
                {
                    Timescale = 10000000,
                },
                Tracks = new List<FilterTrackSelection>()
                {
                    new() {
                        TrackSelections = new List<FilterTrackPropertyCondition>()
                        {
                            new() {
                                Property = FilterTrackPropertyType.Type,
                                Operation = FilterTrackPropertyCompareOperation.Equal,
                                Value = FilterPropertyTypeValue.Video
                            },
                            new() {
                                Property = FilterTrackPropertyType.Bitrate,
                                Operation = FilterTrackPropertyCompareOperation.Equal,
                                Value = "0-1048576"
                            }
                        },
                    },
                    new() {
                        TrackSelections = new List<FilterTrackPropertyCondition>()
                        {
                            new() {
                                Property = FilterTrackPropertyType.Type,
                                Operation = FilterTrackPropertyCompareOperation.Equal,
                                Value = FilterPropertyTypeValue.Audio
                            },
                            new() {
                                Property = FilterTrackPropertyType.FourCC,
                                Operation = FilterTrackPropertyCompareOperation.Equal,
                                Value = "mp4a"
                            }
                        }
                    }
                }
            });

            client.AccountFilters.Delete(filter.Name);





            // *********************
            // transform operations
            // *********************


            var tranform = client.Transforms.CreateOrUpdate("simpleTransformSD", new TransformProperties
            {
                Description = "desc",
                Outputs = new List<TransformOutput>
                {
                    new() {
                        Preset = new BuiltInStandardEncoderPreset(EncoderNamedPreset.H264SingleBitrateSD),
                        RelativePriority = "Normal"
                    }
                }
            });

            // ***************
            // job operations
            // ***************

            // var jobs = client.Jobs.ListAll();

            // var job = client.Jobs.Get("simple", "testjob1");


            //var outputAsset = client.Assets.CreateOrUpdate("outputasset-012", "asset-outputasset-012", config["StorageName"], "output asset for job");

            /*
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

            var outputAssetName = MKIOClient.GenerateUniqueName("output");
            var outputAsset2 = client.Assets.CreateOrUpdate(outputAssetName, "mkioasset-" + outputAssetName, config["StorageName"]!, "output asset for job");

            var jobHttp = client.Jobs.Create(tranform.Name, MKIOClient.GenerateUniqueName("job"), new JobProperties
            {
                Description = "My SD encoding job",
                Priority = "Normal",
                Input = new JobInputHttp(
                    null,
                    new List<string> {
                        "http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/ForBiggerEscapes.mp4"
                    }),
                Outputs = new List<JobOutputAsset>()
                {
                    new()
                    {
                        AssetName = outputAssetName
                    }
                }
            }
            );

            while (jobHttp.Properties.State == JobState.Queued || jobHttp.Properties.State == JobState.Scheduled || jobHttp.Properties.State == JobState.Processing)
            {
                jobHttp = client.Jobs.Get(tranform.Name, jobHttp.Name);
                Console.WriteLine(jobHttp.Properties.State);
                Thread.Sleep(10000);
            }



            //client.Jobs.Cancel("simple", "testjob2");
            client.Jobs.Delete(tranform.Name, jobHttp.Name);




            // **********************
            // live event operations
            // **********************

            var les = client.LiveEvents.List();

            // client.LiveEvents.Delete("liveevent4");

            var le = client.LiveEvents.Create(MKIOClient.GenerateUniqueName("liveEvent"), "francecentral", new LiveEventProperties
            {
                Input = new LiveEventInput { StreamingProtocol = LiveEventInputProtocol.RTMP },
                StreamOptions = new List<string> { "Default" },
                Encoding = new LiveEventEncoding { EncodingType = LiveEventEncodingType.PassthroughBasic }
            });

            le = client.LiveEvents.Update(le.Name, "francecentral", new LiveEventProperties
            {
                Input = new LiveEventInput { StreamingProtocol = LiveEventInputProtocol.SRT },
                StreamOptions = new List<string> { "Default" },
                Encoding = new LiveEventEncoding { EncodingType = LiveEventEncodingType.PassthroughBasic }
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


            // ******************************
            // content key policy operations
            // ******************************

            try
            {
                var ck = client.ContentKeyPolicies.Get("testpolcreate");
            }

            catch
            {

            }



            try
            {
                await client.ContentKeyPolicies.DeleteAsync("testpolcreate");
            }

            catch
            {

            }
            var cks = client.ContentKeyPolicies.List();

            var key = GenerateSymKeyAsBase64();

            var newpol = client.ContentKeyPolicies.Create(
                "testpolcreate",
                new ContentKeyPolicy("My description", new List<ContentKeyPolicyOption>()
                {
                    new(
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

            var ckpolprop = await client.ContentKeyPolicies.GetPolicyPropertiesWithSecretsAsync("testpolcreate");


            // *******************
            // storage operations
            // *******************

            // Creation

            /*
            var storage = client.StorageAccounts.Create(new StorageRequestSchema
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

            // Update the description
            storage2.Spec.Description = "my new description";
            var stor3 = client.StorageAccounts.Update((Guid)storages.First().Metadata.Id, new StorageRequestSchema
            {
                Spec = storage2.Spec
            });

            var creds = client.StorageAccounts.ListCredentials((Guid)storage2.Metadata.Id);

            // var cred = client.StorageAccounts.GetCredential((Guid)storages.First().Metadata.Id, (Guid)creds.First().Metadata.Id);

            // client.StorageAccounts.DeleteCredential((Guid)storages.First().Metadata.Id, (Guid)creds.First().Metadata.Id);

            var cred = client.StorageAccounts.CreateCredential((Guid)storage2.Metadata.Id, new CredentialSchema
            {
                AzureCredential = new AzureCredential
                {
                    SasToken = "mySasToken"
                }
            });

            // Delete
            // client.StorageAccounts.Delete(storages.First().Metadata.Id);



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
                   StreamingPolicyName = PredefinedStreamingPolicy.ClearStreamingOnly
               });

            var pathsl = client.StreamingLocators.ListUrlPaths(mklocator2.Name);

            // client.StreamingLocators.Delete("locator-25452");

        }

        /// <summary>
        /// Generates a random symmetric key as a Base64 string.
        /// </summary>
        /// <returns></returns>
        private static string GenerateSymKeyAsBase64()
        {
            byte[] TokenSigningKey = new byte[40];
            var rng = RandomNumberGenerator.Create();
            rng.GetBytes(TokenSigningKey);
            return Convert.ToBase64String(TokenSigningKey);
        }
    }
}
