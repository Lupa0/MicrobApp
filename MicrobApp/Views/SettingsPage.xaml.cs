namespace MicrobApp.Views
{
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();
            string instanceName = SecureStorage.GetAsync("instanceName").Result;
            var menuItems = new List<MenuItem>
            {
                //new MenuItem { Text = "Notificaciones", IconImageSource = "notifications_icon.svg" },
                new MenuItem { Text = $"Servidor {instanceName}", IconImageSource = "format_list_bulleted_icon.svg" },
                new MenuItem { Text = "Acerca de Microb.uy", IconImageSource = "help_icon.svg" },
                new MenuItem { Text = "Cerrar sesión", IconImageSource = "logout_icon.svg" }
            };

            menuCollectionView.ItemsSource = menuItems;
        }

        private async void MenuCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.FirstOrDefault() is MenuItem selectedItem)
            {
                string text = selectedItem.Text;
                // Maneja la redirección o cualquier otra acción según el ítem seleccionado
                if(text.Equals("Acerca de Microb.uy"))
                {
                    Console.WriteLine("Info de la app");
                    // Redirige a la pantalla de información
                    await Navigation.PushAsync(new InformationPage());
                } else if (text.Equals("Cerrar sesión"))
                {
                    SecureStorage.RemoveAll();
                    await Shell.Current.GoToAsync("../InstancePage");
                } else if (text.StartsWith("Servidor"))
                {
                    Console.WriteLine("Info del servidor");
                    // Redirige a la pantalla de información
                    await Navigation.PushAsync(new ServerInformation());
                }

                // Desmarca la selección del ítem
                menuCollectionView.SelectedItem = null;
            }
        }
    }
}
