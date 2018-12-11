using System;
using System.Collections.Generic;

namespace wpfProjectJK01
{
    public class ExportToJson
    {
        public string meetingCentre { get; set; }
        public string meetingRoom { get; set; }
        public Dictionary<string , List<ReservationExport>> reservations { get; set; }

        public ExportToJson(string mc, string mr, Dictionary<string, List<ReservationExport>> res )
        {
            meetingCentre = mc;
            meetingRoom = mr;
            reservations = res;
        }
    }
}