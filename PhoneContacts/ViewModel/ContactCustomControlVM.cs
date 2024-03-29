using PhoneContacts.Model.Services;
using PhoneContacts.Model;
using System.ComponentModel;
using System.Windows.Input;
using System;

namespace PhoneContacts.ViewModel
{
    /// <summary>
    /// Класс, описывающий ViewModel для главного окна.
    /// </summary>
    public class ContactCustomControlVM : INotifyPropertyChanged, IDataErrorInfo
    {
        /// <summary>
        /// Текущий выбранный контакт.
        /// </summary>
        private Contact _selectedContact;

        /// <summary>
        /// Имя контакта.
        /// </summary>
        private string _name;

        /// <summary>
        /// Телефон контакта.
        /// </summary>
        private string _phone;

        /// <summary>
        /// Почтовый адрес контакта.
        /// </summary>
        private string _email;

        /// <summary>
        /// Событие, отслеживающее изменение значения свойства контакта.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Возвращает и задает команду для работы с выбранным в листбоксе контактом.
        /// </summary>
        public ICommand SelectedContactCommand { get; set; }

        /// <summary>
        /// Возвращает и задает имя контакта.
        /// </summary>
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        /// <summary>
        /// Возвращает и задает номер телефона контакта.
        /// </summary>
        public string Phone
        {
            get => _phone;
            set
            {
                _phone = value;
                OnPropertyChanged(nameof(Phone));
            }
        }

        /// <summary>
        /// Возвращает и задает почтовый адрес контакта.
        /// </summary>
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        /// <summary>
        /// Возвращает сообщение об ошибке.
        /// </summary>
        /// <param name="columnName">Название свойства.</param>
        /// <returns>Текст ошибки.</returns>
        public string this[string columnName]
        {
            get
            {
                string error = null;
                switch (columnName)
                {
                    case "Name":
                        if (!ValueValidator.ValidateName(Name) &&
                            Name != null && Name != String.Empty)
                        {
                            error = "Name can contains only russian, " +
                                "english letters and one space. Example: FirstName LastName";
                        }
                        break;
                    case "Phone":
                        if (!ValueValidator.ValidateNumber(Phone) &&
                            Phone != null && Phone != String.Empty)
                        {
                            error = "Phone Number can contains only digits, " +
                                "spaces and symbols '+()-'. Example: +7 (999) 111-22-33";
                        }
                        break;
                    case "Email":
                        if (!ValueValidator.ValidateEmail(Email) &&
                            Email != null && Email != String.Empty)
                        {
                            error = "Email can contains only digits, " +
                                "english letters and symbol '@'. Example: test@gmail.com";
                        }
                        break;
                }
                return error;
            }
        }

        /// <summary>
        /// Возвращает текст ошибки.
        /// </summary>
        public string Error => null;

        /// <summary>
        /// Возвращает и задает выбранный контакт.
        /// </summary>
        public Contact SelectedContact
        {
            get => _selectedContact;
            set
            {
                if (_selectedContact != value)
                {
                    _selectedContact = value;
                    OnPropertyChanged(nameof(SelectedContact));
                    SelectionChanged();
                }
            }
        }

        /// <summary>
        /// Конструктор класса <see cref="ContactCustomControlVM"/>.
        /// </summary>
        public ContactCustomControlVM()
        {
            SelectedContactCommand = new MyCommand((param) => SelectionChanged());
        }

        /// <summary>
        /// Метод, который вызывает событие <see cref="PropertyChanged"/>.
        /// </summary>
        /// <param name="propertyName">Название измененного свойства.</param>
        private void OnPropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        /// <summary>
        /// Метод для обработки выбранного контакта в ContactsListBox.
        /// </summary>
        private void SelectionChanged()
        {
            if (SelectedContact != null)
            {
                Name = SelectedContact.Name;
                Phone = SelectedContact.Phone;
                Email = SelectedContact.Email;
            }
        }
    }
}