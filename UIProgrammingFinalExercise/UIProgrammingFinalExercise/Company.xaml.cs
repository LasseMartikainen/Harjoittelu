using System;
using System.Collections.Generic;
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
    /// Interaction logic for Company.xaml
    /// </summary>
    public partial class Company : Window
    {
        public static Creditor company;

        public Company(Creditor rakennusOy)
        {
            InitializeComponent();
            company = rakennusOy;
            tbBusinessID.Text = company.BusinessID;
            tbName.Text = company.Name;
            tbStreet.Text = company.Address.Street;
            tbPostalCode.Text = company.Address.PostalCode;
            tbCity.Text = company.Address.City;
            tbIBAN.Text = company.BankAccount;
            tbBIC.Text = company.BankBIC;
        }

        private void Peruuta_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Tallenna_Click(object sender, RoutedEventArgs e)
        {
            company.BusinessID= tbBusinessID.Text;
            company.Name= tbName.Text;
            company.Address.Street= tbStreet.Text;
            company.Address.PostalCode = tbPostalCode.Text;
            company.Address.City = tbCity.Text;
            company.BankAccount = tbIBAN.Text;
            company.BankBIC= tbBIC.Text;
            MainWindow.rakennusOy = company;
            MessageBox.Show("Yrityksen tiedot päivitetty tämän käyttökerran ajalle. Jos haluat muuttaa tietoja pysyvästi, ota yhteyttä järjestelmän ylläpitäjään.");
            Close();
        }
    }


}
