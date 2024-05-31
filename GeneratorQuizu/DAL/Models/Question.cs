namespace GeneratorQuizu.DAL.Encje
{
    public class Question
    {
        public int Id { get; set; }
        public Quiz Quiz { get; set; }
        public int QuizId { get; set; }
        public string Content { get; set; }
        public string Answer1 { get; set; }
        public string Answer2 { get; set; }
        public string Answer3 { get; set; }
        public string Answer4 { get; set; }
        public string CorrectAnswers { get; set; }


        public List<int> GetCorrectAnswerIndexes()
        {
            // return the correct string of answers

            // use the string to create a list of correct answers as list of integers
            if (string.IsNullOrEmpty(CorrectAnswers)) return new List<int>();
            var indexes = CorrectAnswers.Split(',');
            var result = new List<int>();
            foreach (var index in indexes)
            {
                if (int.TryParse(index, out int i))
                {
                    result.Add(i);
                }
            }
            return result;
        }


    }
}
