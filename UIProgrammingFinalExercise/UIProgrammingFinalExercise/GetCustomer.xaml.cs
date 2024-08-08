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
    /// Interaction logic for GetCustomer.xaml
    /// </summary>
    public partial class GetCustomer : Window
    {
        private InvoiceRepository repo;

        public GetCustomer()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            repo = new InvoiceRepository();
            var customers = repo.GetCustomers();

            int insertedNbr = Int32.Parse(tbGetCustomer.Text);

            bool isFound = false;

            foreach (Customer customer in customers)
            {
                if (insertedNbr == customer.CustomerNumber)
                {
                    isFound= true;
                }

                    }

            if (!isFound)
            {
                MessageBox.Show("Eipä löytynyt!");
            }
            else
            {
                new NewInvoice(insertedNbr);
                Close();
            }
        }
    }
}
