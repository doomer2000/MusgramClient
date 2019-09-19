using System;
using System.Collections.Generic;
using GalaSoft.MvvmLight;
using System.Windows.Forms;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Net.Sockets;
using GalaSoft.MvvmLight.Messaging;
using MusgramClient.Services;
using MusgramClient.Models;
using System.Net;
using Unity;
using MusgramClient.Views;
using System.Windows.Controls;
using System.Security;
using System.Runtime.InteropServices;
using System.Diagnostics;

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


        private string regEmail;

        public string RegEmail
        {
            get
            {
                return regEmail;
            }
            set
            {
                regEmail = value;
                RaisePropertyChanged();
            }
        }

        private string regMobileNum;

        public string RegMobileNum
        {
            get
            {
                return regMobileNum;
            }
            set
            {
                regMobileNum = value;
                RaisePropertyChanged();
            }
        }

        private bool isNoRequested;
        public bool IsNoRequested
        {
            get
            {
                return isNoRequested;
            }
            set
            {
                isNoRequested = value;
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
            isNoRequested = true;
            Cur_Page = Page.LogIn;
        }

        private ICommand registrationPageOpen;
        public ICommand RegistrationPageOpen => registrationPageOpen ?? (registrationPageOpen = new RelayCommand(() =>
        {
            Cur_Page = Page.Registration;
        }));

        private ICommand fPSendCode;
        public ICommand FPSendCode => fPSendCode ?? (fPSendCode = new RelayCommand(() =>
        {
        }));

        private ICommand fPEMOpen;
        public ICommand FPEMOpen => fPEMOpen ?? (fPEMOpen = new RelayCommand(() =>
        {
            Cur_Page = Page.ForgotPasswordEM;
        }));

        private ICommand fPECOpen;
        public ICommand FPECOpen => fPECOpen ?? (fPECOpen = new RelayCommand(() =>
        {
            Cur_Page = Page.ForgotPasswordEC;
        }));

        private ICommand fPCPOpen;
        public ICommand FPCPOpen => fPCPOpen ?? (fPCPOpen = new RelayCommand(() =>
        {
            Cur_Page = Page.ForgotPasswordCP;
        }));

        private ICommand logInPageOpen;
        public ICommand LogInPageOpen => logInPageOpen ?? (logInPageOpen = new RelayCommand(() =>
        {
            Cur_Page = Page.LogIn;
        }));

        private ICommand registration;
        public ICommand Registration => registration ?? (registration = new RelayCommand<PasswordBox>((obj) =>
        {
            RegPassword = obj.Password;
            Task.Run(() =>
            {
                Register();
            }).ContinueWith((a) =>
            {
                obj.SecurePassword.Dispose();
                RegPassword = "";
            });
            Cur_Page = Page.LogIn;
        }));
        private ICommand tryLogin;
        public ICommand TryLogin => tryLogin ?? (tryLogin = new RelayCommand<PasswordBox>((obj) =>
        {
            Password = obj.Password;
            User user = new User();
            IsNoRequested = false;
            Task.Run(() =>
            {
                user = connectionService.TryLogin(Login, Password);
            }).ContinueWith(new Action<Task>((a) =>
            {
                Password = "";
                obj.SecurePassword.Dispose();
                IsNoRequested = true;
                if (user.Id > 0)
                {
                    Properties.Settings.Default.Login = user.Login;
                    Properties.Settings.Default.Password = user.Password;
                    Messenger.Default.Send<string>("", "Open Chat");
                    Messenger.Default.Register<string>("", "ready", (b)=>
                    {
                        Messenger.Default.Send<User>(user, "User");
                    });
                }
            }));
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
                Mail = RegEmail,
                MobileNum = RegMobileNum,
                LastTimeOnline = DateTime.Now.ToString()
            };
            connectionService.Register(userToReg);
        }
    }
}
