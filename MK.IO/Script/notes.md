Classes generated from Swagger.

Namespace changed with ps1 script.

In TransformOutput.cs, public OneOfTransformOutputPreset Preset { get; set; } changed to public TransformOutputPreset Preset { get; set; }
In JobProperties.cs, public OneOfJobPropertiesInput Input { get; set; } changed to public JobInput Input { get; set; }
In JobProperties.cs, public OneOfJobPropertiesInput Input { get; set; } changed to public JobInput Input { get; set; }

Deleted : AbsoluteClipTime, AudioAnalyzerPreset, UtcClipTime, OneOfTransformOutputPreset.cs, OneOfJobPropertiesInput.cs, BuiltInStandardEncoderPreset.cs, JobInputAsset.cs, JobInputHttp.cs, ContentKeyPolicyOption
and all OneOfJobInput....cs

In JobOutputAsset.cs, public string EndTime changed to public DateTime? EndTime, and public string StartTime changed to public DateTime? StartTime
Same in StreamingLocatorProperties, JobProperties

public string Id { get; set; } to public string Id { get; private set; } in AccountFilterSchema, AssetFilterSchema, AssetSchema, ContentKeyPolicySchema, JobSchema, StreamingLocatorSchema, StreamingPolicySchema, TransformSchema
public string Name Id { get; set; } to public string Name { get; private set; } in AccountFilterSchema, AssetFilterSchema, AssetSchema, ContentKeyPolicySchema, JobSchema, StreamingLocatorSchema, StreamingPolicySchema, TransformSchema
same for Type in AssetSchema
same for sasTokenSaitied in AzureCredential.cs
same for Type and sysemData in ContentKeyPolicySchema.cs
same for Created and LastModified in JobProperties.cs
same fro created and LastModified in LiveEventProperties.cs
same for Name in LiveEventSchema.cs
same for Created and LastMofied in LiveOutputProperties.cs (and string to DateTime?)
same for Name in LiveOutputSchema.cs
same for Created and LastMofidied in StreamingEndpointProperties.cs
same for Name in StreamingEndpointSchema.cs
same for Id and Name in StreamingLocatorSchema.cs
same for Id, Name, Type in StreamingPolicySchema.cs
same for Created and LastModified in TransformProperties.cs
same for Id, Name, Type in TransformSchema.cs
same for provisisiongState and resourceState in LiveOutputProperties.cs
same for PrivateLinkServiceConnectionStatus in statusstorageschema.cs

in LiveOutputProperties.cs, ResourceState to LiveOutputResourceState

in statusstorageschema.cs PrivateLinkServiceConnectionStatus string to PrivateLinkServiceConnectionState

public SystemDataSchema SystemData { get; set; } to public SystemDataSchema SystemData { get; private set; } everywhere

JobState to JobState? in JobProperties.cs

 public string ToJson()
        {
            return JsonSerializer.Serialize(this, ConverterLE.Settings);
        }
everywhere


TO DO : implement streaming endpoint scale operations