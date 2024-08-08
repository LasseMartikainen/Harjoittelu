using System;

namespace UIProgrammingFinalExercise
{
    /// <summary>
    /// luokka laskuille
    /// </summary>
    public class Invoice
    {
        public int Number { get; set;}
        public DateOnly Date { get; set;}

        public int TermOfPayment { get; set;}
        public string NoticeTime { get; set;}
        public float OverdueInterest { get; set;}
        public string Reference { get; set;}
        public string Info { get; set;}
        public int ReferenceNumber { get; set;}
        public int CustomerNumber { get; set;}
    }
}
