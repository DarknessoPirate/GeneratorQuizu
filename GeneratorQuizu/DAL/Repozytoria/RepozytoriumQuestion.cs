﻿using GeneratorQuizu.DAL.Encje;
using System.Collections.ObjectModel;

namespace GeneratorQuizu.DAL.Repozytoria
{
    public static class RepozytoriumQuestion
    {
        public static bool AddQuestionToDb(Question question)
        {
            bool state = false;

            using (var db = new QuizDbContext())
            {
                if (question != null)
                {
                    db.Questions.Add(question);
                    state = true;
                    db.SaveChanges();
                }
            }
            return state;
        }

        public static bool DeleteQuestionFromDb(Question question)
        {
            bool state = false;

            using (var db = new QuizDbContext())
            {
                var questionToRemove = db.Questions.SingleOrDefault(q => q.Id == question.Id);
                if (questionToRemove != null)
                {
                    db.Questions.Remove(questionToRemove);
                    state = true;
                    db.SaveChanges();
                }
            }
            return state;
        }

        public static bool ModifyQuestionInDb(Question question, int questionIdToModify)
        {
            bool state = false;
            using (var db = new QuizDbContext())
            {
                var questionToModify = db.Questions.SingleOrDefault(q => q.Id == questionIdToModify);
                if (questionToModify != null)
                {
                    questionToModify.Content = question.Content;
                    questionToModify.Answer1 = question.Answer1;
                    questionToModify.Answer2 = question.Answer2;
                    questionToModify.Answer3 = question.Answer3;
                    questionToModify.Answer4 = question.Answer4;
                    questionToModify.CorrectAnswers = question.CorrectAnswers;
                    state = true;
                    db.SaveChanges();
                    
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
