using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TriviaXamarinApp.Views;
using System.Windows.Input;
using Xamarin.Forms;
using TriviaXamarinApp.Models;
using TriviaXamarinApp.Services;
namespace TriviaXamarinApp.ViewModels
{
    class SignUpViewModel:BaseViewModel
    {
        public SignUpViewModel()
        {
            Email = string.Empty;
            Nickname = string.Empty;
            Error = string.Empty;
            Password = string.Empty;
            SignUpCommand = new Command(SignUp);
        }

        private async void SignUp()
        {
            try
            {
                TriviaWebAPIProxy proxy = TriviaWebAPIProxy.CreateProxy();
                User u = new User
                {
                    Email = this.Email,
                    NickName = this.Nickname,
                    Password = this.Password,
                    Questions = new List<AmericanQuestion>()
               
                };
                bool b = await proxy.RegisterUser(u);
                if (b)
                {
                    Error = "Sign Up completed, now log in in order to play";
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
                if (value != email)
                {
                    email = value;
                    OnPropertyChanged();
                }
            }
        }
        private string nickname;
        public string Nickname
        {
            get => nickname;
            set
            {
                if (value != nickname)
                {
                    nickname= value;
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
        public ICommand SignUpCommand { get; set; }
        #endregion
     

        #region Events
        public event Action<Page> Push;
        #endregion
    }
}
