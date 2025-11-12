using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EOSAnalyzer
{
    internal static class CodeAnalyzerExtensions
    {
        public static IEnumerable<SyntaxNode> GetSyntaxNodesByString(this string CSharpFile)
        {
            var tree = CSharpSyntaxTree.ParseText(CSharpFile, encoding: Encoding.UTF8);
            return tree.GetRoot().ChildNodes();
        }

        public static IEnumerable<AttributeData> RecursionAttributeSearch(this ISymbol symbol, Func<AttributeData, bool> match)
        {
            var result = new HashSet<AttributeData>();
            var thisAttribute = symbol.GetAttributes().ToList();
            var flag = false;
            var flag1 = false;
            switch (symbol.Kind)
            {
                case SymbolKind.Field:
                    flag = true;
                    break;

                case SymbolKind.Property:
                    flag = true;
                    break;

                case SymbolKind.Method:
                    flag = true;
                    break;

                case SymbolKind.NamedType:
                    flag = true;
                    flag1 = true;
                    break;

                default:
                    break;
            }
            if (flag)
            {
                var baseType = flag1 ? (symbol as ITypeSymbol).BaseType : symbol.ContainingType.BaseType;
                while (baseType != null)
                {
                    thisAttribute.AddRange(baseType.GetMembers(symbol.Name).SelectMany(x => x.GetAttributes()));
                    baseType = baseType.BaseType;
                }
                var allInterfaces = flag1 ? (symbol as ITypeSymbol).AllInterfaces : symbol.ContainingType.AllInterfaces;
                thisAttribute.AddRange(allInterfaces.SelectMany(x => x.GetMembers(symbol.Name).SelectMany(y => y.GetAttributes())));
            }
            foreach (var attribute in thisAttribute)
            {
                if (match(attribute)) result.Add(attribute);
            }
            return result;
        }

        public static bool RecursionBaseTypeAndInterfacesMatch(this ITypeSymbol source, Func<ITypeSymbol, bool> match)
        {
            if (source == null) return false;
            if (match(source)) return true;
            var baseType = source.BaseType;
            while (baseType != null)
            {
                if (match(baseType)) return true;
                baseType = baseType.BaseType;
            }
            if (source.AllInterfaces.ToList().Any(match)) return true;
            return false;
        }

        public static bool RecursionBaseTypeAndInterfacesMatch(this IMethodSymbol source, Func<IMethodSymbol, bool> match)
        {
            if (source == null) return false;
            if (match(source)) return true;
            var baseType = source.ContainingType.BaseType;
            while (baseType != null)
            {
                var method = baseType.GetMembers(source.Name).FirstOrDefault(x => x is IMethodSymbol && (x.IsAbstract || x.IsVirtual)) as IMethodSymbol;
                if (method == null) break;
                if (match(method)) return true;
                baseType = baseType.BaseType;
            }
            if (source.ContainingType.AllInterfaces.Any(x => x.GetMembers(source.Name).OfType<IMethodSymbol>().Any(match))) return true;
            return false;
        }

        public static IEnumerable<ISymbol> RecursionSearchMember(this ITypeSymbol source, SymbolKind kind, Func<ISymbol, bool> match = null)
        {
            var thisMembers = source.GetMembers()/*.Where(x => x.IsDefinition).ToList()*/;
            var baseMemebrs = new List<ISymbol>();
            var baseType = source.BaseType;
            while (baseType != null)
            {
                baseMemebrs.AddRange(baseType.GetMembers()/*.Where(x => x.IsDefinition)*/);
                baseType = baseType.BaseType;
            }
            var interfacesMemebers = source.AllInterfaces.SelectMany(x => x.GetMembers()/*.Where(x => x.IsDefinition)*/);
            var result = new HashSet<ISymbol>(SymbolEqualityComparer.Default);
            thisMembers.AddRange(baseMemebrs);
            thisMembers.AddRange(interfacesMemebers);
            if (match != null)
            {
                foreach (var member in thisMembers)
                {
                    if (member.Kind == kind && match(member))
                    {
                        result.Add(member);
                    }
                }
            }
            else
            {
                foreach (var member in thisMembers)
                {
                    if (member.Kind == kind)
                    {
                        result.Add(member);
                    }
                }
            }
            return result;
        }
    }
}