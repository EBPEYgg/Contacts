using System;
using System.ComponentModel;
using PhoneContacts.Model.Services;
using PhoneContacts.ViewModel;
using PhoneContacts.Model;
using System.Windows;
using System.Windows.Input;
using System.Runtime.CompilerServices;

namespace PhoneContacts.ViewModel
{
    public class ContactVM : INotifyPropertyChanged, IDataErrorInfo, ICloneable
    {
        /// <summary>
        /// Событие для отслеживания изменений в свойствах.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Экземпляр класса <see cref="Contact"/>.
        /// </summary>
        private Contact _contact;

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
        /// Создаёт глубокую копию представления контакта.
        /// </summary>
        /// <returns>Глубокая копия <see cref="ContactVM"/>.</returns>
        public object Clone()
        {
            ContactVM returnContact = new ContactVM();
            returnContact._contact = (Contact)_contact.Clone();
            return returnContact;
        }

        public ContactVM()
        {
            _contact = new Contact();

            // Кнопка Apply видна, только если
            // данные из текстовых полей прошли валидацию.
            //PropertyChanged += (sender, e) =>
            //{
            //    if (e.PropertyName == nameof(Name) ||
            //    e.PropertyName == nameof(Phone) ||
            //    e.PropertyName == nameof(Email))
            //    {
            //        IsApplyButtonVisibility = IsValidateDataInTextBoxes();
            //    }
            //};
        }

        /// <summary>
        /// Создаёт экземпляр класса <see cref="ContactVM"/>.
        /// </summary>
        /// <param name="name">Имя контакта.</param>
        /// <param name="phone">Номер телефона контакта.</param>
        /// <param name="email">Адрес электронной почты контакта.</param>
        public ContactVM(string name, string phone, string email)
        {
            _contact = new Contact();
            Name = name;
            Phone = phone;
            Email = email;
            OnPropertyChanged();
        }

        /// <summary>
        /// Вызывает событие <see cref="PropertyChanged"/>
        /// </summary>
        /// <param name="property">Название изменённого свойства.</param>
        private void OnPropertyChanged([CallerMemberName] string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        /// <summary>
        /// Метод, который проверяет, правильно ли введены данные в текстовых полях.
        /// </summary>
        /// <returns>True, если данные в текстовых полях 
        /// прошли валидацию, иначе false.</returns>
        private bool IsValidateDataInTextBoxes()
        {
            if (Name != string.Empty &&
                Phone != string.Empty &&
                Email != string.Empty)
            {
                return string.IsNullOrEmpty(this["Name"]) &&
                       string.IsNullOrEmpty(this["Phone"]) &&
                       string.IsNullOrEmpty(this["Email"]);
            }
            return false;
        }
    }
}