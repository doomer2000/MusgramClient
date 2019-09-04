using System;
using System.Collections.Generic;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Net.Sockets;
using MusgramClient.Services;
using MusgramClient.Models;
using System.Net;

namespace MusgramClient.ViewModel
{
    public class AuthenticationVM : ViewModelBase
    {
        private Page cur_Page;
        public Page Cur_Page
        {
            get
            {
                return cur_Page;
            }
            set
            {
                cur_Page = value;
                RaisePropertyChanged();
            }
        }

        private string login;
        public string Login
        {
            get { return login; }
            set
            {
                login = value;
                RaisePropertyChanged();
            }
        }

        private string mailToSendCode;
        public string MailToSendCode
        {
            get
            {
                return mailToSendCode;
            }
            set
            {
                mailToSendCode = value;
                RaisePropertyChanged();
            }
        }

        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                RaisePropertyChanged();
            }
        }

        private string regLogin;
        public string RegLogin
        {
            get
            {
                return regLogin;
            }
            set
            {
                regLogin = value;
                RaisePropertyChanged();
            }
        }
        private string regPassword;
        public string RegPassword
        {
            get
            {
                return regPassword;
            }
            set
            {
                regPassword = value;
                RaisePropertyChanged();
            }
        }

        private IConnection connectionService;

        public AuthenticationVM(IConnection connection)
        {
            connectionService = connection;
            Cur_Page = Page.LogIn;
        }

        private ICommand registrationPageOpen;
        public ICommand RegistrationPageOpen => registrationPageOpen ?? (registrationPageOpen = new RelayCommand(() =>
        {
            Cur_Page = Page.Registration;
        }));

        private ICommand fPSendCode;
        public ICommand FPSendCode => fPSendCode ?? (fPSendCode = new RelayCommand(() => {
        }));

        private ICommand fPEMOpen;
        public ICommand FPEMOpen => fPEMOpen ?? (fPEMOpen = new RelayCommand(() => {
            Cur_Page = Page.ForgotPasswordEM;
        }));

        private ICommand fPECOpen;
        public ICommand FPECOpen => fPECOpen ?? (fPECOpen = new RelayCommand(() => {
            Cur_Page = Page.ForgotPasswordEC;
        }));

        private ICommand fPCPOpen;
        public ICommand FPCPOpen => fPCPOpen ?? (fPCPOpen = new RelayCommand(() => {
            Cur_Page = Page.ForgotPasswordCP;
        }));

        private ICommand logInPageOpen;
        public ICommand LogInPageOpen => logInPageOpen ?? (logInPageOpen = new RelayCommand(() =>
        {
            Cur_Page = Page.LogIn;
        }));

        private ICommand registration;
        public ICommand Registration => registration ?? (registration = new RelayCommand(() =>
        {
            Register();
            Cur_Page = Page.LogIn;
        }));

        public enum Page
        {
            LogIn,
            Registration,
            ForgotPasswordEM,
            ForgotPasswordEC,
            ForgotPasswordCP
        }

        private void Register()
        {
            User userToReg = new User()
            {
                Login = RegLogin,
                Password = RegPassword,
                LastTimeOnline = DateTime.Now
            };
            connectionService.Register(userToReg);
        }
    }
}
