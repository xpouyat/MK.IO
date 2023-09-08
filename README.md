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

### Sample code

```csharp
using MK.IO;
using MK.IO.Models;

// **********************
// MK/IO Client creation
// **********************

var MKIOClient = new MKIOClient("mkiosubscriptionname", "mkiotoken");

var profile = MKIOClient.GetUserInfo();

// Get subscription stats
var stats = MKIOClient.GetStats();

// *******************
// storage operations
// *******************

// Creation
var storage = MKIOClient.CreateStorageAccount(new StorageSpec("amsxpfrstorage", "francecentral", new Uri("https://insertyoursasuri"), "my description"));

// List
var storages = MKIOClient.ListStorageAccounts();

// Get
var storage2 = MKIOClient.GetStorageAccount(storages.First().Metadata.Id);

// Delete
MKIOClient.DeleteStorageAccount(storages.First().Metadata.Id);


// *****************
// asset operations
// *****************

// list assets
var mkioAssets = MKIOClient.ListAssets();

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
var mklocator2 = MKIOClient.CreateStreamingLocator("locator23", new StreamingLocator("copy-9ec48d1bf3-mig", "Predefined_ClearStreamingOnly"));
var pathsl = MKIOClient.ListUrlPathsStreamingLocator("locator-25452");

```

Async operations are also supported. For example :

```csharp
// ******************************
// Streaming endpoint operations
// ******************************

// get streaming endpoint
var mkse = await MKIOClient.GetStreamingEndpointAsync("streamingendpoint1");

// list streaming endpoints
var mkses = await MKIOClient.ListStreamingEndpointsAsync();

// create streaming endpoint
var newSe = await MKIOClient.CreateStreamingEndpointAsync("streamingendpoint2", new StreamingEndpoint("francecentral", "my description", new StreamingEndpointSku("Standard", 600), 0, false), true);

// start, stop, delete streaming endpoint
await MKIOClient.StartStreamingEndpointAsync("streamingendpoint1");
await MKIOClient.StopStreamingEndpointAsync("streamingendpoint1");
await MKIOClient.DeleteStreamingEndpointAsync("streamingendpoint2");

```

### Supported operations

In this version, operations are supported for :
- Assets
- Streaming endpoints
- Streaming locators
- Storage accounts
