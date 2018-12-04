using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace wpfProjectJK01
{
    public class MeetingRoom : ObjectData
    {
        public int Capacity { get; set; }
        public bool VideoConference { get; set; }
        public MeetingCentre MeetingCentre { get; set; }

        public MeetingRoom(string name, string code, string description, int capacity, bool videoConference, MeetingCentre meetingCenter) : base(name, description, code)
        {
            Capacity = capacity;
            VideoConference = videoConference;
            MeetingCentre = meetingCenter;
        }

        public string RoomsName => $"{Name}: {Code}";
    }
}