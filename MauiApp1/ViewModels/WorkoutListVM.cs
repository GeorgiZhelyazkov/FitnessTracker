using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MauiApp1.Models;
using MauiApp1.Services;

namespace MauiApp1.ViewModels
{
    public class WorkoutListVM : INotifyPropertyChanged
    {
        public ObservableCollection<Workout> Workouts { get; set; } = [];
        public string NewName { get; set; } 
        public string SelectedType { get; set; } = "Силова";
        public int NewCalories { get; set; }
        public  double NewIntensity { get; set; } = 1;
        public int NewDuration { get; set; }
        public DateTime NewDate { get; set; }
        public ICommand AddWorkoutCommand { get; }
        public ICommand DeleteWorkoutCommand { get; }
        public ICommand ClearAllCommand { get; }
        public List<string> WorkoutTypes { get; } = ["Силова", "Кардио", "Разтягане"];



        public WorkoutListVM()
        {
            AddWorkoutCommand = new Command(async () => await AddWorkout());
            _ = LoadWorkouts();

            DeleteWorkoutCommand = new Command<Workout>(async (w) => await DeleteWorkout(w));

            ClearAllCommand = new Command(async () => await ClearAll());

        }

        private async Task LoadWorkouts()
        {
            var workouts = await WorkoutDB.GetWorkoutsAsync();
            Workouts.Clear();
            foreach (var w in workouts)
                Workouts.Add(w);
        }

        private async Task AddWorkout()
        {
            if (string.IsNullOrWhiteSpace(NewName) || NewDuration <= 0)
            {
                await Application.Current.MainPage.DisplayAlert("Грешка", "Моля, попълнете всички полета коректно.", "OK");
                return;
            }


            var newWorkout = new Workout
            {
                Name = NewName,
                Type = SelectedType,
                Calories = NewCalories > 0 ? NewCalories : EstimateCalories(SelectedType, NewDuration, NewIntensity),
                Duration = NewDuration,
                Intensity = NewIntensity,
                Date = NewDate
            };

            await WorkoutDB.AddWorkoutAsync(newWorkout);

            Workouts.Insert(0, newWorkout);

            // Нулиране на формата
            NewName = string.Empty;
            NewCalories = 0;
            NewDuration = 0;
            NewDate = DateTime.Now;

            OnPropertyChanged(nameof(NewName));
            OnPropertyChanged(nameof(NewCalories));
            OnPropertyChanged(nameof(NewDuration));
        }

        private int EstimateCalories(string type, int duration, double intensity)
        {
            int factor = type switch
            {
                "Силова" => 6,
                "Кардио" => 8,
                "Разтягане" => 3,
                _ => 5
            };

            return (int)(duration * factor * intensity);
        }

        private async Task DeleteWorkout(Workout workout)
        {
            if (workout == null) return;

            await WorkoutDB.DeleteWorkoutAsync(workout);
            Workouts.Remove(workout);
        }

        private async Task ClearAll()
        {
            await WorkoutDB.ClearAllAsync();
            Workouts.Clear();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}