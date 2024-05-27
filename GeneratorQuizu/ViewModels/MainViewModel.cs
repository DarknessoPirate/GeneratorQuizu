using GeneratorQuizu.DAL.Encje;
using GeneratorQuizu.DAL.Repozytoria;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Znajomi.ViewModel.BaseClass;

namespace GeneratorQuizu.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        
        private ObservableCollection<Quiz> quizes;
        
        
        public MainViewModel()
        {
            quizes = RepozytoriumQuiz.GetAllQuizesFromDb();
            LoadQuestions();
        }

        public bool IsAnswer1Checked { get; set; }
        public bool IsAnswer2Checked { get; set; }
        public bool IsAnswer3Checked { get; set; }
        public bool IsAnswer4Checked { get; set; }


        public ObservableCollection<Quiz> Quizes
        {
            get { return quizes; }
            set {  quizes = value; onPropertyChanged(nameof(Quizes)); }
        }


        #region properties
        private Quiz selectedQuiz;
        public Quiz SelectedQuiz
        {
            get { return selectedQuiz; }
            set { 
                selectedQuiz = value; 
                onPropertyChanged(nameof(SelectedQuiz));
            }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; onPropertyChanged(nameof(Name)); }
        }

        private Question selectedQuestion;
        public Question SelectedQuestion
        {
            get { return selectedQuestion; }
            set { selectedQuestion = value; onPropertyChanged(nameof(SelectedQuestion)); }
        }

        private string questionContent;
        public string QuestionContent
        {
            get { return questionContent; }
            set { questionContent = value; onPropertyChanged( nameof(QuestionContent)); }
        }

        private string answer1Content = string.Empty;
        public string Answer1Content
        {
            get { return answer1Content; }
            set { answer1Content = value; onPropertyChanged(nameof(Answer1Content)); }
        }

        private string answer2Content = string.Empty;
        public string Answer2Content
        {
            get { return answer2Content; }
            set { answer2Content = value; onPropertyChanged(nameof(Answer2Content)); }
        }

        private string answer3Content = string.Empty;
        public string Answer3Content
        {
            get { return answer3Content; }
            set { answer3Content = value; onPropertyChanged(nameof(Answer3Content)); }
        }

        private string answer4Content = string.Empty;
        public string Answer4Content
        {
            get { return answer4Content; }
            set { answer4Content = value; onPropertyChanged(nameof(Answer4Content)); }
        }
        #endregion
        #region Commands
        private ICommand addQuizCommand;
        public ICommand AddQuizCommand
        {
            get
            {
                if(addQuizCommand == null)
                    addQuizCommand = new RelayCommand(
                        arg => AddQuiz(),
                        arg => !string.IsNullOrEmpty(name)
                        );
                return addQuizCommand;
            }
        }

        private ICommand deleteQuizCommand;
        public ICommand DeleteQuizCommand
        {
            get
            {
                if (deleteQuizCommand == null)
                    deleteQuizCommand = new RelayCommand(
                        arg => DeleteQuiz(),
                        arg => SelectedQuiz != null
                    );
                return deleteQuizCommand;
            }
        }

       
        private ICommand addQuestionCommand;
        public ICommand AddQuestionCommand
        {
            get
            {
                if (addQuestionCommand == null)
                    addQuestionCommand = new RelayCommand(
                        arg => AddQuestion(),
                        arg => (SelectedQuiz != null && RepozytoriumQuiz.GetNumberOfQuestionsInDB(SelectedQuiz) < 4 &&
                                Answer1Content != string.Empty &&
                                Answer2Content != string.Empty &&
                                Answer3Content != string.Empty &&
                                Answer4Content != string.Empty &&
                                (IsAnswer1Checked || IsAnswer2Checked || IsAnswer3Checked || IsAnswer4Checked))
                        );
                return addQuestionCommand;
            }
        }

        private ICommand modifyQuestionCommand;
        public ICommand ModifyQuestionCommand
        {
            get
            {
                if (modifyQuestionCommand == null)
                    modifyQuestionCommand = new RelayCommand(
                        arg => ModifyQuestion(),
                        arg => (SelectedQuiz != null && SelectedQuestion != null &&
                                Answer1Content != string.Empty &&
                                Answer2Content != string.Empty &&
                                Answer3Content != string.Empty &&
                                Answer4Content != string.Empty &&
                                (IsAnswer1Checked || IsAnswer2Checked || IsAnswer3Checked || IsAnswer4Checked))
                        );
                return modifyQuestionCommand;
            }
        }

        private ICommand deleteQuestionCommand;
        public ICommand DeleteQuestionCommand
        {
            get
            {
                if (deleteQuestionCommand == null)
                    deleteQuestionCommand = new RelayCommand(
                        arg => DeleteQuestion(),
                        arg => (SelectedQuiz != null && SelectedQuestion != null)
                        );
                return deleteQuestionCommand;
            }
        }

        

        private ICommand playQuizCommand;
        public ICommand PlayQuizCommand
        {
            get
            {
                if (playQuizCommand == null)
                    playQuizCommand = new RelayCommand(
                        arg => PlayQuiz(),
                        arg => SelectedQuiz != null
                    );
                return playQuizCommand;
            }
        }



        private void AddQuestion()
        {
            if (!string.IsNullOrWhiteSpace(QuestionContent))
            {

                var correctAnswers = new List<int>();
                if (IsAnswer1Checked) correctAnswers.Add(1);
                if (IsAnswer2Checked) correctAnswers.Add(2);
                if (IsAnswer3Checked) correctAnswers.Add(3);
                if (IsAnswer4Checked) correctAnswers.Add(4);


                var newQuestion = new Question 
                { 
                    Content=QuestionContent, 
                    Answer1=Answer1Content, 
                    Answer2=Answer2Content,
                    Answer3=Answer3Content,
                    Answer4=Answer4Content, CorrectAnswers= string.Join(',',correctAnswers),
                    QuizId=SelectedQuiz.Id
                };


                if (RepozytoriumQuestion.AddQuestionToDb(newQuestion))
                {
                    
                    SelectedQuiz.Questions.Add(newQuestion);
                    QuestionContent = string.Empty;
                    Answer1Content = string.Empty;
                    Answer2Content = string.Empty;
                    Answer3Content = string.Empty;
                    Answer4Content = string.Empty;
                    ResetCheckBoxes();
                }
            }
        }

        private void ModifyQuestion()
        {
            if (!string.IsNullOrWhiteSpace(QuestionContent))
            {

                var correctAnswers = new List<int>();
                if (IsAnswer1Checked) correctAnswers.Add(1);
                if (IsAnswer2Checked) correctAnswers.Add(2);
                if (IsAnswer3Checked) correctAnswers.Add(3);
                if (IsAnswer4Checked) correctAnswers.Add(4);


                var newQuestion = new Question
                {
                    Content = QuestionContent,
                    Answer1 = Answer1Content,
                    Answer2 = Answer2Content,
                    Answer3 = Answer3Content,
                    Answer4 = Answer4Content,
                    CorrectAnswers = string.Join(',', correctAnswers),
                    QuizId = SelectedQuiz.Id
                };


                if (RepozytoriumQuestion.ModifyQuestionInDb(newQuestion, selectedQuestion.Id))
                {
                    int index = SelectedQuiz.Questions.IndexOf(selectedQuestion);

                    SelectedQuiz.Questions[index] = newQuestion;

                    QuestionContent = string.Empty;
                    Answer1Content = string.Empty;
                    Answer2Content = string.Empty;
                    Answer3Content = string.Empty;
                    Answer4Content = string.Empty;
                    ResetCheckBoxes();
                }
            }
        }

        private void DeleteQuestion()
        {
            if (SelectedQuestion != null)
            {
                if (RepozytoriumQuestion.DeleteQuestionFromDb(SelectedQuestion))
                {
                    SelectedQuiz.Questions.Remove(SelectedQuestion);
                    SelectedQuestion = null;
                }
            }
        }

        private void AddQuiz()
        {
            if (!string.IsNullOrWhiteSpace(Name))
            {
                var newQuiz = new Quiz { Name = Name, Questions = new ObservableCollection<Question>() };
                if (RepozytoriumQuiz.AddQuizToDb(newQuiz))
                {
                    Quizes.Add(newQuiz);
                    Name = string.Empty;
                }
            }
        }

        private void DeleteQuiz()
        {
            if (SelectedQuiz != null)
            {
                if (RepozytoriumQuiz.DeleteQuizFromDb(SelectedQuiz))
                {
                    Quizes.Remove(SelectedQuiz);
                    SelectedQuiz = null;
                }
            }
        }

        private void LoadQuestions()
        {
            foreach (var quiz in quizes)
            {
                quiz.Questions = RepozytoriumQuestion.GetQuestionsFromDb(quiz.Id);
            }
        }
        private void PlayQuiz()
        {
            var playQuizWindow = new PlayGameWindow(SelectedQuiz);
            playQuizWindow.Show();
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
        #endregion

    }

}
