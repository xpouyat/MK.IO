# Sample for filter operations with MK/IO SDK

```csharp
using MK.IO;
using MK.IO.Models;

// **********************
// MK/IO Client creation
// **********************

var client = new MKIOClient("mkiosubscriptionname", "mkiotoken");

// ************************
// asset filter operations
// ************************

var assetFilters = client.AssetFilters.List("liveoutput-c4debfe5");

var assetFilter1 = client.AssetFilters.Get("liveoutput-c4debfe5", assetFilters.First().Name);

var assetFilter = client.AssetFilters.CreateOrUpdate("liveoutput-c4debfe5", MKIOClient.GenerateUniqueName("filter"), new MediaFilterProperties
{
    PresentationTimeRange = new PresentationTimeRange
    {
        Timescale = 10000000,
    },
    Tracks = new List<FilterTrackSelection>()
    {
        new FilterTrackSelection
        {
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

client.AssetFilters.Delete("liveoutput-c4debfe5", assetFilter.Name);

// **************************
// account filter operations
// **************************

var acfilters = client.AccountFilters.List();

var filter = client.AccountFilters.CreateOrUpdate("filter123", new MediaFilterProperties
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

client.AccountFilters.Delete("filter123");

```
