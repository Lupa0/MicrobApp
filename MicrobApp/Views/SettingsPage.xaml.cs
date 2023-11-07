using MicrobApp.Models;

namespace MicrobApp.Views
{
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage(Instance instance)
        {
            InitializeComponent();

            var menuItems = new List<MenuItem>
            {
                new MenuItem { Text = "Notificaciones", IconImageSource = "notifications_icon.svg" },
                new MenuItem { Text = "Servidor", IconImageSource = "format_list_bulleted_icon.svg" },
                new MenuItem { Text = "Acerca de Microb.uy", IconImageSource = "help_icon.svg" },
                new MenuItem { Text = "Cerrar sesión", IconImageSource = "logout_icon.svg" }
            };

            menuCollectionView.ItemsSource = menuItems;
        }

        private async void MenuCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.FirstOrDefault() is MenuItem selectedItem)
            {
                // Maneja la redirección o cualquier otra acción según el ítem seleccionado
                switch (selectedItem.Text)
                {
                    case "Notificaciones":
                        Console.WriteLine("Ajustes Notificaciones");
                        // Redirige a la pantalla de notificaciones
                        await Shell.Current.GoToAsync("..");
                        break;
                    case "Servidor":
                        Console.WriteLine("Informacion del Servidor");
                        // Redirige a la pantalla de reglas
                        await Shell.Current.GoToAsync("..");
                        break;
                    case "Acerca de Microb.uy":
                        Console.WriteLine("Info de la app");
                        // Redirige a la pantalla de información
                        await Navigation.PushAsync(new InformationPage());
                        break;
                    case "Cerrar sesión":
                        SecureStorage.RemoveAll();
                        await Shell.Current.GoToAsync("../LoginPage");
                        break;
                }

                // Desmarca la selección del ítem
                menuCollectionView.SelectedItem = null;
            }
        }
    }
}
