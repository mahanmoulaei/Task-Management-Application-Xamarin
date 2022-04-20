using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MU6_Management_App.Models
{
    internal class Task
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Description { get; set; }

        public string Priority { get; set; }

        public Task(string description, string priority)
        {
            this.ID = ID;
            this.Description = description;
            this.Priority = priority;
        }

    }
}
