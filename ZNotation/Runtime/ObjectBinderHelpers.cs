using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using ScriptNET.Runtime.MutantFramework;

namespace ScriptNET.Runtime
{
  public partial class ObjectBinder
  {
    protected static bool CanBind(MemberInfo member)
      {
        BindableAttribute bindable = member.GetCustomAttributes(typeof(BindableAttribute), true).OfType<BindableAttribute>().FirstOrDefault();
        if (bindable == null)
            bindable = member.DeclaringType.GetCustomAttributes(typeof(BindableAttribute), true).OfType<BindableAttribute>().FirstOrDefault();

        if (bindable == null) return true;
        return bindable.CanBind;
      }

    protected class PropertyHandler : IGetter, ISetter, IHandler
    {
      #region IGetter Members
      public object Get(string name, object instance, Type type, params object[] arguments)
      {
        PropertyInfo pi = type.GetProperty(name);
        if (pi == null) return NoResult;
        if (!CanBind(pi)) return NoResult;

        return pi.GetValue(instance, arguments);
      }
      #endregion

      #region ISetter Members

      public object Set(string name, object instance, Type type, object value, params object[] arguments)
      {
        PropertyInfo pi = GetProperty(type, name);
        if (pi == null) return NoResult;
        if (!CanBind(pi)) return NoResult;

        if (value != null && pi.PropertyType.IsAssignableFrom(value.GetType()))
        {
          pi.SetValue(instance, value, null);
        }
        else
        {
          pi.SetValue(instance, RuntimeHost.Binder.ConvertTo(value, pi.PropertyType), arguments);
        }
        return value;
      }

      private PropertyInfo GetProperty(Type type, string name)
      {
        return type.GetProperties().Where(pi => pi.Name == name).FirstOrDefault();
      }

      #endregion
    }

    protected class FieldHandler : IGetter, ISetter, IHandler
    {
      #region IGetter Members
      public object Get(string name, object instance, Type type, params object[] arguments)
      {
        FieldInfo fi = type.GetField(name);
        if (fi == null) return NoResult;
        if (!CanBind(fi)) return NoResult;

        return fi.GetValue(instance);
      }
      #endregion

      #region ISetter Members

      public object Set(string name, object instance, Type type, object value, params object[] arguments)
      {
        FieldInfo fi = type.GetField(name);
        if (fi == null) return NoResult;
        if (!CanBind(fi)) return NoResult;

        fi.SetValue(instance, RuntimeHost.Binder.ConvertTo(value, fi.FieldType));
        return value;
      }

      #endregion
    }

    protected class EventHandler : IGetter, ISetter, IHandler
    {
      #region IGetter Members
      public object Get(string name, object instance, Type type, params object[] arguments)
      {
        EventInfo ei = type.GetEvent(name);
        if (ei == null) return NoResult;
        if (!CanBind(ei)) return NoResult;

        //Type eventHelper = typeof(EventHelper<>);
        //Type actualHelper = eventHelper.MakeGenericType(ei.EventHandlerType);
        return ei;
      }
      #endregion

      #region ISetter Members

      public object Set(string name, object instance, Type type, object value, params object[] arguments)
      {
        EventInfo ei = type.GetEvent(name);
        if (ei == null) return NoResult;
        if (!CanBind(ei)) return NoResult;

        if (!(value is RemoveDelegate))
          EventHelper.AssignEvent(ei, instance, (IInvokable)value);
        else
          EventHelper.RemoveEvent(ei, instance, (IInvokable)value);

        return value;
      }

      #endregion
    }

    protected class MethodGetter : IGetter
    {
      #region IGetter Members
      public object Get(string name, object instance, Type type, params object[] arguments)
      {
        MethodInfo method = type.GetMethod(name);
        if (method == null) return NoResult;
        if (!CanBind(method)) return NoResult;

        return new InvokableMethod(method, instance);
      }
      #endregion
    }

    protected class MutantHandler : IGetter, ISetter, IHandler
    {
      #region IGetter Members
      public object Get(string name, object instance, Type type, params object[] arguments)
      {
        IMutant dm = instance as IMutant;
        if (dm == null) return NoResult;

        return dm.Get(name, null);
      }
      #endregion

      #region ISetter Members

      public object Set(string name, object instance, Type type, object value, params object[] arguments)
      {
        IMutant dm = instance as IMutant;
        if (dm == null) return NoResult;
        dm.Set(name, value, null);

        return value;
      }

      #endregion
    }

    protected class ScriptableHandler : IGetter, ISetter, IHandler
    {
        #region IGetter Members
        public object Get(string name, object instance, Type type, params object[] arguments)
        {
            IScriptable dm = instance as IScriptable;
            if (dm == null) return NoResult;

            return dm.GetMember(name, arguments).GetValue();
        }
        #endregion

        #region ISetter Members

        public object Set(string name, object instance, Type type, object value, params object[] arguments)
        {
            IScriptable dm = instance as IScriptable;
            if (dm == null) return NoResult;

            dm.GetMember(name, arguments).SetValue(value);
            return value;
        }

        #endregion
    }


    protected class NestedTypeGetter : IGetter
    {
      #region IGetter Members
      public object Get(string name, object instance, Type type, params object[] arguments)
      {
#if !PocketPC && !SILVERLIGHT
        Type nested = type.GetNestedType(name);
#else
            Type nested = type.GetNestedType(name, BindingFlags.Public | BindingFlags.NonPublic);
#endif
        if (nested == null) return NoResult;

        return nested;
      }
      #endregion
    }

    //protected class NameSpaceGetter : IGetter
    //{
    //    #region IGetter Members
    //    public object Get(string name, object instance, Type type, params object[] arguments)
    //    {
    //        NameSpaceMutant nameSpace = NameSpaceCache.Get(type.Namespace); // new NameSpaceMutant(type.Namespace);
    //        object rez = nameSpace.Get(name, null);
    //        if (rez == null) return NoResult;

    //        return rez;
    //    }
    //    #endregion
    //}
  }
}
