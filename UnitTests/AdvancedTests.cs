﻿using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScriptNET;
using System.Threading;
using ScriptNET.Runtime;

namespace UnitTests
{
  /// <summary>
  /// Summary description for ToDo
  /// </summary>
  [TestClass]
  public class AdvancedTests
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
    public void FunctionScoping()
    {
      ScriptContext context = new ScriptContext();
      Script script = Script.Compile(@"
          y = 100;

          function f()
          { 
            y = y - 1;
          }
          f();          

          return y;
      ");

      object result = script.Execute();
      Assert.AreEqual(100, result);
    }


    [TestMethod]
    public void FunctionGlobalScoping()
    {
      ScriptContext context = new ScriptContext();
      Script script = Script.Compile(@"
          y = 100;

          function f() global(y)
          { 
            y = y - 1;
          }
          f();          

          return y;
      ");

      object result = script.Execute();
      Assert.AreEqual(99, result);
    }

    [TestMethod]
    public void FunctionExpressionGlobalScoping()
    {
      ScriptContext context = new ScriptContext();
      Script script = Script.Compile(@"
          y = 100;

          f = function() global(y)
          { 
            y = y - 1;
          };

          f();          

          return y;
      ");

      object result = script.Execute();
      Assert.AreEqual(99, result);
    }

    [TestMethod]
    public void FunctionRecursionWithLocalAssignment()
    {
      ScriptContext context = new ScriptContext();
      Script script = Script.Compile(@"
        function Draw(level)
        {           
          if (level>0)
          {
            level = level - 1;
            return Draw(level)+Draw(level);
          }

          return 1;
        }

        Draw(n);
      ");
      int n = 10;
      script.Context.SetItem("n", n);

      object result = script.Execute();
      Assert.AreEqual((int)Math.Pow(2,n), (int)result);
    }

    [TestMethod]
    public void SimpleSequentialCalls()
    {
      ScriptContext context = new ScriptContext();
      Script script = Script.Compile(@" 
        function f() {return [1,2,3];}
  
        return f()[1];
      ");

      object result = script.Execute();
      Assert.AreEqual(2, result);
    }

    [TestMethod]
    public void SequentialCalls()
    {
      ScriptContext context = new ScriptContext();
      Script script = Script.Compile(@" 
        function f() {return [1,2,3];}
        
        function q() {return [1,f,0];} 
 
        return q()[1]()[2];
      ");

      object result = script.Execute();
      Assert.AreEqual(3, result);
    }

    [TestMethod]
    public void ExternalFunctionCall()
    {
      ScriptContext context = new ScriptContext();
      Script script = Script.Compile(@"
        return Test();
      ");
      script.Context.SetItem("Test", new TestFunction());

      object result = script.Execute();
      Assert.AreEqual(10, result);
    }

    [TestMethod]
    public void Classes()
    {
      ScriptContext context = new ScriptContext();
      Script script = Script.Compile(@"
          school =
          [
            classes ->
              [
                A->['Alexey', 'Ivan','John'],
                B->['Petr','Valya','Masha','Joe']
              ],

             GetClasses->function ()
              {
                result = new List<|object|>(); 
                foreach(class in body.classes.Fields)
                   result.Add(class);
                
                return result;
              },

             GetClass->function (name)
              {
                result = new List<|object|>(); 
                foreach(student in body.classes[name] )
                     result.Add(class);
                
                return result;
              }

          ];

          school.GetClasses();
      ");

      List<object> result = (List<object>) script.Execute();
      Assert.AreEqual(2, result.Count);
    }
     
    [TestMethod]
    public void Graphs()
    {
      ScriptContext context = new ScriptContext();
      Script script = Script.Compile(@"
          //1
          //  2
          //   3
          //  4
          //  5

          u = [ 1, 2 , 3 , 4 , 5];
          v = [ [1,2], [2,3], [1,4], [1,5] ];

          g = [ 
                vertexes -> u,
                edges    -> v
              ];

          function IsTree(g)
          {
           vert = new ArrayList ();
           
           foreach (edge in g.edges)
           {
             if (!vert.Contains(edge[1]))
                  vert.Add(edge[1]);
             else
                  return false;
           } 

           if  ( vert.Count == g.vertexes.Length-1 )
           {
             foreach (root in g.vertexes)
              if ( ! vert.Contains(root))
              {
                 g.root =  root;
                 break;
               }

             return true;
           }
           else
              return false;
          }

          IsTree(g);

          root = g.root;

          s = '';
          foreach (x in v)
           s = s + x[0]+'->' + x[1] + ', ';

      ");

      string result = (string)script.Execute();
      Assert.AreEqual("1->2, 2->3, 1->4, 1->5, ", result);
    }

    //TODO: Review this approach
    [TestMethod]
    public void Threading()
    {
      ScriptContext context = new ScriptContext();
      //context.AddType("Thread", typeof(Thread));
      //context.AddType("ThreadStart", typeof(ThreadStart));
      //context.AddType("ParameterizedThreadStart", typeof(ParameterizedThreadStart));

      Script script = Script.Compile(@"
          function ThreadTest()
          { 
            //MessageBox.Show('Test Thread');
            //x = 'Test';
            return true;
          }

          th = new Thread(new ParameterizedThreadStart(ThreadTest, ThreadTest.ThreadInvoke));
          th.Start(Context);

          Thread.Sleep(1000);
      ");

      object result = script.Execute();
    }

    [TestMethod]
    public void MethodHandling()
    {
      object result = Script.RunCode(@"
             sin = Math.Sin;

             return sin(0.75);
      ");

      Assert.AreEqual(Math.Sin(0.75), result);
    }

    [TestMethod]
    public void MethodThroughInterface()
    {
      object result = Script.RunCode(@"
             a = new TestInterface();

             return a.Get();
      ");

      Assert.AreEqual(15, result);
    }

    [TestMethod]
    public void MethodThroughInterfaceExplicit()
    {
      object result = Script.RunCode(@"
             a = new TestInterface();
             i = new ExplicitInterface(a, ITest);
             
             return i.Get();
      ");

      Assert.AreEqual(2, result);
    }

    [TestMethod]
    public void MethodThroughInterfaceExplicit1()
    {
      object result = Script.RunCode(@"
             a = new TestInterfaceSingle();
             
             return a.Get();
      ");

      Assert.AreEqual(2, result);
    }

    [TestMethod]
    public void ConflictingLexicalTokens()
    {
      //Is a conflict between meta expression token <[   ]>
      //and array resolution with greater operator
      object result = Script.RunCode(@"
           a=[2,5,10];
           if (a[2]>a[1])
             return 2;
      ");

      Assert.AreEqual(2, result);
    }

    [TestMethod]
    public void ComparingStrings()
    {
      object result = Script.RunCode(@"
           a = 'a';
           b = 'b';

           a>b;
           b<a;
           a>=b; 
           a<=b;
           a==b;
      ");

      Assert.AreEqual(false, result);
    }

    [TestMethod]
    public void CallToGenericMethod()
    {
      object result = Script.RunCode(@"
             a = new TestInterface();

             return a.GenericGet<|string|>('Hello World');
      ");

      Assert.AreEqual(new TestInterface().GenericGet<string>("Hello World"), result);
    }

    [TestMethod]
    public void UsingGenericArguments()
    {
      object result = Script.RunCode(@"
             a = new DoubleGeneric<|TestInterface, ITest1|>();

             return a.Get(new TestInterface());
      ");

      Assert.IsInstanceOfType(result, typeof(TestInterface));
    }

    [TestMethod]
    public void EvaluatingNetMethodsToIInvokable()
    {
      object result = Script.RunCode(@"
             a = DateTime.Now;
             b = a.ToString;
          
             return b();
      ");

      Assert.IsInstanceOfType(result, typeof(string));
    }
  }

  public class TestFunction : IInvokable
  {
    #region IInvokable Members

    public bool CanInvoke()
    {
      return true;
    }

    public object Invoke(IScriptContext context, object[] args)
    {
      return 10;
    }

    #endregion
  }

  public interface ITest
  {
    int Get();
  }

  public interface ITest1
  {
    int Get();
  }

  public class TestInterface : ITest, ITest1
  {
    #region ITest Members

    int ITest.Get()
    {
      return 2;
    }

    #endregion

    #region ITest1 Members

    public int Get()
    {
      return 15;
    }

    #endregion

    public string GenericGet<T>(T input)
    {
      return input.ToString();
    }
  }

  public class TestInterfaceSingle : ITest
  {
    int ITest.Get()
    {
      return 2;
    }
  }


  public class DoubleGeneric<T, W> where T : W
  {
    public W Get(T input)
    {
      return (W)input;
    }
  }
}
