using PhoneContacts.Model.Services;
using PhoneContacts.Model;
using System.ComponentModel;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System;

namespace PhoneContacts.ViewModel
{
    /// <summary>
    /// Класс, описывающий ViewModel для главного окна.
    /// </summary>
    public class MainVM : INotifyPropertyChanged, IDataErrorInfo
    {
        /// <summary>
        /// Коллекция с данными о контактах.
        /// </summary>
        private ObservableCollection<Contact> _contacts = new ObservableCollection<Contact>();

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
        /// Индекс текущего выбранного элемента для удаления 
        /// и отображения содержимого элементов.
        /// </summary>
        private int _selectedIndex = -1;

        /// <summary>
        /// Свойство Visibility для кнопки ApplyButton
        /// </summary>
        private bool _isApplyButtonVisibility = false;

        /// <summary>
        /// Свойство IsEnabled для кнопки AddButton.
        /// </summary>
        private bool _isAddButtonEnabled = true;

        /// <summary>
        /// Свойство IsEnabled для кнопки EditButton.
        /// </summary>
        private bool _isEditButtonEnabled = true;

        /// <summary>
        /// Свойство IsEnabled для кнопки RemoveButton.
        /// </summary>
        private bool _isRemoveButtonEnabled = true;

        /// <summary>
        /// Флаг, показывающий статус редактирования контакта.
        /// </summary>
        private bool _isEditing = false;

        /// <summary>
        /// Свойство IsReadOnly для NameTextBox, PhoneTextBox, EmailTextBox.
        /// </summary>
        private bool _isReadOnly = false;

        /// <summary>
        /// Событие, отслеживающее изменение значения свойства контакта.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Возвращает и задает команду для сохранения данных.
        /// </summary>
        public ICommand SaveCommand { get; set; }

        /// <summary>
        /// Возвращает и задает команду для загрузки данных.
        /// </summary>
        public ICommand LoadCommand { get; set; }

        /// <summary>
        /// Возвращает и задает команду для работы с выбранным в листбоксе контактом.
        /// </summary>
        public ICommand SelectedContactCommand { get; set; }

        /// <summary>
        /// Возвращает и задает команду для добавления контакта.
        /// </summary>
        public ICommand AddCommand { get; set; }

        /// <summary>
        /// Возвращает и задает команду для редактирования контакта.
        /// </summary>
        public ICommand EditCommand { get; set; }

        /// <summary>
        /// Возвращает и задает команду для удаления контакта.
        /// </summary>
        public ICommand RemoveCommand { get; set; }

        /// <summary>
        /// Возвращает и задает команду для сохранения данных о контакте.
        /// </summary>
        public ICommand ApplyCommand { get; set; }

        /// <summary>
        /// Возвращает и задает свойство Visibility у кнопки ApplyButton.
        /// </summary>
        public bool IsApplyButtonVisibility
        {
            get => _isApplyButtonVisibility;
            set
            {
                if (_isApplyButtonVisibility != value)
                {
                    _isApplyButtonVisibility = value;
                    OnPropertyChanged(nameof(IsApplyButtonVisibility));
                }
            }
        }

        /// <summary>
        /// Возвращает и задает свойство IsEnabled у кнопки AddButton.
        /// </summary>
        public bool IsAddButtonEnabled
        {
            get => _isAddButtonEnabled;
            set
            {
                if (_isAddButtonEnabled != value)
                {
                    _isAddButtonEnabled = value;
                    OnPropertyChanged(nameof(IsAddButtonEnabled));
                }
            }
        }

        /// <summary>
        /// Возвращает и задает свойство IsEnabled у кнопки EditButton.
        /// </summary>
        public bool IsEditButtonEnabled
        {
            get => _isEditButtonEnabled;
            set
            {
                if (_isEditButtonEnabled != value)
                {
                    _isEditButtonEnabled = value;
                    OnPropertyChanged(nameof(IsEditButtonEnabled));
                }
            }
        }

        /// <summary>
        /// Возвращает и задает свойство IsEnabled у кнопки RemoveButton.
        /// </summary>
        public bool IsRemoveButtonEnabled
        {
            get => _isRemoveButtonEnabled;
            set
            {
                if (_isRemoveButtonEnabled != value)
                {
                    _isRemoveButtonEnabled = value;
                    OnPropertyChanged(nameof(IsRemoveButtonEnabled));
                }
            }
        }

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
        /// Возвращает и задает свойство IsReadOnly для NameTextBox, PhoneTextBox, EmailTextBox.
        /// </summary>
        public bool IsReadOnly
        {
            get => _isReadOnly;
            set
            {
                if (_isReadOnly != value)
                {
                    _isReadOnly = value;
                    OnPropertyChanged(nameof(IsReadOnly));
                }
            }
        }

