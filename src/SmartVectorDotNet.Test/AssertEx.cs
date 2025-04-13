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

    [DebuggerHidden]
#if NET6_0_OR_GREATER
    [StackTraceHidden]
#endif
    public static void SameBehavior<T>(Func<T> expected, Func<T> actual)
    {
        bool expectedIsFailed = false;
        bool actualIsFailed = false;
        T expectedResult = default!;
        T actualResult = default!;
        Exception expectedError = default!;
        Exception actualError = default!;
        try
        {
            expectedResult = expected();
        }
        catch(Exception exc)
        {
            expectedIsFailed = true;
            expectedError = exc;
        }
        try
        {
            actualResult = actual();
        }
        catch (Exception exc)
        {
            actualIsFailed = true;
            actualError = exc;
        }

        switch((expectedIsFailed, actualIsFailed))
        {
        case (true, false):
            Assert.Fail($"Expected to fail but succeeded.\r\nexpected error: {expectedError.GetType()}");
            return;
        case (false, true):
            Assert.Fail($"Expected to succeed but failed.\r\nexpected value: {expectedResult}");
            return;
        case (true, true):
            Assert.Equal(expectedError.GetType(), actualError.GetType());
            return;
        case (false, false):
            Assert.Equal(expectedResult, actualResult);
            return;
        }
    }
}
