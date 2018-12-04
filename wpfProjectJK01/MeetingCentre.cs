using System.Collections.Generic;

namespace wpfProjectJK01
{
    public class MeetingCentre : ObjectData
    {
        public List<MeetingRoom> MeetingRooms { get; set; } = new List<MeetingRoom>();
        public MeetingCentre(string name, string code, string description) : base(name, code, description)
        {

        }

        public string CentreName => $"{Name}: {Code}";

    }
}