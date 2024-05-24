﻿using GeneratorQuizu.DAL.Encje;
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

        public static ObservableCollection<Question> GetQuestionsFromDb(int id)
        {
            var qs = new ObservableCollection<Question>();
            using (var db = new QuizDbContext())
            {
                var questions =  db.Questions.Where(x => x.QuizId == id).ToList();
                foreach (var question in questions)
                {
                    qs.Add(question);
                }
                return qs;
            }

        }

    }
}
