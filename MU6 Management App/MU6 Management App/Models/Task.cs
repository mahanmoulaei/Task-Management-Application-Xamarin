using SQLite;

namespace MU6_Management_App.Models
{
    public class Task
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public string Key { get; set; }
        public string Description { get; set; }
        public string Priority { get; set; }
    }
}
