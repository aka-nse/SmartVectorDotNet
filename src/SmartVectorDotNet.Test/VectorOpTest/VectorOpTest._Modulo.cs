using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SmartVectorDotNet;

public partial class VectorOpTest
{
    [Fact]
    public void ModuloFloat()
    {
        var aa = Enumerable
            .Range(-500, 1001)
            .Select(i => MathF.PI * i)
            .ToArray();
        var bb = Enumerable
            .Range(1, 1000)
            .Select(i => MathF.E * i)
            .ToArray();

        foreach (var a in aa)
        {
            foreach (var b in bb)
            {
                coreF(a, b);
                coreD(a, b);
            }
        }

        static void coreF(float a, float b)
        {
            var exp = a % b;
            var act = VectorOp.Modulo<float>(new(a), new(b))[0];
            Assert.True(
                exp == act,
                $"{a} % {b}\r\n  exp: {exp}\r\n  act: {act}");
        }

        static void coreD(double a, double b)
        {
            var exp = a % b;
            var act = VectorOp.Modulo<double>(new(a), new(b))[0];
            Assert.True(
                exp == act,
                $"{a} % {b}\r\n  exp: {exp}\r\n  act: {act}");
        }
    }


}
