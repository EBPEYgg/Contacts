﻿using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.IO;

namespace PhoneContacts.Model.Services
{
    /// <summary>
    /// Класс, описывающий сериализацию и десериализацию данных.
    /// </summary>
    public static class ContactSerializer
    {
        /// <summary>
        /// Путь к файлу по умолчанию.
        /// </summary>
        private static readonly string _filePath = Path.Combine(Environment.GetFolderPath
            (Environment.SpecialFolder.MyDocuments), "Contacts", "phoneContacts.json");

        /// <summary>
        /// Метод для сохранения объекта контакта в файл.
        /// </summary>
        /// <param name="contact">Экземпляр класса <see cref="Contact"/>.</param>
        public static void SaveContact(ObservableCollection<Contact> contact)
        {
            if (!Directory.Exists(_filePath))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(_filePath));
            }

            var json = JsonConvert.SerializeObject(contact);
            File.WriteAllText(_filePath, json);
        }

        /// <summary>
        /// Метод для загрузки объекта контакта из файла.
        /// </summary>
        /// <returns>Экземпляр класса <see cref="Contact"/>.</returns>
        public static ObservableCollection<Contact> LoadContact()
        {
            if (!File.Exists(_filePath))
            {
                return new ObservableCollection<Contact>();
            }

            var json = File.ReadAllText(_filePath);
            return JsonConvert.DeserializeObject<ObservableCollection<Contact>>(json);
        }
    }
}