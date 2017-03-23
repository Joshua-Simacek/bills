namespace myBills.web.Models
{
    public class BillDto
    {
        public char interval { get; set; }
        public string name { get; set; }
        public decimal amount { get; set; }
        public byte dayofweek { get; set; }
        public System.DateTime seedpayday { get; set; }
        public byte dayofmonth { get; set; }

    }
}