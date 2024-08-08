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
    /// Interaction logic for Invoices.xaml
    /// </summary>
    public partial class Invoices : Window
    {
        public Invoices()
        {
            InitializeComponent();
        }
        private void Peruuta_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Avaa_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
