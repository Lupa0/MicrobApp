using MicrobApp.Models;
using MicrobApp.Services;

namespace MicrobApp.Views;

public partial class ServerInformation : ContentPage
{
    private readonly InstanceService instanceService;
    public Instance Instance { get; private set; }

    public ServerInformation()
	{
		InitializeComponent();
        instanceService = new InstanceService();
        BindingContext = this;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        
        Instance = await instanceService.GetInstanceByDomain();
        if (Instance.Logo != null)
        {
            Logo.Source = Instance.Logo;
        }
        else
        {
            Logo.IsVisible = false;
        }

        PageTitle.Text = $"Acerca de {Instance.Nombre}";
        Description.Text = Instance.Description;
        CreatedIn.Text = Instance.CreationDate.ToShortDateString();

    }
}