using MauiApp1.ViewModels;
using MauiApp1.Services;
namespace MauiApp1.Views;

public partial class StatsPage : ContentPage
{
	public StatsPage()
	{
		InitializeComponent();
	}

    private async void OnGraphClicked(object sender, EventArgs e)
    {
        var workouts = await WorkoutDB.GetWorkoutsAsync();
        await Navigation.PushAsync(new GraphPage(workouts));
    }
}