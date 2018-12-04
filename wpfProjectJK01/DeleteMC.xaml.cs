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
    /// Interaction logic for DeleteMC.xaml
    /// </summary>
    public partial class DeleteMC : Window
    {
        private MeetingCentre Mc { get; } =
            DataStore.DisplayMeetingCentres2()[
                ((MainWindow)Application.Current.MainWindow).MeetingCentresBox.SelectedIndex];
        private readonly MainWindow _mw = ((MainWindow)Application.Current.MainWindow);
        public DeleteMC()
        {
            InitializeComponent();
        }

        private void Btn_Click_Delete_MC(object sender, RoutedEventArgs e)
        {
            try
            {
               DataStore.DeleteMc(Mc);
            }
            catch (Exception exception)
            {
                _mw.textBox1.Text = $"Error while deleting MC {exception}";
            }
            DataStore.Modified = true;

            Close();
        }

        private void Btn_Click_Storno(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
