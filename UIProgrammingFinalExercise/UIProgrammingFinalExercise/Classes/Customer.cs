namespace UIProgrammingFinalExercise
{
    /// <summary>
    /// Luokka laskun vastaanottajalle.
    /// </summary>
    public class Customer : Agent
    {
        public int CustomerNumber { get; set; }

        public Customer(int customerNumber, string name, string street, string postalCode, string city) : base(name, street, postalCode, city)
        {
            CustomerNumber = customerNumber;

        }

        public Customer()
        {
            Address = new Address();


        }

        /// <summary>
        /// Tällä metodilla saadaan tekstinä laskun vastaanottajan tiedot.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "Asiakasnumero: " + CustomerNumber + " Nimi: " + Name + ", Osoite: " + Address.ToString();
        }


    }
}
