using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace SmartVectorDotNet.Generators.Internal;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
internal class UnsafeContextAnalyzer : DiagnosticAnalyzer
{
    private static readonly DiagnosticDescriptor _rule = new(
        $"{GlobalConstants.DiagnosticIDBase}1001",
        "Unsafe context required",
        "Unsafe API `{0}` is required to be used within an `unsafe` context",
        "Security",
        DiagnosticSeverity.Warning,
        isEnabledByDefault: true);

    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } =
        [_rule];

    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
        context.EnableConcurrentExecution();
        context.RegisterSyntaxNodeAction(AnalyzeUnsafeApiCall,
            SyntaxKind.InvocationExpression);
    }

    private static void AnalyzeUnsafeApiCall(SyntaxNodeAnalysisContext context)
    {
        var member = context.SemanticModel.GetSymbolInfo(context.Node).Symbol;
        if (member?.ContainingType is not { } typeSymbol)
        {
            return;
        }
        if (!member.Name.Contains("GatherCore")) { return; }

        switch (typeSymbol.ToDisplayString())
        {
        case "System.Runtime.CompilerServices.Unsafe":
        case "System.Runtime.InteropServices.MemoryMarshal":
            break;
        default:
            var name = member.Name;
            TextWriter.Null.WriteLine(name);
            if(member.GetAttributes().Any(static attr => attr.AttributeClass?.ToDisplayString() == "SmartVectorDotNet.UnsafeApiAttribute"))
            {
                // UnsafeApiAttribute indicates that the type is unsafe
                break;
            }
            // Not an unsafe API
            return;
        }

        foreach(var ancestor in context.Node.Ancestors())
        {
            if (ancestor is UnsafeStatementSyntax)
            {
                // Already in an unsafe context
                return;
            }
            if(ancestor is MethodDeclarationSyntax methodDeclaration
                && methodDeclaration.ChildTokens().Any(static token => token.IsKind(SyntaxKind.UnsafeKeyword)))
            {
                // Already in an unsafe context
                return;
            }
        }

        context.ReportDiagnostic(Diagnostic.Create(
            _rule,
            context.Node.GetLocation(),
            member.ToDisplayString()));
    }
}
