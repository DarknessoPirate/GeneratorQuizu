using GeneratorQuizu.DAL.Encje;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Znajomi.ViewModel.BaseClass;

namespace GeneratorQuizu.ViewModels
{
    public class PlayGameViewModel : ViewModelBase
    {
        private ObservableCollection<Question> questions;
        private int currentQuestionIndex;
        

        public PlayGameViewModel(Quiz quiz)
        {
            Questions = quiz.Questions;
            CurrentQuestion = Questions[0];
            currentQuestionIndex = 0;
            score = 0;
            buttonContent = "Next";
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
            MessageBox.Show($"Curent score: {score}");
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
