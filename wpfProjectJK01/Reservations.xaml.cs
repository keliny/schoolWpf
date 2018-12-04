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
    /// Interaction logic for Reservations.xaml
    /// </summary>
    public partial class Reservations : Window
    {
        public Reservations()
        {
            InitializeComponent();
            CBMeetingCentres.ItemsSource = DataStore.DisplayMeetingCentres2();
        }

        private void Button_Click_Default(object sender, RoutedEventArgs e)
        {

            
        }
    }
}
