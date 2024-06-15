﻿// <auto-generated>
// THIS (.cs) FILE IS GENERATED BY T4. DO NOT CHANGE IT. CHANGE THE .tt FILE INSTEAD.
// </auto-generated>
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;
using BenchmarkDotNet.Attributes;

namespace SmartVectorBenchmarks;

partial class LogarithmBenchmarkContext
{
    public IReadOnlyDictionary<string, double> EvaluateErrors()
    {
        var retval = new Dictionary<string, double>();
        var logEmulated = Use_Log_Emulated();
        retval["Log_5"] = BenchmarkHelper.CalculateErrorAbsolute(logEmulated, Use_Log_5());
        retval["Log_8"] = BenchmarkHelper.CalculateErrorAbsolute(logEmulated, Use_Log_8());
        retval["Log_10"] = BenchmarkHelper.CalculateErrorAbsolute(logEmulated, Use_Log_10());
        retval["Log_20"] = BenchmarkHelper.CalculateErrorAbsolute(logEmulated, Use_Log_20());
        retval["Log_30"] = BenchmarkHelper.CalculateErrorAbsolute(logEmulated, Use_Log_30());
        retval["Log_50"] = BenchmarkHelper.CalculateErrorAbsolute(logEmulated, Use_Log_50());
        return retval;
    }

    [Benchmark]
    public double[] Use_Log_5()
    {
        var retval = new double[BenchmarkSize];
        var retvalAsVector = MemoryMarshal.Cast<double, Vector<double>>(retval);
        var xAsVector = XAsVector;
        for(var i = 0; i < xAsVector.Length; ++i)
        {
            retvalAsVector[i] = Log_5(xAsVector[i]);
        }
        return retval;
    }

    private static Vector<double> Log_5(in Vector<double> x)
    {
        var xx = x * OnePerRoot2 - Vector<double>.One;
        return LogRoot2
            + xx * (LogCoeffs[1 - 1]
            + xx * (LogCoeffs[2 - 1]
            + xx * (LogCoeffs[3 - 1]
            + xx * (LogCoeffs[4 - 1]
            ))));
    }


    [Benchmark]
    public double[] Use_Log_8()
    {
        var retval = new double[BenchmarkSize];
        var retvalAsVector = MemoryMarshal.Cast<double, Vector<double>>(retval);
        var xAsVector = XAsVector;
        for(var i = 0; i < xAsVector.Length; ++i)
        {
            retvalAsVector[i] = Log_8(xAsVector[i]);
        }
        return retval;
    }

    private static Vector<double> Log_8(in Vector<double> x)
    {
        var xx = x * OnePerRoot2 - Vector<double>.One;
        return LogRoot2
            + xx * (LogCoeffs[1 - 1]
            + xx * (LogCoeffs[2 - 1]
            + xx * (LogCoeffs[3 - 1]
            + xx * (LogCoeffs[4 - 1]
            + xx * (LogCoeffs[5 - 1]
            + xx * (LogCoeffs[6 - 1]
            + xx * (LogCoeffs[7 - 1]
            )))))));
    }


    [Benchmark]
    public double[] Use_Log_10()
    {
        var retval = new double[BenchmarkSize];
        var retvalAsVector = MemoryMarshal.Cast<double, Vector<double>>(retval);
        var xAsVector = XAsVector;
        for(var i = 0; i < xAsVector.Length; ++i)
        {
            retvalAsVector[i] = Log_10(xAsVector[i]);
        }
        return retval;
    }

    private static Vector<double> Log_10(in Vector<double> x)
    {
        var xx = x * OnePerRoot2 - Vector<double>.One;
        return LogRoot2
            + xx * (LogCoeffs[1 - 1]
            + xx * (LogCoeffs[2 - 1]
            + xx * (LogCoeffs[3 - 1]
            + xx * (LogCoeffs[4 - 1]
            + xx * (LogCoeffs[5 - 1]
            + xx * (LogCoeffs[6 - 1]
            + xx * (LogCoeffs[7 - 1]
            + xx * (LogCoeffs[8 - 1]
            + xx * (LogCoeffs[9 - 1]
            )))))))));
    }


    [Benchmark]
    public double[] Use_Log_20()
    {
        var retval = new double[BenchmarkSize];
        var retvalAsVector = MemoryMarshal.Cast<double, Vector<double>>(retval);
        var xAsVector = XAsVector;
        for(var i = 0; i < xAsVector.Length; ++i)
        {
            retvalAsVector[i] = Log_20(xAsVector[i]);
        }
        return retval;
    }

    private static Vector<double> Log_20(in Vector<double> x)
    {
        var xx = x * OnePerRoot2 - Vector<double>.One;
        return LogRoot2
            + xx * (LogCoeffs[1 - 1]
            + xx * (LogCoeffs[2 - 1]
            + xx * (LogCoeffs[3 - 1]
            + xx * (LogCoeffs[4 - 1]
            + xx * (LogCoeffs[5 - 1]
            + xx * (LogCoeffs[6 - 1]
            + xx * (LogCoeffs[7 - 1]
            + xx * (LogCoeffs[8 - 1]
            + xx * (LogCoeffs[9 - 1]
            + xx * (LogCoeffs[10 - 1]
            + xx * (LogCoeffs[11 - 1]
            + xx * (LogCoeffs[12 - 1]
            + xx * (LogCoeffs[13 - 1]
            + xx * (LogCoeffs[14 - 1]
            + xx * (LogCoeffs[15 - 1]
            + xx * (LogCoeffs[16 - 1]
            + xx * (LogCoeffs[17 - 1]
            + xx * (LogCoeffs[18 - 1]
            + xx * (LogCoeffs[19 - 1]
            )))))))))))))))))));
    }


