using System.Collections.Generic;

namespace web_04_ef_sqlite.Models.Database
{
    public class Location
    {

        public Location()
        {
            this.Students = new List<Student>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Student> Students { get; set; }

    }
}