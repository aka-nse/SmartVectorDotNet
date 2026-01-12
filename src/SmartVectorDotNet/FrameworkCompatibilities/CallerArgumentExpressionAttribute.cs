#if NETCOREAPP3_0_OR_GREATER
#else
using System.Diagnostics.CodeAnalysis;

#pragma warning disable IDE0130
namespace System.Runtime.CompilerServices
#pragma warning restore IDE0130
{
    [ExcludeFromCodeCoverage]
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
    internal sealed class CallerArgumentExpressionAttribute(string parameterName)
        : Attribute
    {
        public string ParameterName => parameterName;
    }
}
#endif
