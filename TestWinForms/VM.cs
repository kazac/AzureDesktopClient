using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;
using static System.Net.WebRequestMethods;
using static System.Windows.Forms.AxHost;

namespace WinFormsApp1
{
    public partial class ProductCategory : ObservableObject
    {
        [ObservableProperty]
        private int productCategoryId;
        [ObservableProperty]
        private int? parentProductCategoryId;
        [ObservableProperty]
        private string name;
        [ObservableProperty]
        private DateTime modifiedDate;
    }

    public partial class Product : ObservableObject
    {
        [ObservableProperty]
        int productId;
        [ObservableProperty]
        int? productCategoryId;
        [ObservableProperty]
        string name;
        [ObservableProperty]
        string productNumber;
        [ObservableProperty]
        string? color;
        [ObservableProperty]
        decimal listPrice;
    }

    public partial class VM
    {
        public HttpClient client = new();
        public string token = "";
        public VM()
        {
            client.BaseAddress = new Uri("https://eraz51.azurewebsites.net");
            ProductCategories = new ObservableCollection<ProductCategory>();
            Products = new BindingList<Product>();
        }

        public ObservableCollection<ProductCategory> ProductCategories { get; private set; }
        public BindingList<Product> Products { get; private set; }  

        [RelayCommand()]
        public async Task Load()
        {
            var httpClient = new System.Net.Http.HttpClient();
            System.Net.Http.HttpResponseMessage response;
            try
            {
                {
                    response = await client.GetAsync("productCategories");
                    var content = await response.Content.ReadAsStringAsync();
                    var dto = JsonSerializer.Deserialize<ProductCategoryDTO[]>(content);
                    foreach (var x in dto)
                        ProductCategories.Add(new ProductCategory() { ProductCategoryId = x.productCategoryId, ParentProductCategoryId = x.parentCategoryID, Name = x.name, ModifiedDate = x.modifiedDate });
                }
                {
                    response = await client.GetAsync("products");
                    var content = await response.Content.ReadAsStringAsync();
                    var dto = JsonSerializer.Deserialize<ProductDTO[]>(content);
                    foreach (var x in dto)
                        Products.Add(new Product() { ProductCategoryId = x.productCategoryId, ProductId = x.productId, Name = x.name, Color = x.color, ListPrice = x.listPrice, ProductNumber = x.productNumber});
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
        }

        [RelayCommand()]
        public async Task Save()
        {
            var httpClient = new System.Net.Http.HttpClient();
            System.Net.Http.HttpResponseMessage response;
            try
            {
                {
                    var dtos = new List<ProductDTO>();
                    foreach (var p in Products)
                        dtos.Add(new ProductDTO(productCategoryId: p.ProductCategoryId, productId: p.ProductId, name: p.Name, color: p.Color, listPrice: p.ListPrice, productNumber: p.ProductNumber));

                    var json = JsonSerializer.Serialize<ProductDTO[]>(dtos.ToArray());
                    var content = new StringContent(json);
                    response = await client.PutAsync("products", content);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
