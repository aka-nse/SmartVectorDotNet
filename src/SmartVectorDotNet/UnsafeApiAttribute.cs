namespace SmartVectorDotNet;

/// <summary>
/// Marks API to be required to make safe on caller side due to its memory insecurity.
/// </summary>
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, Inherited = true)]
internal sealed class UnsafeApiAttribute : Attribute
{
}
