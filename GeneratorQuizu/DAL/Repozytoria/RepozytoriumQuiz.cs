using GeneratorQuizu.DAL.Encje;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneratorQuizu.DAL.Repozytoria
{
    public static class RepozytoriumQuiz
    {
        public static async Task<bool> AddQuizToDb(Quiz quiz)
        {
            bool state = false;

            using (var db = new QuizDbContext())
            {
                if (quiz != null)
                {
                    await db.Quizes.AddAsync(quiz);
                    state = true;
                    await db.SaveChangesAsync();
                }
            }
            return state;
        }

        public static async Task<bool> DeleteQuizFromDb(Quiz quiz)
        {
            bool state = false;

            using (var db = new QuizDbContext())
            {
                var quizToRemove = await db.Quizes.SingleOrDefaultAsync(q => q.Id == quiz.Id);
                if (quizToRemove != null)
                {
                    db.Quizes.Remove(quizToRemove);
                    state = true;
                    await db.SaveChangesAsync();
                }
            }
            return state;
        }


        public static ObservableCollection<Quiz> GetAllQuizesFromDb()
        {
            var list = new ObservableCollection<Quiz>();
            using (var db = new QuizDbContext())
            {
                var quizes = db.Quizes.ToList();
                foreach (var q in quizes)
                {
                    list.Add(q);
                }
            }
            return list;
        }
        public static int GetNumberOfQuestionsInDB(Quiz quiz)
        {
            return quiz.Questions.Count;
        }
    }
}
