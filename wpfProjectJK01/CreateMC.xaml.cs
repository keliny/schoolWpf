using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace wpfProjectJK01
{
    /// <summary>
    /// Interaction logic for CreateMC.xaml
    /// </summary>
    public partial class CreateMC : Window
    {
        public bool[] Valid { get; set; } = {false, false, false};

        public CreateMC()
        {
            InitializeComponent();
        }

        private void ValidateName(object sender, RoutedEventArgs e)
        {
            Valid[0] = ValidationRules.ValidateName(sender, e);
        }

        private void ValidateCode(object sender, RoutedEventArgs e)
        {
            Valid[1] = ValidationRules.ValidateCode(sender, e);
        }

        private void ValidateDescription(object sender, RoutedEventArgs e)
        {
            Valid[2] = ValidationRules.ValidateDescription(sender, e);
        }

        private void Btn_Click_Create_MC(object sender, RoutedEventArgs e)
        {
            if (Valid.Contains(false))
            {
                MessageBox.Show("Please revise your input, not all parts are valid.");
            }
            else
            {
                DataStore.Modified = true;
                Console.WriteLine(MC_create_Code.Text);
                DataStore.AddMeetingCentre(new MeetingCentre(MC_create_name.Text, MC_create_Code.Text,
                    MC_create_Description.Text));
                Close();
            }
        }

        private void Btn_Click_Storno(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}