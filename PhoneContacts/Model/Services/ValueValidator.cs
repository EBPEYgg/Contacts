using System.Text.RegularExpressions;

namespace PhoneContacts.Model.Services
{
    /// <summary>
    /// Класс, описывающий методы для проверки входящих значений.
    /// </summary>
    internal static class ValueValidator
    {
        /// <summary>
        /// Метод, который проверяет правильность ввода имени контакта.
        /// </summary>
        /// <param name="name">Имя для проверки.</param>
        /// <returns>True - имя введено верно, иначе False.</returns>
        public static bool ValidateName(string name)
        {
            return !string.IsNullOrWhiteSpace(name) && Regex.IsMatch(name, @"^[A-Za-z]+\s[A-Za-z]+$", RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// Метод, который проверяет правильность ввода номера.
        /// </summary>
        /// <param name="number">Номер телефона.</param>
        /// <returns>True - номер введён верно, иначе False.</returns>
        public static bool ValidateNumber(string number)
        {
            return !string.IsNullOrWhiteSpace(number) && Regex.IsMatch(number, @"^\+\d\s\(\d{3}\)\s\d{3}-\d{2}-\d{2}$");
        }        

        /// <summary>
        /// Метод, который проверяет правильность ввода почты контакта.
        /// </summary>
        /// <param name="email">Почта для проверки.</param>
        /// <returns>True - почта введена верно, иначе False.</returns>
        public static bool ValidateEmail(string email)
        {
            return !string.IsNullOrWhiteSpace(email) && Regex.IsMatch(email, @"^[A-Za-z0-9]+@[A-Za-z0-9]+\.[A-Za-z0-9]+$", RegexOptions.IgnoreCase);
        }
    }
}