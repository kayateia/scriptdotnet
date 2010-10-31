namespace ScriptNET {
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

internal class TypeCoercion {
	// Handle type coercion to bool for Javascript-style if(foo) statements.
	static public object CoerceToBool(object obj) {
		if (obj is bool) {
			// Do nothing, we're fine.
		} else if (obj is string) {
			// A non-empty string is true.
			obj = !string.IsNullOrEmpty(obj as string);
		} else {
			// Give the binder a chance to splice in a bool coercion.
			var equalityMethod = Runtime.RuntimeHost.Binder.BindToMethod(obj, "IsTrue", null, new object[0]);
			if (equalityMethod != null) {
				var result = equalityMethod.Invoke(null, new object[0]);
				if (result is bool)
					obj = (bool)result;
				else
					obj = obj != null;
			} else {
				// A non-null object is true.
				obj = obj != null;
			}
		}

		return obj;
	}
}

}
