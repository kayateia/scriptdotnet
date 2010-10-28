using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Irony.Compiler;
using ScriptNET;
using ScriptNET.Runtime;
using ScriptNET.Ast;
using System.Collections;

namespace UnitTests
{
  [TestClass]
  public class AcceptanceTests_FirstPart
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
    public void AccessingEnum()
    {
      object resultVal =
         Script.RunCode(
         @"
            return A_Enum.Two;
          "
         );

      Assert.AreEqual(A_Enum.Two, resultVal);
    }

    [TestMethod]
    public void GetEnumeratorTest()
    {
      string result = "";
      foreach (string s in new A_TestList())
      {
        result += s;
      }

      object resultVal =
         Script.RunCode(
         @"
           result = '';
           foreach (s in new A_TestList())
           {
            result += s;
           }
          "
         );

      Assert.AreEqual(result, resultVal);
    }

    [TestMethod]
    public void CreatesArrayWith1000Objects()
    {
      int N = 1000;
      StringBuilder codeBuilder = new StringBuilder();
      codeBuilder.Append('[');
      codeBuilder.Append('1');
      for (int i = 0; i < N; i++) 
      {
         codeBuilder.Append(',');
         codeBuilder.Append(i);
      }
      codeBuilder.Append("];");

      object[] resultVal = (object[]) Script.RunCode( codeBuilder.ToString() );

      Assert.AreEqual(N + 1, resultVal.Length);
    }

    [TestMethod]
    public void CreatesExpandoWith2000Fields()
    {
      int N = 2000;
      StringBuilder codeBuilder = new StringBuilder();
      codeBuilder.Append("[b->'Hello'");
      for (int i = 0; i < N; i++)
      {
        codeBuilder.AppendFormat(", a{0}->{1}", i, i);
      }
      codeBuilder.Append("];");

      Expando resultVal = (Expando)Script.RunCode(codeBuilder.ToString());

      Assert.AreEqual(N + 1, resultVal.Fields.Count());
    }   
  }

  public class A_TestList
  {
    public IEnumerator GetEnumerator()
    {
      return (new object[] { "Hello", " ", "World" }).GetEnumerator();
    }
  }

  public enum A_Enum
  {
    One,
    Two,
    Three
  }
}
