using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace SmartVectorDotNet.DynamicServices
{
    internal interface IMethodReplacementStrategy
    {
        bool TryReplaceTo(MethodInfo original, out MethodInfo replaceTo);
    }
}
