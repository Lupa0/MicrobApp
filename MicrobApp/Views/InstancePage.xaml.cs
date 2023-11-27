namespace MicrobApp.Views;
using MicrobApp.Models;
using MicrobApp.Services;

public partial class InstancePage : ContentPage
{
    private readonly InstanceService _instanceService;

    public InstancePage(InstanceService instanceService)
	{
		InitializeComponent();
        _instanceService = instanceService;
        StartInstances();
    }

    private async void StartInstances()
    {
        List<Instance> instances = await _instanceService.GetActiveInstance();
        stackList.ItemsSource = instances;
    }

    private async void stackList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
       
        // En algún lugar de tu código donde manejas la selección del ListView
        var selectedItem = e.SelectedItem;

        if (selectedItem != null && selectedItem is Instance)
        {
            // Realizar el casting al tipo Instance
            Instance instanciaSeleccionada = (Instance)selectedItem;

        // Ahora puedes trabajar con la instanciaSeleccionada
            String idTenant =instanciaSeleccionada.TenantInstanceId.ToString();

            await SecureStorage.SetAsync("tenantId", idTenant);
            await Shell.Current.GoToAsync("//LoginPage");




            // Realizar las operaciones necesarias con la instancia seleccionada
        }
    }
}