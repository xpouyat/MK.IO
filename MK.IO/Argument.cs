// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

namespace MK.IO
{
    internal class Argument
    {
        public static void AssertNotNullOrEmpty(string value, string name)
        {
            if (value is null)
            {
                throw new ArgumentNullException(name);
            }

            if (value.Length == 0)
            {
                throw new ArgumentException("Value cannot be an empty string.", name);
            }
        }

        public static void AssertNotNull<T>(T value, string name)
        {
            if (value is null)
            {
                throw new ArgumentNullException(name);
            }
        }

        public static void AssertNotContainsSpace(string value, string name)
        {
            if (value.Contains(' '))
            {
                throw new ArgumentException("Value cannot contain space.", name);
            }
        }

        public static void AssertNotMoreThanLength(string value, string name, int length)
        {
            if (value.Length > length)
            {
                throw new ArgumentException($"Value length cannot exceed {length}.", name);
            }
        }
    }
}