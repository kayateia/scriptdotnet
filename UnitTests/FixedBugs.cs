using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Irony.Compiler;
using ScriptNET;
using ScriptNET.Runtime;
using ScriptNET.Ast;
using ScriptNET.Runtime.Operators;

namespace UnitTests
{
  /// <summary>
  /// Summary description for Bugs_June
  /// </summary>
  [TestClass]
  public class BugsJune
  {
    [TestInitialize]
    public void Setup()
    {
      RuntimeHost.Initialize();
      EventBroker.ClearAllEvents();
    }

    [TestCleanup]
    public void TearDown()
    {
      RuntimeHost.CleanUp();
      EventBroker.ClearAllEvents();
    }

    [TestMethod]
    public void ExpandoFields()
    {
      RuntimeHost.AddType("Array", typeof(List<int>));

      object resultVal =
         Script.RunCode(
         @"
            return [                
                      storage -> new Array(),
                      storage1 -> new Array(),
                      storage2 -> new Array()
                    ];
          "
         );

      Expando data = (Expando)resultVal;

      Assert.IsInstanceOfType(data["storage"], typeof(List<int>));
      Assert.IsInstanceOfType(data["storage1"], typeof(List<int>));
      Assert.IsInstanceOfType(data["storage2"], typeof(List<int>));
    }
    
    [TestMethod]
    public void TestTypeExpr()
    {
      RuntimeHost.AddType("Int", typeof(int));
      ScriptContext context = new ScriptContext();

      object rez = Script.RunCode("a = 2; return a is Int;", context);
      Assert.IsTrue((bool)rez);
    }

    [TestMethod]
    public void TestTypeExprGenerics()
    {
      RuntimeHost.AddType("Int", typeof(int));
      ScriptContext context = new ScriptContext();

      object rez = Script.RunCode("a = new List<|Int|>(); return a is List<|Int|>;", context);
      Assert.IsTrue((bool)rez);
    }

    [TestMethod]
    public void TestTypeExprBug()
    {
      object rez = Script.RunCode("a = 2; return a is int;");
      Assert.IsTrue((bool)rez);
    }

    [TestMethod]
    public void MethodInvocationBug()
    {
      RuntimeHost.AddType("TestOverloading", typeof(TestOverloading));
      object rez = Script.RunCode("b = new TestOverloading(); return b.GetString(2);");
      Assert.AreEqual("Ok", rez);
    }

    [TestMethod]
    public void TestTypeExprGenerics1()
    {
      object rez = Script.RunCode("b = new System.Collections.Generic.Stack<|string|>();");
      Assert.IsInstanceOfType(rez, typeof(Stack<string>));
    }

    [TestMethod]
    public void InternalClassTest()
    {
      object rez = Script.RunCode("Class123.Class234.Value;");
      Assert.AreEqual(Class123.Class234.Value, rez);
    }

    [TestMethod]
    public void TypeConversionConflictsWithArithmeticOperations()
    {
      object rez = Script.RunCode("(1+2+3)-6;");
      Assert.AreEqual(0, rez);
    }

