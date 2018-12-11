using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace wpfProjectJK01
{
    public class Reservation
    {
        /*
         *MeetingRoom – jedna rezervace je vždy naplánována na právě jednu místnost.
        Date – rezervace je vždy naplánována na jeden konkrétní den. Pro zjednodušení předpokládejte, že rezervace nemůže být naplánována na více dní.
        TimeFrom – TimeTo - čas „od kdy do kdy“ je rezervace (např. od 10:00 do 11:30).
        ExpectedPersonsCount – očekávaný počet osob (celé číslo v rozsahu 1..kapacita místnosti).
        Customer – jméno zákazníka (řetězec v rozsahu 2..100 znaků).
        VideoConference – zdali chce zákazník připravit zařízení na video konferenci (pouze pro místnosti, která umožňují video konferenci).
        Note – poznámka k rezervaci (řetězec v rozsahu 0..300 znaků).
         *
         *
         */
        public MeetingRoom MeetingRoom { get; set; }
        public string MRoom { get; set; }
        public DateTime Date { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public int ExpectedPersonsCount { get; set; }
        public string Customer { get; set; }
        public bool VideoConference { get; set; }
        public string Note { get; set; }
        public string DisplayInfo { get; set; }

        public Reservation(MeetingRoom mRoom, DateTime date, string from, string to, int expCount, string customer, bool video, string note)
        {
            MeetingRoom = mRoom;
            Date = date;
            From = from;
            To = to;
            ExpectedPersonsCount = expCount;
            Customer = customer;
            VideoConference = video;
            Note = note;
            DisplayInfo = $"{from} - {to} reservation made by {Customer}.";
        }

        public Reservation(string mRoom, DateTime date, string from, string to, int expCount, string customer, bool video, string note)
        {
            MRoom = mRoom;
            Date = date;
            From = from;
            To = to;
            ExpectedPersonsCount = expCount;
            Customer = customer;
            VideoConference = video;
            Note = note;
        }
    }
}