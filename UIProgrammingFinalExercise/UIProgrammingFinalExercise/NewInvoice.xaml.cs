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
    /// Interaction logic for NewInvoice.xaml
    /// </summary>
    public partial class NewInvoice : Window
    {


        private InvoiceRepository repo;
        private Invoice invoice;
        private Customer customer;
        private InvoiceProduct invoiceProduct;
        public NewInvoice(int custnbr)
        {
            InitializeComponent();


            labelSenderName.Content = MainWindow.rakennusOy.Name + "   (y: " + MainWindow.rakennusOy.BusinessID + ")";
            labelSenderStreet.Content = MainWindow.rakennusOy.Address.Street;
            labelSenderPostal.Content = MainWindow.rakennusOy.Address.PostalCode + " " +MainWindow.rakennusOy.Address.City;
            labelBIC.Content = MainWindow.rakennusOy.BankBIC;
            labelIBAN.Content = MainWindow.rakennusOy.BankAccount;

            repo = new InvoiceRepository();
            var invoices = repo.GetInvoices();
            invoice = invoices[1];
            spInvoice.DataContext= invoice;

            var customers = repo.GetCustomers();

            var invoiceCustomer = customers[custnbr-1];

            foreach (Customer customer in customers)
            {
                if (customer.CustomerNumber == invoice.CustomerNumber)
                {
                    invoiceCustomer = customer;
                }
            }

            tbCustName.Text = invoiceCustomer.Name;
            tbCustStreet.Text = invoiceCustomer.Address.Street;
            tbCustPostalCode.Text = invoiceCustomer.Address.PostalCode;
            tbCustCity.Text = invoiceCustomer.Address.City;

            var listItems = repo.GetInvoiceLines(invoice);
            this.DataContext = listItems;
            productGrid.ItemsSource = listItems;

            float totalTax = 0;
            float totalWoTax = 0;
            float totalWithTax = 0;

            foreach (var item in listItems)
            {
                totalTax += item.TaxAmount;
            }

            foreach (var item in listItems)
            {
                totalWoTax += item.TotalWoTax;
            }

            foreach (var item in listItems)
            {
                totalWithTax += item.TotalWithTax;
            }

            labelTotalTax.Content = totalTax;
            labelTotalWoTax.Content = totalWoTax;
            labelTotalWithTax.Content = totalWithTax;
        }
    

        private void Peruuta_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void GetButtonClick(object sender, RoutedEventArgs e)
        {
            GetCustomer getCustomer = new GetCustomer();
            getCustomer.ShowDialog();
        }
    }
}
