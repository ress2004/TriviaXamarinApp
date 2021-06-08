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
    class MainMenuViewModel:BaseViewModel
    {
        public MainMenuViewModel()
        {
            LogInCommand = new Command(LogIn);
            SignUpCommand = new Command(SignUp);
            PlayCommand = new Command(Play);
            LogOutCommand = new Command(LogOut);
            //AddQuestionCommand = new Command(AddQuestion);
        }
        private void LogIn()
        {
            Push?.Invoke(new LogIn());
        }  
        private void SignUp()
        {
            Push?.Invoke(new SignUp());
        }
        private void Play()
        {
            Push?.Invoke(new Play());
        }
        private void LogOut()
        {
         ((App)App.Current).CurrentUser=null;
            Push?.Invoke(new MainMenu());
        }
        //private void AddQuestion()
        //{
        //    Push?.Invoke(new YourQuestions());
        //}
        #region Commands
        public ICommand LogInCommand { get; set; } 
        public ICommand SignUpCommand { get; set; }
        public ICommand PlayCommand { get; set; } 
        public ICommand LogOutCommand { get; set; } 
        public ICommand AddQuestionCommand { get; set; }
        #endregion

        #region Events
        public event Action<Page> Push;
        #endregion
    }
}
