﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneratorQuizu.DAL.Encje
{
    public class Quiz
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ObservableCollection<Question> Questions { get; set; } = new ObservableCollection<Question>();
    }
}
