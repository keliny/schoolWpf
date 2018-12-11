using System.Collections.Generic;

namespace wpfProjectJK01
{
    public class ReservationJson
    {
        public string Schema { get; set; }
        public string Uri { get; set; }
        public List<ExportToJson> Data { get; set; } = new List<ExportToJson>();
    }
}