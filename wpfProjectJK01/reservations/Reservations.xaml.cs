using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
using Newtonsoft.Json;
using wpfProjectJK01.reservations;
using Path = System.IO.Path;

namespace wpfProjectJK01
{
    /// <summary>
    /// Interaction logic for Reservations.xaml
    /// </summary>
    public partial class Reservations : Window
    {
        public ObservableCollection<Reservation> ReservationsCollection { get; set; } =
            new ObservableCollection<Reservation>();

        public Reservations()
        {
            InitializeComponent();
            CBMeetingCentres.ItemsSource = DataStore.DisplayMeetingCentres2();
            MeetingsList.ItemsSource = ReservationsCollection;
        }


        // Selecting Meeting center
        private void CBMeetingCentres_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var index = CBMeetingCentres.SelectedIndex;
            if (index == -1)
            {
                // Do not allow any change to further logic without selecting a Meeting Center
                CBMeetingRoom.ItemsSource = "";
                CBMeetingRoom.IsEnabled = false;
                DatePlanner.IsEnabled = false;
                BtNewReservation.IsEnabled = false;
                BtnEditReservation.IsEnabled = false;
                BtnDeleteReservation.IsEnabled = false;
                CLearReservationList();
            }
            else
            {
                // Meeting center selected = enable meeting rooms
                CBMeetingRoom.IsEnabled = true;
                CBMeetingRoom.ItemsSource = DataStore.DisplayMeetingRooms2()
                    .Where(x => x.MeetingCentre == DataStore.DisplayMeetingCentres2()[index]);
                CLearReservationList();
                DatePlanner.SelectedDate = null;
            }
        }


        // selecting meeting rooms
        private void CBMeetingRoom_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // room selected = enable date selection
            DatePlanner.SelectedDate = null;
            DatePlanner.IsEnabled = true;

