using MauiApp1.Models;
using System.Linq;
using Microsoft.Maui.Graphics;

namespace MauiApp1.Views;

public partial class GraphPage : ContentPage
{
    public GraphPage(List<Workout>? workouts) : this()
    {
        try
        {
            if (workouts == null || workouts.Count == 0)
            {
                ChartLayout.Children.Add(new Label
                {
                    Text = "No data available",
                    FontSize = 14,
                    HorizontalTextAlignment = TextAlignment.Center,
                    VerticalOptions = LayoutOptions.Center
                });

                return;
            }

            double maxCalories = workouts.Max(w => w.Calories);
            if (maxCalories <= 0)
                maxCalories = 1;

            foreach (var workout in workouts)
            {
                double normalizedHeight = (workout.Calories / maxCalories) * 200.0;

                var stack = new VerticalStackLayout
                {
                    Spacing = 5
                };

                stack.Children.Add(new Label
                {
                    Text = $"{workout.Calories} kcal",
                    FontSize = 12,
                    HorizontalTextAlignment = TextAlignment.Center
                });

                stack.Children.Add(new BoxView
                {
                    HeightRequest = normalizedHeight,
                    WidthRequest = 30,
                    Color = Colors.MediumPurple,
                    VerticalOptions = LayoutOptions.End
                });

                stack.Children.Add(new Label
                {
                    Text = string.IsNullOrWhiteSpace(workout.Name) ? "(Unnamed)" : workout.Name,
                    FontSize = 12,
                    HorizontalTextAlignment = TextAlignment.Center
                });

                ChartLayout.Children.Add(stack);
            }
        }
        catch (Exception ex)
        {
            // Fail gracefully in UI; show simple message instead of crashing the app
            ChartLayout.Children.Clear();
            ChartLayout.Children.Add(new Label
            {
                Text = "Unable to render chart.",
                FontSize = 14,
                TextColor = Colors.Red,
                HorizontalTextAlignment = TextAlignment.Center
            });

            System.Diagnostics.Debug.WriteLine($"GraphPage error: {ex}");
        }
    }

    public GraphPage()
    {
        InitializeComponent();
    }
}
