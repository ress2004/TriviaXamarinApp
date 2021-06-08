using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TriviaXamarinApp.Views;
using System.Windows.Input;
using Xamarin.Forms;
using TriviaXamarinApp.Models;
using TriviaXamarinApp.Services;
using System.Collections.ObjectModel;

namespace TriviaXamarinApp.ViewModels
{
    class PlayViewModel : BaseViewModel
    {

        public PlayViewModel()
        {
            this.CanAnswer = true;
            this.CanGetNext = false;
            this.CanAdd = false;
            this.CorrectAnswers = 0;
            this.Error = string.Empty;
            this.OpacityLevel = 0.1;
            NextQuestionCommand = new Command(NextQuestion);
            //AddYourQuestionCommand = new Command(AddYourQuestion);
            Answers = new ObservableCollection<AnswerViewModel>();
            GetQuestion();
        }

        public void IsCorrect(int index)
        {
            if (CanAnswer)
            {
                if (indexOfCorrect == index)
                {
                    CorrectAnswers++;
                    this.Answers[index].Color = Color.Green;

                    if (CorrectAnswers%3==0)
                    {
                        CanAdd = true;
                        OpacityLevel = 1;
                    }
                }
                else
                {
                    CorrectAnswers = 0;
                    this.Answers[index].Color = Color.Red;
                    this.Answers[indexOfCorrect].Color = Color.Green;
                }
                this.CanGetNext = true;
                this.CanAnswer = false;
            }
        }

        public void NextQuestion()
        {
            if (CanGetNext)
            {
                this.CanGetNext = false;
                for (int i = 0; i < NUM_ANSWERS; i++)
                {
                    Answers[i].Color = Color.White;
                    Answers[i].Answer = string.Empty;
                }
                GetQuestion();
                this.CanAnswer = true;
                this.CanAdd = false;
                this.OpacityLevel = 0.1;
            }
        }
        private async void GetQuestion()
        {
            TriviaWebAPIProxy proxy = TriviaWebAPIProxy.CreateProxy();
            try
            {
                AmericanQuestion q = await proxy.GetRandomQuestion();
                if (q != null)
                {
                    this.CorrectAnswer = new AnswerViewModel()
                    {
                        Answer = q.CorrectAnswer,
                        Color = Color.White,
                        AnswerCommand = new Command<int>(IsCorrect)
                    };
                    this.QText = q.QText;
                    ShuffleAnswers(q);
                }
                else
                {
                    Error = "Oops...Something Went Wrong...";
                }
            }
            catch (Exception)
            {
                Error = "Oops...Something Went Wrong...";
            }
        }
        private void ShuffleAnswers(AmericanQuestion q)
        {
            Random rnd = new Random();
            int counter = 0;
            this.indexOfCorrect = rnd.Next(0, NUM_ANSWERS);
            Answers[indexOfCorrect] = this.CorrectAnswer;
            this.CorrectAnswer.Id = indexOfCorrect;
            for (int i = 0; i < NUM_ANSWERS; i++)
            {
                if (i != indexOfCorrect)
                {
                    Answers[i] = new AnswerViewModel()
                    {
                        Answer = q.OtherAnswers[counter],
                        Color = Color.White,
                        AnswerCommand = new Command<int>(IsCorrect),
                        Id = i
                    };
                    counter++;
                }
            }
        }
        //private void AddYourQuestion()
        //{
        //    if (CanAdd)
        //    {
        //        Push?.Invoke(new AddQuestionPage());

        //        this.CanAdd = false;
        //        this.CorrectAnswers = 0;
        //        this.OpacityLevel = 0.1;
        //    }
        //}

        #region Properties
        private double opacityLevel;
        public double OpacityLevel
        {
            get => opacityLevel;
            set
            {
                if (value != opacityLevel)
                {
                    opacityLevel = value;
                    OnPropertyChanged();
                }
            }
        }
        private int indexOfCorrect;
        private bool canAnswer;
        public bool CanAnswer
        {
            get => canAnswer;
            set
            {
                if (value != canAnswer)
                {
                    canAnswer = value;
                    OnPropertyChanged();
                }
            }
        }
        private bool canAdd;

        public bool CanAdd
        {
            get => canAdd;
            set
            {
                if (value != canAdd)
                {
                    canAdd = value;
                    OnPropertyChanged();
                }
            }
        }
        private bool canGetNext;

        public bool CanGetNext
        {
            get => canGetNext;
            set
            {
                if (value != canGetNext)
                {
                    canGetNext = value;
                    OnPropertyChanged();
                }
            }
        }
        private const int NUM_ANSWERS = 4;

        public ObservableCollection<AnswerViewModel> Answers { get; set; }

        private int correctAnswers;

        public int CorrectAnswers
        {
            get => correctAnswers;
            set
            {
                if (value != correctAnswers)
                {
                    correctAnswers = value;
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

        private string qText;

        public string QText
        {
            get => qText;
            set
            {
                if (value != qText)
                {
                    qText = value;
                    OnPropertyChanged();
                }
            }
        }

        private AnswerViewModel correctAnswer;

        public AnswerViewModel CorrectAnswer
        {
            get => correctAnswer;
            set
            {
                if (value != correctAnswer)
                {
                    correctAnswer = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        #region Commands

        public ICommand IsCorrectCommand { get; set; }
        public ICommand NextQuestionCommand { get; set; }
        public ICommand AddYourQuestionCommand { get; set; }

        #endregion
        #region Events

        public event Action<Page> Push;

        #endregion
    }
}