            var mr = CBMeetingRoom.SelectedItem as MeetingRoom;
            CLearReservationList();
        }

        private void DateSelectedChange(object sender, SelectionChangedEventArgs selectionChangedEventArgs)
        {
            /*
             * if (((DateTime)value).ToShortDateString() == "31/12/2999")
            return "select a date";

            sender: DisplayDate: Day, Month, Year
             */

            // On selecting date enable further functionality - create reservation
            // and fill reservation list with data
            BtNewReservation.IsEnabled = true;
            BtnEditReservation.IsEnabled = false;
            BtnDeleteReservation.IsEnabled = false;
            UpdateMeetingList();
        }

        // edit reservations
        private void Button_Click_EditReservation(object sender, RoutedEventArgs e)
        {
            if (MeetingsList.SelectedIndex == -1)
            {
                MessageBox.Show("No Meeting selected. To edit a Meeting please select one.");
            }
            else
            {
                var meetingRoom = CBMeetingRoom.SelectedItem as MeetingRoom;
                var meetingCenter = CBMeetingCentres.SelectedItem as MeetingCentre;
                DateTime date = (DateTime) DatePlanner.SelectedDate;
                var reserved = MeetingsList.SelectedItem as Reservation;

                var editReserv = new EditReservation(meetingCenter, meetingRoom, date, meetingRoom.Capacity, reserved);
                editReserv.ShowDialog();
                UpdateMeetingList();
            }
        }

        // Add reservation
        private void Button_Click_NewReservation(object sender, RoutedEventArgs e)
        {
            var meetingRoom = CBMeetingRoom.SelectedItem as MeetingRoom;
            var meetingCenter = CBMeetingCentres.SelectedItem as MeetingCentre;
            DateTime date = (DateTime) DatePlanner.SelectedDate;

            var newReserv = new AddReservation(meetingCenter, meetingRoom, date, meetingRoom.Capacity);
            newReserv.ShowDialog();

            UpdateMeetingList();
        }

        private void SelectionChangedMeetingsList(object sender, SelectionChangedEventArgs e)
        {
            if (MeetingsList.SelectedIndex == -1)
            {
                BtnEditReservation.IsEnabled = false;
                BtnDeleteReservation.IsEnabled = false;
            }
            else
            {
                BtnEditReservation.IsEnabled = true;
                BtnDeleteReservation.IsEnabled = true;

                var reserve = MeetingsList.SelectedItem as Reservation;
                var from = reserve.From.Split(':');
                var to = reserve.To.Split(':');
                FromH.Text = from[0];
                FromM.Text = from[1];
                ToH.Text = to[0];
                ToM.Text = to[1];
                ExpectedPersonsCountTextBlock.Text = reserve.ExpectedPersonsCount.ToString();
                CustomerTextBox.Text = reserve.Customer;
                MrVideoConference.IsChecked = reserve.VideoConference;
                NoteTextBox.Text = reserve.Note;
            }
        }

        private void Button_Click_DeleteReservation(object sender, RoutedEventArgs e)
        {
            var res = MeetingsList.SelectedItem as Reservation;
            DataStore.RemoveReservation(res);
            DataStore.Modified = true;
            UpdateMeetingList();
        }

        private void UpdateMeetingList()
        {
            ReservationsCollection.Clear();
            var meetingRoom = CBMeetingRoom.SelectedItem as MeetingRoom;
            foreach (var reservation in DataStore.DisplayReservations().Where(r =>
                r.Date == DatePlanner.SelectedDate && r.MeetingRoom.Code == meetingRoom.Code))
            {
                ReservationsCollection.Add(reservation);
            }

            ReservationsCollection.OrderBy(x => x.From);
        }

        private void CLearReservationList()
        {
            ReservationsCollection.Clear();
            FromH.Text = "";
            FromM.Text = "";
            ToH.Text = "";
            ToM.Text = "";
            ExpectedPersonsCountTextBlock.Text = "";
            CustomerTextBox.Text = "";
            MrVideoConference.IsChecked = false;
            NoteTextBox.Text = "";
        }

        // Exporting only rooms with reservations, rest is ignored
        private void Button_Click_ExportToJson(object sender, RoutedEventArgs e)
        {
            // init the list
            var export = new List<ExportToJson>();
            var res = DataStore.DisplayReservations();
            foreach (var mr in DataStore.DisplayMeetingRooms2())
            {
                // Check if the room has any reservations
                if (res.Any(x => x.MeetingRoom.Code == mr.Code))
                {
                    var dictOfReservations = new Dictionary<string, List<ReservationExport>>();
                    // reservation find 1 or more
                    var reserv = res.Where(x => x.MeetingRoom.Code == mr.Code).ToList();

                    // add them to dictionary
                    foreach (var rese in reserv)
                    {
                        var date = rese.Date.ToShortDateString();
                        if (dictOfReservations.ContainsKey(date))
                        {
                            dictOfReservations[date].Add(new ReservationExport(rese.From, rese.To,
                                rese.ExpectedPersonsCount, rese.Customer, rese.VideoConference, rese.Note));
                        }
                        else
                        {
                            dictOfReservations.Add(date, new List<ReservationExport>());
                            dictOfReservations[date].Add(new ReservationExport(rese.From, rese.To,
                                rese.ExpectedPersonsCount, rese.Customer, rese.VideoConference, rese.Note));
                        }
                    }

                    export.Add(new ExportToJson(mr.MeetingCentre.Code, mr.Code, dictOfReservations));
                }
            }
            // write export to file

            try
            {
                var destPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "export.json");
                var reservationJSON = new ReservationJson
                {
                    Schema = "PLUS4U.EBC.MCS.MeetingRoom_Schedule_1.0",
                    Uri = "ues:UCL-BT:UCL.INF/DEMO_REZERVACE:EBC.MCS.DEMO/MR001/SCHEDULE",
                    Data = export
                };

                var jsonString = JsonConvert.SerializeObject(reservationJSON);

                using (var sw = new StreamWriter(destPath))
                {
                    sw.Write(jsonString);
                }
            }
            catch (Exception a)
            {
                ((MainWindow) Application.Current.MainWindow).textBox1.Text = $"Error while saving data: {a}";
            }
        }
    }
}