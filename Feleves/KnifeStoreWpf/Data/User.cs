﻿using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace KnifeStoreWpf.Data
{
    public class User:ObservableObject
    {
        private string validationName;

        public string ValidationName
        {
            get { return this.validationName; }
            set { this.Set(ref this.validationName, value); }
        }

        private PasswordBox password;

        public PasswordBox Password
        {
            get { return this.password; }
            set { this.Set(ref this.password, value); }
        }
    }
}
