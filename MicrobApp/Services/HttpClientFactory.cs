using System.Net.Http.Headers;

public class HttpClientFactory
{
    public static HttpClient CreateHttpClient()
    {
        var httpClientHandler = new HttpClientHandler();

        // Permite llamadas a localhost desde la aplicación en el emulador Android
        httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;

        var httpClient = new HttpClient(httpClientHandler);
        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        // Establece la base address según la plataforma

        //Url base para Backend local
        httpClient.BaseAddress = new Uri(DeviceInfo.Platform == DevicePlatform.Android ? "https://10.0.2.2:7131" : "https://localhost:7131");

        //Url Base para Backend en la nube
        //httpClient.BaseAddress = new Uri("https://microbuywebapi.azurewebsites.net");

        return httpClient;
    }

    public static HttpClient CreateHttpClientWithToken()
    {
        var httpClient = CreateHttpClient();
        var token = SecureStorage.GetAsync("token").Result;
        httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

        return httpClient;
    }
}
