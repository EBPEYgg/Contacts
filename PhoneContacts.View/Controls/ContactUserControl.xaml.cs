using PhoneContacts.Model.Services;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PhoneContacts.View.Controls
{
    /// <summary>
    /// Логика взаимодействия для ContactUserControl.xaml.
    /// </summary>
    public partial class ContactUserControl : UserControl
    {
        /// <summary>
        /// Свойство блокировки полей контакта. <see cref="DependencyProperty"/>.
        /// </summary>
        public static readonly DependencyProperty IsReadOnlyProperty = 
            DependencyProperty.Register(
                "IsReadOnly", 
                typeof(bool), 
                typeof(ContactUserControl), 
                new FrameworkPropertyMetadata(false));

        /// <summary>
        /// Возвращает и задаёт блокировку полей для ввода данных контакта.
        /// </summary>
        public bool IsReadOnly
        {
            get { return (bool)GetValue(IsReadOnlyProperty); }
            set { SetValue(IsReadOnlyProperty, value); }
        }

        /// <summary>
        /// Конструктор класса <see cref="ContactUserControl"/>.
        /// </summary>
        public ContactUserControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Метод, который перемещает указатель курсора в конец строки с номером.
        /// </summary>
        /// <param name="sender">Объект.</param>
        /// <param name="e">Данные объекта.</param>
        private void PhoneTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            PhoneTextBox.CaretIndex = PhoneTextBox.Text.Length;
        }

        /// <summary>
        /// Метод, который запрещает ввод любых символов, кроме цифр.
        /// </summary>
        /// <param name="sender">Объект.</param>
        /// <param name="e">Данные объекта.</param>
        private void PhoneTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int value;
            if (!Int32.TryParse(e.Text, out value))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Метод, который запрещает вставку символов 
        /// из буфера обмена, не прошедших валидацию.
        /// </summary>
        /// <param name="sender">Объект.</param>
        /// <param name="e">Данные объекта.</param>
        private void PhoneTextBox_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                string pastedText = (string)e.DataObject.GetData(typeof(string));
                if (!ValueValidator.ValidateNumber(pastedText))
                {
                    e.CancelCommand();
                }
            }
            else
            {
                e.CancelCommand();
            }
        }
    }
}
