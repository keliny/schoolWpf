using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace wpfProjectJK01
{
    public static class ValidationRules
    {
        public static bool ValidateName(object sender, RoutedEventArgs e)
        {
            if (((TextBox)sender).Text.Length < 2 || ((TextBox)sender).Text.Length > 100)
            {
                MessageBox.Show("Name can be between 2 and 100 letters.");
                return false;
            }

            return true;
        }

        public static bool ValidateCode(object sender, RoutedEventArgs e)
        {
            {
                var regex = "^[A-Za-z0-9.:_\x2d]*$";
                var re = new Regex("");
                if (((TextBox)sender).Text.Length < 5 || ((TextBox)sender).Text.Length > 50)
                {
                    MessageBox.Show("Code can be between 5 and 50 letters.");
                    return false;
                }
                if (DataStore.DisplayMeetingCentres2().Select(x => x.Code).Contains(((TextBox)sender).Text))
                {
                    MessageBox.Show("Code has to be unique.");
                    return false;
                }
                var match = Regex.Match(((TextBox)sender).Text, regex);
                if (!match.Success)
                {
                    MessageBox.Show("Only A-Z a-z 0-9 .-:_ are allowed");
                    return false;
                }

                return true;
            }
        }

        public static bool ValidateDescription(object sender, RoutedEventArgs e)
        {
            if (((TextBox)sender).Text.Length < 10 || ((TextBox)sender).Text.Length > 300)
            {
                MessageBox.Show("Description can be between 10 and 300 letters.");
                return false;
            }

            return true;
        }

        public static bool ValidateCapacity(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(((TextBox) sender).Text, out var i))
            {
                if (i < 1 || i > 100)
                {
                    MessageBox.Show("Capacity can be between 1 and 100.");
                    return false;
                }

                return true;
            }

            MessageBox.Show("Capacity must be number.");

            return false;
        }
    }
}