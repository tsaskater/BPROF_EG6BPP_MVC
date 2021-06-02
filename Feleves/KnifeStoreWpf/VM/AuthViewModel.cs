using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using KnifeStoreWpf.BL;
using KnifeStoreWpf.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace KnifeStoreWpf.VM
{
    class AuthViewModel:ViewModelBase
    {
        private string userName;
        private IAuthLogic authLogic;

        public AuthViewModel(IAuthLogic authLogic)
        {
            this.authLogic = authLogic;

            this.Login = new RelayCommand<PasswordBox>(x => {
                User u = new User { ValidationName = userName, Password = x.Password };
                string s = authLogic.Auth(u);
                if (s!=string.Empty)
                {
                    MainWindow win = new MainWindow(s,u);
                    win.Show();
                    Application.Current.MainWindow.Close();
                }
            }, true);
        }

        public AuthViewModel()
            : this(IsInDesignModeStatic ? null : ServiceLocator.Current.GetInstance<IAuthLogic>()) // IoC
        {
        }

        public string UserName
        {
            get { return this.userName; }
            set { this.Set(ref this.userName, value); }
        }
        public ICommand Login { get; private set; }

    }
}