    [TestMethod]
    public void InterfaceLimitations()
    {
      object rez = Script.RunCode(@"
         a = new AT1();

         it = a.Method();

         a.SetMethod(it);
         a.Property = it;
         
         it.Value = 2;
         return it.Value;
      ");

      Assert.AreEqual(2, rez);
    }

    [TestMethod]
    public void EqualityOperator()
    {
      object rez = Script.RunCode(@"
          a = new Class1_1();
          if (a.Short == 1 && a.C == 'C')
            return 1;
          else 
            return 2;
      ");

      Assert.AreEqual(1, rez);
    }

    [TestMethod]
    public void EqualityOperatorFullTest()
    {
      EqualsOperator eq = new EqualsOperator();

      Assert.IsTrue((bool)eq.Evaluate(Byte.Parse("1"), Byte.Parse("1")));
      Assert.IsTrue((bool)eq.Evaluate(Byte.Parse("1"), Int16.Parse("1")));
      Assert.IsTrue((bool)eq.Evaluate(Byte.Parse("1"), Int32.Parse("1")));
      Assert.IsTrue((bool)eq.Evaluate(Byte.Parse("1"), Int64.Parse("1")));
      Assert.IsTrue((bool)eq.Evaluate(Byte.Parse("1"), Double.Parse("1")));
      Assert.IsTrue((bool)eq.Evaluate(Byte.Parse("1"), Single.Parse("1")));

      Assert.IsTrue((bool)eq.Evaluate(Int16.Parse("1"), Byte.Parse("1"))); 
      Assert.IsTrue((bool)eq.Evaluate(Int16.Parse("1"), Int16.Parse("1")));
      Assert.IsTrue((bool)eq.Evaluate(Int16.Parse("1"), Int32.Parse("1")));
      Assert.IsTrue((bool)eq.Evaluate(Int16.Parse("1"), Int64.Parse("1")));
      Assert.IsTrue((bool)eq.Evaluate(Int16.Parse("1"), Double.Parse("1")));
      Assert.IsTrue((bool)eq.Evaluate(Int16.Parse("1"), Single.Parse("1")));

      Assert.IsTrue((bool)eq.Evaluate(Int32.Parse("1"), Byte.Parse("1")));
      Assert.IsTrue((bool)eq.Evaluate(Int32.Parse("1"), Int16.Parse("1")));
      Assert.IsTrue((bool)eq.Evaluate(Int32.Parse("1"), Int32.Parse("1")));
      Assert.IsTrue((bool)eq.Evaluate(Int32.Parse("1"), Int64.Parse("1")));
      Assert.IsTrue((bool)eq.Evaluate(Int32.Parse("1"), Double.Parse("1")));
      Assert.IsTrue((bool)eq.Evaluate(Int32.Parse("1"), Single.Parse("1")));

      Assert.IsTrue((bool)eq.Evaluate(Int64.Parse("1"), Byte.Parse("1")));
      Assert.IsTrue((bool)eq.Evaluate(Int64.Parse("1"), Int16.Parse("1")));
      Assert.IsTrue((bool)eq.Evaluate(Int64.Parse("1"), Int32.Parse("1")));
      Assert.IsTrue((bool)eq.Evaluate(Int64.Parse("1"), Int64.Parse("1")));
      Assert.IsTrue((bool)eq.Evaluate(Int64.Parse("1"), Double.Parse("1")));
      Assert.IsTrue((bool)eq.Evaluate(Int64.Parse("1"), Single.Parse("1")));

      Assert.IsTrue((bool)eq.Evaluate(Double.Parse("1"), Byte.Parse("1")));
      Assert.IsTrue((bool)eq.Evaluate(Double.Parse("1"), Int16.Parse("1")));
      Assert.IsTrue((bool)eq.Evaluate(Double.Parse("1"), Int32.Parse("1")));
      Assert.IsTrue((bool)eq.Evaluate(Double.Parse("1"), Int64.Parse("1")));
      Assert.IsTrue((bool)eq.Evaluate(Double.Parse("1"), Double.Parse("1")));
      Assert.IsTrue((bool)eq.Evaluate(Double.Parse("1"), Single.Parse("1")));

      Assert.IsTrue((bool)eq.Evaluate(Single.Parse("1"), Byte.Parse("1")));
      Assert.IsTrue((bool)eq.Evaluate(Single.Parse("1"), Int16.Parse("1")));
      Assert.IsTrue((bool)eq.Evaluate(Single.Parse("1"), Int32.Parse("1")));
      Assert.IsTrue((bool)eq.Evaluate(Single.Parse("1"), Int64.Parse("1")));
      Assert.IsTrue((bool)eq.Evaluate(Single.Parse("1"), Double.Parse("1")));
      Assert.IsTrue((bool)eq.Evaluate(Single.Parse("1"), Single.Parse("1")));

      Assert.IsTrue((bool)eq.Evaluate("C", 'C'));

    }

  }

  #region Interfaces
  public interface IT1
  {
    int Value { get; set; }
  }

  public class AT1 : IT1
  {
    #region IT1 Members

    public int Value
    {
      get;
      set;
    }

    #endregion

    public IT1 Method()
    {
      return this;
    }

    public void SetMethod(IT1 v)
    {
    }

    public IT1 Property
    {
      get;
      set;
    }
  }

  #endregion

  #region Test Class
  public class Class1_1
  {
    public short Short { get { return 1; } }

    public char C { get { return 'C'; } }
  }
  
  public class Class123
  {
    public class Class234
    {
      public static string Value = "Test";

      public string Instance = "Test1";
    }
  }
  #endregion

  #region Helpers
  class TestOverloading
  {
    public string GetString(bool value)
    {
      return "Error";
    }

    public string GetString(int value)
    {
      return "Ok";
    }
  }
  #endregion
}
