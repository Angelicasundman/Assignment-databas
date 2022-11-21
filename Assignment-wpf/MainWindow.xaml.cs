using Assignment.Context;
using Assignment.Models;
using Assignment.Models.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Assignment_wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<OrderRowsEntity> _produts = new();


        public MainWindow()
        {
            InitializeComponent();
            PopulateProductCb().ConfigureAwait(false);

        }

        public async Task PopulateProductCb()
        {
            var collection = new ObservableCollection<KeyValuePair<int, string>>();
            using var client = new HttpClient();

            foreach (var item in await client.GetFromJsonAsync<IEnumerable<ProductModel>>("https://localhost:7231/api/products"))
                collection.Add(new KeyValuePair<int, string>(item.Id, item.Name));

            cb_product.ItemsSource = collection;
            await PopulateCustomerCb().ConfigureAwait(false);
        }
        public async Task PopulateCustomerCb()
        {
            var customer = new ObservableCollection<KeyValuePair<int, string>>();
            using var client = new HttpClient();

            foreach (var customers in await client.GetFromJsonAsync<IEnumerable<CustomerModel>>("https://localhost:7231/api/Customers"))
                customer.Add(new KeyValuePair<int, string>(customers.Id, customers.Name));

            cb_customer.ItemsSource = customer;

        }

        private async void btn_AddToList_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var product = (KeyValuePair<int, string>)cb_product.SelectedItem;
                var productId = product.Key;
                var productValue = product.Value;
                using var client = new HttpClient();
                _produts.Add(await client.GetFromJsonAsync<OrderRowsEntity>($"https://localhost:7231/api/produts/{productId}"));
                
                lvProducts.ItemsSource = _produts;
                cb_product.SelectedIndex = -1;


            }
            catch { MessageBox.Show("Failed"); }
        }

        private void btn_PutOrder_Click(object sender, RoutedEventArgs e)
        {

        }
    }

    

}

    

