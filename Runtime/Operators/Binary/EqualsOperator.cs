using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptNET.Runtime.Operators
{
   public class EqualsOperator : IOperator 
{
public EqualsOperator() 
{
}
#region IOperator Members 
public string Name 
{
get { return "=="; } 
}
public bool Unary 
{
get { return false; } 
}
public object Evaluate(object value) 
{
throw new NotImplementedException(); 
}
public object Evaluate(object left, object right) 
{
if (left == null || right == null) 
return object.Equals(left, right); 
IObjectBind equalityMethod = null; 
equalityMethod = 
RuntimeHost.Binder.BindToMethod(left, "op_Equality", null, new object[] { left, right }); 
if (equalityMethod != null) 
return equalityMethod.Invoke(null, new object[] { left, right }); 
equalityMethod = 
RuntimeHost.Binder.BindToMethod(right, "op_Equality", null, new object[] { right, left }); 
if (equalityMethod != null) 
return equalityMethod.Invoke(null, new object[] { right, left }); 
else 
return object.Equals(left, right); 
}
#endregion
}

}
