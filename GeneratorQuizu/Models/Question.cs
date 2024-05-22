using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneratorQuizu.Models
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
        public string CorrectAnswers {  get; set; }
        
    }
}
