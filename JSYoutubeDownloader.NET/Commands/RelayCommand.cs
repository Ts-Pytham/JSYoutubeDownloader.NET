namespace JSYoutubeDownloader.NET.Commands;

internal class RelayCommand(Action<object> execute, Func<object, bool>? canExecute = null) : ICommand
{
    private readonly Action<object> _execute = execute;

    private readonly Func<object, bool>? _canExecute = canExecute;

    public event EventHandler? CanExecuteChanged
    {
        add { CommandManager.RequerySuggested += value; }
        remove { CommandManager.RequerySuggested -= value; }
    }

    public bool CanExecute(object? parameter)
    {
        if (parameter is null)
            return true;

        return _canExecute is null || CanExecute(parameter);
    }

    public void Execute(object? parameter)
    {
        _execute(parameter!);
    }
}
