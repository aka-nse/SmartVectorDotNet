using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using SmartVectorDotNet;

namespace System
{
    internal static class RandomExtensions
    {
        public static void NextBytes(this Random random, Span<byte> destination)
        {
            using var buffer = new TemporaryBuffer<byte>(destination.Length);
            random.NextBytes(buffer.Array);
            buffer.Span.CopyTo(destination);
        }

        public static long NextInt64(this Random random)
        {
            var buffer = new byte[8];
            random.NextBytes(buffer);
            return (long)(Unsafe.As<byte, ulong>(ref buffer[0]) & 0x7FFF_FFFF_FFFF_FFFFuL);
        }
    }
}
