using Contacts.Model;
using Contacts.Model.Services;
using System.ComponentModel;

namespace Contacts.ViewModel
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
        /// Событие, отслеживающее изменение значения свойства контакта.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Возвращает и задает команду для сохранения данных.
        /// </summary>
        public MyCommand SaveCommand { get; set; }

        /// <summary>
        /// Возвращает и задает команду для загрузки данных.
        /// </summary>
        public MyCommand LoadCommand { get; set; }

        /// <summary>
        /// Возвращает и задает имя контакта.
        /// </summary>
        public string Name
        {
            get => contact.Name;
            set
            {
                contact.Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        /// <summary>
        /// Возвращает и задает номер телефона контакта.
        /// </summary>
        public string Phone
        {
            get => contact.Phone;
            set
            {
                contact.Phone = value;
                OnPropertyChanged(nameof(Phone));
            }
        }

        /// <summary>
        /// Возвращает и задает почтовый адрес контакта.
        /// </summary>
        public string Email
        {
            get => contact.Email;
            set
            {
                contact.Email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        /// <summary>
        /// Конструктор класса <see cref="MainVM"/>.
        /// </summary>
        public MainVM()
        {
            contact = new Contact();
            SaveCommand = new MyCommand((param) => ContactSerializer.SaveContact(contact));
            LoadCommand = new MyCommand((param) =>
            {
                contact = ContactSerializer.LoadContact();
                OnPropertyChanged(nameof(Name));
                OnPropertyChanged(nameof(Phone));
                OnPropertyChanged(nameof(Email));
            });
        }

        /// <summary>
        /// Метод, который вызывает событие <see cref="PropertyChanged"/>.
        /// </summary>
        /// <param name="propertyName">Название измененного свойства.</param>
        private void OnPropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}