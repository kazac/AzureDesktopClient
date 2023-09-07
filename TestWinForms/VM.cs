using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;
using static System.Net.WebRequestMethods;
using static System.Windows.Forms.AxHost;

namespace WinFormsApp1
{
    public partial class VM : ObservableObject
    {
        public HttpClient client = new();
        public string token = "";
        public VM()
        {
            client.BaseAddress = new Uri("https://eraz51.azurewebsites.net");
        }

        [ObservableProperty]
        string txt = "";

        [RelayCommand()]
        public async Task GetWeather()
        {
            var httpClient = new System.Net.Http.HttpClient();
            System.Net.Http.HttpResponseMessage response;
       //     try
       //     {
       //         var request = new System.Net.Http.HttpRequestMessage(System.Net.Http.HttpMethod.Get, "https://eraz51.azurewebsites.net/products");
       //         request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
       //         response = await httpClient.SendAsync(request);
       //         var content = await response.Content.ReadAsStringAsync();
       //     }
       //     catch (Exception ex)
       //     {
       //         int a = 2;
       ////         return ex.ToString();
       //     }

            try
            {
                var txt = await client.GetStringAsync("products");
                Txt = txt; // Property Txt is data bound to the Text Box
            }
            catch (Exception ex) 
            {
                var w = ex.Message;
            }
        }
    }
}
