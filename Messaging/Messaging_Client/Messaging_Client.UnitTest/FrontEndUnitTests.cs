namespace Messaging_Client.UnitTest
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Linq;
    using System.Reflection;

    [TestClass]
    public class FrontEndUnitTests
    {
        #region Public Methods

        [TestMethod]
        public void AllVMs_DerivedFromBaseVM()
        {
            Assembly vmAssembly = typeof(ViewModels.BaseViewModel).GetTypeInfo().Assembly;
            Type[] typelist = GetTypesInNamespace(vmAssembly, "Messaging_Client.ViewModels");

            foreach (var type in typelist)
            {
                if (type != typeof(ViewModels.BaseViewModel))
                {
                    Assert.AreEqual(typeof(ViewModels.BaseViewModel), type.BaseType);
                }
            }
        }

        #endregion Public Methods

        #region Private Methods

        private Type[] GetTypesInNamespace(Assembly assembly, string nameSpace)
        {
            return
              assembly.GetTypes()
                      .Where(t => String.Equals(t.Namespace, nameSpace, StringComparison.Ordinal))
                      .ToArray();
        }

        #endregion Private Methods
    }
}