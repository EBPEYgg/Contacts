using PhoneContacts.Model;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PhoneContacts.ViewModel.Services;

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
        /// Возвращает и задает команду для загрузки данных.
        /// </summary>
        public RelayCommand LoadCommand { get; set; }

        /// <summary>
        /// Возвращает и задает команду для работы с выбранным в листбоксе контактом.
        /// </summary>
        public RelayCommand SelectedContactCommand { get; set; }

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
                OnPropertyChanged(nameof(IsEditing));
                OnPropertyChanged(nameof(IsEditButtonEnabled));
                OnPropertyChanged(nameof(IsApplyButtonVisibility));
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
                    IsReadOnly = !value;
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
                    if (Contacts.Contains(value))
                    {
                        IsAdding = false;
                        if (IsEditing)
                        {
                            IsEditing = false;
                        }
                    }
                    OnPropertyChanged(nameof(SelectedContact));
                }
            }
        }

        /// <summary>
        /// Возвращает и задает список товаров.
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
        }

        /// <summary>
        /// Метод, который разрешает возможность добавить новый контакт.
        /// </summary>
        private void Add()
        {
            IsReadOnly = false;
            ToggleEnableButtons(false);
            SelectedContact = null;
            SelectedContact = new ContactVM();
            IsAdding = true;
            OnPropertyChanged(nameof(IsAdding));
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
                SelectedContact = null;
            }

            else
            {
                _selectedIndex = Contacts.IndexOf(SelectedContact);
                Contacts[_selectedIndex] = SelectedContact;
                OnPropertyChanged(nameof(BeforeEditingContact));
                SelectedContact = Contacts[_selectedIndex];
                IsEditing = false;
            }

            SaveCommand.Execute(_contacts);
            ToggleEnableButtons(false);
            IsAddButtonEnabled = true;
            IsApplyButtonVisibility = false;
            IsEditing = false;
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
            IsApplyButtonVisibility = true;
            ToggleEnableButtons(false);
            OnPropertyChanged(nameof(Edit));
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
    }
}