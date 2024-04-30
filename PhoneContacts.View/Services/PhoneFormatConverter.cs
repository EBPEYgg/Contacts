using System;
using System.Globalization;
using System.Windows.Data;

namespace PhoneContacts.View.Services
{
    /// <summary>
    /// Класс, описывающий конвертер форматирования номера телефона.
    /// </summary>
    public class PhoneFormatConverter : IValueConverter
    {
        /// <summary>
        /// Метод, представляющий собой конвертер, который преобразует строку, 
        /// представляющую телефонный номер, в отформатированный телефонный номер.
        /// </summary>
        /// <param name="value">Строковое значение, представляющее телефонный номер.</param>
        /// <param name="targetType">Тип, в который будет преобразовано значение.</param>
        /// <param name="parameter">Необязательный параметр, используемый при преобразовании.</param>
        /// <param name="culture">Язык, который будет использоваться в конвертере.</param>
        /// <returns>Отформатированный телефонный номер.</returns>
        public object Convert(object value, Type targetType, 
            object parameter, CultureInfo culture)
        {
            if (value is string phoneNumber)
            {
                if (phoneNumber.Length == 1)
                {
                    return $"+{phoneNumber} (";
                }
                // Если пользователь вводит 7 символ, добавляем ") "
                if (phoneNumber.Length == 7)
                {
                    return $"{phoneNumber}) ";
                }
                // Если пользователь вводит 13 символ, добавляем "-"
                if (phoneNumber.Length == 13)
                {
                    return $"{phoneNumber.Substring(0, 12)}-{phoneNumber.Substring(12)}";
                }
                // Если пользователь вводит 16 символ, добавляем "-"
                if (phoneNumber.Length == 16)
                {
                    return $"{phoneNumber.Substring(0, 15)}-{phoneNumber.Substring(15)}";
                }

                return phoneNumber;
            }

            return value;
        }

        /// <summary>
        /// ConverterBack не поддерживается.
        /// </summary>
        /// <param name="value">Значение для обратного преобразования.</param>
        /// <param name="targetType">Тип, к которому значение будет преобразовано обратно.</param>
        /// <param name="parameter">Необязательный параметр, используемый при преобразовании.</param>
        /// <param name="culture">Язык, который будет использоваться в конвертере.</param>
        /// <returns>Возвращает value.</returns>
        public object ConvertBack(object value, Type targetType, 
            object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}