    [Benchmark]
    public double[] Use_Log_30()
    {
        var retval = new double[BenchmarkSize];
        var retvalAsVector = MemoryMarshal.Cast<double, Vector<double>>(retval);
        var xAsVector = XAsVector;
        for(var i = 0; i < xAsVector.Length; ++i)
        {
            retvalAsVector[i] = Log_30(xAsVector[i]);
        }
        return retval;
    }

    private static Vector<double> Log_30(in Vector<double> x)
    {
        var xx = x * OnePerRoot2 - Vector<double>.One;
        return LogRoot2
            + xx * (LogCoeffs[1 - 1]
            + xx * (LogCoeffs[2 - 1]
            + xx * (LogCoeffs[3 - 1]
            + xx * (LogCoeffs[4 - 1]
            + xx * (LogCoeffs[5 - 1]
            + xx * (LogCoeffs[6 - 1]
            + xx * (LogCoeffs[7 - 1]
            + xx * (LogCoeffs[8 - 1]
            + xx * (LogCoeffs[9 - 1]
            + xx * (LogCoeffs[10 - 1]
            + xx * (LogCoeffs[11 - 1]
            + xx * (LogCoeffs[12 - 1]
            + xx * (LogCoeffs[13 - 1]
            + xx * (LogCoeffs[14 - 1]
            + xx * (LogCoeffs[15 - 1]
            + xx * (LogCoeffs[16 - 1]
            + xx * (LogCoeffs[17 - 1]
            + xx * (LogCoeffs[18 - 1]
            + xx * (LogCoeffs[19 - 1]
            + xx * (LogCoeffs[20 - 1]
            + xx * (LogCoeffs[21 - 1]
            + xx * (LogCoeffs[22 - 1]
            + xx * (LogCoeffs[23 - 1]
            + xx * (LogCoeffs[24 - 1]
            + xx * (LogCoeffs[25 - 1]
            + xx * (LogCoeffs[26 - 1]
            + xx * (LogCoeffs[27 - 1]
            + xx * (LogCoeffs[28 - 1]
            + xx * (LogCoeffs[29 - 1]
            )))))))))))))))))))))))))))));
    }


    [Benchmark]
    public double[] Use_Log_50()
    {
        var retval = new double[BenchmarkSize];
        var retvalAsVector = MemoryMarshal.Cast<double, Vector<double>>(retval);
        var xAsVector = XAsVector;
        for(var i = 0; i < xAsVector.Length; ++i)
        {
            retvalAsVector[i] = Log_50(xAsVector[i]);
        }
        return retval;
    }

    private static Vector<double> Log_50(in Vector<double> x)
    {
        var xx = x * OnePerRoot2 - Vector<double>.One;
        return LogRoot2
            + xx * (LogCoeffs[1 - 1]
            + xx * (LogCoeffs[2 - 1]
            + xx * (LogCoeffs[3 - 1]
            + xx * (LogCoeffs[4 - 1]
            + xx * (LogCoeffs[5 - 1]
            + xx * (LogCoeffs[6 - 1]
            + xx * (LogCoeffs[7 - 1]
            + xx * (LogCoeffs[8 - 1]
            + xx * (LogCoeffs[9 - 1]
            + xx * (LogCoeffs[10 - 1]
            + xx * (LogCoeffs[11 - 1]
            + xx * (LogCoeffs[12 - 1]
            + xx * (LogCoeffs[13 - 1]
            + xx * (LogCoeffs[14 - 1]
            + xx * (LogCoeffs[15 - 1]
            + xx * (LogCoeffs[16 - 1]
            + xx * (LogCoeffs[17 - 1]
            + xx * (LogCoeffs[18 - 1]
            + xx * (LogCoeffs[19 - 1]
            + xx * (LogCoeffs[20 - 1]
            + xx * (LogCoeffs[21 - 1]
            + xx * (LogCoeffs[22 - 1]
            + xx * (LogCoeffs[23 - 1]
            + xx * (LogCoeffs[24 - 1]
            + xx * (LogCoeffs[25 - 1]
            + xx * (LogCoeffs[26 - 1]
            + xx * (LogCoeffs[27 - 1]
            + xx * (LogCoeffs[28 - 1]
            + xx * (LogCoeffs[29 - 1]
            + xx * (LogCoeffs[30 - 1]
            + xx * (LogCoeffs[31 - 1]
            + xx * (LogCoeffs[32 - 1]
            + xx * (LogCoeffs[33 - 1]
            + xx * (LogCoeffs[34 - 1]
            + xx * (LogCoeffs[35 - 1]
            + xx * (LogCoeffs[36 - 1]
            + xx * (LogCoeffs[37 - 1]
            + xx * (LogCoeffs[38 - 1]
            + xx * (LogCoeffs[39 - 1]
            + xx * (LogCoeffs[40 - 1]
            + xx * (LogCoeffs[41 - 1]
            + xx * (LogCoeffs[42 - 1]
            + xx * (LogCoeffs[43 - 1]
            + xx * (LogCoeffs[44 - 1]
            + xx * (LogCoeffs[45 - 1]
            + xx * (LogCoeffs[46 - 1]
            + xx * (LogCoeffs[47 - 1]
            + xx * (LogCoeffs[48 - 1]
            + xx * (LogCoeffs[49 - 1]
            )))))))))))))))))))))))))))))))))))))))))))))))));
    }

}