using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Models
{
    public class Workout
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Type { get; set; }  = string.Empty;
        public string Name { get; set; } = string.Empty;
        public int Duration { get; set; }
        public int Calories { get; set; }
        public double Intensity { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
