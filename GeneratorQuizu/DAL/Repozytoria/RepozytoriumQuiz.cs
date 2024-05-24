﻿using GeneratorQuizu.DAL.Encje;
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
        public static bool AddQuizToDb(Quiz quiz)
        {
            bool state = false;

            using (var db = new QuizDbContext())
            {
                if (quiz != null)
                {
                    db.Quizes.Add(quiz);
                    state = true;
                    db.SaveChanges();
                }
            }
            return state;
        }

        public static bool DeleteQuizFromDb(Quiz quiz)
        {
            bool state = false;

            using (var db = new QuizDbContext())
            {
                var quizToRemove = db.Quizes.SingleOrDefault(q => q.Id == quiz.Id);
                if (quizToRemove != null)
                {
                    db.Quizes.Remove(quizToRemove);
                    state = true;
                    db.SaveChanges();
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
    }
}
