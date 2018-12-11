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

namespace wpfProjectJK01.reservations
{
    /// <summary>
    /// Interaction logic for EditReservation.xaml
    /// </summary>
    public partial class EditReservation : Window
    {
        // validation status for: count, hour, minutes, hour, minutes, customer, note(we are modifying existing reservation so all should be valid)
        public bool[] Valid { get; set; } = { true, true, true, true, true, true, true };
        public MeetingCentre MCenter { get; set; }
        public MeetingRoom MRoom { get; set; }
        public DateTime Date { get; set; }
        public int Capacity { get; set; }
        public bool VideoConference { get; set; }
        public Reservation Reservat { get; set; }

        public EditReservation(MeetingCentre mCenter, MeetingRoom mRoom, DateTime date, int capacity, Reservation reservation)
        {
            InitializeComponent();
            MCenter = mCenter;
            MRoom = mRoom;
            Date = date;
            Capacity = capacity;
            VideoConference = MRoom.VideoConference;
            Reservation_VideoConference.IsEnabled = VideoConference;
            Reservat = reservation;

            // Pre fill data
            MC_Name.SelectedText = MCenter.CentreName;
            MR_Name.SelectedText = MRoom.RoomsName;
            Reservation_Date.SelectedDate = Date;

            FromHour.Text = reservation.From.Split(':')[0];
            FromMinutes.Text = reservation.From.Split(':')[1];
            ToHour.Text = reservation.From.Split(':')[0];
            ToMinutes.Text = reservation.From.Split(':')[1];
            Reservation_person_count.Text = reservation.ExpectedPersonsCount.ToString();
            Reservation_customer.Text = reservation.Customer;
            Reservation_VideoConference.IsChecked = reservation.VideoConference;
            Reservation_Note.Text = reservation.Note;
        }

        private void Btn_Click_Update_reservation(object sender, RoutedEventArgs e)
        {
            if (Valid.Contains(false))
            {
                MessageBox.Show("Not all data are valid.");
            }
            else
            {
                var from = FromHour.Text + ":" + FromMinutes.Text;
                var to = ToHour.Text + ":" + ToMinutes.Text;
                var count = int.Parse(Reservation_person_count.Text);
                var customer = Reservation_customer.Text;
                var video = (bool)Reservation_VideoConference.IsChecked;
                var note = Reservation_Note.Text;

                /*
                Reservat.Customer = customer;
                Reservat.ExpectedPersonsCount = int.Parse(Reservation_person_count.Text);
                Reservat.From = FromHour.Text + ":" + FromMinutes.Text;
                Reservat.To = ToHour.Text + ":" + ToMinutes.Text;
                Reservat.Note = Reservation_Note.Text;
                Reservat.VideoConference = (bool)Reservation_VideoConference.IsChecked;
                Reservat.DisplayInfo = $"{from} - {to} reservation made by {customer}.";
                */

                DataStore.RemoveReservation(Reservat);
                DataStore.AddReservation(new Reservation(MRoom, Date, from, to, count, customer, video, note));
                DataStore.Modified = true;
                Close();
            }

        }

        private void Btn_Click_Storno(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ValidateCount(object sender, RoutedEventArgs e)
        {
            Valid[0] = ValidationRules.ValidateCount(sender, e, Capacity);
        }

        private void ValidateFromH1(object sender, RoutedEventArgs e)
        {
            Valid[1] = ValidationRules.ValidateTimeHour(sender, e);
        }

        private void ValidateFromH2(object sender, RoutedEventArgs e)
        {
            Valid[2] = ValidationRules.ValidateTimeHour(sender, e);
        }

        private void ValidateFromM1(object sender, RoutedEventArgs e)
        {
            Valid[3] = ValidationRules.ValidateTimeMinutes(sender, e);
        }

        private void ValidateFromM2(object sender, RoutedEventArgs e)
        {
            Valid[4] = ValidationRules.ValidateTimeMinutes(sender, e);
        }

        private void ValidateCustomer(object sender, RoutedEventArgs e)
        {
            Valid[5] = ValidationRules.ValidateName(sender, e);
        }

        private void ValidateNote(object sender, RoutedEventArgs e)
        {
            Valid[6] = ValidationRules.ValidateNote(sender, e);
        }

    }
}
