﻿using System.Windows.Input;

namespace Znajomi.ViewModel.BaseClass
{
    class RelayCommand : ICommand
    {
       

        readonly Action<object> _execute;
        readonly Predicate<object> _canExecute;


        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException(nameof(execute));
            else
                _execute = execute;
            _canExecute = canExecute;
        }
       
        public bool CanExecute(object? parameter)
        {
            return _canExecute == null ? true : _canExecute(parameter);
        }

        //zdarzenie informujące o możliwości wykonania polecenie
        public event EventHandler? CanExecuteChanged
        {
            add
            {
                if (_canExecute != null) CommandManager.RequerySuggested += value;
            }
            remove
            {
                if (_canExecute != null) CommandManager.RequerySuggested -= value;
            }
        }

        public void Execute(object? parameter)
        {
            _execute(parameter);
        }
        
    }
}
