// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System;

namespace Azure.AI.MetricsAdvisor.Models
{
    /// <summary> The AlertResult. </summary>
    public partial class AlertResult
    {
        /// <summary> Initializes a new instance of AlertResult. </summary>
        /// <param name="timestamp"> anomaly time. </param>
        /// <param name="createdTime"> created time. </param>
        /// <param name="modifiedTime"> modified time. </param>
        internal AlertResult(DateTimeOffset timestamp, DateTimeOffset createdTime, DateTimeOffset modifiedTime)
        {
            Timestamp = timestamp;
            CreatedTime = createdTime;
            ModifiedTime = modifiedTime;
        }

        /// <summary> Initializes a new instance of AlertResult. </summary>
        /// <param name="alertId"> alert id. </param>
        /// <param name="timestamp"> anomaly time. </param>
        /// <param name="createdTime"> created time. </param>
        /// <param name="modifiedTime"> modified time. </param>
        internal AlertResult(string alertId, DateTimeOffset timestamp, DateTimeOffset createdTime, DateTimeOffset modifiedTime)
        {
            AlertId = alertId;
            Timestamp = timestamp;
            CreatedTime = createdTime;
            ModifiedTime = modifiedTime;
        }

        /// <summary> alert id. </summary>
        public string AlertId { get; }
        /// <summary> anomaly time. </summary>
        public DateTimeOffset Timestamp { get; }
        /// <summary> created time. </summary>
        public DateTimeOffset CreatedTime { get; }
        /// <summary> modified time. </summary>
        public DateTimeOffset ModifiedTime { get; }
    }
}
