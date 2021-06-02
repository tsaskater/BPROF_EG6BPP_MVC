using GalaSoft.MvvmLight.Messaging;
using KnifeStoreWpf.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace KnifeStoreWpf
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {

        private AuthViewModel vM;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public Login()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Window win = GetWindow(this);
            win.KeyDown += Win_KeyDown;
            this.vM = this.FindResource("VM") as AuthViewModel;
            Messenger.Default.Register<string>(this, "AuthResult", msg =>
            {
                MessageBox.Show(msg);
            });
        }

        private void Win_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Enter))
            {
                vM.Login.Execute(txtPassword);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Messenger.Default.Unregister(this);
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
                if (this.DataContext != null)
                { ((dynamic)this.DataContext).Password = ((PasswordBox)sender).SecurePassword; }
        }
    }
}
