using GeneratorQuizu.DAL.Encje;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using Znajomi.ViewModel.BaseClass;

namespace GeneratorQuizu.ViewModels
{
    public class PlayGameViewModel : ViewModelBase
    {
        private ObservableCollection<Question> questions;
        private int currentQuestionIndex;
        private Stopwatch stopwatch;
        private DispatcherTimer timer;

        public PlayGameViewModel(Quiz quiz)
        {
            Questions = quiz.Questions;
            CurrentQuestion = Questions[0];
            currentQuestionIndex = 0;
            score = 0;
            buttonContent = currentQuestionIndex == Questions.Count - 1 ? "Finish Quiz" : "Next";
            stopwatch = new Stopwatch();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            QuizTime = "00:00:00";
        }

        private string quizTime;
        public string QuizTime
        {
            get { return quizTime; }
            set {quizTime = value; onPropertyChanged(nameof(QuizTime)); }
        }

        public bool IsAnswer1Checked { get; set; }
        public bool IsAnswer2Checked { get; set; }
        public bool IsAnswer3Checked { get; set; }
        public bool IsAnswer4Checked { get; set; }

        private string buttonContent;
        public string ButtonContent
        {
            get { return buttonContent; }
            set { buttonContent = value; onPropertyChanged(nameof(ButtonContent)); }
        }

        public int score;
        public int Score
        {
            get { return score; }
            private set { score = value; onPropertyChanged(nameof(Score)); }
        }

        public ObservableCollection<Question> Questions
        {
            get { return questions; }
            set { questions = value; onPropertyChanged(nameof(Questions)); }
        }

        private Question currentQuestion;
        public Question CurrentQuestion
        {
            get { return currentQuestion; }
            set { currentQuestion = value; onPropertyChanged(nameof(CurrentQuestion)); }
        }

        private ICommand startTimeCommand;
        public ICommand StartTimeCommand
        {
            get
            {
                if (startTimeCommand == null)
                    startTimeCommand = new RelayCommand(
                        arg => StartTime(),
                        arg => true
                        );
                return startTimeCommand;
            }
        }

        private ICommand stopTimeCommand;
        public ICommand StopTimeCommand
        {
            get
            {
                if (stopTimeCommand == null)
                    stopTimeCommand = new RelayCommand(
                        arg => EndTime(),
                        arg => true
                        );
                return stopTimeCommand;
            }
        }

        private ICommand nextQuestionCommand;
        public ICommand NextQuestionCommand
        {
            get
            {
                if (nextQuestionCommand == null)
                    nextQuestionCommand = new RelayCommand(
                        arg => NextQuestion(),
                        arg => true
                    );
                return nextQuestionCommand;
            }
        }


        public event EventHandler RequestClose;

        protected void OnRequestClose()
        {
            RequestClose?.Invoke(this, EventArgs.Empty);
        }

        private void StartTime()
        {
            stopwatch.Start();
            timer.Start();
        }

        private void EndTime()
        {
            stopwatch.Stop();
            timer.Stop();
            QuizTime = stopwatch.Elapsed.ToString(@"hh\:mm\:ss");

        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            QuizTime = stopwatch.Elapsed.ToString(@"hh\:mm\:ss");
        }

        private void NextQuestion()
        {
            if (CanMoveToNextQuestion())
            {
                CheckAnswersAndUpdateScore();
                CurrentQuestion = Questions[++currentQuestionIndex];
                ResetCheckBoxes();

                if(currentQuestionIndex == Questions.Count - 1)
                {
                    ButtonContent = "Finish Quiz";
                }
            }
            else
            {
                CheckAnswersAndUpdateScore();
                OnRequestClose();
            }
        }


        private bool CanMoveToNextQuestion()
        {
            return currentQuestionIndex < Questions.Count - 1;
        }

        public void CheckAnswersAndUpdateScore()
        {
            var correctAnswers = CurrentQuestion.GetCorrectAnswerIndexes();
            var userAnswers = new List<int>();
            if (IsAnswer1Checked) userAnswers.Add(1);
            if (IsAnswer2Checked) userAnswers.Add(2);
            if (IsAnswer3Checked) userAnswers.Add(3);
            if (IsAnswer4Checked) userAnswers.Add(4);

            var intersection = userAnswers.Intersect(correctAnswers).Count();
            score += intersection;
           
            onPropertyChanged(nameof(Score));
            if (currentQuestionIndex != Questions.Count - 1)
            {
                MessageBox.Show($"Curent score: {score}");
            }
            else
            {
                MessageBox.Show($"Final score: {score}\nElapsed time: {QuizTime}");
            }
            
        }

        private void ResetCheckBoxes()
        {
            IsAnswer1Checked = false;
            IsAnswer2Checked = false;
            IsAnswer3Checked = false;
            IsAnswer4Checked = false;
            onPropertyChanged(nameof(IsAnswer1Checked));
            onPropertyChanged(nameof(IsAnswer2Checked));
            onPropertyChanged(nameof(IsAnswer3Checked));
            onPropertyChanged(nameof(IsAnswer4Checked));
        }

        
    }
}
