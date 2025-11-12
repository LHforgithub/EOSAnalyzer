using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Operations;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;
using System.Text;

namespace EOSAnalyzer
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class EOSAnalyzer : DiagnosticAnalyzer
    {
        public EOSAnalyzer()
        { }

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } =
            ImmutableArray.Create(
                EOSAnalyzerDataBase.Rule_EOS001AOR,
                EOSAnalyzerDataBase.Rule_EOS002AOR,
                EOSAnalyzerDataBase.Rule_EOS003AOR,
                EOSAnalyzerDataBase.Rule_EOS004AOR,
                EOSAnalyzerDataBase.Rule_EOS005AOR,
                EOSAnalyzerDataBase.Rule_EOS006AOR,
                EOSAnalyzerDataBase.Rule_EOS007AOR,
                EOSAnalyzerDataBase.Rule_EOS008AOR,
                EOSAnalyzerDataBase.Rule_EOS009AOR,
                EOSAnalyzerDataBase.Rule_EOS001B,
                EOSAnalyzerDataBase.Rule_EOS002B,
                EOSAnalyzerDataBase.Rule_EOS003B,
                EOSAnalyzerDataBase.Rule_EOS004B,
                EOSAnalyzerDataBase.Rule_EOS005B,
                EOSAnalyzerDataBase.Rule_EOS006B,
                EOSAnalyzerDataBase.Rule_EOS007B,
                EOSAnalyzerDataBase.Rule_EOS008B,
                EOSAnalyzerDataBase.Rule_EOS009B,
                EOSAnalyzerDataBase.Rule_EOS010B,
                EOSAnalyzerDataBase.Rule_EOS011B,
                EOSAnalyzerDataBase.Rule_EOS001C,
                EOSAnalyzerDataBase.Rule_EOS002C,
                EOSAnalyzerDataBase.Rule_EOS003C,
                EOSAnalyzerDataBase.Rule_EOS004C,
                EOSAnalyzerDataBase.Rule_EOS005C,
                EOSAnalyzerDataBase.Rule_EOS006C,
                EOSAnalyzerDataBase.Rule_EOS001L,
                EOSAnalyzerDataBase.Rule_EOS002L,
                EOSAnalyzerDataBase.Rule_EOS003L,
                EOSAnalyzerDataBase.Rule_EOS004L,
                EOSAnalyzerDataBase.Rule_EOS005L,
                EOSAnalyzerDataBase.Rule_EOS006L,
                EOSAnalyzerDataBase.Rule_EOS007L,
                EOSAnalyzerDataBase.Rule_EOS008L,
                EOSAnalyzerDataBase.Rule_EOS009L,
                EOSAnalyzerDataBase.Rule_EOS010L
            );

        private Compilation TempCompilation
        {
            get
            {
                return tempCompilation;
            }
            set
            {
                tempCompilation = value;
                eventCodeAttribute = null;
                interfaceEventCode = null;
                eventListenerAttribute = null;
                eventCodeMethodAttribute = null;
                systemString = null;
                systemType = null;
            }
        }

        private Compilation tempCompilation = null;

        private INamedTypeSymbol EventCodeAttribute
        {
            get
            {
                if (eventCodeAttribute is null)
                    eventCodeAttribute = TempCompilation?.GetTypeByMetadataName(typeof(EOS.Attributes.EventCodeAttribute).FullName);
                return eventCodeAttribute;
            }
        }

        private INamedTypeSymbol eventCodeAttribute = null;

        private INamedTypeSymbol InterfaceEventCode
        {
            get
            {
                if (interfaceEventCode is null)
                    interfaceEventCode = TempCompilation?.GetTypeByMetadataName(typeof(EOS.IEventCode).FullName);
                return interfaceEventCode;
            }
        }

        private INamedTypeSymbol interfaceEventCode = null;

        private INamedTypeSymbol EventListenerAttribute
        {
            get
            {
                if (eventListenerAttribute is null)
                    eventListenerAttribute = TempCompilation?.GetTypeByMetadataName(typeof(EOS.Attributes.EventListenerAttribute).FullName);
                return eventListenerAttribute;
            }
        }

        private INamedTypeSymbol eventListenerAttribute = null;

        private INamedTypeSymbol EventCodeMethodAttribute
        {
            get
            {
                if (eventCodeMethodAttribute is null)
                    eventCodeMethodAttribute = TempCompilation?.GetTypeByMetadataName(typeof(EOS.Attributes.EventCodeMethodAttribute).FullName);
                return eventCodeMethodAttribute;
            }
        }

        private INamedTypeSymbol eventCodeMethodAttribute = null;

        private INamedTypeSymbol SystemObject
        {
            get
            {
                if (systemObject is null)
                    systemObject = TempCompilation?.GetTypeByMetadataName(typeof(object).FullName);
                return systemObject;
            }
        }

        private INamedTypeSymbol systemObject = null;

        private INamedTypeSymbol SystemString
        {
            get
            {
                if (systemString is null)
                    systemString = TempCompilation?.GetTypeByMetadataName(typeof(string).FullName);
                return systemString;
            }
        }

        private INamedTypeSymbol systemString = null;

        private INamedTypeSymbol SystemType
        {
            get
            {
                if (systemType is null)
                    systemType = TempCompilation?.GetTypeByMetadataName(typeof(Type).FullName);
                return systemType;
            }
        }

        private INamedTypeSymbol systemType = null;
        //private INamedTypeSymbol SystemNull
        //{
        //    get
        //    {
        //        systemNull ??= TempCompilation?.GetTypeByMetadataName(typeof(null).FullName);
        //        return systemNull;
        //    }

        //}
        //private INamedTypeSymbol systemNull = null;

        private Func<INamedTypeSymbol, bool> EventCodeFilter => (symbol) =>
        {
            var eventCodeAttribute = EventCodeAttribute;
            var interfaceEventCode = InterfaceEventCode;
            return SymbolComparer.Equals(symbol, eventCodeAttribute)
                || symbol.GetAttributes().Any(x => SymbolComparer.Equals(x.AttributeClass, eventCodeAttribute))
                || symbol.Interfaces.Any(x => SymbolComparer.Equals(x, interfaceEventCode));
        };

        private Func<ISymbol, bool> EventCodeMethodFilter => (symbol) =>
        {
            var eventCodeMethodAttribute = EventCodeMethodAttribute;
            return SymbolComparer.Equals(symbol, eventCodeMethodAttribute)
            || symbol.GetAttributes().Any(x => SymbolComparer.Equals(x.AttributeClass, eventCodeMethodAttribute));
        };

        private Func<ISymbol, bool> EventListenerAttributeFilter => (symbol) =>
        {
            var attribute = EventListenerAttribute;
            return SymbolComparer.Equals(symbol, attribute)
                || symbol.GetAttributes().Any(x => SymbolComparer.Equals(x.AttributeClass, attribute));
        };

        public Queue<Diagnostic> diagnosticsQueue = new Queue<Diagnostic>();
        private static readonly SymbolEqualityComparer SymbolComparer = SymbolEqualityComparer.Default;

        public override void Initialize(AnalysisContext context)
        {
            if (context is null)
                return;
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
            context.EnableConcurrentExecution();
            //context.RegisterSyntaxNodeAction(EOSSyntaxCheck, SyntaxKind.CompilationUnit);
            //context.RegisterCompilationStartAction(EOSSyntaxCheckCompilation);
            context.RegisterSymbolAction(EOSCheckClassSymbol, SymbolKind.NamedType);
            context.RegisterSymbolAction(EOSCheckMethodSymbol, SymbolKind.Method);
            context.RegisterOperationAction(EOSCheckInvocationmethodSymbol, OperationKind.Invocation);
        }

        private void EOSCheckClassSymbol(SymbolAnalysisContext context)
        {
            var type = context.Symbol as INamedTypeSymbol;
            var syntax = type.DeclaringSyntaxReferences.SingleOrDefault().GetSyntax() as TypeDeclarationSyntax;
            if (type.TypeKind == TypeKind.Class || type.TypeKind == TypeKind.Interface)
            {
                //try
                {
                    TempCompilation = context.Compilation;
                    if (EventCodeFilter(type))
                    {
                        foreach (var dia in ReportSingleEventCodeClass(type, syntax))
                        {
                            //diagnosticsQueue.Enqueue(dia);
                            context.ReportDiagnostic(dia);
                        }
                    }
                    if (type.RecursionBaseTypeAndInterfacesMatch(EventListenerAttributeFilter))
                    {
                        foreach (var dia in ReportSingleEventListenerClass(type, syntax))
                        {
                            //diagnosticsQueue.Enqueue(dia);
                            context.ReportDiagnostic(dia);
                        }
                    }
                }
                //catch (Exception ex)
                {
                    // EOSAnalyzerLogger.Log(ex.ToString());
                }
            }
        }

        private void EOSCheckMethodSymbol(SymbolAnalysisContext context)
        {
            var method = context.Symbol as IMethodSymbol;
            TempCompilation = context.Compilation;
            if (EventCodeMethodFilter(method))
            {
                var type = method.ContainingType;
                if (type == null) return;
                if (EventCodeFilter(type)) return;
                //try
                {
                    foreach (var dia in ReportSingleEventCodeMethod(method))
                    {
                        //diagnosticsQueue.Enqueue(dia);
                        context.ReportDiagnostic(dia);
                    }
                }
                //catch (Exception ex)
                {
                    // EOSAnalyzerLogger.Log(ex.ToString());
                }

            }
            if (EventListenerAttributeFilter(method))
            {
                var type = method.ContainingType;
                if (type == null) return;
                if (type.RecursionBaseTypeAndInterfacesMatch(EventListenerAttributeFilter)) return;
                //try
                {
                    foreach (var dia in ReportSingleEventListenerMethod(method))
                    {
                        //diagnosticsQueue.Enqueue(dia);
                        context.ReportDiagnostic(dia);
                    }
                }
                //catch (Exception ex)
                {
                    // EOSAnalyzerLogger.Log(ex.ToString());
                }
            }
        }

        private void EOSCheckInvocationmethodSymbol(OperationAnalysisContext context)
        {
            var operation = context.Operation as IInvocationOperation;
            var callMethod = operation.TargetMethod;
            var compilation = context.Compilation;
            var eosAssembly = compilation.GetTypeByMetadataName(typeof(EOS.EOSManager).FullName).ContainingAssembly;
            if (callMethod is IMethodSymbol symbol && SymbolComparer.Equals(eosAssembly, symbol.ContainingAssembly))
            {
                //try
                {
                    TempCompilation = context.Compilation;
                    var semanticModel = operation.SemanticModel;
                    var syntax = operation.Syntax as InvocationExpressionSyntax;
                    if (symbol.Name.Contains(nameof(EOS.EOSManager.AddListener)))
                    {
                        foreach (var dia in ReportSingleAddOrRemoveMethod(semanticModel, symbol, syntax))
                        {
                            //diagnosticsQueue.Enqueue(dia);
                            context.ReportDiagnostic(dia);
                        }
                    }
                    else if (symbol.Name.Contains(nameof(EOS.EOSManager.RemoveListener)))
                    {
                        foreach (var dia in ReportSingleAddOrRemoveMethod(semanticModel, symbol, syntax))
                        {
                            //diagnosticsQueue.Enqueue(dia);
                            context.ReportDiagnostic(dia);
                        }
                    }
                    else if (symbol.Name.Contains(nameof(EOS.EOSManager.BroadCast)))
                    {
                        foreach (var dia in ReportSingleBoardCastMethod(semanticModel, symbol, syntax))
                        {
                            //diagnosticsQueue.Enqueue(dia);
                            context.ReportDiagnostic(dia);
                        }
                    }
                }
                //catch (Exception ex)
                {
                    // EOSAnalyzerLogger.Log(ex.ToString());
                }
            }
        }

