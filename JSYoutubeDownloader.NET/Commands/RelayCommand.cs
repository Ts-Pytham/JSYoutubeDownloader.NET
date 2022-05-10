using System;
using System.Windows.Input;

namespace JSYoutubeDownloader.NET.Commands;

internal class RelayCommand : ICommand
{
    private readonly Action<object> _execute;

    private readonly Func<object, bool>? _canExecute;

    public event EventHandler? CanExecuteChanged
    {
        add { CommandManager.RequerySuggested += value; }
        remove { CommandManager.RequerySuggested -= value; }
    }

    public RelayCommand(Action<object> execute)
    {
        _execute = execute;
        _canExecute = null;
    }

    public RelayCommand(Action<object> execute, Func<object, bool> canExecute)
    {
        _execute = execute;
        _canExecute = canExecute;
    }

    
    public bool CanExecute(object? parameter)
    {
        if (parameter == null)
            return true;

        return _canExecute == null || CanExecute(parameter);
    }

    public void Execute(object? parameter)
    {
        _execute(parameter!);
    }
}
