using CommunityToolkit.Maui.Core.Platform;

namespace MicrobApp.Views;

public partial class SearchPage : ContentPage
{
    public SearchPage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        searchBar.Text = string.Empty;
        searchResults.EmptyView = null;
    }

    private async void searchButtonPressed(object sender, EventArgs e)
    {
        var results = new List<string>();
        searchResults.ItemsSource = results;
        if (results == null || results.Count == 0)
        {
            searchResults.EmptyView = Resources["EmptyView"];
        }
        _ = await KeyboardExtensions.HideKeyboardAsync(searchBar, default);
    }

    private void searchBar_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (string.IsNullOrEmpty(e.NewTextValue))
        {
            searchResults.EmptyView = null;
            searchResults.ItemsSource = null;
        }
    }
}