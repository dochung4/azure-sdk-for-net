// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System;
using System.ComponentModel;

namespace Azure.AI.MetricsAdvisor.Models
{
    /// <summary> mark if the data feed need rollup. </summary>
    public readonly partial struct DataFeedRollupType : IEquatable<DataFeedRollupType>
    {
        private readonly string _value;

        /// <summary> Determines if two <see cref="DataFeedRollupType"/> values are the same. </summary>
        /// <exception cref="ArgumentNullException"> <paramref name="value"/> is null. </exception>
        public DataFeedRollupType(string value)
        {
            _value = value ?? throw new ArgumentNullException(nameof(value));
        }

        private const string NoRollupValue = "NoRollup";
        private const string NeedRollupValue = "NeedRollup";
        private const string AlreadyRollupValue = "AlreadyRollup";

        /// <summary> NoRollup. </summary>
        public static DataFeedRollupType NoRollup { get; } = new DataFeedRollupType(NoRollupValue);
        /// <summary> NeedRollup. </summary>
        public static DataFeedRollupType NeedRollup { get; } = new DataFeedRollupType(NeedRollupValue);
        /// <summary> AlreadyRollup. </summary>
        public static DataFeedRollupType AlreadyRollup { get; } = new DataFeedRollupType(AlreadyRollupValue);
        /// <summary> Determines if two <see cref="DataFeedRollupType"/> values are the same. </summary>
        public static bool operator ==(DataFeedRollupType left, DataFeedRollupType right) => left.Equals(right);
        /// <summary> Determines if two <see cref="DataFeedRollupType"/> values are not the same. </summary>
        public static bool operator !=(DataFeedRollupType left, DataFeedRollupType right) => !left.Equals(right);
        /// <summary> Converts a string to a <see cref="DataFeedRollupType"/>. </summary>
        public static implicit operator DataFeedRollupType(string value) => new DataFeedRollupType(value);

        /// <inheritdoc />
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override bool Equals(object obj) => obj is DataFeedRollupType other && Equals(other);
        /// <inheritdoc />
        public bool Equals(DataFeedRollupType other) => string.Equals(_value, other._value, StringComparison.InvariantCultureIgnoreCase);

        /// <inheritdoc />
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override int GetHashCode() => _value?.GetHashCode() ?? 0;
        /// <inheritdoc />
        public override string ToString() => _value;
    }
}
