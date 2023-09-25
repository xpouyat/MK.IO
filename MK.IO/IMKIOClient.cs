﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using MK.IO.Asset.Operations;
using Newtonsoft.Json;
using System.Collections.Specialized;
using System.Net.Http.Headers;
using System.Web;

namespace MK.IO
{
    /// <summary>
    /// REST Base Client for MKIO
    /// https://io.mediakind.com
    /// 
    /// </summary>
    public partial interface IMKIOClient : System.IDisposable
    {
        string baseUrl { get; }
        string _MKIOSubscriptionName { get; }

        IAssetsOperations Assets { get; }
    }
}