using CommunityToolkit.Mvvm.Input;
using Microsoft.Identity.Client;
using System.Net.Http;
using System.Reflection;
using System.Xml.Linq;
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
            treeProductCategories.Nodes.Clear();
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
                    MessageBox.Show($"Error Acquiring Token:{System.Environment.NewLine}{msalex}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unexpected Error Acquiring Token Silently:{System.Environment.NewLine}{ex}");
                return;
            }

            if (authResult != null)
            {
                vm.token = authResult.AccessToken;
                vm.client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authResult.AccessToken);
                this.btnLoad.DataBindings.Add(new System.Windows.Forms.Binding("Command", vm, "LoadCommand", true));
                this.btnSave.DataBindings.Add(new System.Windows.Forms.Binding("Command", vm, "SaveCommand", true));
                txtProductId.DataBindings.Add(new Binding("Text", bsP, nameof(Product.ProductId)));
                txtProductCategoryId.DataBindings.Add(new Binding("Text", bsP, nameof(Product.ProductCategoryId)));
                txtColor.DataBindings.Add(new Binding("Text", bsP, nameof(Product.Color)));
                txtListCost.DataBindings.Add(new Binding("Text", bsP, nameof(Product.ListPrice)));
                txtName.DataBindings.Add(new Binding("Text", bsP, nameof(Product.Name)));
                DataContext = vm;

                treeProductCategories.Nodes.Add("0", "All");
                vm.ProductCategories.CollectionChanged += (s, e) =>
                {
                    foreach (ProductCategory pc in e.NewItems)
                    {
                        if (0 == pc.ProductCategoryId)
                        {
                            throw new ApplicationException("unexpected");
                        }
                        var node = treeProductCategories.Nodes.Find((pc.ParentProductCategoryId ?? 0).ToString(), true).FirstOrDefault();
                        node.Nodes.Add(pc.ProductCategoryId.ToString(), pc.Name);
                    }
                };
            }
        }

        private void Form1_DataContextChanged(object sender, EventArgs e)
        {
            bsPC.DataSource = DataContext;
            bsP.DataSource = DataContext;
        }

        private void treeProductCategories_AfterSelect(object sender, TreeViewEventArgs e)
        {
            FilterGridForNode(e.Node, false);
        }

        private void FilterGridForNode(TreeNode node, bool force)
        {
            var key = int.Parse(node.Name);
            dataGridView2.CurrentCell = null;
            foreach (DataGridViewRow dgvr in dataGridView2.Rows)
            {
                var p = dgvr.DataBoundItem as Product;
                if (p != null)
                {
                    var show = (p.ProductCategoryId == key);
                    dgvr.Visible = show;
                }
            }
        }
    }
}