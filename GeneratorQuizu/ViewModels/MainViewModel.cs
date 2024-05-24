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
            
        }

        public ObservableCollection<Quiz> Quizes
        {
            get { return quizes; }
            set {  quizes = value; onPropertyChanged(nameof(Quizes)); }
        }

        private Quiz selectedQuiz;
        public Quiz SelectedQuiz
        {
            get { return selectedQuiz; }
            set { selectedQuiz = value; onPropertyChanged(nameof(SelectedQuiz)); }
        }

        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; onPropertyChanged(nameof(Name)); }
        }

        private string questionContent;
        public string QuestionContent
        {
            get { return questionContent; }
            set { questionContent = value; onPropertyChanged( nameof(QuestionContent)); }
        }

        private string answer1Content;
        public string Answer1Content
        {
            get { return answer1Content; }
            set { answer1Content = value; onPropertyChanged(nameof(Answer1Content)); }
        }

        private string answer2Content;
        public string Answer2Content
        {
            get { return answer2Content; }
            set { answer2Content = value; onPropertyChanged(nameof(Answer2Content)); }
        }

        private string answer3Content;
        public string Answer3Content
        {
            get { return answer3Content; }
            set { answer3Content = value; onPropertyChanged(nameof(Answer3Content)); }
        }

        private string answer4Content;
        public string Answer4Content
        {
            get { return answer4Content; }
            set { answer4Content = value; onPropertyChanged(nameof(Answer4Content)); }
        }

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

        private void AddQuiz()
        {
            if (!string.IsNullOrWhiteSpace(Name))
            {
                var newQuiz = new Quiz { Name = Name, Questions = new List<Question>() };
                if (RepozytoriumQuiz.AddQuizToDb(newQuiz))
                {
                    Quizes.Add(newQuiz);
                    Name = string.Empty; 
                }
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
        #endregion

    }

}
