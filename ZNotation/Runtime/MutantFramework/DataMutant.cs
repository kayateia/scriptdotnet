using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Collections.ObjectModel;

namespace ScriptNET.Runtime.MutantFramework
{
  #region DataMutant
  /// <summary>
  /// Data Mutant Class. 
  /// Implements Generalized Class on the data level. It is able:
  /// Invoke any method of a Victim class;
  /// Set and Get fields of Victim object.
  /// </summary>
  /// <example>
  ///   mObj = [ Text -> 'Hello' , X -> 10, Y -> 150];
  ///   point = new Point(20,300);
  ///   % Mutantic Assignment:
  ///   point:= mObj;
  /// </example>
#if !PocketPC && !SILVERLIGHT
  [ComVisible(true)]
  [ProgId("DataMutant")]
#endif
  public class DataMutant : IConvertible, IMutant
  {
    #region Properties
    /// <summary>
    /// Object captured by Data Mutant
    /// </summary>
    protected object Victim;

    /// <summary>
    /// Indicates the Fields (or Properties overrident by the Mutant)
    /// </summary>
    protected Dictionary<string, object> MutanticFields;
    /// <summary>
    /// Behavior of the Data Mutant, default value Nothing
    /// </summary>
    protected DataMutantBehavior Behavior;

    /// <summary>
    /// Quick access to the fields
    /// </summary>
    /// <param name="FieldName"></param>
    /// <returns></returns>
    public object this[string FieldName]
    {
      get
      {
        return Get(FieldName, null);
      }

      set
      {
        Set(FieldName, value, null);
      }
    }

    public ReadOnlyCollection<string> Names
    {
        get
        {
            return new ReadOnlyCollection<string>(MutanticFields.Keys.ToArray());
        }
    }
    #endregion

