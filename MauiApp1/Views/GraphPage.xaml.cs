using MauiApp1.Models;

namespace MauiApp1.Views;

public partial class GraphPage : ContentPage
{
    public GraphPage(List<Workout> workouts)
	{
		InitializeComponent();

        double maxCalories = workouts.Max(w => w.Calories);
        maxCalories = maxCalories == 0 ? 1 : maxCalories;

        foreach (var workout in workouts)
        {
            double normalizedHeight = (workout.Calories / maxCalories) * 200;

            var stack = new VerticalStackLayout
            {
                Spacing = 5,
                Children =
                    {
                        new Label
                        {
                            Text = $"{workout.Calories} ккал",
                            FontSize = 12,
                            HorizontalTextAlignment = TextAlignment.Center
                        },
                        new BoxView
                        {
                            HeightRequest = normalizedHeight,
                            WidthRequest = 30,
                            Color = Colors.MediumPurple,
                            VerticalOptions = LayoutOptions.End
                        },
                        new Label
                        {
                            Text = workout.Name,
                            FontSize = 12,
                            HorizontalTextAlignment = TextAlignment.Center
                        }
                    }
            };

            ChartLayout.Children.Add(stack);
        }

    }

    public GraphPage()
    {
        InitializeComponent();
    }
}