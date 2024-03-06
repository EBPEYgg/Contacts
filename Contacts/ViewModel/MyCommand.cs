using System;
using System.Windows.Input;

namespace Contacts.ViewModel
{
    /// <summary>
    /// Класс, описывающий команду для загрузку данных.
    /// </summary>
    public class MyCommand : ICommand
    {
        /// <summary>
        /// Действие, которое должно быть выполнено при выполнении команды.
        /// </summary>
        private readonly Action<object> _executeAction;

        /// <summary>
        /// Создает экземпляр класса <see cref="MyCommand"/> 
        /// с указанным действием для выполнения.
        /// </summary>
        /// <param name="executeAction">Действие, которое должно быть 
        /// выполнено при выполнении команды.</param>
        public MyCommand(Action<object> executeAction)
        {
            _executeAction = executeAction;
        }

        /// <summary>
        /// Метод, который определяет, может ли 
        /// команда быть выполнена в текущем состоянии.
        /// </summary>
        /// <param name="parameter">Параметр команды.</param>
        /// <returns>True, если команда может быть выполнена; иначе false.</returns>
        public bool CanExecute(object parameter)
            => true;

        /// <summary>
        /// Метод, который выполняет действие, связанное с командой.
        /// </summary>
        /// <param name="parameter">Параметр команды.</param>
        public void Execute(object parameter)
            => _executeAction(parameter);

        /// <summary>
        /// Событие, которое происходит при изменении состояния, 
        /// влияющего на то, должна ли команда выполняться.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested += value; }
        }
    }
}