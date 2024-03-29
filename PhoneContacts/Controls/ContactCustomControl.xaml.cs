using PhoneContacts.Model.Services;
using System;
using System.Windows;
using System.Windows.Controls;

namespace PhoneContacts.Controls
{
    /// <summary>
    /// Логика взаимодействия для ContactCustomControl.xaml
    /// </summary>
    public partial class ContactCustomControl : UserControl
    {
        public ContactCustomControl()
        {
            InitializeComponent();
        }

        private void PhoneTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            PhoneTextBox.CaretIndex = PhoneTextBox.Text.Length;
        }

        private void PhoneTextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            int value;
            if (!Int32.TryParse(e.Text, out value))
            {
                e.Handled = true;
            }
        }

        private void PhoneTextBox_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                string pastedText = (string)e.DataObject.GetData(typeof(string));
                if (!ValueValidator.ValidateNumber(pastedText))
                {
                    e.CancelCommand();
                }
            }
            else
            {
                e.CancelCommand();
            }
        }
    }
}