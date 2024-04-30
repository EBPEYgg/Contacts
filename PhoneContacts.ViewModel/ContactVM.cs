using PhoneContacts.Model.Services;
using PhoneContacts.Model;
using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel;
using System;

namespace PhoneContacts.ViewModel
{
    public class ContactVM : ObservableObject, IDataErrorInfo, ICloneable
    {
        /// <summary>
        /// Экземпляр класса <see cref="Contact"/>.
        /// </summary>
        private Contact _contact;

        /// <summary>
        /// Наличие ошибок валидации.
        /// </summary>
        private bool _hasValidationErrors;

        /// <summary>
        /// Возвращает и задаёт имя контакта.
        /// </summary>
        public string Name
        {
            get => _contact.Name;
            set
            {
                _contact.Name = value;
                OnPropertyChanged(nameof(Name));
                Validate();
            }
        }

        /// <summary>
        /// Вовзращает и задаёт номер контакта.
        /// </summary>
        public string Phone
        {
            get => _contact.Phone;
            set
            {
                _contact.Phone = value;
                OnPropertyChanged(nameof(Phone));
                Validate();
            }
        }

        /// <summary>
        /// Возвращает и задаёт адрес электронной почты.
        /// </summary>
        public string Email
        {
            get => _contact.Email;
            set
            {
                _contact.Email = value;
                OnPropertyChanged(nameof(Email));
                Validate();
            }
        }

        /// <summary>
        /// Возвращает и задает наличие ошибок валидации данных.
        /// </summary>
        public bool HasValidationErrors
        {
            get => _hasValidationErrors;
            set
            {
                if (_hasValidationErrors != value)
                {
                    _hasValidationErrors = value;
                    OnPropertyChanged(nameof(HasValidationErrors));
                }
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
                        if (!ValueValidator.ValidateName(Name) && Name != string.Empty)
                        {
                            error = "Name can contains only russian, " +
                                "english letters and one space. Example: FirstName LastName";
                        }
                        break;
                    case "Phone":
                        if (!ValueValidator.ValidateNumber(Phone) && Phone != string.Empty)
                        {
                            error = "Phone Number can contains only digits, " +
                                "spaces and symbols '+()-'. Example: +7 (999) 111-22-33";
                        }
                        break;
                    case "Email":
                        if (!ValueValidator.ValidateEmail(Email) && Email != string.Empty)
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
        /// Создаёт глубокую копию представления контакта.
        /// </summary>
        /// <returns>Глубокая копия <see cref="ContactVM"/>.</returns>
        public object Clone()
        {
            ContactVM returnContact = new ContactVM();
            returnContact._contact = (Contact)_contact.Clone();
            return returnContact;
        }

        /// <summary>
        /// Создаёт экземпляр класса <see cref="ContactVM"/>.
        /// </summary>
        public ContactVM()
        {
            _contact = new Contact();
            OnPropertyChanged();
        }

        /// <summary>
        /// Метод, который проверяет, правильно ли введены данные в текстовых полях.
        /// </summary>
        /// <returns>True, если данные в текстовых полях
        /// прошли валидацию, иначе false.</returns>
        private void Validate()
        {
            bool isValid = ValueValidator.ValidateName(Name) && Name != string.Empty
                && ValueValidator.ValidateNumber(Phone) && Phone != string.Empty
                && ValueValidator.ValidateEmail(Email) && Email != string.Empty;
            HasValidationErrors = !isValid;
        }
    }
}