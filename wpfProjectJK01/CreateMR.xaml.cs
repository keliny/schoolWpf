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
    /// Interaction logic for CreateMR.xaml
    /// </summary>
    public partial class CreateMR : Window
    {
        private MeetingCentre Mc { get; } =
            DataStore.DisplayMeetingCentres2()[
                ((MainWindow) Application.Current.MainWindow).MeetingCentresBox.SelectedIndex];

        private readonly MainWindow _mw = ((MainWindow) Application.Current.MainWindow);
        public bool[] Valid { get; set; } = {false, false, false, false};

        public CreateMR()
        {
            InitializeComponent();
            MR_create_MeetingCentre.Text = Mc.CentreName;
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

        private void ValidateCapacity(object sender, RoutedEventArgs e)
        {
            Valid[3] = ValidationRules.ValidateCapacity(sender, e);
        }

        private void Btn_Click_Create_MR(object sender, RoutedEventArgs e)
        {
            if (Valid.Contains(false))
            {
                MessageBox.Show("Seems like not all data are valid.");
            }
            else
            {
                DataStore.Modified = true;

                DataStore.AddMeetingRoom(new MeetingRoom(MR_create_name.Text, MR_create_Code.Text,
                    MR_create_Description.Text, int.Parse(MR_create_Capacity.Text),
                    MR_create_VideoConference.IsChecked.Value, Mc));
                Close();
            }
           
        }

        private void Btn_Click_Storno(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}