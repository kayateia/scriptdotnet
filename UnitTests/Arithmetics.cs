using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Irony.Compiler;
using ScriptNET;
using ScriptNET.Runtime;

namespace UnitTests
{
  [TestClass]
  public class Arithmetics
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
    public void ArithmeticExpressions()
    {
      IScriptContext context = new ScriptContext();
      object result =
         Script.RunCode(
         @"
              a=1.0;
              b = 2.0; 
              c = 3.0; 
              d = 2.0;
              e = 18.0;
              f = 6.0;

              p = 2.0; u = 3.0; v = 1.0; r = 2.0; s = 5.0; t = 12.0;

              // r1 = 9
              r1 = a + b + c*d;

              // r2 = -2.5
              r2 = a*(b - c/d )- e/f;

              //r3 = -4.5
              r3 = a*b*((c - d )*a - p*(u - v)*(r + s))/t;

              //r4 = 65536
              r4 = 2 * d^(c*5);

              //r5 = 2
              r5 = 5 % 3;
           
              v1 = -3;
        ",
         context);

      Assert.AreEqual(9.0, context.GetItem("r1", true));
      Assert.AreEqual(-2.5, context.GetItem("r2", true));
      Assert.AreEqual(-4.5, context.GetItem("r3", true));
      Assert.AreEqual((double)65536, context.GetItem("r4", true));
      Assert.AreEqual((Int32)2, context.GetItem("r5", true));
      Assert.AreEqual(-3, context.GetItem("v1", true));
    }

  }
}
