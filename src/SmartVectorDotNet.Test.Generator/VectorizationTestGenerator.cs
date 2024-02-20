using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Microsoft.CodeAnalysis;

namespace SmartVectorDotNet.Test.Generator;

[Generator(LanguageNames.CSharp)]
public class VectorizationTestGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput(static cxt =>
        {
            var source = new VectorizationTest();
            cxt.AddSource("VectorizationTest.g.cs", source.TransformText());
        });
    }
}

partial class VectorizationTest
{
    public MethodInfo[] Methods { get; }

    public VectorizationTest()
    {
        Methods = typeof(Vectorization).GetMethods(BindingFlags.Static | BindingFlags.Public);
    }
}