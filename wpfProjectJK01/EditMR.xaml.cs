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
    /// Interaction logic for EditMR.xaml
    /// </summary>
    public partial class EditMR : Window
    {
        private MeetingRoom Mr { get; } =
            DataStore.DisplayMeetingRooms2()[
                ((MainWindow) Application.Current.MainWindow).MeetingRoomsBox.SelectedIndex];

        private readonly MainWindow _mw = ((MainWindow) Application.Current.MainWindow);
        public bool[] Valid { get; set; } = {true, true, true, true};

        public EditMR()
        {
            InitializeComponent();
            MR_update_name.Text = Mr.Name;
            MR_update_Code.Text = Mr.Code;
            MR_update_Description.Text = Mr.Description;
            MR_update_VideoConference.IsChecked = Mr.VideoConference;
            MR_update_Capacity.Text = Mr.Capacity.ToString();
            MR_update_MeetingCentre.Text = Mr.MeetingCentre.CentreName;
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

        private void Btn_Click_Save_MR(object sender, RoutedEventArgs e)
        {
            if (Valid.Contains(false))
            {
                MessageBox.Show("Please revise your input, not all parts are valid.");
            }
            else
            {
                try
                {
                    // Update and save MR
                    Mr.Name = MR_update_name.Text;
                    Mr.Code = MR_update_Code.Text;
                    Mr.Description = MR_update_Description.Text;
                    Mr.Capacity = int.Parse(MR_update_Capacity.Text);
                    Mr.VideoConference = MR_update_VideoConference.IsChecked.Value;

                    DataStore.Modified = true;

                    // Refresh data in MainWindow
                    _mw.MeetingRoomsBox.Items.Refresh();
                    _mw.MR_Name.GetBindingExpression(TextBox.TextProperty)?.UpdateTarget();
                    _mw.MR_Code.GetBindingExpression(TextBox.TextProperty)?.UpdateTarget();
                    _mw.MR_Description.GetBindingExpression(TextBox.TextProperty)?.UpdateTarget();
                    _mw.MR_Capacity.GetBindingExpression(TextBox.TextProperty)?.UpdateTarget();
                    _mw.MR_Video_Conference.GetBindingExpression(CheckBox.IsCheckedProperty)?.UpdateTarget();
                }
                catch (Exception exception)
                {
                    _mw.textBox1.Text = $"There was an error while saving your update {exception}.";
                }

                Close();
            }
        }

        private void Btn_Click_Storno(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}