#if false
        private void EOSSyntaxCheck(SyntaxNodeAnalysisContext context)
        {
            try
            {
                var compilation = context.Compilation;
                var tree = context.Node.SyntaxTree;
                EOSSyntaxTreeCheck(tree, compilation);
                while (diagnosticsQueue.Count > 0)
                {
                    context.ReportDiagnostic(diagnosticsQueue.Dequeue());
                }
            }
            catch (Exception e)
            {
                EOSAnalyzerLogger.Log(e.ToString());
            }
        }

        //Check One Tree
        public void EOSSyntaxTreeCheck(SyntaxTree syntaxTree, Compilation compilation)
        {
            if (syntaxTree == null || compilation == null) return;
            var root = syntaxTree.GetCompilationUnitRoot();
            var semanticModel = compilation.GetSemanticModel(syntaxTree);
            if (!CheckIsUsingEOS(semanticModel, compilation, root))
            {
                return;
            }
            TempCompilation = compilation;
            SearchCodeAndListener(semanticModel, compilation, root,
                out var eventCodeClasses, out var eventListenerClasses, out var eventCodeMethodsWithNoCode, out var eventListenerMethodsWithNoListener);
            ReportEventCodeDiagnostic(eventCodeClasses, eventCodeMethodsWithNoCode);
            ReportEventListenerDiagnostic(eventListenerClasses, eventListenerMethodsWithNoListener);
            ReportBroadCastDiagnostic(semanticModel, compilation, root);
            ReportAddOrRemoveListenerDiagnostic(semanticModel, compilation, root);
            TempCompilation = null;
        }

        //Is Using EOS Namespace
        private bool CheckIsUsingEOS(SemanticModel semanticModel, Compilation compilation, CompilationUnitSyntax root)
        {
            var eosAssembly = compilation.GetTypeByMetadataName(typeof(EOS.EOSManager).FullName)?.ContainingAssembly;
            if (eosAssembly is null)
            {
                return false;
            }
            var references = from node in root.Usings select node.Name;
            foreach (var nspace in references)
            {
                var symbol = semanticModel.GetSymbolInfo(nspace);
                if (SymbolComparer.Equals(eosAssembly, symbol.Symbol?.ContainingAssembly))
                {
                    return true;
                }
            }
            return false;
        }

        //Valid Classes
        private void SearchCodeAndListener(SemanticModel semanticModel, Compilation compilation, CompilationUnitSyntax root,
            out Dictionary<TypeDeclarationSyntax, INamedTypeSymbol> eventCodeClasses, out Dictionary<TypeDeclarationSyntax, INamedTypeSymbol> eventListenerClasses,
            out List<IMethodSymbol> eventCodeMethodsWithNoCode, out List<IMethodSymbol> eventListenerMethodsWithNoListener)
        {
            eventCodeClasses = new Dictionary<TypeDeclarationSyntax, INamedTypeSymbol>();
            eventListenerClasses = new Dictionary<TypeDeclarationSyntax, INamedTypeSymbol>();
            eventCodeMethodsWithNoCode = new List<IMethodSymbol>();
            eventListenerMethodsWithNoListener = new List<IMethodSymbol>();
            var allNodes = root.DescendantNodes();
            var classesSyntaxes = from node in allNodes
                                  where node.IsKind(SyntaxKind.ClassDeclaration) || node.IsKind(SyntaxKind.InterfaceDeclaration)
                                  select (TypeDeclarationSyntax)node;
            if (!classesSyntaxes.Any())
                return;

            foreach (var classSyntax in classesSyntaxes)
            {
                var symbol = semanticModel.GetDeclaredSymbol(classSyntax);
                var flag1 = false;
                var flag2 = false;
                if (EventCodeFilter(symbol))
                {
                    eventCodeClasses.Add(classSyntax, symbol);
                    flag1 = true;
                }
                if (symbol.RecursionBaseTypeAndInterfacesMatch(EventListenerAttributeFilter))
                {
                    eventListenerClasses.Add(classSyntax, symbol);
                    flag2 = true;
                }
                if (!flag1 || !flag2)
                {
                    var members = symbol.GetMembers();
                    var methods = from node in members where node.Kind == SymbolKind.Method select (IMethodSymbol)node;
                    foreach (var method in methods)
                    {
                        if (!flag1 && EventCodeMethodFilter(method))
                        {
                            eventCodeMethodsWithNoCode.Add(method);
                        }
                        if(!flag2 && EventListenerAttributeFilter(method))
                        {
                            eventListenerMethodsWithNoListener.Add(method);
                        }
                    }
                }
            }
        }

