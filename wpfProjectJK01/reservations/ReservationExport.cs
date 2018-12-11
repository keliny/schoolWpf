namespace wpfProjectJK01
{
    public class ReservationExport
    {
        public string From { get; set; }
        public string To { get; set; }
        public int ExpectedPersonsCount { get; set; }
        public string Customer { get; set; }
        public bool VideoConference { get; set; }
        public string Note { get; set; }

        public ReservationExport(string from, string to, int expCount, string customer, bool video, string note)
        {
            From = from;
            To = to;
            ExpectedPersonsCount = expCount;
            Customer = customer;
            VideoConference = video;
            Note = note;
        }
       

    }
}