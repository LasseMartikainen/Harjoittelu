using MySqlConnector;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Windows.Controls;
using System.Xml.Linq;

namespace UIProgrammingFinalExercise
{
    public class InvoiceRepository
    {


        private const string local = @"Server=127.0.0.1; Port=3306; User ID=opiskelija; Pwd=opiskelija1;";
        public const string localWithDb = @"Server=127.0.0.1; Port=3306; User ID=opiskelija; Pwd=opiskelija1; Database=InvoiceDb;";

        /// <summary>
        /// Tämä metodi luo tietokannan
        /// </summary>
        public void CreateInvoiceDb()
        {

            using (MySqlConnection conn = new MySqlConnection(local))
            {
                conn.Open();


                MySqlCommand cmd = new MySqlCommand("DROP DATABASE IF EXISTS InvoiceDb", conn);
                cmd.ExecuteNonQuery();

                cmd = new MySqlCommand("CREATE DATABASE InvoiceDb", conn);
                cmd.ExecuteNonQuery();
            }
        }


        /// <summary>
        /// Luo taulun laskuille
        /// </summary>
        public void CreateInvoiceTable()
        {
            using (MySqlConnection conn = new MySqlConnection(localWithDb))
            {
                conn.Open();

                string createTable = "CREATE TABLE Invoice" +
"(InvoiceNumber INT NOT NULL AUTO_INCREMENT," +
  "InvoiceDate DATE NOT NULL DEFAULT CURRENT_DATE," +
  "InvoiceTermOfPayment INT NOT NULL DEFAULT 30," +
  "InvoiceNoticeTime VARCHAR(40)," +
  "InvoiceOverdueInterest FLOAT NOT NULL DEFAULT 0," +
"InvoiceReference VARCHAR(40)," +
"InvoiceInfo VARCHAR(200)," +
  "CustomerNumber INT NOT NULL," +
  "PRIMARY KEY(InvoiceNumber)," +
  "FOREIGN KEY(CustomerNumber) REFERENCES Customer(CustomerNumber));";

                MySqlCommand cmd = new MySqlCommand(createTable, conn);
                cmd.ExecuteNonQuery();

            }
        }

        /// <summary>
        /// Luo taulun tuotteille
        /// </summary>
        public void CreateProductTable()
        {
            using (MySqlConnection conn = new MySqlConnection(localWithDb))
            {
                conn.Open();

                string createTable = "CREATE TABLE Product" +
"(ProductNumber INT NOT NULL AUTO_INCREMENT," +
  "ProductDesc VARCHAR(40) NOT NULL," +
  "ProductPrice FLOAT NOT NULL DEFAULT 0," +
  "ProductTax FLOAT NOT NULL DEFAULT 0," +
      "ProductQuantityDesc VARCHAR(5) NOT NULL," +
  "PRIMARY KEY (ProductNumber));";

                MySqlCommand cmd = new MySqlCommand(createTable, conn);
                cmd.ExecuteNonQuery();

            }
        }

        /// <summary>
        /// Luo taulun asiakkaille
        /// </summary>
        public void CreateCustomerTable()
        {
            using (MySqlConnection conn = new MySqlConnection(localWithDb))
            {
                conn.Open();

                string createTable = "CREATE TABLE Customer" +
"(CustomerNumber INT NOT NULL AUTO_INCREMENT," +
  "CustomerName VARCHAR(40) NOT NULL," +
  "CustomerStreet VARCHAR(40)," +
  "CustomerPostalCode VARCHAR(5)," +
  "CustomerCity VARCHAR(35)," +
  "PRIMARY KEY (CustomerNumber));";

                MySqlCommand cmd = new MySqlCommand(createTable, conn);
                cmd.ExecuteNonQuery();

            }
        }

