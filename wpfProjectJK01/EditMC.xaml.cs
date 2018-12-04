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
    /// Interaction logic for EditMC.xaml
    /// </summary>
    public partial class EditMC : Window
    {
        private bool[] Valid { get; } = {true, true, true};

        private MeetingCentre Mc { get; } =
            DataStore.DisplayMeetingCentres2()[
                ((MainWindow) Application.Current.MainWindow).MeetingCentresBox.SelectedIndex];

        private readonly MainWindow _mw = ((MainWindow) Application.Current.MainWindow);

        public EditMC()
        {
            InitializeComponent();
            MC_update_name.Text = Mc.Name;
            MC_update_Code.Text = Mc.Code;
            MC_update_Description.Text = Mc.Description;
        }

        private void ValidateName(object sender, RoutedEventArgs e)
        {
            Valid[0] = ValidationRules.ValidateName(sender, e);
        }

        private void ValidateCode(object sender, RoutedEventArgs e)
        {
            Valid[0] = ValidationRules.ValidateCode(sender, e);
        }

        private void ValidateDescription(object sender, RoutedEventArgs e)
        {
            Valid[0] = ValidationRules.ValidateDescription(sender, e);
        }

        private void Btn_Click_Save_MC(object sender, RoutedEventArgs e)
        {
            if (Valid.Contains(false))
            {
                MessageBox.Show("Please revise your input, not all parts are valid.");
            }
            else
            {
                try
                {
                    // Update Meeting Rooms with new code
                    DataStore.DisplayMeetingRooms2().Where(y => y.Code == Mc.Code).Select(x =>
                    {
                        x.Code = MC_update_Code.Text;
                        return x;
                    }).ToList();


                    // Update and save MC
                    Mc.Name = MC_update_name.Text;
                    Mc.Code = MC_update_Code.Text;
                    Mc.Description = MC_update_Description.Text;

                    DataStore.Modified = true;

                    // Refresh data in MainWindow
                    _mw.MeetingCentresBox.Items.Refresh();
                    _mw.MC_Name.GetBindingExpression(TextBox.TextProperty)?.UpdateTarget();
                    _mw.MC_Code.GetBindingExpression(TextBox.TextProperty)?.UpdateTarget();
                    _mw.MC_Description.GetBindingExpression(TextBox.TextProperty)?.UpdateTarget();


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