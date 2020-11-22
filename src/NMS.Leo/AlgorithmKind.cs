using System;

namespace NMS.Leo
{
    /// <summary>
    /// Kind of algorithm to find tree
    /// </summary>
    [Flags]
    public enum AlgorithmKind
    {
        Fuzzy,
        Hash,
        Precision
    }
}