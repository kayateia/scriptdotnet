using System.Collections.Generic;

//(c) Petro Protsyk
//Created: 27 October 2007, around 18 o'clock
namespace ScriptNET.Runtime.MutantFramework
{
  #region Mutator
  /// <summary>
  /// This auxilary class is used to Mutate different objects
  /// </summary>
  public sealed class Mutator
  {
    private Mutator() { }

    #region Mutant Array
    /// <summary>
    /// Convert each object in the array to DataMutant 
    /// </summary>
    /// <param name="ar"></param>
    /// <returns></returns>
    public static DataMutant[] MutateArray(object[] ar)
    {
      List<DataMutant> rez = new List<DataMutant>();
      foreach (object o in ar)
        rez.Add(new DataMutant(o));

      return rez.ToArray();
    }

    /// <summary>
    /// Resolve array of Mutants
    /// </summary>
    /// <param name="dms"></param>
    /// <returns></returns>
    public static object[] ResolveArray(DataMutant[] dms)
    {
      List<object> rez = new List<object>();
      foreach (DataMutant dm in dms)
        rez.Add(dm.Resolve());

      return rez.ToArray();
    }
    #endregion
  }
  #endregion

  #region Enums
  /// <summary>
  /// Describes Mutant run-time behavior
  /// </summary>
  public enum DataMutantBehavior
  {
    /// <summary>
    /// Throw exception
    /// </summary>
    ThrowException,
    /// <summary>
    /// Do nothing
    /// </summary>
    Nothing,
    /// <summary>
    /// Create new field if Victim is null or there is no field with specified Name
    /// </summary>
    CreateMutantField
  }
  #endregion
}
