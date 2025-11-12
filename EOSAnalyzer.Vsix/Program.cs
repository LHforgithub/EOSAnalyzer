// See https://aka.ms/new-console-template for more information
using EOS;
using EOS.Attributes;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.DependencyModel;
using System.Reflection;
using System.Text;

var @class =
@"
using System;
using System.Runtime;
using EOS;
using EOS.Attributes;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    public class TestAttribute : Attribute{
    }
    [EventListener]
    public class TEST { }
    //[EventCode]
    public class Listener : EClss
    {
        public Listener() { }

        public void DFunc_First(string message)
        {
            //EOSManager.AddListener(typeof(TEST), new TEST());
            EOSManager.BroadCast(typeof(EFirst));
            //Console.WriteLine($""DFunc Event First received: {message}"");
        }

        [EventListener(typeof(ESecond))]
        public void DFunc_Second(EClss message)
        {
            var str = ""t m""
            //EOSManager.BroadCast<ESecond>(str);
            //Console.WriteLine($""DFunc Event First received: {message}"");
        }
        public string Test
        {
            get{
                return """";
            }
            set{
                //EOSManager.BroadCast<EFirst>(""t m"");
                _ = value;
            } 
        }

        public void EClss()
        {
            Console.WriteLine(""EClss"");
        }
    }

    [EventListener]
    [EventCode]
    interface EClss
    {
        [EventCodeMethod]
        [EventListener(typeof(EClss))]
        void EClss();

    }

    [EventCode]
    interface EFirst
    {
        [EventCodeMethod]
        void DFunc_First(string message);
    }
    [EventCode]
    interface ESecond
    {
        [EventCodeMethod]
        void DFunc_Second(Listener message);
    }
}
";
var tree = CSharpSyntaxTree.ParseText(@class);
var compilation = CSharpCompilation.Create("HelloWorld")
         .AddSyntaxTrees(tree)
         .AddReferences(MetadataReference.CreateFromFile(
             typeof(object).GetTypeInfo().Assembly.Location), MetadataReference.CreateFromFile(
             typeof(List<>).GetTypeInfo().Assembly.Location), MetadataReference.CreateFromFile(
             typeof(EOS.EOSManager).GetTypeInfo().Assembly.Location))
         .WithOptions(new CSharpCompilationOptions(
             OutputKind.DynamicallyLinkedLibrary));
foreach (var abName in typeof(EOS.EOSManager).GetTypeInfo().Assembly.GetReferencedAssemblies())
{
    var re = Assembly.Load(abName.FullName);
    compilation = compilation.AddReferences(MetadataReference.CreateFromFile(re.Location));
}
EOSAnalyzer.EOSAnalyzer analyzer = new EOSAnalyzer.EOSAnalyzer();
analyzer.EOSSyntaxTreeCheck(tree, compilation);

foreach(var dia in analyzer.diagnosticsQueue)
{
    Console.WriteLine(dia.GetMessage().ToString());
}

Console.ReadKey();
namespace ConsoleApplication1
{
    public class TestAttribute : Attribute
    {
    }
    [EventListener]
    public class TEST { }
    //[EventCode]
    public class Listener : EClss
    {
        public Listener() { }

        public void DFunc_First(string message)
        {
            //EOSManager.AddListener(typeof(TEST), new TEST());
            EOSManager.BroadCast(typeof(EFirst), "");
            //Console.WriteLine($""DFunc Event First received: {message}"");
        }

        [EventListener(typeof(ESecond))]
        public void DFunc_Second(EClss message)
        {
            var str = "t m";
            //EOSManager.BroadCast<ESecond>(str);
            //Console.WriteLine($""DFunc Event First received: {message}"");
        }
        public string Test
        {
            get
            {
                return "";
            }
            set
            {
                //EOSManager.BroadCast<EFirst>(""t m"");
                _ = value;
            }
        }

        public void EClss()
        {
            Console.WriteLine("EClss");
        }
    }

    [EventListener]
    [EventCode]
    public interface EClss
    {
        [EventCodeMethod]
        [EventListener(typeof(EClss))]
        void EClss();

    }

    [EventCode]
    interface EFirst
    {
        [EventCodeMethod]
        void DFunc_First(string message);
    }
    [EventCode]
    interface ESecond
    {
        [EventCodeMethod]
        void DFunc_Second(Listener message);
    }
}