        /// <summary>
        /// Возвращает и задает статус редактирования контакта.
        /// </summary>
        public bool IsEditing
        {
            get => _isEditing;
            set
            {
                _isEditing = value;
                IsReadOnly = !value;
                OnPropertyChanged(nameof(IsEditing));
                OnPropertyChanged(nameof(IsEditButtonEnabled));
                OnPropertyChanged(nameof(IsApplyButtonVisibility));
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
        /// Возвращает и задает список товаров.
        /// </summary>
        public ObservableCollection<Contact> Contacts
        {
            get => _contacts;
            set
            {
                if (_contacts != value)
                {
                    _contacts = value;
                    OnPropertyChanged(nameof(Contacts));
                }
            }
        }

        /// <summary>
        /// Конструктор класса <see cref="MainVM"/>.
        /// </summary>
        public MainVM()
        {
            Contacts = ContactSerializer.LoadContact();
            SaveCommand = new MyCommand((param) => ContactSerializer.SaveContact(_contacts));
            AddCommand = new MyCommand((param) => Add());
            ApplyCommand = new MyCommand((param) => Apply());
            EditCommand = new MyCommand((param) => Edit());
            RemoveCommand = new MyCommand((param) => Remove());
            SelectedContactCommand = new MyCommand((param) => SelectionChanged());

            IsEditButtonEnabled = false;
            IsRemoveButtonEnabled = false;
            IsReadOnly = true;

            // Кнопка Apply видна, только если
            // данные из текстовых полей прошли валидацию.
            PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == nameof(Name) || 
                e.PropertyName == nameof(Phone) || 
                e.PropertyName == nameof(Email))
                {
                    IsApplyButtonVisibility = IsValidateDataInTextBoxes();
                }
            };
        }

        /// <summary>
        /// Метод, который проверяет, правильно ли введены данные в текстовых полях.
        /// </summary>
        /// <returns>True, если данные в текстовых полях 
        /// прошли валидацию, иначе false.</returns>
        private bool IsValidateDataInTextBoxes()
        {
            if (Name != String.Empty && 
                Phone != String.Empty && 
                Email != String.Empty)
            {
                return string.IsNullOrEmpty(this["Name"]) &&
                       string.IsNullOrEmpty(this["Phone"]) &&
                       string.IsNullOrEmpty(this["Email"]);
            }
            return false;
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
                ToggleEnableButtons(true);
                _selectedIndex = Contacts.IndexOf(SelectedContact);
                IsEditing = false;
                IsAddButtonEnabled = true;
                IsApplyButtonVisibility = false;
            }
        }

        /// <summary>
        /// Метод, который разрешает возможность добавить новый контакт.
        /// </summary>
        private void Add()
        {
            IsReadOnly = false;
            ClearContactInfo();
            ToggleEnableButtons(false);
            SelectedContact = null;
        }

        /// <summary>
        /// Метод для добавления нового контакта или 
        /// принятия измененных данных существующего контакта.
        /// </summary>
        private void Apply()
        {
            if (!IsEditing)
            {
                Contact newContact = new Contact(Name, Phone, Email);
                _contacts.Add(newContact);
                SelectedContact = newContact;
            }

            else
            {
                SelectedContact.Name = Name;
                SelectedContact.Phone = Phone;
                SelectedContact.Email = Email;
            }

            SaveCommand.Execute(_contacts);
            ToggleEnableButtons(false);
            IsAddButtonEnabled = true;
            IsApplyButtonVisibility = false;
            IsEditing = false;
            OnPropertyChanged(nameof(SelectedContact));
            SelectionChanged();
        }

        /// <summary>
        /// Метод, который позволяет редактировать выбранный контакт в ContactsListBox.
        /// </summary>
        private void Edit()
        {
            IsEditing = true;
            IsApplyButtonVisibility = true;
            ToggleEnableButtons(false);
        }

        /// <summary>
        /// Метод, который позволяет удалить выбранный контакт в ContactsListBox.
        /// </summary>
        private void Remove()
        {
            if (SelectedContact != null)
            {
                _selectedIndex = Contacts.IndexOf(SelectedContact);
                Contacts.Remove(SelectedContact);
                SaveCommand.Execute(_contacts);

                if (Contacts.Count > 0)
                {
                    // Если удаленный контакт был последним в списке,
                    // устанавливаем выделение на последний контакт после удаления
                    if (_selectedIndex >= Contacts.Count)
                    {
                        _selectedIndex = Contacts.Count - 1;
                    }

                    // Устанавливаем выбранный контакт на следующий после удаленного
                    SelectedContact = Contacts[_selectedIndex];
                }
                else
                {
                    SelectedContact = null;
                    ClearContactInfo();
                    IsEditButtonEnabled = false;
                    IsRemoveButtonEnabled = false;
                }
            }
        }

        /// <summary>
        /// Метод, который переключает свойство IsEnabled у кнопок Add, Edit, Remove.
        /// </summary>
        /// <param name="value"></param>
        private void ToggleEnableButtons(bool value)
        {
            IsAddButtonEnabled = value;
            IsEditButtonEnabled = value;
            IsRemoveButtonEnabled = value;
        }

        /// <summary>
        /// Метод, который очищает текстовые поля на форме.
        /// </summary>
        private void ClearContactInfo()
        {
            Name = string.Empty;
            Phone = string.Empty;
            Email = string.Empty;
        }
    }
}