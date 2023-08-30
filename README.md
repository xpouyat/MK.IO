# MK.IO Client Library

A client library for [MediaKind MK/IO](https://io.mediakind.com/).

[Link to MK/IO Nuget package](https://www.nuget.org/packages/MK.IO)

## Usage (C#, .NET)

### MK/IO API Token

You need the MK/IO API token `mkiotoken` to connect to the API. You can grab it using [Fiddler](https://www.telerik.com/fiddler) when you connect to the MK/IO portal with your browser.
It is displayed in the header as `x-mkio-token`. For example, you should see it on the second REST call to https://api.io.mediakind.com/api/ams/mkiosubscriptionname/stats/.

### Sample code

```csharp
using MK.IO;
using MK.IO.Models;

// **********************
// MK/IO Client creation
// **********************

var MKIOClient = new MKIOClientRest("mkiosubscriptionname", "mkiotoken");


// *****************
// asset operations
// *****************

// list assets
var mkioAssets = MKIOClient.ListAssets();

// get asset
var mkasset = MKIOClient.GetAsset("mmyassetname");

// create asset
var newasset = MKIOClient.CreateOrUpdateAsset("asset-33adc1873f", new MKIOAsset("asset-67c25a02-a672-40cd-a4da-dcc48b89acae", "description of asset", "storagename"));

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
var newSe = MKIOClient.CreateStreamingEndpoint("streamingendpoint2", new MKIOStreamingEndpoint("francecentral", "my description", new MKIOStreamingEndpointSku("Standard", 600), 0, false), true);

// start, stop, delete streaming endpoint
MKIOClient.StartStreamingEndpoint("streamingendpoint1");
MKIOClient.StopStreamingEndpoint("streamingendpoint1");
MKIOClient.DeleteStreamingEndpoint("streamingendpoint2");

```

Async operations are also supported for operations.

Example :

```csharp
// ******************************
// Streaming endpoint operations
// ******************************

// get streaming endpoint
var mkse = await MKIOClient.GetStreamingEndpointAsync("streamingendpoint1");

// list streaming endpoints
var mkses = await MKIOClient.ListStreamingEndpointsAsync();

// create streaming endpoint
var newSe = await MKIOClient.CreateStreamingEndpointAsync("streamingendpoint2", new MKIOStreamingEndpoint("francecentral", "my description", new MKIOStreamingEndpointSku("Standard", 600), 0, false), true);

// start, stop, delete streaming endpoint
await MKIOClient.StartStreamingEndpointAsync("streamingendpoint1");
await MKIOClient.StopStreamingEndpointAsync("streamingendpoint1");
await MKIOClient.DeleteStreamingEndpointAsync("streamingendpoint2");

```

### Supported operations

Operations are supported for :
- Assets
- Streaming endpoints
- 
To come :
- Storage accounts
- Live events