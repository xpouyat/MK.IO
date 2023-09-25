// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.


using MK.IO.Models;

namespace MK.IO
{
    public interface ITransformsOperations
    {
        TransformSchema Create(string transformName, TransformProperties properties);
        Task<TransformSchema> CreateAsync(string transformName, TransformProperties properties);
        void Delete(string transformName);
        Task DeleteAsync(string transformName);
        TransformSchema Get(string transformName);
        Task<TransformSchema> GetAsync(string transformName);
        List<TransformSchema> List();
        Task<List<TransformSchema>> ListAsync();
    }
}