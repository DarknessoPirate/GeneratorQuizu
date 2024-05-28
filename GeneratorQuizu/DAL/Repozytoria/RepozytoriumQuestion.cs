using GeneratorQuizu.DAL.Encje;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneratorQuizu.DAL.Repozytoria
{
    public static class RepozytoriumQuestion
    {
        public static async Task<bool> AddQuestionToDb(Question question)
        {
            bool state = false;

            using (var db = new QuizDbContext())
            {
                if (question != null)
                {
                    await db.Questions.AddAsync(question);
                    state = true;
                    await db.SaveChangesAsync();
                }
            }
            return state;
        }

        public static async Task<bool> DeleteQuestionFromDb(Question question)
        {
            bool state = false;

            using (var db = new QuizDbContext())
            {
                var questionToRemove = await db.Questions.SingleOrDefaultAsync(q => q.Id == question.Id);
                if (questionToRemove != null)
                {
                    db.Questions.Remove(questionToRemove);
                    state = true;
                    await db.SaveChangesAsync();
                }
            }
            return state;
        }

        public static async Task<bool> ModifyQuestionInDb(Question question, int questionIdToModify)
        {
            bool state = false;
            using (var db = new QuizDbContext())
            {
                var questionToModify = await db.Questions.SingleOrDefaultAsync(q => q.Id == questionIdToModify);
                if (questionToModify != null)
                {
                    questionToModify.Content = question.Content;
                    questionToModify.Answer1 = question.Answer1;
                    questionToModify.Answer2 = question.Answer2;
                    questionToModify.Answer3 = question.Answer3;
                    questionToModify.Answer4 = question.Answer4;
                    questionToModify.CorrectAnswers = question.CorrectAnswers;
                    state = true;
                    await db.SaveChangesAsync();
                }
            }
            return state;
        }

        public static ObservableCollection<Question> GetQuestionsFromDb(int id)
        {
            var qs = new ObservableCollection<Question>();
            using (var db = new QuizDbContext())
            {
                var questions = db.Questions.Where(x => x.QuizId == id).ToList();
                foreach (var question in questions)
                {
                    qs.Add(question);
                }
                return qs;
            }

        }

    }
}
