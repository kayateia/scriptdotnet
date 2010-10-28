#region using

using Irony.Compiler;
using ScriptNET.Runtime;
#endregion

namespace ScriptNET.Ast
{
  /// <summary>
  /// Script Array Constructor Expression
  /// </summary>
  internal class ScriptCondition : ScriptAst
  {
    private ScriptExpr conditionExpression;

    /// <summary>
    /// Returns condition
    /// </summary>
    public ScriptExpr Expression
    {
      get
      {
        return conditionExpression;
      }
      set
      {
        conditionExpression = value;
      }
    }

    public ScriptCondition(AstNodeArgs args)
        : base(args)
    {
      conditionExpression = (ScriptExpr)ChildNodes[0];
    }

    public override void Evaluate(IScriptContext context)
    {     
      conditionExpression.Evaluate(context);

		// If the answer isn't bool, coerce it to bool if possible.
		context.Result = TypeCoercion.CoerceToBool(context.Result);

		// Old code: this prevents Javascript-esque if(foo) syntax.
/* #if DEBUG
      if (!(context.Result is bool))
        throw new ScriptException("Condition expression evaluates non boolean value");      
#endif */
    }
  }
}