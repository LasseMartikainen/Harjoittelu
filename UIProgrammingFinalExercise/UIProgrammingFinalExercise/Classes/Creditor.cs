namespace UIProgrammingFinalExercise
{
    /// <summary>
    /// Luokka laskun lähettäjälle.
    /// </summary>
    public class Creditor : Agent
    {
        public Creditor(string name, string street, string postalCode, string city) : base(name, street, postalCode, city)
        {
        }

        public Creditor()
        {
            Address = new Address();
        }

        public string BusinessID { get; set; }

        public string BankAccount { get; set; }
        public string BankBIC { get; set; }


    }
}
