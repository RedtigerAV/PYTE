using System.Windows.Input;

namespace Pyte
{
    /// <summary>
    /// Расширение <see cref="ICommand"/>, позволяющее вызывать вручную <see cref="ICommand.CanExecuteChanged"/>
    /// </summary>
    internal interface IChangedCommand : ICommand
    {
        void RaiseCanExecuteChanged();
    }
}
