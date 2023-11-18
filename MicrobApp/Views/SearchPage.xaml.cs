using CommunityToolkit.Maui.Core.Platform;
using MicrobApp.Services;

namespace MicrobApp.Views;

public partial class SearchPage : ContentPage
{
    private string searchedUsername = null;
    private string searchedText = null;

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

    private async void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (string.IsNullOrEmpty(e.NewTextValue))
        {
            searchResults.EmptyView = null;
            searchResults.ItemsSource = null;
        }
        else
        {
            searchedText = e.NewTextValue;
            string instanceDomain = await SecureStorage.GetAsync("instanceDomain");
            searchedUsername = $"{searchedText}@{instanceDomain}";
            List<string> suggestions = new()
            {
            $"Ir a {searchedUsername}",
            $"Publicaciones con '{searchedText}'",
            };
            searchResults.ItemsSource = suggestions;

        }
    }

    private async void searchResults_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is string selectedItem)
        {
            if (selectedItem.StartsWith("Publicaciones con"))
            {
                Console.WriteLine(selectedItem);
            }
            else if (selectedItem.StartsWith("Ir a "))
            {                
                await Navigation.PushAsync(new ProfilePage(new UserService(), new PostService(), searchedUsername));
            }

            searchResults.SelectedItem = null;
            searchResults.EmptyView = null;
            searchResults.ItemsSource = null;
        }
    }
}