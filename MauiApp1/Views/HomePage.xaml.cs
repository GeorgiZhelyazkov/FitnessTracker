namespace MauiApp1.Views;

public partial class HomePage : ContentPage
{
	public HomePage()
	{
		InitializeComponent();
	}

    private async void OnStatsClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new StatsPage());
    }
}