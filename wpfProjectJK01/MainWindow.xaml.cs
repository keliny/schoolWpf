using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Path = System.IO.Path;

namespace wpfProjectJK01
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public DataStore dataStore { get; set; } = new DataStore();

        public MainWindow()
        {
            InitializeComponent();
            MeetingCentresBox.ItemsSource = dataStore.DisplayMeetingCentres();
            DataStore.ImportDataFromXml();
        }

        private void MenuItem_Click_Import(object sender, RoutedEventArgs e)
        {
            ImportDataApp();
        }

        private void MenuItem_Click_Reservation(object sender, RoutedEventArgs e)
        {
            var reserv = new Reservations();
            reserv.ShowDialog();
        }


        private void MenuItem_Click_Exit(object sender, RoutedEventArgs e)
        {
            
            if (DataStore.Modified)
            {
                var exit = new ExitCheck();
                exit.ShowDialog();
            }
            else
            {
                Application.Current.Shutdown();
            }
        }

        private void MenuItem_Click_Save(object sender, RoutedEventArgs e)
        {
            SaveFile();
        }

        private void ImportDataApp()
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".csv";
            dlg.Filter = "CSV Files (*.csv)|*.csv";


            // Display OpenFileDialog by calling ShowDialog method 
            dlg.ShowDialog();

            try
            {
                // read the file
                using (var srReader = new StreamReader(dlg.FileName))
                {
                    dataStore.ResetData();
                    var destination = 1; // 1 = Meeting Centre, 2 = Meeting Room
                    string currentLine;
                    while ((currentLine = srReader.ReadLine()) != null)
                    {
                        if (currentLine.Contains("MEETING_ROOMS"))
                        {
                            destination = 2;
                        }
                        else if (currentLine.Contains("MEETING_CENTRES"))
                        {
                            destination = 1;
                        }
                        else
                        {
                            switch (destination)
                            {
                                case 1:
                                {
                                    //public MeetingCentre(string name, string code, string description) : base(name, code, description)
                                    var processLine = currentLine.Split(',');
                                    dataStore.AddItem(new MeetingCentre(processLine[0], processLine[1],
                                        processLine[2]));
                                    break;
                                }
                                case 2:
                                    var processLine2 = currentLine.Split(',');
                                    dataStore.AddItem(new MeetingRoom(processLine2[0],
                                        processLine2[1],
                                        processLine2[2],
                                        Int32.Parse(processLine2[3]),
                                        (processLine2[4] == "YES"),
                                        dataStore.DisplayMeetingCentres()
                                            .First(x =>
                                                x.Code == processLine2[
                                                    5]))); //,dataStore.DisplayMeetingCentres().Find(x => x.Code == processLine2[5])
                                    break;
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                textBox1.Text = ($"Error while reading the file. {exception.Message}");
            }
        }

        private void SaveFile()
        {
            DataStore.SaveFile();
        }


        private void MeetingCentresBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var index = MeetingCentresBox.SelectedIndex;
            if (index == -1)
            {
                // empty MeetingRoombox after importing data
                MeetingRoomsBox.ItemsSource = "";
            }
            else
            {
                MeetingRoomsBox.ItemsSource = dataStore.DisplayMeetingRooms()
                    .Where(x => x.MeetingCentre == dataStore.DisplayMeetingCentres()[index]);
            }
        }

        private void Button_Click_New_MC(object sender, RoutedEventArgs e)
        {
            var createMc = new CreateMC();
            createMc.ShowDialog();
        }

        private void Button_Click_Edit_MC(object sender, RoutedEventArgs e)
        {
            if (MeetingCentresBox.SelectedIndex == -1)
            {
                MessageBox.Show("No Meeting Centre selected. To modify Meeting Centre please first select one.");
            }
            else
            {
                var editMc = new EditMC();
                editMc.ShowDialog();
            }
        }

        private void Button_Click_Delete_MC(object sender, RoutedEventArgs e)
        {
            if (MeetingCentresBox.SelectedIndex == -1)
            {
                MessageBox.Show("No Meeting Centre selected. To delete Meeting Centre please first select one.");
            }
            else
            {
                var deleteMc = new DeleteMC();
                deleteMc.ShowDialog();
            }
        }

        private void Button_Click_Create_MR(object sender, RoutedEventArgs e)
        {
            if (MeetingCentresBox.SelectedIndex == -1)
            {
                MessageBox.Show(
                    "No Meeting Centre selected. To create a Meeting Room please select Meeting Centre first.");
            }
            else
            {
                var createMr = new CreateMR();
                createMr.ShowDialog();
            }
        }

        private void Button_Click_Edit_MR(object sender, RoutedEventArgs e)
        {
            if (MeetingCentresBox.SelectedIndex == -1)
            {
                MessageBox.Show("No Meeting Room selected. To edit Meeting Room please first select one.");
            }
            else
            {
                var editMr = new EditMR();
                editMr.ShowDialog();
            }
        }

        private void Button_Click_Delete_MR(object sender, RoutedEventArgs e)
        {
            if (MeetingCentresBox.SelectedIndex == -1)
            {
                MessageBox.Show("No Meeting Room selected. To delete Meeting Room please first select one.");
            }
            else
            {
                var deleteMr = new DeleteMR();
                deleteMr.ShowDialog();
            }
        }
    }
}