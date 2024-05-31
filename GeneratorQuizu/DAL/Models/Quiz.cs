using System.Collections.ObjectModel;

namespace GeneratorQuizu.DAL.Encje
{
    public class Quiz
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ObservableCollection<Question> Questions { get; set; } = new ObservableCollection<Question>();
    }
}
