using System.Runtime.InteropServices;

namespace ScriptNET.Runtime.MutantFramework
{
  /// <summary>
  /// Base interface for Mutantic Objects
  /// </summary>
  [ComVisible(true)]
  public interface IMutant
  {
    #region Victim
    /// <summary>
    /// Get The Victim
    /// </summary>
    /// <returns></returns>
    object Resolve();

    /// <summary>
    /// Capture object, Assigns Mutant Fields to Victim
    /// </summary>
    /// <param name="Victim"></param>
    void Mutate(object Victim);
    #endregion

    #region Manipulations
    /// <summary>
    /// Invoke Victim Method
    /// </summary>
    /// <param name="FuncName"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    object Invoke(string FuncName, object[] param);

    /// <summary>
    /// Invoke Victim Method
    /// </summary>
    /// <param name="FuncName"></param>
    /// <param name="param"></param>
    /// <returns></returns>
    object Invoke(string[] FuncName, object[] param);

    /// <summary>
    /// Set the value of the field
    /// </summary>
    /// <param name="Name"></param>
    /// <param name="value"></param>
    /// <param name="index"></param>
    void Set(string Name, object value, object[] index);

    /// <summary>
    /// Get the value of the field
    /// </summary>
    /// <param name="Name"></param>
    /// <param name="index"></param>
    /// <returns></returns>
    object Get(string Name, object[] index);

    /// <summary>
    /// Gets all Mutant fields 
    /// </summary>
    /// <returns></returns>
    string[] GetMutantFields();
    #endregion

    #region Operations
    /// <summary>
    /// Assigns Mutant Object to object o
    /// </summary>
    /// <param name="o">Object to be assigned</param>
    void AssignTo(object o);

    /// <summary>
    /// Capture Fields of Mutant
    /// </summary>
    /// <param name="mt"></param>
    void CaptureFields(IMutant mt);
    #endregion

    #region Accesibility
    /// <summary>
    /// Returns string representation of mutant. e.g [ f->v, ... ]
    /// </summary>
    /// <returns></returns>
    string FriendlyString();
    #endregion
  }
}
