# Sample for transform and job operations with MK/IO SDK

```csharp
using MK.IO;
using MK.IO.Models;

// **********************
// MK/IO Client creation
// **********************

var client = new MKIOClient("mkiosubscriptionname", "mkiotoken");

// *********************
// transform operations
// *********************

// Create a transform
var transform = client.Transforms.CreateOrUpdate("simpletransform", new TransformProperties
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
var jobs = client.Jobs.ListAll();

// create output asset
var outputAsset = client.Assets.CreateOrUpdate("outputasset-012", "asset-outputasset-012", config["StorageName"], "output asset for job");
// create a job with the output asset created and with an asset as a source
var newJob = client.Jobs.Create("simpletransform", MKIOClient.GenerateUniqueName("job"), new JobProperties
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
var newJobH = client.Jobs.Create("simple", MKIOClient.GenerateUniqueName("job"), new JobProperties
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
var job2 = client.Jobs.Get("simpletransform", "testjob1");

// Cancel a job
client.Jobs.Cancel("simpletransform", "testjob2");

// Delete a job
client.Jobs.Delete("simpletransform", "testjob1");

```