        /// <summary>
        /// Luo taulun laskulta löytyville tuotteille
        /// </summary>
        public void CreateInvoiceProductTable()
        {
            using (MySqlConnection conn = new MySqlConnection(localWithDb))
            {
                conn.Open();

                string createTable = "CREATE TABLE InvoiceProduct" +
"(LineNumber INT NOT NULL AUTO_INCREMENT," +
  "Quantity FLOAT NOT NULL," +
"Discount FLOAT NOT NULL DEFAULT 0," +
  "InvoiceNumber INT NOT NULL," +
  "ProductNumber INT NOT NULL," +
  "PRIMARY KEY (LineNumber)," +
  "FOREIGN KEY (InvoiceNumber) REFERENCES Invoice(InvoiceNumber)," +
  "FOREIGN KEY (ProductNumber) REFERENCES Product(ProductNumber));";

                MySqlCommand cmd = new MySqlCommand(createTable, conn);
                cmd.ExecuteNonQuery();

            }
        }

        /// <summary>
        /// Tämä luo valmiita laskuja testattavaksi
        /// </summary>
        public void AddDefaultInvoices()
        {
            using (MySqlConnection conn = new MySqlConnection(localWithDb))
            {
                conn.Open();

                string invoice1 = "INSERT INTO Invoice (InvoiceNumber, InvoiceDate, InvoiceTermOfPayment, InvoiceNoticeTime, InvoiceOverdueInterest, InvoiceReference, CustomerNumber, InvoiceInfo) VALUES(101, '230326', 14, '7 päivää', 7.5, '', 1, '')";
                string invoice2 = "INSERT INTO Invoice (InvoiceNumber, InvoiceDate, InvoiceTermOfPayment, InvoiceNoticeTime, InvoiceOverdueInterest, InvoiceReference, CustomerNumber, InvoiceInfo) VALUES(102, '230322', 30, '30 päivää', 8.5, '', 1, '')";
                string invoice3 = "INSERT INTO Invoice (InvoiceNumber, InvoiceDate, InvoiceTermOfPayment, InvoiceNoticeTime, InvoiceOverdueInterest, InvoiceReference, CustomerNumber, InvoiceInfo) VALUES(103, '230226', 14, '', 7.5, '', 3, 'Toinen ikkunoista oli viallinen')";

                MySqlCommand cmd = new MySqlCommand(invoice1, conn);
                cmd.ExecuteNonQuery();

                cmd = new MySqlCommand(invoice2, conn);
                cmd.ExecuteNonQuery();

                cmd = new MySqlCommand(invoice3, conn);
                cmd.ExecuteNonQuery();


            }
        }

