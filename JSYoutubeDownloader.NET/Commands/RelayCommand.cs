using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace JSYoutubeDownloader.NET.Commands
{
    internal class RelayCommand : ICommand
    {
        private readonly Action<object>? _execute;
        private readonly Predicate<object>? _canExecute;

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }


        public RelayCommand(Action<object> execute) : this(execute, null)
        {
        }

        public RelayCommand(Action<object> execute, Predicate<object>? canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }
        public bool CanExecute(object? parameter)
        {
            if (parameter == null)
                throw new ArgumentNullException(nameof(parameter));

            return _canExecute == null || _canExecute(parameter);
        }

       
        public void Execute(object? parameter)
        {
            if(parameter == null)
              throw new ArgumentNullException(nameof(parameter));
            if(_execute == null)
                return;

            _execute(parameter);
        }
    }
}
