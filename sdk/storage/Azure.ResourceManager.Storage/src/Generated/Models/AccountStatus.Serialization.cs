// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System;

namespace Azure.ResourceManager.Storage.Models
{
    internal static class AccountStatusExtensions
    {
        public static string ToSerialString(this AccountStatus value) => value switch
        {
            AccountStatus.Available => "available",
            AccountStatus.Unavailable => "unavailable",
            _ => throw new ArgumentOutOfRangeException(nameof(value), value, "Unknown AccountStatus value.")
        };

        public static AccountStatus ToAccountStatus(this string value)
        {
            if (string.Equals(value, "available", StringComparison.InvariantCultureIgnoreCase)) return AccountStatus.Available;
            if (string.Equals(value, "unavailable", StringComparison.InvariantCultureIgnoreCase)) return AccountStatus.Unavailable;
            throw new ArgumentOutOfRangeException(nameof(value), value, "Unknown AccountStatus value.");
        }
    }
}