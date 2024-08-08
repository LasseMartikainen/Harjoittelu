using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace UIProgrammingFinalExercise
{
    /// <summary>
    /// Interaction logic for Products.xaml
    /// </summary>
    public partial class Products : Window
    {
        private InvoiceRepository repo;

        public Products()
        {
            InitializeComponent();

            repo = new InvoiceRepository();
            var products = repo.GetProducts();
            this.DataContext = products;
            myGrid.ItemsSource = products;
        }


        private void Peruuta_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Tallentaa dataan tehdyt muutokset
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Tallenna_Click(object sender, RoutedEventArgs e)
        {
            var products = (ObservableCollection<Product>)this.DataContext;
            repo.SaveProducts(products);

            products = repo.GetProducts();
            this.DataContext = products;
            myGrid.ItemsSource = products;

            Close();
        }
    }
}
