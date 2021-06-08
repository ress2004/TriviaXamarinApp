
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using TriviaXamarinApp.Services;
using TriviaXamarinApp.Models;
using System.Threading.Tasks;
using TriviaXamarinApp.Views;


namespace TriviaXamarinApp.ViewModels
{
    class LogInViewModel:BaseViewModel
    {
        public LogInViewModel()
        {
            Email = string.Empty;
            Error = string.Empty;
            Password = string.Empty;
            LogInCommand = new Command(Login);
        }

        private async void Login()
        {
            TriviaWebAPIProxy proxy = TriviaWebAPIProxy.CreateProxy();
            try
            {         
                User u = await proxy.LoginAsync(Email, Password);
                if (u.Email!=null)
                {
                    ((App)App.Current).CurrentUser = u;
                    Push?.Invoke(new MainMenu());
                }
                else
                {
                    Error = "Oops, Something went wrong...";
                }
            }
            catch (Exception)
            {
                Error = "Oops, Something went wrong...";
               
            }
        }

        #region Properties
        private string email;
        public string Email
        {
            get => email;
            set
            {
                if (value!=email)
                {
                    email = value;
                    OnPropertyChanged();
                }
            }
        }
        private string password;
        public string Password
        {
            get => password;
            set
            {
                if (value != password)
                {
                    password = value;
                    OnPropertyChanged();
                }
            }
        }
        private string error;
        public string Error
        {
            get => error;
            set
            {
                if (value != error)
                {
                    error = value;
                    OnPropertyChanged();
                }
            }
        }

        #endregion

        #region Commands
        public ICommand LogInCommand { get; set; }


        #endregion

        #region Events
        public event Action<Page> Push;
        #endregion

    }
}