#endif

        //EOS Code Info
        private void ReportEventCodeDiagnostic(Dictionary<TypeDeclarationSyntax, INamedTypeSymbol> eventCodeClasses,
            List<IMethodSymbol> eventCodeMethodsWithNoCode)
        {
            foreach (var eventCodeClassSyntax in eventCodeClasses.Keys)
            {
                foreach (var dig in ReportSingleEventCodeClass(eventCodeClasses[eventCodeClassSyntax], eventCodeClassSyntax))
                {
                    diagnosticsQueue.Enqueue(dig);
                }
            }

            //Code Method
            foreach (var methodSymbol in eventCodeMethodsWithNoCode)
            {
                foreach (var dia in ReportSingleEventCodeMethod(methodSymbol))
                {
                    diagnosticsQueue.Enqueue(dia);
                }
            }
        }

        private IEnumerable<Diagnostic> ReportSingleEventCodeClass(INamedTypeSymbol symbol, TypeDeclarationSyntax eventCodeClassSyntax)
        {
            var result = new List<Diagnostic>();
            //Code Class
            if (symbol.IsGenericType)
            {
                //泛型类型不能作为事件码使用
                var syntax = eventCodeClassSyntax.Identifier;
                var diagnostic = Diagnostic.Create(EOSAnalyzerDataBase.Rule_EOS006C, syntax.GetLocation());
                result.Add(diagnostic);
                return result;
            }

            var members = symbol.GetMembers();
            var methods = from node in members where node.Kind == SymbolKind.Method select (IMethodSymbol)node;

            var attriMethodCount = 0;
            foreach (var method in methods)
            {
                if (!(method.MethodKind == MethodKind.PropertyGet
                    || method.MethodKind == MethodKind.PropertySet
                    || method.MethodKind == MethodKind.Ordinary))
                {
                    continue;
                }
                var attri = method.GetAttributes().FirstOrDefault(x => EventCodeMethodFilter(x.AttributeClass));
                if (attri?.AttributeClass != null)
                {
                    if (method.MethodKind == MethodKind.PropertyGet || method.MethodKind == MethodKind.PropertySet)
                    {
                        //不能将属性方法作为事件方法声明
                        var syntax = attri.ApplicationSyntaxReference.GetSyntax();
                        var diagnostic = Diagnostic.Create(EOSAnalyzerDataBase.Rule_EOS002C, syntax.GetLocation());
                        result.Add(diagnostic);
                    }
                    if (method.IsGenericMethod)
                    {
                        //不能将泛型方法作为事件方法声明
                        var syntax = attri.ApplicationSyntaxReference.GetSyntax();
                        var diagnostic = Diagnostic.Create(EOSAnalyzerDataBase.Rule_EOS005C, syntax.GetLocation());
                        result.Add(diagnostic);
                    }
                    attriMethodCount++;
                    if (attriMethodCount > 1)
                    {
                        //过多事件方法声明
                        var syntax = attri.ApplicationSyntaxReference.GetSyntax();
                        var diagnostic = Diagnostic.Create(EOSAnalyzerDataBase.Rule_EOS003C, syntax.GetLocation());
                        result.Add(diagnostic);
                    }
                }
            }
            if (attriMethodCount < 1)
            {
                //声明事件码类型中缺少事件方法声明
                var syntax = eventCodeClassSyntax.ChildTokens().FirstOrDefault(x => x.IsKind(SyntaxKind.IdentifierToken));
                var diagnostic = Diagnostic.Create(EOSAnalyzerDataBase.Rule_EOS001C, syntax.GetLocation());
                result.Add(diagnostic);
            }
            return result;
        }

        private IEnumerable<Diagnostic> ReportSingleEventCodeMethod(IMethodSymbol methodSymbol)
        {
            var result = new List<Diagnostic>();
            var attri = methodSymbol.GetAttributes().FirstOrDefault(x => EventCodeMethodFilter(x.AttributeClass));
            //已声明事件方法但未声明事件码
            var syntax = attri.ApplicationSyntaxReference.GetSyntax();
            var diagnostic = Diagnostic.Create(EOSAnalyzerDataBase.Rule_EOS004C, syntax.GetLocation());
            //diagnosticsQueue.Enqueue(diagnostic);
            result.Add(diagnostic);
            if (methodSymbol.MethodKind == MethodKind.PropertyGet || methodSymbol.MethodKind == MethodKind.PropertySet)
            {
                //不能将属性方法作为事件方法声明
                diagnostic = Diagnostic.Create(EOSAnalyzerDataBase.Rule_EOS002C, syntax.GetLocation());
                //diagnosticsQueue.Enqueue(diagnostic);
                result.Add(diagnostic);
            }
            if (methodSymbol.IsGenericMethod)
            {
                //不能将泛型方法作为事件方法声明
                diagnostic = Diagnostic.Create(EOSAnalyzerDataBase.Rule_EOS005C, syntax.GetLocation());
                //diagnosticsQueue.Enqueue(diagnostic);
                result.Add(diagnostic);
            }
            return result;
        }

        //EOS Listener Info
        private void ReportEventListenerDiagnostic(Dictionary<TypeDeclarationSyntax, INamedTypeSymbol> eventListenerClasses,
            List<IMethodSymbol> eventListenerMethodsWithNoListener)
        {
            foreach (var eventListenerClassSyntax in eventListenerClasses.Keys)
            {
                foreach (var dig in ReportSingleEventListenerClass(eventListenerClasses[eventListenerClassSyntax], eventListenerClassSyntax))
                {
                    diagnosticsQueue.Enqueue(dig);
                }
            }

            //Listener Methods
            foreach (var methodSymbol in eventListenerMethodsWithNoListener)
            {
                foreach (var dia in ReportSingleEventListenerMethod(methodSymbol))
                {
                    diagnosticsQueue.Enqueue(dia);
                }
            }
        }

        private IEnumerable<Diagnostic> ReportSingleEventListenerClass(INamedTypeSymbol symbol, TypeDeclarationSyntax eventListenerClassSyntax)
        {
            var result = new List<Diagnostic>();
            if (symbol.IsGenericType)
            {
                //泛型类型不能作为事件接收者使用
                var syntax = eventListenerClassSyntax.Identifier;
                var diagnostic = Diagnostic.Create(EOSAnalyzerDataBase.Rule_EOS009L, syntax.GetLocation());
                result.Add(diagnostic);
                return result;
            }
            var methods = symbol.GetMembers().Where(x => x.Kind == SymbolKind.Method).OfType<IMethodSymbol>();
            var lisCount = 0;
            var HashCode = new Dictionary<INamedTypeSymbol, IMethodSymbol>(SymbolComparer);
            foreach (var method in methods)
            {
                if (!(method.MethodKind == MethodKind.PropertyGet
                    || method.MethodKind == MethodKind.PropertySet
                    || method.MethodKind == MethodKind.Ordinary
                    || method.MethodKind == MethodKind.ExplicitInterfaceImplementation))
                {
                    continue;
                }
                var attri = method.GetAttributes().SingleOrDefault(x => EventListenerAttributeFilter(x.AttributeClass));
                if (attri != null)
                {
                    lisCount++;
                    if (method.MethodKind == MethodKind.PropertyGet || method.MethodKind == MethodKind.PropertySet)
                    {
                        //不能将属性方法作为事件方法声明
                        var syntax = attri.ApplicationSyntaxReference.GetSyntax();
                        var diagnostic = Diagnostic.Create(EOSAnalyzerDataBase.Rule_EOS002C, syntax.GetLocation());
                        result.Add(diagnostic);
                        continue;
                    }
                    if (method.IsGenericMethod)
                    {
                        //不能将泛型方法作为事件方法声明
                        var syntax = attri.ApplicationSyntaxReference.GetSyntax();
                        var diagnostic = Diagnostic.Create(EOSAnalyzerDataBase.Rule_EOS005C, syntax.GetLocation());
                        result.Add(diagnostic);
                        continue;
                    }
                    var attriParams = attri.ConstructorArguments.Where(x => x.Kind == TypedConstantKind.Type).Select(x => x.Value as INamedTypeSymbol).ToList();

                    if (attriParams.Count < 1)
                    {
                        //声明的事件方法接收者缺少指向的事件码
                        var syntax = attri.ApplicationSyntaxReference.GetSyntax();
                        var diagnostic = Diagnostic.Create(EOSAnalyzerDataBase.Rule_EOS001L, syntax.GetLocation());
                        result.Add(diagnostic);
                        continue;
                    }
                    if (attriParams.Count > 1) continue;
                    if (attriParams[0] != null)
                    {
                        var codeClass = attriParams[0];
                        var @as = codeClass.GetAttributes();
                        var attribute = codeClass.GetAttributes().FirstOrDefault(x => EventCodeFilter(x.AttributeClass));
                        if (attribute?.AttributeClass == null)
                        {
                            //事件方法指向的事件码类型参数未定义
                            var syntax = attri.ApplicationSyntaxReference.GetSyntax();
                            var diagnostic = Diagnostic.Create(EOSAnalyzerDataBase.Rule_EOS002L, syntax.GetLocation(), codeClass);
                            result.Add(diagnostic);
                            continue;
                        }
                        if (HashCode.Keys.Contains(codeClass, SymbolComparer))
                        {
                            //类型中已有指向了相同事件码的事件方法
                            var syntax = (method.DeclaringSyntaxReferences.SingleOrDefault().GetSyntax() as MethodDeclarationSyntax)?.Identifier;
                            var diagnostic = Diagnostic.Create(EOSAnalyzerDataBase.Rule_EOS010L, syntax?.GetLocation(), codeClass, HashCode[codeClass]);
                            result.Add(diagnostic);
                            continue;
                        }
                        HashCode.Add(codeClass, method);
                        var codeClassMembers = codeClass.GetMembers();
                        var codeClassMethods = from node in codeClassMembers where node.Kind == SymbolKind.Method select (IMethodSymbol)node;
                        IMethodSymbol lastCode = null;
                        //Code Class
                        foreach (var codeMethod in codeClassMethods)
                        {
                            var attribute2 = codeMethod.GetAttributes().FirstOrDefault(x => EventCodeMethodFilter(x.AttributeClass));
                            if (attribute2?.AttributeClass != null)
                            {
                                lastCode = codeMethod;
                                break;
                            }
                        }
                        if (lastCode == null)
                        {
                            //指向的事件码类型未声明事件方法定义
                            var syntax = attri.ApplicationSyntaxReference.GetSyntax();
                            var diagnostic = Diagnostic.Create(EOSAnalyzerDataBase.Rule_EOS003L, syntax.GetLocation(), codeClass);
                            result.Add(diagnostic);
                            continue;
                        }
                        var declaredParams = lastCode.Parameters;
                        var methodParams = method.Parameters;
                        if (declaredParams.Length != methodParams.Length)
                        {
                            //事件方法的参数数量与指向的事件方法定义的参数数量不符
                            var syntax = method.DeclaringSyntaxReferences.FirstOrDefault().GetSyntax() as MethodDeclarationSyntax;
                            var identify = syntax.Identifier;
                            var diagnostic = Diagnostic.Create(EOSAnalyzerDataBase.Rule_EOS004L, identify.GetLocation());
                            result.Add(diagnostic);
                            continue;
                        }
                        for (var i = 0; i < declaredParams.Length; i++)
                        {
                            if (declaredParams[i].Type.RecursionBaseTypeAndInterfacesMatch(
                                x => SymbolComparer.Equals(x, methodParams[i].Type)))
                            {
                                continue;
                            }
                            else if (methodParams[i].Type.RecursionBaseTypeAndInterfacesMatch(
                                x => SymbolComparer.Equals(x, declaredParams[i].Type)))
                            {
                                //事件方法的参数类型必须为事件方法定义的参数类型的基类或接口，而非相反
                                var syntax = methodParams[i].DeclaringSyntaxReferences.FirstOrDefault().GetSyntax() as ParameterSyntax;
                                var identify = syntax.Type;
                                var diagnostic = Diagnostic.Create(EOSAnalyzerDataBase.Rule_EOS008L, syntax.GetLocation(), methodParams[i].Type, declaredParams[i].Type);
                                result.Add(diagnostic);
                                continue;
                            }
                            else
                            {
                                //事件方法的参数类型与指向的事件方法定义的参数类型不符
                                var syntax = methodParams[i].DeclaringSyntaxReferences.FirstOrDefault().GetSyntax() as ParameterSyntax;
                                var identify = syntax.Type;
                                var diagnostic = Diagnostic.Create(EOSAnalyzerDataBase.Rule_EOS005L, syntax.GetLocation(), methodParams[i].Type, declaredParams[i].Type);
                                result.Add(diagnostic);
                            }
                        }
                    }
                }
            }
            if (lisCount < 1)
            {
                //声明了事件接收者的类型内没有任何事件方法
                var syntax = symbol.DeclaringSyntaxReferences.FirstOrDefault().GetSyntax() as TypeDeclarationSyntax;
                var identify = syntax.Identifier;
                var diagnostic = Diagnostic.Create(EOSAnalyzerDataBase.Rule_EOS006L, identify.GetLocation(), symbol);
                result.Add(diagnostic);
            }
            return result;
        }

        private IEnumerable<Diagnostic> ReportSingleEventListenerMethod(IMethodSymbol methodSymbol)
        {
            var result = new List<Diagnostic>();
            var attri = methodSymbol.GetAttributes().SingleOrDefault(x => EventListenerAttributeFilter(x.AttributeClass));
            //声明了事件方法但所在类型未声明为事件接收者
            var syntax = attri.ApplicationSyntaxReference.GetSyntax();
            var diagnostic = Diagnostic.Create(EOSAnalyzerDataBase.Rule_EOS007L, syntax.GetLocation());
            //diagnosticsQueue.Enqueue(diagnostic);
            result.Add(diagnostic);
            if (methodSymbol.MethodKind == MethodKind.PropertyGet || methodSymbol.MethodKind == MethodKind.PropertySet)
            {
                //不能将属性方法作为事件方法声明
                diagnostic = Diagnostic.Create(EOSAnalyzerDataBase.Rule_EOS002C, syntax.GetLocation());
                //diagnosticsQueue.Enqueue(diagnostic);
                result.Add(diagnostic);
            }
            if (methodSymbol.IsGenericMethod)
            {
                //不能将泛型方法作为事件方法声明
                diagnostic = Diagnostic.Create(EOSAnalyzerDataBase.Rule_EOS005C, syntax.GetLocation());
                //diagnosticsQueue.Enqueue(diagnostic);
                result.Add(diagnostic);
            }
            return result;
        }

        //EOS BroadCast
        private void ReportBroadCastDiagnostic(SemanticModel semanticModel, Compilation compilation, CompilationUnitSyntax root)
        {
            #region define symbol

            var boardCastMethodDefinitionSymbols = compilation.GetTypeByMetadataName(typeof(EOS.EOSManager).FullName)
                .GetMembers()
                .Where(x => x.Kind == SymbolKind.Method
                    && (x as IMethodSymbol).MethodKind == MethodKind.Ordinary
                    && (x as IMethodSymbol).Name.Contains(nameof(EOS.EOSManager.BroadCast)))
                .ToList();
            boardCastMethodDefinitionSymbols.AddRange(compilation.GetTypeByMetadataName(typeof(EOS.EOSControler).FullName)
                .GetMembers()
                 .Where(x => x.Kind == SymbolKind.Method
                    && (x as IMethodSymbol).MethodKind == MethodKind.Ordinary
                    && (x as IMethodSymbol).Name.Contains(nameof(EOS.EOSControler.BroadCast))));

            #endregion define symbol

            var callMethods = from node in root.DescendantNodes() where node.IsKind(SyntaxKind.InvocationExpression) select (InvocationExpressionSyntax)node;
            var callMethodsDic = new Dictionary<InvocationExpressionSyntax, IMethodSymbol>();
            foreach (var callMethod in callMethods)
            {
                if (semanticModel.GetSymbolInfo(callMethod).Symbol is IMethodSymbol symbol)
                {
                    if (boardCastMethodDefinitionSymbols.Any(x => SymbolComparer.Equals(symbol.ConstructedFrom, x)))
                    {
                        callMethodsDic.Add(callMethod, symbol);
                    }
                }
            }
            foreach (var syntaxKey in callMethodsDic.Keys)
            {
                var symbol = callMethodsDic[syntaxKey];
                foreach (var dia in ReportSingleBoardCastMethod(semanticModel, symbol, syntaxKey))
                {
                    diagnosticsQueue.Enqueue(dia);
                }
            }
        }

        private IEnumerable<Diagnostic> ReportSingleBoardCastMethod(SemanticModel semanticModel, IMethodSymbol symbol, InvocationExpressionSyntax syntaxKey)
        {
            var result = new List<Diagnostic>();
            ITypeSymbol typeArg = null;
            var symbolArgsSyntax = syntaxKey.ArgumentList.Arguments.ToList();
            if (symbol.IsGenericMethod)
            {
                if (symbol.TypeArguments.Length != 1) return result;
                typeArg = symbol.TypeArguments[0];
                var attri = typeArg?.GetAttributes().FirstOrDefault(x => EventCodeFilter(x.AttributeClass));
                if (attri == null)
                {
                    //尝试广播的事件码未定义
                    var syntax = syntaxKey.DescendantNodes().OfType<GenericNameSyntax>().SingleOrDefault().TypeArgumentList.Arguments.FirstOrDefault();
                    var diagnostic = Diagnostic.Create(EOSAnalyzerDataBase.Rule_EOS001B, syntax.GetLocation(), typeArg);
                    //diagnosticsQueue.Enqueue(diagnostic);
                    result.Add(diagnostic);
                    return result;
                }
            }
            else
            {
                if (symbolArgsSyntax.Count < 1)
                {
                    //未输入应当广播的事件的事件码
                    var syntax = syntaxKey;
                    var diagnostic = Diagnostic.Create(EOSAnalyzerDataBase.Rule_EOS002B, syntax.GetLocation());
                    //diagnosticsQueue.Enqueue(diagnostic);
                    result.Add(diagnostic);
                    return result;
                }
                var expression = symbolArgsSyntax[0].Expression;
                var typeOrKey = semanticModel.GetTypeInfo(expression).Type;
                symbolArgsSyntax.RemoveAt(0);
                if (SymbolComparer.Equals(typeOrKey, SystemString))
                {
                    //不推荐直接使用字符串作为事件码
                    var syntax = expression.Parent;
                    var diagnostic = Diagnostic.Create(EOSAnalyzerDataBase.Rule_EOS003B, syntax.GetLocation());
                    //diagnosticsQueue.Enqueue(diagnostic);
                    result.Add(diagnostic);
                    return result;
                }
                if (!SymbolComparer.Equals(typeOrKey, SystemType))
                {
                    //未输入应当广播的事件的事件码
                    var syntax = expression.Parent;
                    var diagnostic = Diagnostic.Create(EOSAnalyzerDataBase.Rule_EOS002B, syntax.GetLocation());
                    result.Add(diagnostic);
                    return result;
                }
                if (!(expression is TypeOfExpressionSyntax))
                {
                    //建议使用常量值类型作为事件码
                    var syntax = expression.Parent;
                    var diagnostic = Diagnostic.Create(EOSAnalyzerDataBase.Rule_EOS004B, syntax.GetLocation());
                    //diagnosticsQueue.Enqueue(diagnostic);
                    result.Add(diagnostic);
                    return result;
                }
                var typeofExpression = expression as TypeOfExpressionSyntax;
                typeArg = semanticModel.GetTypeInfo(typeofExpression.Type).Type;
                var attri = typeArg?.GetAttributes().FirstOrDefault(x => EventCodeFilter(x.AttributeClass));
                if (attri == null)
                {
                    //尝试广播的事件码未定义
                    var syntax = expression.Parent;
                    var diagnostic = Diagnostic.Create(EOSAnalyzerDataBase.Rule_EOS001B, syntax.GetLocation(), typeArg);
                    result.Add(diagnostic);
                    return result;
                }
            }
            var codeClassMembers = typeArg.GetMembers();
            var codeClassMethods = from node in codeClassMembers where node.Kind == SymbolKind.Method select (IMethodSymbol)node;
            IMethodSymbol lastCode = null;
            //Code Class
            foreach (var codeMethod in codeClassMethods)
            {
                var attribute2 = codeMethod.GetAttributes().FirstOrDefault(x => EventCodeMethodFilter(x.AttributeClass));
                if (attribute2?.AttributeClass != null)
                {
                    lastCode = codeMethod;
                    break;
                }
            }
            if (lastCode == null)
            {
                //声明事件码类型中缺少事件方法声明
                var syntax = typeArg.DeclaringSyntaxReferences.SingleOrDefault().GetSyntax();
                var diagnostic = Diagnostic.Create(EOSAnalyzerDataBase.Rule_EOS001C, syntax.GetLocation());
                result.Add(diagnostic);
                return result;
            }
            if (symbolArgsSyntax.Count == 1 && semanticModel.GetTypeInfo(symbolArgsSyntax[0].Expression).Type is IArrayTypeSymbol arraySymbol
                && arraySymbol.TypeKind == TypeKind.Array && SymbolComparer.Equals(arraySymbol.ElementType, SystemObject))
            {
                if (symbolArgsSyntax[0].Expression is LiteralExpressionSyntax les && les.IsKind(SyntaxKind.DefaultLiteralExpression))
                {
                    //事件广播时填入的参数类型与定义的类型不符
                    var syntax = symbolArgsSyntax[0];
                    var diagnostic = Diagnostic.Create(EOSAnalyzerDataBase.Rule_EOS006B, syntax.GetLocation(), "default", "object[]");
                    result.Add(diagnostic);
                }
                else
                {
                    //事件广播参数输入参数为数组
                    var syntax = symbolArgsSyntax[0];
                    var diagnostic = Diagnostic.Create(EOSAnalyzerDataBase.Rule_EOS009B, syntax.GetLocation(), typeArg);
                    result.Add(diagnostic);
                }
                return result;
            }
            var originalMethodParams = lastCode.Parameters.ToList();
            if (symbolArgsSyntax.Count > originalMethodParams.Count)
            {
                //尝试广播的事件填入的参数过多
                var syntax = syntaxKey;
                var diagnostic = Diagnostic.Create(EOSAnalyzerDataBase.Rule_EOS005B, syntax.GetLocation(), typeArg);
                result.Add(diagnostic);
                return result;
            }
            for (int i = 0; i < originalMethodParams.Count; i++)
            {
                var parameterDef = originalMethodParams[i];
                if (symbolArgsSyntax.Count > i)
                {
                    var parameterRe = symbolArgsSyntax[i];
                    var originalType = parameterDef.Type;
                    if (parameterRe.Expression is LiteralExpressionSyntax les && les.IsKind(SyntaxKind.DefaultLiteralExpression))
                    {
                        if (originalType.IsReferenceType)
                        {
                            //事件广播中可能的null引用参数
                            var syntax0 = parameterRe;
                            var diagnostic0 = Diagnostic.Create(EOSAnalyzerDataBase.Rule_EOS010B, syntax0.GetLocation(), typeArg);
                            result.Add(diagnostic0);
                        }

                        continue;
                    }
                    var paramType = semanticModel.GetTypeInfo(parameterRe.Expression).Type;
                    if (paramType == null && originalType.IsReferenceType)
                    {
                        //事件广播中存在可能的null引用参数
                        var syntax0 = parameterRe;
                        var diagnostic0 = Diagnostic.Create(EOSAnalyzerDataBase.Rule_EOS010B, syntax0.GetLocation(), typeArg);
                        result.Add(diagnostic0);
                        continue;
                    }
                    if (paramType.RecursionBaseTypeAndInterfacesMatch(x => SymbolComparer.Equals(x, originalType)))
                    {
                        //正常通过
                        continue;
                    }
                    if (originalType.RecursionBaseTypeAndInterfacesMatch(x => SymbolComparer.Equals(x, paramType)))
                    {
                        //事件广播中填入的参数必须为事件方法定义中的参数类型的子类或继承，而非相反
                        var syntax0 = parameterRe;
                        var diagnostic0 = Diagnostic.Create(EOSAnalyzerDataBase.Rule_EOS011B, syntax0.GetLocation(), paramType, originalType);
                        result.Add(diagnostic0);
                        continue;
                    }
                    //事件广播时填入的参数类型与定义的类型不符
                    var syntax = parameterRe;
                    var diagnostic = Diagnostic.Create(EOSAnalyzerDataBase.Rule_EOS006B, syntax.GetLocation(), paramType, originalType);
                    result.Add(diagnostic);
                }
                else
                {
                    if (parameterDef.HasExplicitDefaultValue)
                    {
                        //事件广播中有可选填入的参数存在
                        var syntax = syntaxKey;
                        var diagnostic = Diagnostic.Create(EOSAnalyzerDataBase.Rule_EOS008B, syntax.GetLocation(), typeArg);
                        result.Add(diagnostic);
                        continue;
                    }
                    else
                    {
                        //尝试广播的事件填入的参数不足
                        var syntax = syntaxKey;
                        var diagnostic = Diagnostic.Create(EOSAnalyzerDataBase.Rule_EOS007B, syntax.GetLocation(), typeArg);
                        result.Add(diagnostic);
                        break;
                    }
                }
            }
            return result;
        }

        //EOS Add Or Remove Listener
        private void ReportAddOrRemoveListenerDiagnostic(SemanticModel semanticModel, Compilation compilation, CompilationUnitSyntax root)
        {
            #region define symbol

            var AddListenerDefinitionSymbols = compilation.GetTypeByMetadataName(typeof(EOS.EOSManager).FullName)
                .GetMembers()
                .Where(x => x.Kind == SymbolKind.Method
                    && (x as IMethodSymbol).MethodKind == MethodKind.Ordinary
                    && (x as IMethodSymbol).Name.Contains(nameof(EOS.EOSManager.AddListener)))
                .ToList();
            AddListenerDefinitionSymbols.AddRange(compilation.GetTypeByMetadataName(typeof(EOS.EOSControler).FullName)
                .GetMembers()
                .Where(x => x.Kind == SymbolKind.Method
                    && (x as IMethodSymbol).MethodKind == MethodKind.Ordinary
                    && (x as IMethodSymbol).Name.Contains(nameof(EOS.EOSControler.AddListener))));
            var RemoveListenerDefinitionSymbols = compilation.GetTypeByMetadataName(typeof(EOS.EOSManager).FullName)
                .GetMembers()
                .Where(x => x.Kind == SymbolKind.Method
                    && (x as IMethodSymbol).MethodKind == MethodKind.Ordinary
                    && (x as IMethodSymbol).Name.Contains(nameof(EOS.EOSManager.RemoveListener)))
                .ToList();
            RemoveListenerDefinitionSymbols.AddRange(compilation.GetTypeByMetadataName(typeof(EOS.EOSControler).FullName)
               .GetMembers()
               .Where(x => x.Kind == SymbolKind.Method
                   && (x as IMethodSymbol).MethodKind == MethodKind.Ordinary
                   && (x as IMethodSymbol).Name.Contains(nameof(EOS.EOSControler.RemoveListener))));

            #endregion define symbol

            var callMethods = from node in root.DescendantNodes() where node.IsKind(SyntaxKind.InvocationExpression) select (InvocationExpressionSyntax)node;
            var callAddMethodsDic = new Dictionary<InvocationExpressionSyntax, IMethodSymbol>();
            var callRemoveMethodsDic = new Dictionary<InvocationExpressionSyntax, IMethodSymbol>();
            foreach (var callMethod in callMethods)
            {
                if (semanticModel.GetSymbolInfo(callMethod).Symbol is IMethodSymbol symbol)
                {
                    if (AddListenerDefinitionSymbols.Any(x => SymbolComparer.Equals(symbol.ConstructedFrom, x)))
                    {
                        callAddMethodsDic.Add(callMethod, symbol);
                        continue;
                    }
                    if (RemoveListenerDefinitionSymbols.Any(x => SymbolComparer.Equals(symbol.ConstructedFrom, x)))
                    {
                        callRemoveMethodsDic.Add(callMethod, symbol);
                        continue;
                    }
                }
            }
            //Add
            foreach (var syntaxKey in callAddMethodsDic.Keys)
            {
                foreach (var dig in ReportSingleAddOrRemoveMethod(semanticModel, callAddMethodsDic[syntaxKey], syntaxKey))
                {
                    diagnosticsQueue.Enqueue(dig);
                }
            }

            //Remove
            foreach (var syntaxKey in callRemoveMethodsDic.Keys)
            {
                foreach (var dig in ReportSingleAddOrRemoveMethod(semanticModel, callRemoveMethodsDic[syntaxKey], syntaxKey))
                {
                    diagnosticsQueue.Enqueue(dig);
                }
            }
        }

        private IEnumerable<Diagnostic> ReportSingleAddOrRemoveMethod(SemanticModel semanticModel, IMethodSymbol symbol, InvocationExpressionSyntax syntaxKey)
        {
            var result = new List<Diagnostic>();
            var symbolArgsSyntax = syntaxKey.ArgumentList.Arguments.ToList();
            if (symbolArgsSyntax.Count < 1) return result;
            if (symbol.IsGenericMethod)
            {
                if (symbol.TypeArguments.Length != 1) return result;
                var typeArg = symbol.TypeArguments[0];
                var attri = typeArg.GetAttributes().FirstOrDefault(x => EventCodeFilter(x.AttributeClass));
                if (attri == null)
                {
                    //尝试添加或移除事件接收者的事件码未定义
                    var syntax = typeArg.DeclaringSyntaxReferences.SingleOrDefault().GetSyntax();
                    var diagnostic = Diagnostic.Create(EOSAnalyzerDataBase.Rule_EOS001AOR, syntax.GetLocation(), typeArg);
                    result.Add(diagnostic);
                    return result;
                }
                var firstParamSyntax = symbolArgsSyntax[0];
                var paramType = semanticModel.GetTypeInfo(firstParamSyntax.Expression).Type;
                if (paramType == null)
                {
                    //添加或移除事件接收者时，试图添加或移除项目上下文不支持的类型或null引用
                    var syntax = firstParamSyntax;
                    var diagnostic = Diagnostic.Create(EOSAnalyzerDataBase.Rule_EOS003AOR, syntax.GetLocation());
                    result.Add(diagnostic);
                    return result;
                }
                if (symbolArgsSyntax.Count > 1)
                {
                    //确认添加或移除事件时输入的方法名称和参数定义可以被反射查找
                    var syntax = syntaxKey;
                    var diagnostic = Diagnostic.Create(EOSAnalyzerDataBase.Rule_EOS002AOR, syntax.GetLocation(), symbolArgsSyntax[1].Expression.GetText(Encoding.UTF8));
                    result.Add(diagnostic);
                }
                var attriListener = paramType.RecursionAttributeSearch(x => EventListenerAttributeFilter(x.AttributeClass));
                if (attriListener.Count() < 1)
                {
                    //添加或移除事件接收者时，输入参数对应的类型不是已定义的事件接收者类型
                    var syntax = firstParamSyntax;
                    var diagnostic = Diagnostic.Create(EOSAnalyzerDataBase.Rule_EOS004AOR, syntax.GetLocation(), firstParamSyntax, paramType);
                    result.Add(diagnostic);
                    return result;
                }
                if (symbolArgsSyntax.Count > 1) return result;
                var methodTarget = paramType.RecursionSearchMember(SymbolKind.Method)
                    .OfType<IMethodSymbol>()
                    .Where(m => m.GetAttributes().FirstOrDefault(x => EventListenerAttributeFilter(x.AttributeClass)) != null)
                    .Where(x => x != null).ToList();
                if (methodTarget.Count != 1)
                {
                    //添加或移除事件接收者时，输入参数对应的类型中，定义的对应于此事件码的事件方法数量错误
                    var syntax = firstParamSyntax;
                    var diagnostic = Diagnostic.Create(EOSAnalyzerDataBase.Rule_EOS005AOR, syntax.GetLocation(), firstParamSyntax, paramType, typeArg);
                    result.Add(diagnostic);
                    return result;
                }
            }
            else
            {
                var firstParamSyntax = symbolArgsSyntax[0];
                var paramType = semanticModel.GetTypeInfo(firstParamSyntax.Expression).Type;
                if (SymbolComparer.Equals(paramType, SystemType))
                {
                    if (symbolArgsSyntax.Count < 2)
                    {
                        //缺少事件接收者对象
                        var syntax = syntaxKey.GetLastToken();
                        var diagnostic = Diagnostic.Create(EOSAnalyzerDataBase.Rule_EOS006AOR, syntax.GetLocation());
                        result.Add(diagnostic);
                        return result;
                    }
                    if (!(firstParamSyntax.Expression is TypeOfExpressionSyntax))
                    {
                        //建议使用常量值类型作为输入的事件接收者类型
                        var syntax = firstParamSyntax;
                        var diagnostic = Diagnostic.Create(EOSAnalyzerDataBase.Rule_EOS007AOR, syntax.GetLocation());
                        result.Add(diagnostic);
                        return result;
                    }
                    var typeofExpression = firstParamSyntax.Expression as TypeOfExpressionSyntax;
                    var eventiListenerArg = semanticModel.GetTypeInfo(typeofExpression.Type).Type;
                    paramType = semanticModel.GetTypeInfo(symbolArgsSyntax[1].Expression).Type;
                    if (paramType == null)
                    {
                        if (eventiListenerArg.IsStatic) return result;
                        //非静态类型事件接收者实例参数不能为null
                        var syntax = symbolArgsSyntax[1];
                        var diagnostic = Diagnostic.Create(EOSAnalyzerDataBase.Rule_EOS008AOR, syntax.GetLocation());
                        result.Add(diagnostic);
                        return result;
                    }
                    if (!SymbolComparer.Equals(paramType, eventiListenerArg))
                    {
                        //输入的事件接收者类型和输入的事件接收者实例类型不一致
                        var syntax = symbolArgsSyntax[1].Parent;
                        var diagnostic = Diagnostic.Create(EOSAnalyzerDataBase.Rule_EOS009AOR, syntax.GetLocation(), paramType, eventiListenerArg);
                        result.Add(diagnostic);
                        return result;
                    }
                }
                if (paramType == null)
                {
                    //添加或移除事件接收者时，试图添加或移除项目上下文不支持的类型或null引用
                    var syntax = firstParamSyntax;
                    var diagnostic = Diagnostic.Create(EOSAnalyzerDataBase.Rule_EOS003AOR, syntax.GetLocation());
                    result.Add(diagnostic);
                    return result;
                }
                var attriListener = paramType.RecursionAttributeSearch(x => EventListenerAttributeFilter(x.AttributeClass));
                if (attriListener.Count() < 1)
                {
                    //添加或移除事件接收者时，输入参数对应的类型不是已定义的事件接收者类型
                    var syntax = firstParamSyntax;
                    var diagnostic = Diagnostic.Create(EOSAnalyzerDataBase.Rule_EOS004AOR, syntax.GetLocation(), firstParamSyntax, paramType);
                    result.Add(diagnostic);
                    return result;
                }
                var methodTarget = paramType.RecursionSearchMember(SymbolKind.Method)
                    .OfType<IMethodSymbol>()
                    .Where(m => m.GetAttributes().FirstOrDefault(x => EventListenerAttributeFilter(x.AttributeClass)) != null)
                    .Where(x => x != null).ToList();
                if (methodTarget.Count < 1)
                {
                    //声明了事件接收者的类型内没有任何事件方法
                    var syntax = firstParamSyntax;
                    var diagnostic = Diagnostic.Create(EOSAnalyzerDataBase.Rule_EOS006L, syntax.GetLocation(), paramType);
                    result.Add(diagnostic);
                    return result;
                }
            }
            return result;
        }

    }
}