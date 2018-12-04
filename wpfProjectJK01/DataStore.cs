using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;

namespace wpfProjectJK01
{
    public class DataStore
    {
        private static ObservableCollection<MeetingCentre> MeetingCentres { get; set; } = new ObservableCollection<MeetingCentre>();
        private static ObservableCollection<MeetingRoom> MeetingRooms { get; set; } = new ObservableCollection<MeetingRoom>();
        public static bool Modified { get; set; } = false;

        public void AddItem(MeetingRoom mr)
        {
            MeetingRooms.Add(mr);
        }

        public void AddItem(MeetingCentre mc)
        {
            MeetingCentres.Add(mc);
        }

        public static void AddMeetingCentre(MeetingCentre mc)
        {
            MeetingCentres.Add(mc);
        }

        public static void AddMeetingRoom(MeetingRoom mr)
        {
            MeetingRooms.Add(mr);
        }

        public ObservableCollection<MeetingRoom> DisplayMeetingRooms()
        {
            return MeetingRooms;
        }

        public ObservableCollection<MeetingCentre> DisplayMeetingCentres()
        {
            return MeetingCentres;
        }

        public static ObservableCollection<MeetingCentre> DisplayMeetingCentres2()
        {
            return MeetingCentres;
        }

        public static ObservableCollection<MeetingRoom> DisplayMeetingRooms2()
        {
            return MeetingRooms;
        }

        public List<MeetingRoom> DisMeetingRooms(MeetingCentre mc)
        {
            return MeetingRooms.Where(x => x.MeetingCentre == mc).ToList();
        }

        public static void SaveFile()
        {
            try
            {
                var destPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "export.csv");
                var sb = new StringBuilder();
                sb.AppendLine("MEETING_CENTRES");
                foreach (var mc in DataStore.DisplayMeetingCentres2())
                {
                    sb.AppendLine($"{mc.Name},{mc.Code},{mc.Description}");
                }

                sb.AppendLine("MEETING_ROOMS");
                foreach (var mr in DataStore.DisplayMeetingRooms2())
                {
                    var videoconferencing = mr.VideoConference ? "YES" : "NO";
                    sb.AppendLine(
                        $"{mr.Name},{mr.Code},{mr.Description},{mr.Capacity},{videoconferencing},{mr.MeetingCentre.Code}");
                }

                System.IO.File.WriteAllText(destPath, sb.ToString()); // File.WriteAllText() - not working????
            }
            catch (Exception e)
            {
                ((MainWindow)Application.Current.MainWindow).textBox1.Text = $"Error while saving data: {e}";
            }
        }
        

        public static void DeleteMc(MeetingCentre mc)
        {
            // Delete all mcs base on code
            var delmr = DisplayMeetingRooms2().Where(x => x.MeetingCentre == mc).ToList();
            foreach (var dmr in delmr)
            {
                MeetingRooms.Remove(dmr);
            }
            MeetingCentres.Remove(mc);

        }


        public static void DeleteMr(MeetingRoom mr)
        {
            // Delete mr
            MeetingRooms.Remove(mr);
        }

        public void ResetData()
        {
            MeetingCentres.Clear();
            MeetingRooms.Clear();
        }
    }
}