using System;
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
        public DateTime Date { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public int ExpectedPersonsCount { get; set; }
        public string Customer { get; set; }
        public bool VideoConference { get; set; }
        [Required(ErrorMessage = "Note is required!!!")]
        public string Note { get; set; }

    }
}