// <copyright file="App.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace KnifeStoreWpf
{
    using System.Windows;
    using CommonServiceLocator;
    using GalaSoft.MvvmLight.Messaging;
    using KnifeStoreWpf.BL;
    using KnifeStoreWpf.UI;
    using Logic;
    using Models;
    using Repository;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="App"/> class.
        /// </summary>
        public App()
        {
            ServiceLocator.SetLocatorProvider(() => MyIoc.Instance);

            MyIoc.Instance.Register<IEditorService, EditorServiceViaWindow>();
            MyIoc.Instance.Register<IMessenger>(() => Messenger.Default);
            MyIoc.Instance.Register<IKnifeStoreLogic, KnifeStoreLogic>();
            MyIoc.Instance.Register<IKnifeLogic, KnifeLogic>();
        }
    }
}