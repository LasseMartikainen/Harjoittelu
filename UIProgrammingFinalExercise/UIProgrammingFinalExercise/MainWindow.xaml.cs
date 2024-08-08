using MySqlConnector;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UIProgrammingFinalExercise
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private InvoiceRepository repo;
        public static Creditor? rakennusOy;

        public MainWindow()
        {
            InitializeComponent();


            //Tässä luodaan tietokanta ohjelman käynnistyessä
            repo = new InvoiceRepository();
            repo.CreateInvoiceDb();
            repo.CreateCustomerTable();
            repo.CreateInvoiceTable();
            repo.CreateProductTable();
            repo.CreateInvoiceProductTable();

            //alkutiedot laskun lähettäjälle
            rakennusOy = new Creditor("Rakennus Oy", "Rakentajantie 2", "80100", "JOENSUU");
            rakennusOy.BusinessID = "987564-2";
            rakennusOy.BankBIC = "OKOYFIHH";
            rakennusOy.BankAccount = "FI12 1234 1234 1234 12";

            //testidatan generointi
            repo.AddDefaultCustomers();
            repo.AddDefaultProducts();
            repo.AddDefaultInvoices();
            repo.AddDefaultInvoiceProducts();

            repo.GetCustomers();
        }

        /// <summary>
        /// Kun painaa nappia, avautuu laskunkirjoitusikkuna
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickNewInvoice(object sender, RoutedEventArgs e)
        {
            NewInvoice newInvoiceWindow = new NewInvoice(1);
            this.Visibility = Visibility.Hidden;
            newInvoiceWindow.ShowDialog();
            this.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Painalluksesta asiakaslistaus
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickCustomers(object sender, RoutedEventArgs e)
        {
            Customers customersWindow = new Customers();
            this.Visibility = Visibility.Hidden;
            customersWindow.ShowDialog();
            this.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Painalluksesta asiakaslistaus
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickCompany(object sender, RoutedEventArgs e)
        {
            Company companyWindow = new Company(rakennusOy);
            this.Visibility = Visibility.Hidden;
            companyWindow.ShowDialog();
            this.Visibility = Visibility.Visible;
        }

        private void TietoaClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Tekijä: Lasse Martikainen, 2023", "Tietoa ohjelmasta");
        }

        private void ClickBackup(object sender, RoutedEventArgs e)
        {
            string path = @"c:\temp\backup.txt";

            if (File.Exists(path))
            {
                File.Delete(path);
            }

            using (StreamWriter sw = File.CreateText(path))
            {
                sw.WriteLine("Asiakasnumero, Nimi");

                using (MySqlConnection conn = new MySqlConnection(InvoiceRepository.localWithDb))
                {
                    conn.Open();

                    MySqlCommand cmd = new MySqlCommand("SELECT * FROM Customer", conn);

                    var dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        int number = dr.GetInt32("CustomerNumber");
                        string numberStr = number.ToString();
                        string name = dr.GetString("CustomerName");
                        sw.Write(numberStr + " ");
                        sw.WriteLine(name + " ");
                    }
                }


            }

            MessageBox.Show("Asiakastiedot varmuuskopioitu onnistuneesti!", "Varmuuskopio");
        }

        private void CloseClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ClickProducts(object sender, RoutedEventArgs e)
        {
            Products productsWindow = new Products();
            this.Visibility = Visibility.Hidden;
            productsWindow.ShowDialog();
            this.Visibility = Visibility.Visible;
        }
    }
}
