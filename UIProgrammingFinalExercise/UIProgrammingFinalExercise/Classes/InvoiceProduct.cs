namespace UIProgrammingFinalExercise
{
    /// <summary>
    /// luokka laskuriveille
    /// </summary>
        public class InvoiceProduct
        {
            public int LineNumber { get; set; }
            public float Quantity { get; set; }
            public float Discount { get; set; }
            public int InvoiceNumber { get; set; }
            public int ProductNumber { get; set; }

            public InvoiceProduct() { }
            public InvoiceProduct(int lineNumber, float quantity, float discount, int invoiceNumber, int productNumber)
        {
            LineNumber = lineNumber;
            Quantity = quantity;
            Discount = discount;
            InvoiceNumber = invoiceNumber;
            ProductNumber = productNumber;
        }
    }
    
}
