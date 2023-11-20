namespace MicrobApp.Views;
using MicrobApp.Models;
using MicrobApp.Services;
using System.Text.Json;

public partial class InstancePage : ContentPage
{
    private readonly InstanceService _instanceService;

    public InstancePage(InstanceService instanceService)
	{
		InitializeComponent();
        _instanceService = instanceService;
        Loaded += InstancePage_Loaded;
    }
             //   await SecureStorage.SetAsync("tenantId", instance.TenantInstanceId.ToString());
             //   await Shell.Current.GoToAsync("//HomePage");

    async void InstancePage_Loaded(object sender, EventArgs e)
    {
        _ = StartInstances();
    }
    private async Task StartInstances()
    {

        List<Instance> instances = await _instanceService.GetActiveInstance();
        if (instances.Count == 0)
        {
            await DisplayAlert("Advertencia", "Lo lamento, no hay instancias activas", "OK");
            return;
        }
        var instancesList = new List<Task>();
        await stackList.TranslateTo(0, -75);
        instancesList.Add(stackParentOfAll.FadeTo(1, 1000, Easing.BounceOut));
        instancesList.Add(stackList.TranslateTo(0, 0, 1000));

        await Task.WhenAll(instancesList);
    }
}