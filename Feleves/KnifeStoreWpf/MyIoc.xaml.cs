// <copyright file="MyIoc.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace KnifeStoreWpf
{
    using CommonServiceLocator;
    using GalaSoft.MvvmLight.Ioc;

    /// <summary>
    /// Interaction logic for App.xaml .
    /// </summary>
    internal class MyIoc : SimpleIoc, IServiceLocator
    {
        /// <summary>
        /// Gets creates a static Instence of an Ioc objact.
        /// </summary>
        public static MyIoc Instance { get; private set; } = new MyIoc();
    }
}