using PhoneContacts.Model;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PhoneContacts.ViewModel.Services;
using System.ComponentModel;

namespace PhoneContacts.ViewModel
{
    /// <summary>
    /// Класс, описывающий ViewModel для главного окна.
    /// </summary>
    public class MainVM : ObservableObject
    {
        /// <summary>
        /// Коллекция с данными о контактах.
        /// </summary>
        private ObservableCollection<Contact> _contacts = new ObservableCollection<Contact>();

        /// <summary>
        /// Текущий выбранный контакт.
        /// </summary>
        private ContactVM _selectedContact;

        /// <summary>
        /// Контакт, хранящий неизменённые данные. 
        /// </summary>
        private ContactVM _beforeEditingContact;

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
        /// Статус добавления нового контакта.
        /// </summary>
        private bool _isAdding = false;

        /// <summary>
        /// Возвращает и задает команду для сохранения данных.
        /// </summary>
        public RelayCommand SaveCommand { get; set; }

        /// <summary>
        /// Возвращает и задает команду для добавления контакта.
        /// </summary>
        public RelayCommand AddCommand { get; set; }

        /// <summary>
        /// Возвращает и задает команду для редактирования контакта.
        /// </summary>
        public RelayCommand EditCommand { get; set; }

        /// <summary>
        /// Возвращает и задает команду для удаления контакта.
        /// </summary>
        public RelayCommand RemoveCommand { get; set; }

        /// <summary>
        /// Возвращает и задает команду для сохранения данных о контакте.
        /// </summary>
        public RelayCommand ApplyCommand { get; set; }

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
            }
        }

        /// <summary>
        /// Возвращает и задаёт статус добавления контакта <see cref="ContactVM"/>.
        /// </summary>
        public bool IsAdding
        {
            get => _isAdding;
            set
            {
                if (_isAdding != value)
                {
                    _isAdding = value;
                    OnPropertyChanged(nameof(IsAdding));
                }
            }
        }

        /// <summary>
        /// Возвращает и задаёт неизмененные данные.
        /// </summary>
        public ContactVM BeforeEditingContact
        {
            get => _beforeEditingContact;
            set
            {
                _beforeEditingContact = value;
            }
        }

        /// <summary>
        /// Возвращает и задает выбранный контакт.
        /// </summary>
        public ContactVM SelectedContact
        {
            get => _selectedContact;
            set
            {
                if (_selectedContact != value)
                {
                    _selectedContact = value;
                    OnPropertyChanged(nameof(SelectedContact));
                    SelectionChange();
                }
            }
        }

        /// <summary>
        /// Возвращает и задает список контактов.
        /// </summary>
        public ObservableCollection<ContactVM> Contacts { get; set; }

        /// <summary>
        /// Конструктор класса <see cref="MainVM"/>.
        /// </summary>
        public MainVM()
        {
            Contacts = ContactSerializer.LoadContact();
            SaveCommand = new RelayCommand(SaveContacts);
            AddCommand = new RelayCommand(Add);
            ApplyCommand = new RelayCommand(Apply);
            EditCommand = new RelayCommand(Edit);
            RemoveCommand = new RelayCommand(Remove);

            IsEditButtonEnabled = false;
            IsRemoveButtonEnabled = false;
            IsReadOnly = true;

            if (Contacts.Count > 0)
            {
                SelectedContact = Contacts[0];
            }
        }

        /// <summary>
        /// Метод, который обрабатывает смену выбранного контакта.
        /// </summary>
        private void SelectionChange()
        {
            if (SelectedContact != null)
            {
                ToggleEnableButtons(true);
                IsEditing = false;
                SelectedContact.PropertyChanged += OnContactPropertyChanged;
            }
        }

        /// <summary>
        /// Метод, который разрешает возможность добавить новый контакт.
        /// </summary>
        private void Add()
        {
            if (Contacts.Count > 0)
            {
                SelectedContact.PropertyChanged -= OnContactPropertyChanged;
            }
            IsAdding = true;
            SelectedContact = null;
            SelectedContact = new ContactVM();
            ToggleEnableButtons(false);
            IsReadOnly = false;
        }

        /// <summary>
        /// Метод для добавления нового контакта или 
        /// принятия измененных данных существующего контакта.
        /// </summary>
        private void Apply()
        {
            if (IsAdding)
            {
                Contacts.Add(SelectedContact);
                IsAdding = false;
            }

            else
            {
                _selectedIndex = Contacts.IndexOf(BeforeEditingContact);
                Contacts[_selectedIndex] = SelectedContact;
                SelectedContact = Contacts[_selectedIndex];
            }

            SaveCommand.Execute(_contacts);
            IsApplyButtonVisibility = false;
            IsEditing = false;
            ToggleEnableButtons(true);
            OnPropertyChanged(nameof(SelectedContact));
        }

        /// <summary>
        /// Метод, который позволяет редактировать выбранный контакт в ContactsListBox.
        /// </summary>
        private void Edit()
        {
            BeforeEditingContact = SelectedContact;
            SelectedContact = (ContactVM)SelectedContact.Clone();
            IsEditing = true;
            UpdateApplyButtonVisibility();
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
        /// <inheritdoc cref="ContactSerializer.SaveContact(ObservableCollection{ContactVM})"/>
        /// </summary>
        private void SaveContacts()
        {
            ContactSerializer.SaveContact(Contacts);
        }

        /// <summary>
        /// Метод, который отслеживает изменения свойства 
        /// <see cref="ContactVM.HasValidationErrors"/> в <see cref="ContactVM"/>.
        /// </summary>
        /// <param name="sender">Ссылка на объект, который инициировал событие.</param>
        /// <param name="e">Объект с информацией о событии измененнного свойства.</param>
        private void OnContactPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ContactVM.HasValidationErrors))
            {
                UpdateApplyButtonVisibility();
            }
        }

        /// <summary>
        /// Метод, который меняет видимость кнопки Apply 
        /// в зависимости от результата валидации данных.
        /// </summary>
        private void UpdateApplyButtonVisibility()
        {
            IsApplyButtonVisibility = !SelectedContact.HasValidationErrors;
        }
    }
}