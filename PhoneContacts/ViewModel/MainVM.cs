using PhoneContacts.Model.Services;
using PhoneContacts.Model;
using System.ComponentModel;
using System.Windows.Input;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace PhoneContacts.ViewModel
{
    /// <summary>
    /// Класс, описывающий ViewModel для главного окна.
    /// </summary>
    public class MainVM : INotifyPropertyChanged
    {
        /// <summary>
        /// Инициализация экземпляра класса <see cref="Contact"/>.
        /// </summary>
        private Contact contact;

        /// <summary>
        /// Коллекция с данными о контактах.
        /// </summary>
        private ObservableCollection<Contact> _contacts = new ObservableCollection<Contact>();

        /// <summary>
        /// Копия текущего выбранного контакта.
        /// </summary>
        private Contact _cloneCurrentContact = new Contact();

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
        /// Индекс текущего выбранного элемента для добавления и редактирования элементов.
        /// </summary>
        private int _selectedIndex;

        /// <summary>
        /// Свойство Visibility для кнопки ApplyButton
        /// </summary>
        private bool _isApplyButtonVisibility = false;

        /// <summary>
        /// Свойство IsEnabled для кнопок AddButton, EditButton, ApplyButton.
        /// </summary>
        private bool _isButtonsEnabled = true;

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
        /// Возвращает и задает свойство IsEnabled у кнопок AddButton, EditButton, RemoveButton.
        /// </summary>
        public bool IsButtonsEnabled
        {
            get => _isButtonsEnabled;
            set
            {
                if (_isButtonsEnabled != value)
                {
                    _isButtonsEnabled = value;
                    OnPropertyChanged(nameof(IsButtonsEnabled));
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
            //contact = new Contact();
            Contacts = ContactSerializer.LoadContact();
            SaveCommand = new MyCommand((param) => ContactSerializer.SaveContact(_contacts));
            AddCommand = new MyCommand((param) => Add());
            ApplyCommand = new MyCommand((param) => Apply());
            EditCommand = new MyCommand((param) => Edit());
            RemoveCommand = new MyCommand((param) => Remove());
            SelectedContactCommand = new MyCommand((param) => SelectedContact2());
        }

        /// <summary>
        /// Метод, который вызывает событие <see cref="PropertyChanged"/>.
        /// </summary>
        /// <param name="propertyName">Название измененного свойства.</param>
        private void OnPropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private void SelectedContact2()
        {
            ClearContactInfo();
            IsButtonsEnabled = true;
            //not 0
            _selectedIndex = 0;
        }
        
        private void Add()
        {
            ClearContactInfo();
            IsApplyButtonVisibility = true;
            IsButtonsEnabled = false;
            //снятие выделение текущего контакта
        }

        private void Apply()
        {
            Contact _currentContact = new Contact(Name, Phone, Email);
            _contacts.Add(_currentContact);
            SaveCommand.Execute(_contacts);
            ClearContactInfo();
            IsButtonsEnabled = true;
            IsApplyButtonVisibility = false;
        }

        private void Edit()
        {

        }

        private void Remove()
        {
            _contacts.RemoveAt(_selectedIndex);
        }

        private void ClearContactInfo()
        {
            //снятие выделение текущего контакта
            Name = string.Empty;
            Phone = string.Empty;
            Email = string.Empty;
        }
    }
}