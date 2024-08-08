namespace UIProgrammingFinalExercise
{
    /// <summary>
    /// Oma luokka osoitteille.
    /// </summary>
    public class Address
    {
        public string Street { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }

        public Address(string street, string postalCode, string city)
        {
            Street = street;
            PostalCode = postalCode;
            City = city;
        }

        public Address()
        { }

        /// <summary>
        /// Tällä saa osoitetiedot tekstinä.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Street + ", " + PostalCode + " " + City;
        }
    }
}