        /// <summary>
        /// Tämä luo valmiita tuotteita testiin
        /// </summary>
        public void AddDefaultProducts()
        {
            using (MySqlConnection conn = new MySqlConnection(localWithDb))
            {
                conn.Open();

                string product1 = "INSERT INTO Product (ProductNumber, ProductDesc, ProductPrice, ProductTax, ProductQuantityDesc) VALUES(1, 'Työ', 22.30, 24, 'h')";
                string product2 = "INSERT INTO Product (ProductNumber, ProductDesc, ProductPrice, ProductTax, ProductQuantityDesc) VALUES(2, 'Ikkuna', 200, 24, 'kpl')";
                string product3 = "INSERT INTO Product (ProductNumber, ProductDesc, ProductPrice, ProductTax, ProductQuantityDesc) VALUES(3, 'Kirja', 22.30, 10, 'kpl')";
                string product4 = "INSERT INTO Product (ProductNumber, ProductDesc, ProductPrice, ProductTax, ProductQuantityDesc) VALUES(4, 'Nauloja', 3.40, 24, 'ltk')";

                MySqlCommand cmd = new MySqlCommand(product1, conn);
                cmd.ExecuteNonQuery();

                cmd = new MySqlCommand(product2, conn);
                cmd.ExecuteNonQuery();

                cmd = new MySqlCommand(product3, conn);
                cmd.ExecuteNonQuery();

                cmd = new MySqlCommand(product4, conn);
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Tämä luo pari asiakasta testaustarkoituksiin
        /// </summary>
        public void AddDefaultCustomers()
        {
            using (MySqlConnection conn = new MySqlConnection(localWithDb))
            {
                conn.Open();

                string customer1 = "INSERT INTO Customer (CustomerNumber, CustomerName, CustomerStreet, CustomerPostalCode, CustomerCity) VALUES(1, 'Taavi Tilaaja', 'Tilauskuja 3', '83900', 'JUUKA')";
                string customer2 = "INSERT INTO Customer (CustomerNumber, CustomerName, CustomerStreet, CustomerPostalCode, CustomerCity) VALUES(2, 'Mummo Ankka', 'Maatilantie 14', '00500', 'ANKKALINNA')";
                string customer3 = "INSERT INTO Customer (CustomerNumber, CustomerName, CustomerStreet, CustomerPostalCode, CustomerCity) VALUES(3, 'Nettiasiakas', NULL, NULL, NULL)";

                MySqlCommand cmd = new MySqlCommand(customer1, conn);
                cmd.ExecuteNonQuery();

                cmd = new MySqlCommand(customer2, conn);
                cmd.ExecuteNonQuery();

                cmd = new MySqlCommand(customer3, conn);
                cmd.ExecuteNonQuery();

            }
        }

        /// <summary>
        /// Tämä luo testirivejä laskuille
        /// </summary>
        public void AddDefaultInvoiceProducts()
        {
            using (MySqlConnection conn = new MySqlConnection(localWithDb))
            {
                conn.Open();

                string invProd1 = "INSERT INTO InvoiceProduct (Quantity, Discount, InvoiceNumber, ProductNumber) VALUES(2, 0, 101, 1)";
                string invProd2 = "INSERT INTO InvoiceProduct (Quantity, Discount, InvoiceNumber, ProductNumber) VALUES(5, 10, 101, 2)";
                string invProd3 = "INSERT INTO InvoiceProduct (Quantity, Discount, InvoiceNumber, ProductNumber) VALUES(1, 0, 101, 3)";
                string invProd4 = "INSERT INTO InvoiceProduct (Quantity, Discount, InvoiceNumber, ProductNumber) VALUES(7, 5, 101, 4)";
                string invProd5 = "INSERT INTO InvoiceProduct (Quantity, Discount, InvoiceNumber, ProductNumber) VALUES(0.5, 0, 102, 1)";
                string invProd6 = "INSERT INTO InvoiceProduct (Quantity, Discount, InvoiceNumber, ProductNumber) VALUES(2, 0, 102, 4)";
                string invProd7 = "INSERT INTO InvoiceProduct (Quantity, Discount, InvoiceNumber, ProductNumber) VALUES(1, 0, 103, 2)";
                string invProd8 = "INSERT INTO InvoiceProduct (Quantity, Discount, InvoiceNumber, ProductNumber) VALUES(1, 50, 103, 2)";

                MySqlCommand cmd = new MySqlCommand(invProd1, conn);
                cmd.ExecuteNonQuery();

                cmd = new MySqlCommand(invProd2, conn);
                cmd.ExecuteNonQuery();

                cmd = new MySqlCommand(invProd3, conn);
                cmd.ExecuteNonQuery();

                cmd = new MySqlCommand(invProd4, conn);
                cmd.ExecuteNonQuery();

                cmd = new MySqlCommand(invProd5, conn);
                cmd.ExecuteNonQuery();

                cmd = new MySqlCommand(invProd6, conn);
                cmd.ExecuteNonQuery();

                cmd = new MySqlCommand(invProd7, conn);
                cmd.ExecuteNonQuery();

                cmd = new MySqlCommand(invProd8, conn);
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Hakee asiakkaat tietokannasta
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<Customer> GetCustomers()
        {
            var customers = new ObservableCollection<Customer>();

            using (MySqlConnection conn = new MySqlConnection(localWithDb))
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("SELECT * FROM Customer", conn);

                var dr = cmd.ExecuteReader();

                while (dr.Read())
                {

                    if (!dr.IsDBNull("CustomerStreet"))
                    {
                        customers.Add(new Customer(dr.GetInt32("CustomerNumber"), dr.GetString("CustomerName"), dr.GetString("CustomerStreet"), dr.GetString("CustomerPostalCode"), dr.GetString("CustomerCity")));
                    }
                    else
                    {
                        customers.Add(new Customer
                        {
                            CustomerNumber = dr.GetInt32("CustomerNumber"),
                            Name = dr.GetString("CustomerName"),
                        });
                    }

                }
            }

            return customers;
        }


        /// <summary>
        /// Tallentaa muutokset asiakkaiden tietoihin
        /// </summary>
        /// <param name="customers"></param>
        public void SaveCustomers(ObservableCollection<Customer> customers)
        {
            using (MySqlConnection conn = new MySqlConnection(localWithDb))
            {
                conn.Open();

                foreach (var customer in customers)
                {

                    if (customer.CustomerNumber == 0)
                    {
                        // INSERT
                        MySqlCommand cmdIns = new MySqlCommand("INSERT INTO Customer (CustomerName, CustomerStreet, CustomerPostalCode, CustomerCity) VALUES(@name, @street, @postalcode, @city)", conn);
                        cmdIns.Parameters.AddWithValue("@name", customer.Name);
                        cmdIns.Parameters.AddWithValue("@street", customer.Address.Street);
                        cmdIns.Parameters.AddWithValue("@postalcode", customer.Address.PostalCode);
                        cmdIns.Parameters.AddWithValue("@city", customer.Address.City);
                        cmdIns.ExecuteNonQuery();
                    }
                    else
                    {
                        // UPDATE
                        MySqlCommand cmdUpd = new MySqlCommand("UPDATE Customer SET CustomerName=@name, CustomerStreet=@street, CustomerPostalCode=@postalcode, CustomerCity=@city WHERE CustomerNumber=@number", conn);
                        cmdUpd.Parameters.AddWithValue("@name", customer.Name);
                        cmdUpd.Parameters.AddWithValue("@street", customer.Address.Street);
                        cmdUpd.Parameters.AddWithValue("@number", customer.CustomerNumber);
                        cmdUpd.Parameters.AddWithValue("@postalcode", customer.Address.PostalCode);
                        cmdUpd.Parameters.AddWithValue("@city", customer.Address.City);
                        cmdUpd.ExecuteNonQuery();

                    }


                }
            }
        }

        /// <summary>
        /// Hakee tuotteet tietokannasta
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<Product> GetProducts()
        {
            var products = new ObservableCollection<Product>();

            using (MySqlConnection conn = new MySqlConnection(localWithDb))
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("SELECT * FROM Product", conn);

                var dr = cmd.ExecuteReader();

                while (dr.Read())
                {

                        products.Add(new Product(dr.GetInt32("ProductNumber"), dr.GetString("ProductDesc"), dr.GetFloat("ProductPrice"), dr.GetFloat("ProductTax"), dr.GetString("ProductQuantityDesc")));

                }
            }

            return products;
        }

        /// <summary>
        /// Tallentaa muutokset tuotetietoihin
        /// </summary>
        /// <param name="products"></param>
        public void SaveProducts(ObservableCollection<Product> products)
        {
            using (MySqlConnection conn = new MySqlConnection(localWithDb))
            {
                conn.Open();

                foreach (var product in products)
                {

                    if (product.Number == 0)
                    {
                        // INSERT
                        MySqlCommand cmdIns = new MySqlCommand("INSERT INTO Product (ProductDesc, ProductPrice, ProductTax, ProductQuantityDesc) VALUES(@desc, @price, @tax, @quantityDesc)", conn);
                        cmdIns.Parameters.AddWithValue("@desc", product.Desc);
                        cmdIns.Parameters.AddWithValue("@price", product.Price);
                        cmdIns.Parameters.AddWithValue("@tax", product.Tax);
                        cmdIns.Parameters.AddWithValue("@quantityDesc", product.QuantityDesc);
                        cmdIns.ExecuteNonQuery();
                    }
                    else
                    {
                        // UPDATE
                        MySqlCommand cmdUpd = new MySqlCommand("UPDATE Product SET ProductDesc=@desc, ProductPrice=@price, ProductTax=@tax, ProductQuantityDesc=@quantityDesc WHERE ProductNumber=@number", conn);
                        cmdUpd.Parameters.AddWithValue("@desc", product.Desc);
                        cmdUpd.Parameters.AddWithValue("@price", product.Price);
                        cmdUpd.Parameters.AddWithValue("@tax", product.Tax);
                        cmdUpd.Parameters.AddWithValue("@quantityDesc", product.QuantityDesc);
                        cmdUpd.Parameters.AddWithValue("@number", product.Number);
                        cmdUpd.ExecuteNonQuery();

                    }


                }
            }
        }

        /// <summary>
        /// Hakee laskutiedot tietokannasta
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<Invoice> GetInvoices()
        {
            var invoices = new ObservableCollection<Invoice>();

            using (MySqlConnection conn = new MySqlConnection(localWithDb))
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("SELECT * FROM Invoice", conn);

                var dr = cmd.ExecuteReader();

                while (dr.Read())
                {

                    if (!dr.IsDBNull("InvoiceInfo"))
                    {
                        invoices.Add(new Invoice
                        {
                            Number = dr.GetInt32("InvoiceNumber"),
                            Date = dr.GetDateOnly("InvoiceDate"),
                            TermOfPayment = dr.GetInt32("InvoiceTermOfPayment"),
                            NoticeTime = dr.GetString("InvoiceNoticeTime"),
                            OverdueInterest = dr.GetFloat("InvoiceOverdueInterest"),
                            Info = dr.GetString("InvoiceInfo"),
                            CustomerNumber = dr.GetInt32("CustomerNumber")
                        });
                    }
                    else
                    {
                        invoices.Add(new Invoice
                        {
                            Number = dr.GetInt32("InvoiceNumber"),
                            Date = dr.GetDateOnly("InvoiceDate"),
                            TermOfPayment = dr.GetInt32("InvoiceTermOfPayment"),
                            NoticeTime = dr.GetString("InvoiceNoticeTime"),
                            OverdueInterest = dr.GetFloat("InvoiceOverdueInterest"),
                            CustomerNumber = dr.GetInt32("CustomerNumber")
                        });
                    }

                }
            }

            return invoices;
        }

        public void AddInvoice(Invoice invoice)
        {
            using (MySqlConnection conn = new MySqlConnection(localWithDb))
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("INSERT INTO Invoice(InvoiceNumber, InvoiceDate, InvoiceTermOfPayment, InvoiceNoticeTime, InvoiceOverdueInterest, InvoiceReference, InvoiceInfo, CustomerNumbere) VALUES(@number, @date, @termOfPayment, @noticeTime, @overdueInterest, @reference, @info, @customerNumber)", conn);
                cmd.Parameters.AddWithValue("@number", invoice.Number);
                cmd.Parameters.AddWithValue("@date", invoice.Date);
                cmd.Parameters.AddWithValue("@termOfPayment", invoice.TermOfPayment);
                cmd.Parameters.AddWithValue("@noticeTime", invoice.NoticeTime);
                cmd.Parameters.AddWithValue("@overdueInterest", invoice.OverdueInterest);
                cmd.Parameters.AddWithValue("@reference", invoice.Reference);
                cmd.Parameters.AddWithValue("@info", invoice.Info);
                cmd.Parameters.AddWithValue("@customerNumber", invoice.CustomerNumber);
                cmd.ExecuteNonQuery();

            }

        }

