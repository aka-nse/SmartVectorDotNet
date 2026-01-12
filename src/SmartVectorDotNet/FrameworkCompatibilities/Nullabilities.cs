#if NETSTANDARD2_0
#pragma warning disable IDE0130
namespace System.Diagnostics.CodeAnalysis;

/// <summary />
[ExcludeFromCodeCoverage]
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Property, Inherited = false)]
internal class AllowNullAttribute : Attribute;

/// <summary />
[ExcludeFromCodeCoverage]
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Property, Inherited = false)]
internal class DisallowNullAttribute : Attribute;

/// <summary />
[ExcludeFromCodeCoverage]
[AttributeUsage(AttributeTargets.Method, Inherited = false)]
internal class DoesNotReturnAttribute : Attribute;

/// <summary />
[ExcludeFromCodeCoverage]
[AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
internal class DoesNotReturnIfAttribute(bool parameterValue) : Attribute
{
    /// <summary />
    public bool ParameterValue { get; } = parameterValue;
}

/// <summary />
[ExcludeFromCodeCoverage]
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Property | AttributeTargets.ReturnValue, Inherited = false)]
internal class MaybeNullAttribute : Attribute;

/// <summary />
[ExcludeFromCodeCoverage]
[AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
internal class MaybeNullWhenAttribute(bool returnValue) : Attribute
{
    /// <summary />
    public bool ReturnValue { get; } = returnValue;
}

/// <summary />
[ExcludeFromCodeCoverage]
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = true, Inherited = false)]
internal class MemberNotNullAttribute(string[] members) : Attribute
{
    /// <summary />
    public string[] Members { get; } = members;

    /// <summary />
    public MemberNotNullAttribute(string member) : this([member])
    {
    }
}

/// <summary />
[ExcludeFromCodeCoverage]
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = true, Inherited = false)]
internal class MemberNotNullWhenAttribute : Attribute;

/// <summary />
[ExcludeFromCodeCoverage]
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.Property | AttributeTargets.ReturnValue, Inherited = false)]
internal class NotNullAttribute : Attribute;

/// <summary />
[ExcludeFromCodeCoverage]
[AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Property | AttributeTargets.ReturnValue, AllowMultiple = true, Inherited = false)]
internal class NotNullIfNotNullAttribute(string parameterName) : Attribute
{
    /// <summary />
    public string ParameterName { get; } = parameterName;
}

/// <summary />
[ExcludeFromCodeCoverage]
[AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
internal class NotNullWhenAttribute(bool returnValue) : Attribute
{
    /// <summary />
    public bool ReturnValue { get; } = returnValue;
}

#pragma warning restore IDE0130
#endif