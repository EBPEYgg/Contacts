using Newtonsoft.Json;

namespace PhoneContacts.Model
{
    /// <summary>
    /// Класс, описывающий контакт в телефоне.
    /// </summary>
    public class Contact
    {
        /// <summary>
        /// Уникальный идентификатор контакта.
        /// </summary>
        private int _id;

        /// <summary>
        /// Счетчик контаков.
        /// </summary>
        private static int _allContactsCount;

        /// <summary>
        /// Имя контакта.
        /// </summary>
        private string _name;

        /// <summary>
        /// Номер телефона контакта.
        /// </summary>
        private string _phone;

        /// <summary>
        /// Почтовый адрес контакта.
        /// </summary>
        private string _email;

        /// <summary>
        /// Возвращает и задает имя контакта.
        /// </summary>
        public string Name
        {
            get => _name;
            set => _name = value;
        }

        /// <summary>
        /// Возвращает и задает номер телефона контакта.
        /// </summary>
        public string Phone
        {
            get => _phone;
            set => _phone = value;
        }

        /// <summary>
        /// Возвращает и задает почтовый адрес контакта.
        /// </summary>
        public string Email
        {
            get => _email;
            set => _email = value;
        }

        /// <summary>
        /// Возвращает счетчик контактов.
        /// </summary>
        [JsonProperty]
        public int AllContactsCount
        {
            get => _allContactsCount;
            private set => _allContactsCount = value;
        }

        /// <summary>
        /// Возвращает уникальный идентификатор товара.
        /// </summary>
        [JsonProperty]
        public int Id
        {
            get => _id;
            private set => _id = value;
        }

        /// <summary>
        /// Создает пустой экземпляр класса <see cref="Contact"/>.
        /// </summary>
        public Contact()
        {

        }

        /// <summary>
        /// Создает экземпляр класса <see cref="Contact"/>.
        /// </summary>
        /// <param name="name">Имя контакта.</param>
        /// <param name="phone">Номер телефона контакта.</param>
        /// <param name="email">Почтовый адрес контакта.</param>
        public Contact(string name, string phone, string email)
        {
            Name = name;
            Phone = phone;
            Email = email;
            AllContactsCount++;
            Id = _allContactsCount;
        }

        /// <summary>
        /// Переопределение метода ToString() для класса <see cref="Contact"/>.
        /// </summary>
        /// <returns>Строка вида: "Имя".</returns>
        public override string ToString()
        {
            return $"{Name}";
        }
    }
}