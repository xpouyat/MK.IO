# MK.IO Client Library

A client library for [MediaKind MK/IO](https://io.mediakind.com/).

[Link to MK/IO Nuget package](https://www.nuget.org/packages/MK.IO)

## Usage (C#, .NET)

### MK/IO API Token

You need the MK/IO API token `mkiotoken` to connect to the API.

To do so,
1. Open a web browser and log into https://io.mediakind.com (sign in with Microsoft SSO).
1. Once you are logged in, open a second tab on the same browser and open this link in the new tab: https://api.io.mediakind.com/auth/token/
 
This should provide you with your user_id and token. Note that this token is valid for 1 year.

Another way to get the token is to use [Fiddler](https://www.telerik.com/fiddler) when you connect to the MK/IO portal with your browser.
It is displayed in the header as `x-mkio-token`. For example, you should see it on the second REST call to https://api.io.mediakind.com/api/ams/mkiosubscriptionname/stats/.

### Supported operations

In the current version, operations are supported for :
- Assets
- Streaming endpoints
- Streaming locators
- Storage accounts
- Content key policies
- Transforms
- Jobs
- Live events

### Sample code

```csharp
using MK.IO;
using MK.IO.Models;

// **********************
// MK/IO Client creation
// **********************

var client = new MKIOClient("mkiosubscriptionname", "mkiotoken");

var profile = client.GetUserInfo();

// Get subscription stats
var stats = client.GetStats();

// *******************
// storage operations
// *******************

// Creation
var storage = client.CreateStorageAccount(new StorageRequestSchema
            {
                Spec = new StorageSchema
                {
                    Name = "amsxpfrstorage",
                    Location = "francecentral",
                    Description = "my description",
                    AzureStorageConfiguration = new BlobStorageAzureProperties
                    {
                        Url = "https://insertyoursasuri"
                    }
                }
            }
            );

// List
var storages = client.ListStorageAccounts();

// Get
var storage2 = client.GetStorageAccount(storages.First().Metadata.Id);

// Delete
client.DeleteStorageAccount(storages.First().Metadata.Id);

// List credentials of a storage
var credentials = client.ListStorageAccountCredentials((Guid)storages.First().Metadata.Id);

// Get specific credential
var credential = client.GetStorageAccountCredential((Guid)storages.First().Metadata.Id, (Guid)creds.First().Metadata.Id);


// *****************
// asset operations
// *****************

// list assets
var mkioAssets = client.Assets.List();

// list assets with pages
var mkioAssetsResult = client.Assets.ListAsPage("name desc", 10);
do
{
    mkioAssetsResult = client.Assets.ListAsPageNext(mkioAssetsResult.NextPageLink);
    // do stuff
} while (mkioAssetsResult.NextPageLink != null);

// get asset
var mkasset = client.Assets.Get("mmyassetname");

// create asset
var newasset = client.Assets.CreateOrUpdate("asset-33adc1873f", new Asset("asset-67c25a02-a672-40cd-a4da-dcc48b89acae", "description of asset", "storagename"));

// delete asset
MKIOClient.Assets.Delete("asset-33adc1873f");

// get streaming locators for asset
var locatorsAsset = client.Assets.ListStreamingLocators("copy-1b510ee166");

// Get tracks and directory of an asset
var tracksAndDir = client.Assets.ListTracksAndDirListing("copy-ef2058b692");


// ******************************
// Streaming endpoint operations
// ******************************

// get streaming endpoint
var mkse = client.GetStreamingEndpoint("streamingendpoint1");

// list streaming endpoints
var mkses = client.ListStreamingEndpoints();

// create streaming endpoint
var newSe = client.CreateStreamingEndpoint("streamingendpoint2", "francecentral", new StreamingEndpointProperties
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

// start, stop, delete streaming endpoint
client.StartStreamingEndpoint("streamingendpoint1");
client.StopStreamingEndpoint("streamingendpoint1");
client.DeleteStreamingEndpoint("streamingendpoint2");


// ******************************
// Streaming locator operations
// ******************************

var mklocators = client.ListStreamingLocators();
var mklocator1 = client.GetStreamingLocator("locator-25452");

var mklocator2 = client.CreateStreamingLocator(
                locatorName,
                new StreamingLocatorProperties
                {
                    AssetName = "copy-ef2058b692",
                    StreamingPolicyName = "Predefined_ClearStreamingOnly"
                });

var pathsl = client.ListUrlPathsStreamingLocator("locator-25452");


// ******************************
// content key policy operations
// ******************************

var ck = await client.GetContentKeyPolicyAsync("testpol1");
var cks = client.ListContentKeyPolicies();
client.DeleteContentKeyPolicy("testpolcreate");

var newpol = client.CreateContentKeyPolicy(
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


// *********************
// transform operations
// *********************

// Create a transform
var tranform = client.CreateTransform("simpletransform", new TransformProperties
            {
                Description = "Encoding to 720p single bitrate",
                Outputs = new List<TransformOutput>() {
                    new TransformOutput {
                        Preset = new BuiltInStandardEncoderPreset(EncoderNamedPreset.H264SingleBitrate720P),
                        RelativePriority = "Normal" } }
            });


// ***************
// job operations
// ***************

// list all jobs
var jobs = client.ListAllJobs();

// create output asset
var outputAsset = client.Assets.CreateOrUpdate("outputasset-012", "asset-outputasset-012", config["StorageName"], "output asset for job");
// create a job with the output asset created and with an asset as a source
var newJob = client.CreateJob("simpletransform", "testjob2", new JobProperties
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

// with http source as a source
var newJobH = client.CreateJob("simple", "testjob3", new JobProperties
            {
                Description = "My job",
                Priority = "Normal",
                Input = new JobInputHttp(
                    null,
                    new List<string> {
                        "https://myurltovideofile.mp4"
                    }),
                Outputs = new List<JobOutputAsset>()
                {
                    new JobOutputAsset()
                    {
                        AssetName="outputasset-014"
                    }
                }
            }
            );

// Get a job
var job2 = client.GetJob("simpletransform", "testjob1");

// Cancel a job
client.CancelJob("simpletransform", "testjob2");

// Delete a job
client.DeleteJob("simpletransform", "testjob1");


// **********************
// live event operations
// **********************

var list_le = client.LiveEvents.List();

// Creation
var le = client.LiveEvents.Create("liveevent4", "francecentral", new LiveEventProperties
{
    Input = new LiveEventInput { StreamingProtocol = "RTMP" },
    StreamOptions = new List<string> { "Default" },
    Encoding = new LiveEventEncoding { EncodingType = "PassthroughBasic" }
});

// Delete
client.LiveEvents.Delete("liveevent4");

```

Async operations are also supported. For example :

```csharp

// *****************
// asset operations
// *****************

// Retrieve assets with pages for better performances
var mkioAssetsResult = await client.ListAssetsAsPageAsync("name desc", 10);
        do
        {
            mkioAssetsResult = await client.ListAssetsAsPageNextAsync(mkioAssetsResult.NextPageLink);
            // do stuff
        } while (mkioAssetsResult.NextPageLink != null);


// ******************************
// Streaming endpoint operations
// ******************************

// get streaming endpoint
var mkse = await client.GetStreamingEndpointAsync("streamingendpoint1");

// list streaming endpoints
var mkses = await client.ListStreamingEndpointsAsync();

// create streaming endpoint
var newSe = await client.CreateStreamingEndpointAsync("streamingendpoint2", "francecentral", new StreamingEndpointProperties
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

// start, stop, delete streaming endpoint
await client.StartStreamingEndpointAsync("streamingendpoint1");
await client.StopStreamingEndpointAsync("streamingendpoint1");
await client.DeleteStreamingEndpointAsync("streamingendpoint2");

```

