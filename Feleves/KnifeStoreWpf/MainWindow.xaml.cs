// <copyright file="MainWindow.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace KnifeStoreWpf
{
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Messaging;
    using KnifeStoreWpf.Data;
    using KnifeStoreWpf.VM;

    /// <summary>
    /// Interaction logic for MainWindow.xaml .
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel vM;
        private string token;
        private SimpUser user;
        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow(string token,SimpUser u)
        {
            if (DesignerProperties.GetIsInDesignMode(this))
            {
                this.Resources.Add("username", "CurrentUser");
            }
            else
            {
                this.token = token;
                this.Resources.Add("token", token);
                this.user = u;
                this.Resources.Add("username", user.UserName);
            }
            this.InitializeComponent();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.vM = this.FindResource("VM") as MainViewModel;
            this.vM.UpdateToken.Execute(this);
            Messenger.Default.Register<string>(this, "LogicResult", msg =>
            {
                MessageBox.Show(msg);
            });
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Messenger.Default.Unregister(this);
        }

        private void Logout(object sender, RoutedEventArgs e)
        {
            this.Resources.Clear();
            Login win = new Login();
            win.Show();
            this.Close();
        }
    }
}