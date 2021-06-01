﻿// <copyright file="MainWindow.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace KnifeStoreWpf
{
    using System.Windows;
    using GalaSoft.MvvmLight.Messaging;
    using KnifeStoreWpf.VM;

    /// <summary>
    /// Interaction logic for MainWindow.xaml .
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel vM;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            this.InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.vM = this.FindResource("VM") as MainViewModel;
            Messenger.Default.Register<string>(this, "LogicResult", msg =>
            {
                MessageBox.Show(msg);
            });
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Messenger.Default.Unregister(this);
        }
    }
}