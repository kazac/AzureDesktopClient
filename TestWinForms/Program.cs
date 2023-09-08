using Microsoft.Identity.Client;
using Microsoft.VisualBasic.ApplicationServices;

namespace WinFormsApp1
{
    internal static class Program
    {
        private static IPublicClientApplication _clientApp;
        private static string ClientId = "c2ebe4c8-e6b9-436b-a9bd-d63eb301bcc7"; 

        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            _clientApp = PublicClientApplicationBuilder.Create(ClientId)
            .WithAuthority(AzureCloudInstance.AzurePublic, "3525791b-235b-42df-bd1d-d42b70c7cec7")
            .WithRedirectUri("http://localhost:1234")
            .Build();
            Application.Run(new Form1());
        }

        public static IPublicClientApplication PublicClientApp { get { return _clientApp; } }

    }
}
