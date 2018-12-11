using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Xml;
using System.Xml.Linq;

namespace wpfProjectJK01
{
    public class DataStore
    {
        private static ObservableCollection<MeetingCentre> MeetingCentres { get; set; } =
            new ObservableCollection<MeetingCentre>();

        private static ObservableCollection<MeetingRoom> MeetingRooms { get; set; } =
            new ObservableCollection<MeetingRoom>();

        private static ObservableCollection<Reservation> ReservationList { get; set; } =
            new ObservableCollection<Reservation>();

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
                ((MainWindow) Application.Current.MainWindow).textBox1.Text = $"Error while saving data: {e}";
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

        public static ObservableCollection<Reservation> DisplayReservations()
        {
            return ReservationList;
        }

        public static void AddReservation(Reservation reservation)
        {
            ReservationList.Add(reservation);
        }

        public static void ExportXmlEndData()
        {
            var destPathRes = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "reservations.xml");
            var destPathMC = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MeetingCentres.xml");
            var destPathMR = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MeetingRooms.xml");

            XmlWriterSettings setting = new XmlWriterSettings();
            setting.ConformanceLevel = ConformanceLevel.Auto;

            // Load a list of reservations
            var reservations = DisplayReservations();
            // create xml from reservations list
            var xmlfromLINQReservations = new XElement("Reservations",
                from r in reservations
                select new XElement("Reservation",
                    new XElement("reservationMRCode", r.MeetingRoom.Code),
                    new XElement("reservationDate", r.Date),
                    new XElement("reservationFrom", r.From),
                    new XElement("reservationTo", r.To),
                    new XElement("reservationExpectedPersonsCount", r.ExpectedPersonsCount),
                    new XElement("reservationCustomer", r.Customer),
                    new XElement("reservationVideoConference", r.VideoConference),
                    new XElement("reservationNote", r.Note)
                )
            );

            // save reservations to a xml file
            using (var sw = new StreamWriter(destPathRes))
            {
                sw.Write(xmlfromLINQReservations);
            }

            // New backup for MC
            var mcs = DisplayMeetingCentres2();
            var xmlfromLINQMC = new XElement("MeetingCentres",
                from c in mcs
                select new XElement("MeetingCentre",
                    new XElement("McName", c.Name),
                    new XElement("McCode", c.Code),
                    new XElement("McDescription", c.Description)
                )
            );

            using (var sw = new StreamWriter(destPathMC))
            {
                sw.Write(xmlfromLINQMC);
            }

            // New backup for MR
            var mrs = DisplayMeetingRooms2();
            var xmlfromLINQMR = new XElement("MeetingRooms",
                from r in mrs
                select new XElement("MeetingRoom",
                    new XElement("MrName", r.Name),
                    new XElement("MrCode", r.Code),
                    new XElement("MrDescription", r.Description),
                    new XElement("MrCapacity", r.Capacity),
                    new XElement("MrVideoconference", r.VideoConference),
                    new XElement("MrMeetingCentreCode", r.MeetingCentre.Code)
                )
            );

            using (var sw = new StreamWriter(destPathMR))
            {
                sw.Write(xmlfromLINQMR);
            }
        }

        public static void ImportDataFromXml()
        {
            var destPathRes = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "reservations.xml");
            var destPathMC = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MeetingCentres.xml");
            var destPathMR = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MeetingRooms.xml");

            // Meeting centres load
            var mcs = XElement.Load(destPathMC);
            var meetingCentres = mcs.Elements();

            foreach (var mc in meetingCentres)
            {
                AddMeetingCentre(new MeetingCentre(mc.Element("McName")?.Value, mc.Element("McCode")?.Value,
                    mc.Element("McDescription")?.Value));
            }

            // Meeting Rooms load
            var mrs = XElement.Load(destPathMR);
            var meetingRooms = mrs.Elements();

            foreach (var mr in meetingRooms)
            {
                AddMeetingRoom(new MeetingRoom(
                    mr.Element("MrName").Value,
                    mr.Element("MrCode").Value,
                    mr.Element("MrDescription").Value,
                    int.Parse(mr.Element("MrCapacity").Value),
                    mr.Element("MrVideoconference").Value != "false",
                    DisplayMeetingCentres2().First(x => x.Code == (mr.Element("MrMeetingCentreCode").Value))
                ));
            }

            // Reservations load
            var res = XElement.Load(destPathRes);
            var reservations = res.Elements();

            foreach (var re in reservations)
            {
                AddReservation(new Reservation(
                    DisplayMeetingRooms2().First(x => x.Code == (re.Element("reservationMRCode").Value)),
                    Convert.ToDateTime(re.Element("reservationDate").Value),
                    re.Element("reservationFrom").Value,
                    re.Element("reservationTo").Value,
                    int.Parse(re.Element("reservationExpectedPersonsCount").Value),
                    re.Element("reservationCustomer").Value,
                    re.Element("reservationVideoConference").Value != "false",
                    re.Element("reservationNote").Value
                ));
            }
        }

        public static void RemoveReservation(Reservation reservation)
        {
            ReservationList.Remove(reservation);
        }
    }
}