    #region Constructors
    private void Init(object Victim, DataMutantBehavior Behavior)
    {
      this.Victim = Victim;
      this.MutanticFields = new Dictionary<string, object>();
      this.Behavior = Behavior;
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="Victim"></param>
    /// <param name="Behavior"></param>
    public DataMutant(object Victim, DataMutantBehavior Behavior)
    {
      Init(Victim, Behavior);
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="Victim"></param>
    public DataMutant(object Victim)
    {
      Init(Victim, DataMutantBehavior.CreateMutantField);
    }

    /// <summary>
    /// Constructor
    /// </summary>
    public DataMutant()
    {
      Init(new object(), DataMutantBehavior.Nothing);
    }
    #endregion

    #region Victim Methods
    /// <summary>
    /// Invoke method of the Victim
    /// </summary>
    /// <param name="FuncName"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    public virtual object Invoke(string FuncName, object[] param)
    {
      return Invoke(new string[] { FuncName }, param);
    }

    /// <summary>
    /// Invokes method of the Victim
    /// </summary>
    /// <param name="FuncName"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    public virtual object Invoke(string[] FuncName, object[] param)
    {
      Type tp = Victim.GetType();

      IEnumerable<MethodInfo> mis = tp.GetMethods().Where(m => FuncName.Contains(m.Name));
      //Find Method with appropreate name
      //TODO: RuntimeHost.Binder.BindToMethod(Victim, mf, ...)
      foreach (MethodInfo mf in mis)
      {
        ParameterInfo[] pis = mf.GetParameters();
        //Try convert parameters
        if (pis.Length == param.Length)
        {
          List<object> cpars = new List<object>();
          for (int i = 0; i < pis.Length; i++)
          {
            ParameterInfo pi = pis[i];
            try
            {
              if (param[i] is IConvertible)
              {
#if PocketPC || SILVERLIGHT
                object o = Convert.ChangeType(param[i], pi.ParameterType, System.Globalization.CultureInfo.InvariantCulture);
#else
                object o = Convert.ChangeType(param[i], pi.ParameterType);
#endif
                cpars.Add(o);
              }
              else
                if (param[i] == null || param[i].GetType().IsSubclassOf(pi.ParameterType) || param[i].GetType() == pi.ParameterType)
                {
                  cpars.Add(param[i]);
                }
            }
            catch
            {
              break;
            }
          }
          //All Parameters converted
          if (cpars.Count == pis.Length)
          {
            return mf.Invoke(Victim, cpars.ToArray());
          }
        }
      }

      string message = "";
      foreach (object t in param)
      {
        if (message == "") message += t.GetType().Name;
        else message += ", " + t.GetType().Name;
      }
      throw new Exception("Semantic: There is no method with such signature(" + message + ")");

    }

    /// <summary>
    /// Set value of the property. If victim specified it is also assgined to its property
    /// </summary>
    /// <param name="Name"></param>
    /// <param name="value"></param>
    /// <param name="index"></param>
    public virtual void Set(string Name, object value, object[] index)
    {
      Type tp = Victim.GetType();
      MemberInfo[] mis = tp.GetMembers();
      foreach (MemberInfo mi in mis)
      {
        if (mi.Name == Name)
        {
          if (mi.MemberType == MemberTypes.Field)
          {
            FieldInfo fi = tp.GetField(Name);
#if !PocketPC && !SILVERLIGHT
            object result = Convert.ChangeType(value, fi.FieldType);
#else
            object result = Convert.ChangeType(value, fi.FieldType, System.Globalization.CultureInfo.InvariantCulture);
#endif
            fi.SetValue(Victim, result);
            return;
          }
          else
            if (mi.MemberType == MemberTypes.Property)
            {
              PropertyInfo pi = tp.GetProperty(Name);
              if (pi.CanWrite)
              {
#if !PocketPC && !SILVERLIGHT
                object result = Convert.ChangeType(value, pi.PropertyType);
#else
                object result = Convert.ChangeType(value, pi.PropertyType, System.Globalization.CultureInfo.InvariantCulture);
#endif
                pi.SetValue(Victim, result, index);
                return;
              }
              else
              {
                if (Behavior == DataMutantBehavior.ThrowException)
                  throw new Exception("Property " + Name + " is readonly");
                else
                  return;
              }
            }
        }
      }

      if (Behavior == DataMutantBehavior.ThrowException)
        throw new Exception("No such Field or Property found: " + Name);
      else
        if (Behavior == DataMutantBehavior.CreateMutantField)
        {
          if (this.MutanticFields.ContainsKey(Name))
            this.MutanticFields[Name] = value;
          else
            this.MutanticFields.Add(Name, value);
        }
    }
 
    /// <summary>
    /// Gets the value of the property or field
    /// </summary>
    /// <param name="Name"></param>
    /// <param name="index"></param>
    /// <returns></returns>
    public virtual object Get(string Name, object[] index)
    {
      Type tp = Victim.GetType();
      MemberInfo[] mis = tp.GetMembers();
      foreach (MemberInfo mi in mis)
      {
        if (mi.Name == Name)
        {
          if (mi.MemberType == MemberTypes.Field)
          {
            FieldInfo fi = tp.GetField(Name);
            return fi.GetValue(Victim);
          }
          else
            if (mi.MemberType == MemberTypes.Property)
            {
              PropertyInfo pi = tp.GetProperty(Name);
              return pi.GetValue(Victim, index);
            }
        }
      }

      if (Behavior == DataMutantBehavior.ThrowException)
        throw new Exception("No such Field or Property found: " + Name);
      else
        if (Behavior == DataMutantBehavior.CreateMutantField)
        {
          if (this.MutanticFields.ContainsKey(Name))
            return this.MutanticFields[Name];
          else
            return null;
        }
        else
          return null;
    }
    #endregion

    #region Methods
    public bool Has(string name, object value)
    {
        if (!HasName(name)) return false;
        return MutanticFields[name] == value;
    }

    public bool HasName(string name)
    {
        return MutanticFields.ContainsKey(name);
    }

    public bool ContainsName(string name)
    {
        return HasName(name);
    }

    public bool Contains(string name, object value)
    {
        return Has(name, value);
    }

    public void AddValue(string name, object value)
    {
        if (HasName(name))
            MutanticFields.Add(name, value);
        else
            MutanticFields[name] = value;
    }
    #endregion

    #region Mutant Specifics
    /// <summary>
    /// Returns victim
    /// </summary>
    /// <returns></returns>
    public virtual object Resolve()
    {
      return Victim;
    }

    /// <summary>
    /// Mutantic Assignment 
    /// assigns all Properties and Fields of Victim
    /// to the corresponding Properties and Fields of given object
    /// </summary>
    /// <param name="o"></param>
    public virtual void AssignTo(object o)
    {
      Type tp = o.GetType();
      MemberInfo[] mis = tp.GetMembers();

      foreach (MemberInfo mi in mis)
      {
        object val_unc = Get(mi.Name, null);
        object val = null;

        if (val_unc != null)
        {
          if (mi.MemberType == MemberTypes.Field)
          {
            FieldInfo fi = tp.GetField(mi.Name);

            if (val_unc is IConvertible)
            {
#if PocketPC || SILVERLIGHT
              val = Convert.ChangeType(val_unc, fi.FieldType, System.Globalization.CultureInfo.InvariantCulture);
#else
              val = Convert.ChangeType(val_unc, fi.FieldType);
#endif
            }
            else
              if (val_unc == null || val_unc.GetType().IsSubclassOf(fi.FieldType) || val_unc.GetType() == fi.FieldType)
              {
                val = val_unc;
              }

            if (val != null)
              fi.SetValue(o, val);
          }
          else
            if (mi.MemberType == MemberTypes.Property)
            {
              PropertyInfo pi = tp.GetProperty(mi.Name);

              if (val_unc is IConvertible)
              {
#if PocketPC || SILVERLIGHT
                val = Convert.ChangeType(val_unc, pi.PropertyType, System.Globalization.CultureInfo.InvariantCulture);
#else
                val = Convert.ChangeType(val_unc, pi.PropertyType);
#endif
              }
              else
                if (val_unc == null || val_unc.GetType().IsSubclassOf(pi.PropertyType) || val_unc.GetType() == pi.PropertyType)
                {
                  val = val_unc;
                }

              if (val != null && pi.CanWrite)
                pi.SetValue(o, val, null);
            }
        }
      }

    }

    /// <summary>
    /// Mutate Victim, assigns all mutantic fields to the target Victim
    /// </summary>
    /// <param name="Victim"></param>
    public virtual void Mutate(object Victim)
    {
      AssignTo(Victim);
      this.Victim = Victim;
    }

    /// <summary>
    /// Returns a collection of Mutantic Fields only
    /// </summary>
    /// <returns></returns>
    public virtual string[] GetMutantFields()
    {
      string[] rez = new string[MutanticFields.Keys.Count];
      MutanticFields.Keys.CopyTo(rez, 0);
      return rez;
    }

    /// <summary>
    /// Assigns all mutantic field of the given Mutantic object
    /// </summary>
    /// <param name="mt"></param>
    public virtual void CaptureFields(IMutant mt)
    {
      string[] flds = mt.GetMutantFields();
      foreach (string fld in flds)
      {
        Set(fld, mt.Get(fld, null), null);
      }
    }

    /// <summary>
    /// Convert mutant to string
    /// </summary>
    /// <returns></returns>
    public virtual string FriendlyString()
    {
      string rez = "[";
      int index = 0;
      foreach (string fld in MutanticFields.Keys)
      {
        object val = MutanticFields[fld];
        if (val is IMutant)
        {
          rez += " " + fld + "->" + ((IMutant)val).FriendlyString();
        }
        else
          rez += " " + fld + "->" + val.ToString();
        if (index < MutanticFields.Keys.Count - 1) rez += ",";
        index++;
      }
      rez += "]";

      return rez;
    }
    #endregion

    #region Object Overrides
    /// <summary>
    /// Check equality
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override bool Equals(object obj)
    {
      return Victim.Equals(obj);
    }

    /// <summary>
    /// returns Hash code
    /// </summary>
    /// <returns></returns>
    public override int GetHashCode()
    {
      return Victim.GetHashCode();
    }

    /// <summary>
    /// returns string representation of the victim object
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
      return FriendlyString();
    }
    #endregion

    #region IConvertible Members
    /// <summary>
    /// Return type code of the Victim or TypeCode.Object
    /// </summary>
    /// <returns></returns>
    public TypeCode GetTypeCode()
    {
      if (Victim is IConvertible)
        return ((IConvertible)Victim).GetTypeCode();
      else
        return TypeCode.Object;
    }

    /// <summary>
    /// Converts Victim to corresponding type
    /// </summary>
    /// <param name="provider"></param>
    /// <returns></returns>
    public bool ToBoolean(IFormatProvider provider)
    {
      if (Victim is IConvertible)
        return ((IConvertible)Victim).ToBoolean(provider);

      throw new Exception("Can't covert to type boolean");
    }

    /// <summary>
    /// Converts Victim to corresponding type
    /// </summary>
    /// <param name="provider"></param>
    /// <returns></returns>
    public byte ToByte(IFormatProvider provider)
    {
      if (Victim is IConvertible)
        return ((IConvertible)Victim).ToByte(provider);

      throw new Exception("Can't covert to type byte");
    }

    /// <summary>
    /// Converts Victim to corresponding type
    /// </summary>
    /// <param name="provider"></param>
    /// <returns></returns>
    public char ToChar(IFormatProvider provider)
    {
      if (Victim is IConvertible)
        return ((IConvertible)Victim).ToChar(provider);

      throw new Exception("Can't covert to type char");
    }

    /// <summary>
    /// Converts Victim to corresponding type
    /// </summary>
    /// <param name="provider"></param>
    /// <returns></returns>
    public DateTime ToDateTime(IFormatProvider provider)
    {
      if (Victim is IConvertible)
        return ((IConvertible)Victim).ToDateTime(provider);

      throw new Exception("Can't covert to type DateTime");
    }

    /// <summary>
    /// Converts Victim to corresponding type
    /// </summary>
    /// <param name="provider"></param>
    /// <returns></returns>
    public decimal ToDecimal(IFormatProvider provider)
    {
      if (Victim is IConvertible)
        return ((IConvertible)Victim).ToDecimal(provider);

      throw new Exception("Can't covert to type Decimal");
    }

    /// <summary>
    /// Converts Victim to corresponding type
    /// </summary>
    /// <param name="provider"></param>
    /// <returns></returns>
    public double ToDouble(IFormatProvider provider)
    {
      if (Victim is IConvertible)
        return ((IConvertible)Victim).ToDouble(provider);

      throw new Exception("Can't covert to type double");
    }

    /// <summary>
    /// Converts Victim to corresponding type
    /// </summary>
    /// <param name="provider"></param>
    /// <returns></returns>
    public short ToInt16(IFormatProvider provider)
    {
      if (Victim is IConvertible)
        return ((IConvertible)Victim).ToInt16(provider);

      throw new Exception("Can't covert to type int16");
    }

    /// <summary>
    /// Converts Victim to corresponding type
    /// </summary>
    /// <param name="provider"></param>
    /// <returns></returns>
    public int ToInt32(IFormatProvider provider)
    {
      if (Victim is IConvertible)
        return ((IConvertible)Victim).ToInt32(provider);

      throw new Exception("Can't covert to type int32");
    }

    /// <summary>
    /// Converts Victim to corresponding type
    /// </summary>
    /// <param name="provider"></param>
    /// <returns></returns>
    public long ToInt64(IFormatProvider provider)
    {
      if (Victim is IConvertible)
        return ((IConvertible)Victim).ToInt64(provider);

      throw new Exception("Can't covert to type Int64");
    }

    /// <summary>
    /// Converts Victim to corresponding type
    /// </summary>
    /// <param name="provider"></param>
    /// <returns></returns>
    public sbyte ToSByte(IFormatProvider provider)
    {
      if (Victim is IConvertible)
        return ((IConvertible)Victim).ToSByte(provider);

      throw new Exception("Can't covert to type sbyte");
    }

    /// <summary>
    /// Converts Victim to corresponding type
    /// </summary>
    /// <param name="provider"></param>
    /// <returns></returns>
    public float ToSingle(IFormatProvider provider)
    {
      if (Victim is IConvertible)
        return ((IConvertible)Victim).ToSingle(provider);

      throw new Exception("Can't covert to type Single");
    }

    /// <summary>
    /// Converts Victim to corresponding type
    /// </summary>
    /// <param name="provider"></param>
    /// <returns></returns>
    public string ToString(IFormatProvider provider)
    {
      if (Victim is IConvertible)
        return ((IConvertible)Victim).ToString(provider);

      throw new Exception("Can't covert to type string");
    }

    /// <summary>
    /// Converts Victim to corresponding type
    /// </summary>
    /// <param name="conversionType"></param>
    /// <param name="provider"></param>
    /// <returns></returns>
    public object ToType(Type conversionType, IFormatProvider provider)
    {
      if (conversionType == typeof(IMutant) ||
          conversionType == this.GetType())
      {
        return this;
      }

      if (Victim is IConvertible)
        return ((IConvertible)Victim).ToType(conversionType, provider);

      throw new Exception("Can't covert to type " + conversionType.Name);
    }

    /// <summary>
    /// Converts Victim to corresponding type
    /// </summary>
    /// <param name="provider"></param>
    /// <returns></returns>
    public ushort ToUInt16(IFormatProvider provider)
    {
      if (Victim is IConvertible)
        return ((IConvertible)Victim).ToUInt16(provider);

      throw new Exception("Can't covert to type UInt16");
    }

    /// <summary>
    /// Converts Victim to corresponding type
    /// </summary>
    /// <param name="provider"></param>
    /// <returns></returns>
    public uint ToUInt32(IFormatProvider provider)
    {
      if (Victim is IConvertible)
        return ((IConvertible)Victim).ToUInt32(provider);

      throw new Exception("Can't covert to type UInt32");
    }

    /// <summary>
    /// Converts Victim to corresponding type
    /// </summary>
    /// <param name="provider"></param>
    /// <returns></returns>
    public ulong ToUInt64(IFormatProvider provider)
    {
      if (Victim is IConvertible)
        return ((IConvertible)Victim).ToUInt64(provider);

      throw new Exception("Can't covert to type UInt64");
    }

    #endregion

    #region Convert Opertators
    /// <summary>
    /// Converts Victim to int
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    public static implicit operator int(DataMutant x)
    {
#if PocketPC || SILVERLIGHT
      return (int)Convert.ChangeType(x, typeof(int), System.Globalization.CultureInfo.InvariantCulture);     
#else
      return (int)Convert.ChangeType(x, typeof(int));
#endif
    }

    /// <summary>
    /// Converts Victim to float
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    public static implicit operator float(DataMutant x)
    {
#if PocketPC || SILVERLIGHT
      return (float)Convert.ChangeType(x, typeof(float), System.Globalization.CultureInfo.InvariantCulture);
#else
      return (float)Convert.ChangeType(x, typeof(float));
#endif
    }

    /// <summary>
    /// Converts Victim to double
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    public static implicit operator double(DataMutant x)
    {
#if PocketPC || SILVERLIGHT
      return (double)Convert.ChangeType(x, typeof(double), System.Globalization.CultureInfo.InvariantCulture);
#else
      return (double)Convert.ChangeType(x, typeof(double));
#endif
    }

    /// <summary>
    /// Converts Victim to string
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    public static implicit operator string(DataMutant x)
    {
#if PocketPC || SILVERLIGHT
      return (string)Convert.ChangeType(x, typeof(string), System.Globalization.CultureInfo.InvariantCulture);
#else
      return (string)Convert.ChangeType(x, typeof(string));
#endif

    }

    /// <summary>
    /// Converts Victim to object[]
    /// </summary>
    /// <param name="x"></param>
    /// <returns></returns>
    public static explicit operator object[](DataMutant x)
    {
      return (object[])x.Victim;
    }
    #endregion
  }
  #endregion
}
