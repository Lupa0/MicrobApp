using MicrobApp.Models;
using MicrobApp.Services;
using System.Collections.ObjectModel;

namespace MicrobApp.Views;

public partial class HomePage : ContentPage
{
    private readonly PostService _postService;
    private readonly UserService _userService;

    private ObservableCollection<Post> posts = new();
    private readonly string username;
    private bool hasNext = false;
    private int currentPage = 1;
    private int totalCount = 0;
    private readonly int pageSize = 15;
    public Command LoadMorePostsCommand { get; set; }

    public HomePage()
    {
        InitializeComponent();
        _postService = new PostService();
        _userService = new UserService();
        username = SecureStorage.GetAsync("username").Result;
        BindingContext = this;
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        LoadPosts();
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        posts.Clear();
        hasNext = false;
        currentPage = 1;
        totalCount = 0;
        postListView.ItemsSource = null;
        loaderInit.IsVisible = true;

    }

    private async void LoadPosts()
    {
        loaderInit.IsRunning = true;
        try
        {
            PostPaginated pagination = await _userService.GetUserTimeline(currentPage, pageSize);
            postListView.ItemsSource = pagination.Posts;
            hasNext = pagination.HasNext;
            currentPage = pagination.CurrentPage;
            totalCount = pagination.TotalCount;
            postListView.RemainingItemsThreshold = hasNext ? 5 : 0;
            loaderInit.IsRunning = false;
            loaderInit.IsVisible = false;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error al obtener los posts: " + ex.Message);
            await DisplayAlert("Error", "Ha ocurrido un problema. Por favor vuelve a intentar mas tarde.", "OK");
        }
    }

    private async void LoadMorePosts(object sender, EventArgs e)
    {
        if (loaderInit.IsRunning) { return; }
        else if (posts.Count < totalCount)
        {
            loaderFooter.IsRunning = true;
            PostPaginated pagination = await _userService.GetUserTimeline(currentPage + 1, pageSize);
            foreach (Post post in pagination.Posts)
            {
                posts.Add(post);
            }
            hasNext = pagination.HasNext;
            currentPage = pagination.CurrentPage;
            postListView.RemainingItemsThreshold = hasNext ? 1 : 0;
            loaderFooter.IsRunning = false;
            loaderFooter.IsVisible = false;
        }
    }

    private void Add_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new PostPage());
    }

    private void GoToUserPerfil(object sender, EventArgs e)
    {
        var ListItem = sender as ImageButton;
        string userName = ListItem.CommandParameter.ToString(); //Username del usuario del post seleccionado
        Navigation.PushAsync(new ProfilePage(new UserService(), new PostService(), userName));
    }

    private void Redirect_to_settings(object sender, EventArgs e)
    {
        Navigation.PushAsync(new SettingsPage());
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        var ListItem = sender as Button;
        if (ListItem.BackgroundColor == Color.FromRgb(234, 92, 85))
        {
            ListItem.BorderColor = Color.FromRgb(255, 255, 255);
            String idPost = ListItem.CommandParameter.ToString();
            await _postService.LikePost(idPost);
        }
            
        else
            ListItem.BackgroundColor = Color.FromRgb(234,92,85);
    }

    private void OnLabelTapped(object sender, TappedEventArgs e)
    {
        //var ListItem = sender as TapGestureRecognizer;
        //String idPost = ListItem.CommandParameter.ToString();
        Label label = (Label)sender;

        // Obtén el BindingContext de la Label, que debe ser un objeto Post
        Post post = (Post)label.BindingContext;

        //Post seleccionado
        Navigation.PushAsync(new ViewPostPage(post));
    }

    private void AnswerPost(object sender, EventArgs e)
    {
        var ListItem = sender as Button;
        string inResponseTo = ListItem.CommandParameter.ToString();
        Navigation.PushAsync(new PostPage(inResponseTo, false));
    }
}