using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SmartVectorDotNet;

public static class AssertEx
{
    [DebuggerHidden]
#if NET6_0_OR_GREATER
    [StackTraceHidden]
#endif
    public static void Equal<T>(T expected, T actual, string message)
    {
        if(!Equals(expected, actual))
        {
            Assert.Fail($"{message}\r\n  expected: {expected}\r\n  acutual: {actual}");
        }
    }
}
