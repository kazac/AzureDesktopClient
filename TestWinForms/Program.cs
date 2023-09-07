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

// https://blog.jongallant.com/2021/02/azure-rest-apis-postman-2021/
// az ad sp create-for-rbac --role Contributor --scopes /subscriptions/d21cbd9d-0910-4702-93de-6f244c0a146c
//{
//    "appId": "74c369a5-5528-4332-af54-9dac0237e187",
//  "displayName": "azure-cli-2023-09-01-05-45-36",
//  "password": "TKz8Q~JTbMG6.6E66Ea5XpTxpR2OPJEwfM_.Namo",
//  "tenant": "3525791b-235b-42df-bd1d-d42b70c7cec7"
//}


// https://learn.microsoft.com/en-us/samples/azure-samples/active-directory-dotnet-native-aspnetcore-v2/1-desktop-app-calls-web-api/

// https://learn.microsoft.com/en-us/samples/azure-samples/active-directory-dotnet-native-aspnetcore-v2/1-desktop-app-calls-web-api/