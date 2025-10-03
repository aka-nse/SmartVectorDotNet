using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.CodeAnalysis;

namespace SmartVectorDotNet.Generators;

[Generator(LanguageNames.CSharp)]
public partial class VectorizationFormulaInterceptorGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput(static cxt =>
        {
            cxt.AddSource("Vectorization.VectorizationFormulaExtension.g.cs", VectorizationFormulaExtension);
        });
    }
}
