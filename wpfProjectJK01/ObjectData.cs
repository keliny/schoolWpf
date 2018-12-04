using System.ComponentModel.DataAnnotations;
using System.Data;

namespace wpfProjectJK01
{
    public abstract class ObjectData
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }

        protected ObjectData(string name, string code, string description)
        {
            Name = name;
            Code = code;
            Description = description;
        }

    }
}