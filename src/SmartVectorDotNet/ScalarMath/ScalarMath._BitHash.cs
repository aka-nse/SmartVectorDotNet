using System;
using System.Collections.Generic;
using System.Text;

namespace SmartVectorDotNet;
using H = InternalHelpers;


partial class ScalarMath
{
    /// <summary>
    /// Maps an integer with only one bit being <c>1</c> into <c>0</c>-<c>31</c> bijectively and fastly.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    internal static int BitHash(uint value)
    {
        const uint deBruijnHashKey = 0x04653ADFu;
        return (int)((deBruijnHashKey * value) >> 27);
    }

    /// <summary>
    /// Maps an integer with only one bit being <c>1</c> into <c>0</c>-<c>63</c> bijectively and fastly.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    internal static int BitHash(ulong value)
    {
        const ulong deBruijnHashKey = 0x03F566ED27179461uL;
        return (int)((deBruijnHashKey * value) >> 58);
    }
}