        public void RemoveInvoice(Invoice invoice)
        {
            using (MySqlConnection conn = new MySqlConnection(localWithDb))
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("DELETE FROM Invoices WHERE InvoiceNumber=@number" + "DELETE FROM InvoiceProduct WHERE InvoiceNumber=@number", conn);
                cmd.Parameters.AddWithValue("@number", invoice.Number);
                cmd.ExecuteNonQuery();

            }

        }

        public ObservableCollection<InvoiceProduct> GetInvoiceProducts()
        {
            var invoiceProducts = new ObservableCollection<InvoiceProduct>();

            using (MySqlConnection conn = new MySqlConnection(localWithDb))
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("SELECT * FROM InvoiceProduct", conn);

                var dr = cmd.ExecuteReader();

                while (dr.Read())
                {

                    invoiceProducts.Add(new InvoiceProduct(dr.GetInt32("LineNumber"), dr.GetFloat("Quantity"), dr.GetFloat("Discount"), dr.GetInt32("InvoiceNumber"), dr.GetInt32("ProductNumber")));

                }
            }

            return invoiceProducts;
        }

        public ObservableCollection<Line> GetInvoiceLines(Invoice invoice)
        {
            var lines = new ObservableCollection<Line>();

            using (MySqlConnection conn = new MySqlConnection(localWithDb))
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("SELECT p.ProductNumber, p.ProductDesc, ip.Quantity, p.ProductQuantityDesc, p.ProductPrice, ip.Discount, p.ProductTax FROM Product AS p, InvoiceProduct AS ip WHERE p.ProductNumber = ip.ProductNumber AND ip.InvoiceNumber = @invoiceNumber", conn);
                cmd.Parameters.AddWithValue("@invoiceNumber", invoice.Number);

                var dr = cmd.ExecuteReader();

                while (dr.Read())
                {

                    lines.Add(new Line
                    {
                        Code = dr.GetInt32("ProductNumber"),
                        Desc = dr.GetString("ProductDesc"),
                        Amount = dr.GetFloat("Quantity"),
                        Unit = dr.GetString("ProductQuantityDesc"),
                        Price = dr.GetFloat("ProductPrice"),
                        Discount = dr.GetFloat("Discount"),
                        TaxPer = dr.GetFloat("ProductTax"),
                        TaxAmount = dr.GetFloat("ProductPrice") * dr.GetFloat("Quantity") * (dr.GetFloat("ProductTax")/100),
                        TotalWoTax = dr.GetFloat("ProductPrice") * dr.GetFloat("Quantity"),
                        TotalWithTax = dr.GetFloat("ProductPrice") * dr.GetFloat("Quantity") + dr.GetFloat("ProductPrice") * dr.GetFloat("Quantity") * (dr.GetFloat("ProductTax") / 100)
                    });
                }
            }

            return lines;
        }

        public void AddInvoiceProduct(InvoiceProduct invoiceProduct)
        {
            using (MySqlConnection conn = new MySqlConnection(localWithDb))
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand("INSERT INTO InvoiceProduct (LineNumber, Quantity, Discount, InvoiceNumber, ProductNumber) VALUES(@lineNumber, @quantity, @discount, @invoiceNumber, @productNumber)", conn);
                cmd.Parameters.AddWithValue("@lineNumber", invoiceProduct.LineNumber);
                cmd.Parameters.AddWithValue("@quantity", invoiceProduct.Quantity);
                cmd.Parameters.AddWithValue("@discount", invoiceProduct.Discount);
                cmd.Parameters.AddWithValue("@invoiceNumber", invoiceProduct.InvoiceNumber);
                cmd.Parameters.AddWithValue("@productNumber", invoiceProduct.ProductNumber);
                cmd.ExecuteNonQuery();

            }

        }

    }
}
