using MauiApp1.Models;
using MauiApp1.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.ViewModels
{
    public class WorkoutStatsVM : INotifyPropertyChanged
    {
        public int TotalWorkouts { get; set; }
        public int TotalCalories { get; set; }
        public double AverageCalories { get; set; }
        public int TotalMinutes { get; set; }
        public double AverageMinutes { get; set; }
        public double AverageIntensity { get; set; }
        public Dictionary<string, int> WorkoutsByType { get; set; } = new();

        // INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public WorkoutStatsVM()
        {
            // Start loading stats without using async void. Caller can await InitializeAsync if needed.
            _ = LoadStatsAsync();
        }

        private async Task LoadStatsAsync()
        {
            var workouts = await WorkoutDB.GetWorkoutsAsync() ?? new System.Collections.Generic.List<Workout>();

            TotalWorkouts = workouts.Count;
            TotalCalories = workouts.Sum(w => w.Calories);
            TotalMinutes = workouts.Sum(w => w.Duration);

            if (workouts.Any())
            {
                AverageCalories = Math.Round(workouts.Average(w => w.Calories), 1);
                AverageMinutes = Math.Round(workouts.Average(w => w.Duration), 1);
                AverageIntensity = Math.Round(workouts.Average(w => w.Intensity), 1);
            }
            else
            {
                AverageCalories = 0;
                AverageMinutes = 0;
                AverageIntensity = 0;
            }

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

