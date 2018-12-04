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
    /// Interaction logic for DeleteMR.xaml
    /// </summary>
    public partial class DeleteMR : Window
    {
        private MeetingRoom Mr { get; set; } 
        private readonly MainWindow _mw = ((MainWindow)Application.Current.MainWindow);
        public DeleteMR()
        {
            InitializeComponent();
            Mr = (MeetingRoom) _mw.MeetingRoomsBox.SelectedItem;
        }

        private void Btn_Click_Delete_MR(object sender, RoutedEventArgs e)
        {
            try
            {
                DataStore.Modified = true;

                _mw.textBox1.Text = Mr.Code;
                DataStore.DeleteMr(Mr);

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
                _mw.textBox1.Text = $"Error while deleting MC {exception}";
            }
            Close();
        }

        private void Btn_Click_Storno(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
