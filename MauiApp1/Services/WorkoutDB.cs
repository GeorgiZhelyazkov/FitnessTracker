using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using MauiApp1.Models;

namespace MauiApp1.Services
{
    public class WorkoutDB
    {
        private static SQLiteAsyncConnection? _database;

        public static async Task Init()
        {
            if (_database != null)
                return;

            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "workouts.db");
            _database = new SQLiteAsyncConnection(dbPath);

            await _database.CreateTableAsync<Workout>();
        }

        public static async Task<List<Workout>> GetWorkoutsAsync()
        {
            await Init();
            return await _database!.Table<Workout>().OrderByDescending(w => w.Date).ToListAsync();
        }

        public static async Task AddWorkoutAsync(Workout workout)
        {
            await Init();
            await _database!.InsertAsync(workout);
        }

        public static async Task DeleteWorkoutAsync(Workout workout)
        {
            await Init();
            await _database!.DeleteAsync(workout);
        }

        public static async Task ClearAllAsync()
        {
            await Init();
            await _database!.DeleteAllAsync<Workout>();
        }
    }
}
