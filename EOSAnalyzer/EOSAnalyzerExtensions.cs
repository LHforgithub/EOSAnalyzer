using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace EOSAnalyzer
{
    internal static class EOSAnalyzerExtensions
    {

        #region Regex

        public const string EOSName = "EOS.dll";
        public static string EOSAssemblyFullPath = Path.Combine(Path.GetDirectoryName(typeof(EOSAnalyzerDataBase).Assembly.Location), EOSName);

        private const string assemblyCulture = "culture";
        private const string assemblyName = "assemblyName";
        private const string assemblyPublicKeyToken = "publicKeyToken";
        private const string assemblyVersion = "version";

        private const string extractAssemblyInfo = @"^(?<"
            + assemblyName + @">[^,]+),\s*Version=(?<"
            + assemblyVersion + @">\d+\.\d+\.\d+\.\d+),\s*Culture=(?<"
            + assemblyCulture + @">[^,]+),\s*PublicKeyToken=(?<"
            + assemblyPublicKeyToken + @">[a-fA-F0-9]+)$";

        private const string validAssemblyInfo = @"^\s*([^,]+)\s*,\s*Version\s*=\s*(\d+\.\d+\.\d+\.\d+)\s*,\s*Culture\s*=\s*([^,]+)\s*,\s*PublicKeyToken\s*=\s*([a-fA-F0-9]+)\s*$";
        private static readonly Regex extractAssemblyInfoRegex = new Regex(extractAssemblyInfo);
        private static readonly Regex validAssemblyInfoRegex = new Regex(validAssemblyInfo);

        public static bool AssemblyInfoIsMatch(this Assembly assembly, string Name, int[] Version = null, string Culture = "", string PublicKeyToken = "")
        {
            var fullName = assembly.FullName;
            if (!string.IsNullOrWhiteSpace(fullName) && validAssemblyInfoRegex.IsMatch(fullName))
            {
                var groups = extractAssemblyInfoRegex.Match(fullName);
                if (groups.Success)
                {
                    if (string.IsNullOrWhiteSpace(Name) || groups.Groups[assemblyName].Value != Name) return false;
                    if (Version != null && Version.Length > 0)
                    {
                        var splits = groups.Groups[assemblyVersion].Value.Split('.');
                        for (int i = 0; i < Version.Length; i++)
                        {
                            if (Version[i] != int.Parse(splits[i])) return false;
                        }
                    }
                    if (!string.IsNullOrWhiteSpace(Culture) && groups.Groups[assemblyCulture].Value != Culture) return false;
                    if (!string.IsNullOrWhiteSpace(PublicKeyToken) && groups.Groups[assemblyPublicKeyToken].Value != PublicKeyToken) return false;
                    return true;
                }
            }
            return false;
        }

        #endregion Regex

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