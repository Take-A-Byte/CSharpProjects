namespace Messaging_Client.UnitTest
{
    using System;
    using System.Linq;
    using System.Reflection;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Messaging_Client.ViewModels;
    using Messaging_Client.Core;
    using System.ComponentModel;
    using Messaging_Client.PacketFactory;

    [TestClass]
    public class FrontEndUnitTests
    {
        #region Public Methods

        #region Design & Architecture

        [TestMethod]
        public void AllVMs_DerivedFromBaseVM()
        {
            #region Set

            Assembly vmAssembly = typeof(ViewModels.BaseViewModel).GetTypeInfo().Assembly;
            Type[] typelist = GetTypesInNamespace(vmAssembly, "Messaging_Client.ViewModels");

            #endregion Set

            #region Assert

            foreach (var type in typelist)
            {
                if (type != typeof(ViewModels.BaseViewModel))
                {
                    Assert.AreEqual(typeof(ViewModels.BaseViewModel), type.BaseType);
                }
            }

            #endregion Assert
        }

        #endregion Design & Architecture

        #region Send Message

        [TestMethod]
        public void MainWindow_MessageCount_SendEmptyMessage()
        {
            #region Set

            MainViewModel mainVM = new MainViewModel();

            #endregion Set

            #region Act

            mainVM.SendMessageCommand.Execute(null);

            #endregion Act

            #region Assert

            Assert.AreEqual(0, mainVM.Messages.Count);

            #endregion Assert
        }

        [TestMethod]
        public void MainWindow_MessagesCount_SendNonEmptyMessage()
        {
            #region Set

            string newMessageTxt = "send New message";
            MainViewModel mainVM = new MainViewModel();
            mainVM.NewMessageText = newMessageTxt;

            #endregion Set

            #region Act

            mainVM.SendMessageCommand.Execute(null);

            #endregion Act

            #region Assert

            Assert.AreEqual(1, mainVM.Messages.Count);

            #endregion Assert
        }

        [TestMethod]
        public void MainWindow_MessageText_SendNonEmptyMessage()
        {
            #region Set

            string newMessageTxt = "send New message";
            MainViewModel mainVM = new MainViewModel();
            mainVM.NewMessageText = newMessageTxt;

            #endregion Set

            #region Act

            mainVM.SendMessageCommand.Execute(null);

            #endregion Act

            #region Assert

            Assert.AreEqual(newMessageTxt, mainVM.Messages[0].MessageText);

            #endregion Assert
        }

        [TestMethod]
        public void MainWindow_MessageTime_SendNonEmptyMessage()
        {
            #region Set

            string newMessageTxt = "send New message";
            MainViewModel mainVM = new MainViewModel();
            mainVM.NewMessageText = newMessageTxt;

            #endregion Set

            #region Act

            mainVM.SendMessageCommand.Execute(null);

            #endregion Act

            #region Assert

            Assert.AreEqual(1, DateTime.Compare(DateTime.Now, mainVM.Messages[0].Time));

            #endregion Assert
        }

        #endregion Send Message

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