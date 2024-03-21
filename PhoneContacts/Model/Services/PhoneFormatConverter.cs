using System;
using System.Globalization;
using System.Windows.Data;

namespace PhoneContacts.Model.Services
{
    /// <summary>
    /// Класс, описывающий конвертер форматирования номера телефона.
    /// </summary>
    public class PhoneFormatConverter : IValueConverter
    {
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

        public object ConvertBack(object value, Type targetType, 
            object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}