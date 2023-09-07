using CommunityToolkit.Mvvm.Input;
using Microsoft.Identity.Client;
using System.Net.Http;
using System.Reflection;
using static System.Formats.Asn1.AsnWriter;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            var vm = new VM();
            DataContext = vm;
            this.button1.DataBindings.Add(new System.Windows.Forms.Binding("Command", this.bindingSource1, "GetWeatherCommand", true));
            // https://learn.microsoft.com/en-us/azure/active-directory/develop/tutorial-v2-windows-desktop
            AuthenticationResult authResult = null;
            var app = Program.PublicClientApp;
            var accounts = await app.GetAccountsAsync();
            var firstAccount = accounts.FirstOrDefault();
            string[] scopes = new string[] { "api://c2ebe4c8-e6b9-436b-a9bd-d63eb301bcc7/access_as_user" }; 
            try
            {
                authResult = await app.AcquireTokenSilent(scopes, firstAccount).ExecuteAsync();
            }
            catch (MsalUiRequiredException ex)
            {
                // A MsalUiRequiredException happened on AcquireTokenSilent.
                // This indicates you need to call AcquireTokenInteractive to acquire a token
                System.Diagnostics.Debug.WriteLine($"MsalUiRequiredException: {ex.Message}");
                try
                {
                    authResult = await app.AcquireTokenInteractive(scopes)
                  .WithPrompt(Prompt.SelectAccount)
                  .ExecuteAsync();
                }
                catch (MsalException msalex)
                {
                    int a = 2;
                    //      ResultText.Text = $"Error Acquiring Token:{System.Environment.NewLine}{msalex}";
                }
            }
            catch (Exception ex)
            {
                int a = 2;
                //             ResultText.Text = $"Error Acquiring Token Silently:{System.Environment.NewLine}{ex}";
                return;
            }

            if (authResult != null)
            {
                vm.token = authResult.AccessToken;
                vm.client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authResult.AccessToken);
            }
        }

        private void Form1_DataContextChanged(object sender, EventArgs e)
        {
            bindingSource1.DataSource = DataContext;
        }
    }
}