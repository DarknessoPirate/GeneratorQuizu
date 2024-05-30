using GeneratorQuizu.DAL.Encje;
using GeneratorQuizu.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GeneratorQuizu
{
    /// <summary>
    /// Interaction logic for PlayGameWindow.xaml
    /// </summary>
    public partial class PlayGameWindow : Window
    {
        public PlayGameWindow(Quiz selectedQuiz)
        {
            InitializeComponent();
            var viewmodel = new PlayGameViewModel(selectedQuiz);
            DataContext = viewmodel;
            viewmodel.RequestClose += (s, e) => Close();

        }
    }
}
