// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

namespace MK.IO
{
    public partial class MKIOClient
    {
        public partial class ApiException : Exception
        {
            public int StatusCode { get; private set; }

            public string? Response { get; private set; }

            public ApiException(string message, int statusCode, string? response, Exception? innerException)
                : base(message + "\n\nStatus: " + statusCode + "\nResponse: \n" + ((response == null) ? "(null)" : response.Substring(0, response.Length >= 512 ? 512 : response.Length)), innerException)
            {
                StatusCode = statusCode;
                Response = response;
            }

            public override string ToString()
            {
                return string.Format("HTTP Response: \n\n{0}\n\n{1}", Response, base.ToString());
            }
        }
    }
}