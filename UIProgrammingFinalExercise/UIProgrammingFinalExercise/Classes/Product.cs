
namespace UIProgrammingFinalExercise
{
    public class Product
    {
        public int Number { get; set;}
        public string Desc { get; set;}
        public float Price { get; set;}
        public float Tax { get; set;}
        public string QuantityDesc { get; set;}

        public Product(int number, string desc, float price, float tax, string quantityDesc)
        {
            Number= number;
            Desc= desc;
            Price= price;
            Tax= tax;
            QuantityDesc= quantityDesc;
        }

        public Product()
        { 
        
        }
    }
}
