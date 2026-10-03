using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiApp1.Models;
using MauiApp1.Services;

namespace MauiApp1.ViewModels
{
    public class WorkoutStatsVM : WorkoutListVM
    {
        public int TotalWorkouts { get; set; }
        public int TotalCalories { get; set; }
        public double AverageCalories { get; set; }
        public int TotalMinutes { get; set; }
        public double AverageMinutes { get; set; }
        public double AverageIntensity { get; set; }
        public Dictionary<string, int> WorkoutsByType { get; set; } = new();

        public WorkoutStatsVM()
        {
            LoadStats();
        }

        private async void LoadStats()
        {
            var workouts = await WorkoutDB.GetWorkoutsAsync();

            TotalWorkouts = workouts.Count;
            TotalCalories = workouts.Sum(w => w.Calories);
            AverageCalories = Math.Round(workouts.Average(w => w.Calories), 1);
            TotalMinutes = workouts.Sum(w => w.Duration);
            AverageMinutes = Math.Round(workouts.Average(w => w.Duration), 1);
            AverageIntensity = Math.Round(workouts.Average(w => w.Intensity), 1);

            WorkoutsByType = workouts
                .GroupBy(w => w.Type)
                .ToDictionary(g => g.Key, g => g.Count());

            OnPropertyChanged(nameof(TotalWorkouts));
            OnPropertyChanged(nameof(TotalCalories));
            OnPropertyChanged(nameof(AverageCalories));
            OnPropertyChanged(nameof(TotalMinutes));
            OnPropertyChanged(nameof(AverageMinutes));
            OnPropertyChanged(nameof(AverageIntensity));
            OnPropertyChanged(nameof(WorkoutsByType));
        }
    }
}
