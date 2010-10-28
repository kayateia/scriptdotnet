
using System.IO;
using System.Reflection;
namespace ScriptNET.Runtime.MathExtensions
{
    /// <summary>
    /// Run-time configuration manager for Script.net
    /// </summary>
    public static class MathRuntimeHost
    {
        public static void Initialize()
        {
          ScriptNET.Runtime.RuntimeHost.Initialize(DefaultMathConfig);
        }

        public static void CleanUp()
        {
          ScriptNET.Runtime.RuntimeHost.CleanUp();
        }

        #region Config
        public static Stream DefaultMathConfig
        {
            get
            {
              Stream configStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("ScriptNET.MathExtensions.RuntimeMathConfig.xml");
                configStream.Seek(0, SeekOrigin.Begin);
                return configStream;
            }
        }
        #endregion
    }
}
