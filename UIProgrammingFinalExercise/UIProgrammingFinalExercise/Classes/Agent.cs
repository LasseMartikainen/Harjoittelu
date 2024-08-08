namespace UIProgrammingFinalExercise
{
    /// <summary>
    /// Pääluokka laskun lähettäjille ja saajille.
    /// </summary>
    public abstract class Agent
    {
        public string Name { get; set; }
        public Address Address { get; set; }

        public Agent(string name, string street, string postalCode, string city)
        {
            Name = name;
            Address = new Address(street, postalCode, city);
        }

        public Agent()
        {
            Address = new Address();
        }
    }
}
