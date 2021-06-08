
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TriviaXamarinApp.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TriviaXamarinApp.ViewModels;
using System.Windows.Input;


namespace TriviaXamarinApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainMenu : ContentPage
    {
        Button PlayButton;
        Button LogInButton;
        Button LogoutButton;
        Button YourQuestionsButton;
        Button SignUpButton;
        public MainMenu()
        {
            InitializeComponent();
            MainMenuViewModel mmvm = new MainMenuViewModel();
            BindingContext = mmvm;
            mmvm.Push += (p) => Navigation.PushAsync(p);

            UpdateMainMenu();

        }

        private void UpdateMainMenu()
        {
            if (((App)App.Current).CurrentUser != null)
            {
                PlayButton = new Button {Text="Play"};
                PlayButton.SetBinding(Button.CommandProperty, "PlayCommand");
                menuGrid.Children.Add(PlayButton,0,0);
              
                LogoutButton = new Button { Text = "LogOut" };
                LogoutButton.SetBinding(Button.CommandProperty, "LogOutCommand");
                menuGrid.Children.Add(LogoutButton, 0, 1);

                YourQuestionsButton = new Button { Text = "Your Questions" };
                YourQuestionsButton.SetBinding(Button.CommandProperty, "YourQuestionsCommand");
                menuGrid.Children.Add(YourQuestionsButton, 0, 2);


            }
            else
            {
                PlayButton = new Button { Text = "Play" };
                PlayButton.SetBinding(Button.CommandProperty, "PlayCommand");
                menuGrid.Children.Add(PlayButton, 0, 0);
                LogInButton = new Button { Text = "LogIn" };
                LogInButton.SetBinding(Button.CommandProperty, "LogInCommand");
                menuGrid.Children.Add(LogInButton, 0, 1);
                SignUpButton = new Button { Text = "SignUp" };
                SignUpButton.SetBinding(Button.CommandProperty, "SignUpCommand"); 
                menuGrid.Children.Add(SignUpButton, 0, 2);
            }
        }
    }
}