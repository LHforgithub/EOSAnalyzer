using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Composition;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EOSAnalyzer
{
    [ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(EOSAnalyzerCodeFixProvider)), Shared]
    public class EOSAnalyzerCodeFixProvider : CodeFixProvider
    {
        public override sealed ImmutableArray<string> FixableDiagnosticIds { get; } =
            ImmutableArray.Create(
                EOSAnalyzerDataBase.DiagnosticId_EOS001AOR,
                EOSAnalyzerDataBase.DiagnosticId_EOS002AOR,
                EOSAnalyzerDataBase.DiagnosticId_EOS003AOR,
                EOSAnalyzerDataBase.DiagnosticId_EOS004AOR,
                EOSAnalyzerDataBase.DiagnosticId_EOS005AOR,
                EOSAnalyzerDataBase.DiagnosticId_EOS006AOR,
                EOSAnalyzerDataBase.DiagnosticId_EOS007AOR,
                EOSAnalyzerDataBase.DiagnosticId_EOS008AOR,
                EOSAnalyzerDataBase.DiagnosticId_EOS009AOR,
                EOSAnalyzerDataBase.DiagnosticId_EOS001B,
                EOSAnalyzerDataBase.DiagnosticId_EOS002B,
                EOSAnalyzerDataBase.DiagnosticId_EOS003B,
                EOSAnalyzerDataBase.DiagnosticId_EOS004B,
                EOSAnalyzerDataBase.DiagnosticId_EOS005B,
                EOSAnalyzerDataBase.DiagnosticId_EOS006B,
                EOSAnalyzerDataBase.DiagnosticId_EOS007B,
                EOSAnalyzerDataBase.DiagnosticId_EOS008B,
                EOSAnalyzerDataBase.DiagnosticId_EOS009B,
                EOSAnalyzerDataBase.DiagnosticId_EOS010B,
                EOSAnalyzerDataBase.DiagnosticId_EOS011B,
                EOSAnalyzerDataBase.DiagnosticId_EOS001C,
                EOSAnalyzerDataBase.DiagnosticId_EOS002C,
                EOSAnalyzerDataBase.DiagnosticId_EOS003C,
                EOSAnalyzerDataBase.DiagnosticId_EOS004C,
                EOSAnalyzerDataBase.DiagnosticId_EOS005C,
                EOSAnalyzerDataBase.DiagnosticId_EOS006C,
                EOSAnalyzerDataBase.DiagnosticId_EOS001L,
                EOSAnalyzerDataBase.DiagnosticId_EOS002L,
                EOSAnalyzerDataBase.DiagnosticId_EOS003L,
                EOSAnalyzerDataBase.DiagnosticId_EOS004L,
                EOSAnalyzerDataBase.DiagnosticId_EOS005L,
                EOSAnalyzerDataBase.DiagnosticId_EOS006L,
                EOSAnalyzerDataBase.DiagnosticId_EOS007L,
                EOSAnalyzerDataBase.DiagnosticId_EOS008L,
                EOSAnalyzerDataBase.DiagnosticId_EOS009L
            );

        public override sealed FixAllProvider GetFixAllProvider()
        {
            // See https://github.com/dotnet/roslyn/blob/main/docs/analyzers/FixAllProvider.md for more information on Fix All Providers
            return WellKnownFixAllProviders.BatchFixer;
        }

        public override sealed async Task RegisterCodeFixesAsync(CodeFixContext context)
        {
            var root = await context.Document.GetSyntaxRootAsync(context.CancellationToken);
            var tree = await context.Document.GetSyntaxTreeAsync(context.CancellationToken);
            var semanticModel = await context.Document.GetSemanticModelAsync(context.CancellationToken);
            foreach (var diagnostic in context.Diagnostics)
            {
                var solutions = new List<CodeAction>();
                switch (diagnostic.Id)
                {
                    case EOSAnalyzerDataBase.DiagnosticId_EOS002C:
                        {
                            var syntaxSpan = diagnostic.Location.SourceSpan;
                            if (root.FindNode(syntaxSpan) is AttributeSyntax syntax)
                            {
                                solutions.Add(CodeAction.Create(
                                    title: CodeFixResources.CodeFixTitle_RemoveIncorrectAttribute,
                                    createChangedSolution: c => Solute_RemoveIncorrectAttribute(context.Document, c, syntax),
                                    equivalenceKey: nameof(CodeFixResources.CodeFixTitle_RemoveIncorrectAttribute))
                                );
                            }
                            break;
                        }
                    case EOSAnalyzerDataBase.DiagnosticId_EOS003C:
                        {
                            var syntaxSpan = diagnostic.Location.SourceSpan;
                            if (root.FindNode(syntaxSpan) is AttributeSyntax syntax)
                            {
                                solutions.Add(CodeAction.Create(
                                    title: CodeFixResources.CodeFixTitle_RemoveIncorrectAttribute,
                                    createChangedSolution: c => Solute_RemoveIncorrectAttribute(context.Document, c, syntax),
                                    equivalenceKey: nameof(CodeFixResources.CodeFixTitle_RemoveIncorrectAttribute))
                                );
                            }
                            break;
                        }
                    case EOSAnalyzerDataBase.DiagnosticId_EOS004C:
                        {
                            var syntaxSpan = diagnostic.Location.SourceSpan;
                            if (root.FindNode(syntaxSpan)?.Parent?.Parent is MethodDeclarationSyntax syntax)
                            {
                                var method = semanticModel.GetDeclaredSymbol(syntax);
                                var classSyntax = await method?.ContainingType?.DeclaringSyntaxReferences.SingleOrDefault()?.GetSyntaxAsync();
                                if (classSyntax is ClassDeclarationSyntax cSyntax)
                                {
                                    solutions.Add(CodeAction.Create(
                                        title: CodeFixResources.CodeFixTitle_AddEventCodeAttribute,
                                        createChangedSolution: c => Solute_AddEventCodeAttribute(context.Document, c, cSyntax),
                                        equivalenceKey: nameof(CodeFixResources.CodeFixTitle_AddEventCodeAttribute))
                                    );
                                }
                            }
                            break;
                        }
                    case EOSAnalyzerDataBase.DiagnosticId_EOS005C:
                        {
                            var syntaxSpan = diagnostic.Location.SourceSpan;
                            if (root.FindNode(syntaxSpan) is AttributeSyntax syntax)
                            {
                                solutions.Add(CodeAction.Create(
                                    title: CodeFixResources.CodeFixTitle_RemoveIncorrectAttribute,
                                    createChangedSolution: c => Solute_RemoveIncorrectAttribute(context.Document, c, syntax),
                                    equivalenceKey: nameof(CodeFixResources.CodeFixTitle_RemoveIncorrectAttribute))
                                );
                            }
                            break;
                        }
                    case EOSAnalyzerDataBase.DiagnosticId_EOS006C:
                        {
                            var syntaxSpan = diagnostic.Location.SourceSpan;
                            if (root.FindNode(syntaxSpan) is ClassDeclarationSyntax syntax)
                            {
                                var codeAttri = semanticModel.Compilation.GetTypeByMetadataName(typeof(EOS.Attributes.EventCodeAttribute).FullName);
                                var attri = syntax.AttributeLists
                                    .SelectMany(x => x.Attributes)
                                    .SingleOrDefault(x =>
                                        {
                                            var attriSymbol = semanticModel.GetTypeInfo(x).Type;
                                            return SymbolEqualityComparer.Default.Equals(attriSymbol, codeAttri);
                                        });
                                if (attri != null)
                                {
                                    solutions.Add(CodeAction.Create(
                                        title: CodeFixResources.CodeFixTitle_RemoveIncorrectAttribute,
                                        createChangedSolution: c => Solute_RemoveIncorrectAttribute(context.Document, c, attri),
                                        equivalenceKey: nameof(CodeFixResources.CodeFixTitle_RemoveIncorrectAttribute))
                                    );
                                    solutions.Add(CodeAction.Create(
                                        title: CodeFixResources.CodeFixTitle_RemoveGenericType,
                                        createChangedSolution: c => Solute_RemoveGenericType(context.Document, c, syntax),
                                        equivalenceKey: nameof(CodeFixResources.CodeFixTitle_RemoveGenericType))
                                    );
                                }
                            }
                            break;
                        }
                    case EOSAnalyzerDataBase.DiagnosticId_EOS004L:
                        {
                            var syntaxSpan = diagnostic.Location.SourceSpan;
                            if (root.FindNode(syntaxSpan) is MethodDeclarationSyntax syntax)
                            {
                                solutions.Add(CodeAction.Create(
                                    title: CodeFixResources.CodeFixTitle_ExactMathParameter,
                                    createChangedSolution: c => Solute_ExactMatchParameters_EventListener(context.Document, c, syntax),
                                    equivalenceKey: nameof(CodeFixResources.CodeFixTitle_ExactMathParameter))
                                );
                            }
                            break;
                        }
                    case EOSAnalyzerDataBase.DiagnosticId_EOS005L:
                        {
                            var syntaxSpan = diagnostic.Location.SourceSpan;
                            if (root.FindNode(syntaxSpan)?.Parent?.Parent is MethodDeclarationSyntax syntax)
                            {
                                solutions.Add(CodeAction.Create(
                                    title: CodeFixResources.CodeFixTitle_ExactMathParameter,
                                    createChangedSolution: c => Solute_ExactMatchParameters_EventListener(context.Document, c, syntax),
                                    equivalenceKey: nameof(CodeFixResources.CodeFixTitle_ExactMathParameter))
                                );
                            }
                            break;
                        }
                    case EOSAnalyzerDataBase.DiagnosticId_EOS007L:
                        {
                            var syntaxSpan = diagnostic.Location.SourceSpan;
                            if (root.FindNode(syntaxSpan)?.Parent?.Parent is MethodDeclarationSyntax syntax)
                            {
                                var method = semanticModel.GetDeclaredSymbol(syntax);
                                var classSyntax = await method?.ContainingType?.DeclaringSyntaxReferences.SingleOrDefault()?.GetSyntaxAsync();
                                if (classSyntax is ClassDeclarationSyntax cSyntax)
                                {
                                    solutions.Add(CodeAction.Create(
                                        title: CodeFixResources.CodeFixTitle_AddEventListsnerAttribute,
                                        createChangedSolution: c => Solute_AddEventListenerAttribute(context.Document, c, cSyntax),
                                        equivalenceKey: nameof(CodeFixResources.CodeFixTitle_AddEventListsnerAttribute))
                                    );
                                }
                            }
                            break;
                        }
                    case EOSAnalyzerDataBase.DiagnosticId_EOS009L:
                        {
                            var syntaxSpan = diagnostic.Location.SourceSpan;
                            if (root.FindNode(syntaxSpan) is ClassDeclarationSyntax syntax)
                            {
                                var codeAttri = semanticModel.Compilation.GetTypeByMetadataName(typeof(EOS.Attributes.EventListenerAttribute).FullName);
                                var attri = syntax.AttributeLists
                                    .SelectMany(x => x.Attributes)
                                    .SingleOrDefault(x =>
                                    {
                                        var attriSymbol = semanticModel.GetTypeInfo(x).Type;
                                        return SymbolEqualityComparer.Default.Equals(attriSymbol, codeAttri);
                                    });
                                if (attri != null)
                                {
                                    solutions.Add(CodeAction.Create(
                                        title: CodeFixResources.CodeFixTitle_RemoveIncorrectAttribute,
                                        createChangedSolution: c => Solute_RemoveIncorrectAttribute(context.Document, c, attri),
                                        equivalenceKey: nameof(CodeFixResources.CodeFixTitle_RemoveIncorrectAttribute))
                                    );
                                    solutions.Add(CodeAction.Create(
                                        title: CodeFixResources.CodeFixTitle_RemoveGenericType,
                                        createChangedSolution: c => Solute_RemoveGenericType(context.Document, c, syntax),
                                        equivalenceKey: nameof(CodeFixResources.CodeFixTitle_RemoveGenericType))
                                    );
                                }
                            }
                            break;
                        }
                    default:
                        break;
                }
                foreach (var solution in solutions)
                {
                    context.RegisterCodeFix(solution, diagnostic);
                }
            }
        }

        /// <summary>
        /// 在类型上添加事件码
        /// </summary>
        private async Task<Solution> Solute_AddEventCodeAttribute(Document document, CancellationToken cancellationToken, ClassDeclarationSyntax syntax)
        {
            var root = await document.GetSyntaxRootAsync(cancellationToken);
            var str = $"{nameof(EOS.Attributes.EventCodeAttribute).Replace(nameof(Attribute), "")}";
            var attriList =
                SyntaxFactory.AttributeList(
                    SyntaxFactory.SingletonSeparatedList(
                        SyntaxFactory.Attribute(
                            SyntaxFactory.IdentifierName(str))));
            // 创建语法节点：添加了Attribute的类声明
            var newClassSyntax = syntax.AddAttributeLists(attriList);

            // 创建中间文件：替换原节点为新节点
            var newRoot = root.ReplaceNode(syntax, newClassSyntax);

            // 创建最终文件：返回包含新语法树的解决方案
            return document.Project.Solution.WithDocumentSyntaxRoot(document.Id, newRoot);
        }

        /// <summary>
        /// 在类型上添加事件接收者
        /// </summary>
        private async Task<Solution> Solute_AddEventListenerAttribute(Document document, CancellationToken cancellationToken, ClassDeclarationSyntax syntax)
        {
            var root = await document.GetSyntaxRootAsync(cancellationToken);
            var str = $"{nameof(EOS.Attributes.EventListenerAttribute).Replace(nameof(Attribute), "")}";
            var attriList =
                SyntaxFactory.AttributeList(
                    SyntaxFactory.SingletonSeparatedList(
                        SyntaxFactory.Attribute(
                            SyntaxFactory.IdentifierName(str))));
            // 创建语法节点：添加了Attribute的类声明
            var newClassSyntax = syntax.AddAttributeLists(attriList);

            // 创建中间文件：替换原节点为新节点
            var newRoot = root.ReplaceNode(syntax, newClassSyntax);

            // 创建最终文件：返回包含新语法树的解决方案
            return document.Project.Solution.WithDocumentSyntaxRoot(document.Id, newRoot);
        }

        /// <summary>
        /// 修正事件方法参数列表
        /// </summary>
        private async Task<Solution> Solute_ExactMatchParameters_EventListener(Document document, CancellationToken cancellationToken, MethodDeclarationSyntax syntax)
        {
            var root = await document.GetSyntaxRootAsync(cancellationToken);
            var semanticModel = await document.GetSemanticModelAsync(cancellationToken);

            var listenerAttibute = semanticModel.Compilation.GetTypeByMetadataName(typeof(EOS.Attributes.EventListenerAttribute).FullName);
            var codeMethodAttibute = semanticModel.Compilation.GetTypeByMetadataName(typeof(EOS.Attributes.EventCodeMethodAttribute).FullName);

            var methodSymbol = semanticModel.GetDeclaredSymbol(syntax);
            var attri = methodSymbol.GetAttributes().SingleOrDefault(x => SymbolEqualityComparer.Default.Equals(x.AttributeClass, listenerAttibute));
            var codeClass = attri.ConstructorArguments[0].Value as INamedTypeSymbol;
            var method = codeClass.GetMembers()
                .Where(x => x.Kind== SymbolKind.Method)
                .OfType<IMethodSymbol>()
                .SingleOrDefault(
                    x => x.GetAttributes()
                    .Any(y => SymbolEqualityComparer.Default.Equals(y.AttributeClass, codeMethodAttibute)));

            var newParamList = SyntaxFactory.ParseParameterList(
                (method.DeclaringSyntaxReferences.SingleOrDefault().GetSyntax() as MethodDeclarationSyntax).ParameterList.ToFullString());
            var newMethod = syntax.WithParameterList(newParamList);
            var newRoot = root.ReplaceNode(syntax, newMethod);
            return document.Project.Solution.WithDocumentSyntaxRoot(document.Id, newRoot);
        }

        /// <summary>
        /// 移除泛型
        /// </summary>
        private async Task<Solution> Solute_RemoveGenericType(Document document, CancellationToken cancellationToken, ClassDeclarationSyntax syntax)
        {
            var root = await document.GetSyntaxRootAsync(cancellationToken);
            var TypeList = syntax.TypeParameterList;
            SyntaxNode intermediaRoot = root.RemoveNode(TypeList, SyntaxRemoveOptions.KeepEndOfLine | SyntaxRemoveOptions.KeepLeadingTrivia | SyntaxRemoveOptions.KeepTrailingTrivia);
            var newRoot = intermediaRoot;
            return document.Project.Solution.WithDocumentSyntaxRoot(document.Id, newRoot);
        }

        /// <summary>
        /// 移除输入的特性
        /// </summary>
        private async Task<Solution> Solute_RemoveIncorrectAttribute(Document document, CancellationToken cancellationToken, AttributeSyntax syntax)
        {
            var root = await document.GetSyntaxRootAsync(cancellationToken);
            var attriList = syntax.Parent as AttributeListSyntax;
            SyntaxNode intermediaRoot = null;
            if (attriList.Attributes.Count < 2)
            {
                intermediaRoot = root.RemoveNode(attriList, SyntaxRemoveOptions.KeepNoTrivia);
            }
            else
            {
                intermediaRoot = root.RemoveNode(syntax, SyntaxRemoveOptions.KeepNoTrivia);
            }
            var newRoot = intermediaRoot;
            return document.Project.Solution.WithDocumentSyntaxRoot(document.Id, newRoot);
        }
    }
}