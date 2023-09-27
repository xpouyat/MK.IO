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

For more information, please read this [article](https://support.mediakind.com/portal/en/kb/articles/how-to-use-mkio-apis-step-by-step).

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
- Live outputs
- Asset filters
- Account filters

### Sample code

```csharp
using MK.IO;
using MK.IO.Models;

// **********************
// MK/IO Client creation
// **********************

var client = new MKIOClient("mkiosubscriptionname", "mkiotoken");

var profile = client.Subscription.GetUserInfo();

// Get subscription stats
var stats = client.Subscription.GetStats();

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
var newasset = client.Assets.CreateOrUpdate(MKIOClient.GenerateUniqueName("asset"), "asset-67c25a02-a672-40cd-a4da-dcc48b89acae", "description of asset", "storagename");

// delete asset
client.Assets.Delete(newsasset.Name);

// get streaming locators for asset
var locatorsAsset = client.Assets.ListStreamingLocators("copy-1b510ee166");

// Get tracks and directory of an asset
var tracksAndDir = client.Assets.ListTracksAndDirListing("copy-ef2058b692");


// ******************************
// Streaming endpoint operations
// ******************************

// get streaming endpoint
var mkse = client.StreamingEndpoints.Get("streamingendpoint1");

// list streaming endpoints
var mkses = client.StreamingEndpoints.List();

// create streaming endpoint
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

// start, stop, delete streaming endpoint
client.StreamingEndpoints.Start("streamingendpoint1");
client.StreamingEndpoints.Stop("streamingendpoint1");
client.StreamingEndpoints.Delete("streamingendpoint2");


// ******************************
// Streaming locator operations
// ******************************

var mklocators = client.StreamingLocators.List();
var mklocator1 = client.StreamingLocators.Get("locator-25452");

var mklocator2 = client.StreamingLocators.Create(
                MKIOClient.GenerateUniqueName("locator"),
                new StreamingLocatorProperties
                {
                    AssetName = "copy-ef2058b692",
                    StreamingPolicyName = "Predefined_ClearStreamingOnly"
                });

var pathsl = client.StreamingLocators.ListUrlPaths("locator-25452");


// ******************************
// content key policy operations
// ******************************

var ck = client.ContentKeyPolicies.Get("testpol1");
var cks = client.ContentKeyPolicies.List();
client.ContentKeyPolicies.Delete("testpolcreate");

var newpol = client.ContentKeyPolicies.Create(
                MKIOClient.GenerateUniqueName("ckpolicy"),
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


```

Additional samples are available :

- [live operations](SampleLiveOperations.md) 
- [storage operations](SampleStorageOperations.md)
- [transform and job operations](SampleTransformAndJobOperations.md)
- [account filter and asset filter operations](SampleFilterOperations.md)
- [content key policy and streaming locator operations](SampleContentKeyPolicyOperations.md)


Async operations are also supported. For example :

```csharp

// *****************
// asset operations
// *****************

// Retrieve assets with pages for better performances
var mkioAssetsResult = await client.Assets.ListAsPageAsync("name desc", 10);
        do
        {
            mkioAssetsResult = await client.Assets.ListAsPageNextAsync(mkioAssetsResult.NextPageLink);
            // do stuff
        } while (mkioAssetsResult.NextPageLink != null);


// ******************************
// Streaming endpoint operations
// ******************************

// get streaming endpoint
var mkse = await client.StreamingEndpoints.GetAsync("streamingendpoint1");

// list streaming endpoints
var mkses = await client.StreamingEndpoints.ListAsync();

// create streaming endpoint
var newSe = await client.StreamingEndpoints.CreateAsync("streamingendpoint2", "francecentral", new StreamingEndpointProperties
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
await client.StreamingEndpoints.StartAsync("streamingendpoint1");
await client.StreamingEndpoints.StopAsync("streamingendpoint1");
await client.StreamingEndpoints.DeleteAsync("streamingendpoint2");

```

