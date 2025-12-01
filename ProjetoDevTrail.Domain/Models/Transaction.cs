namespace ProjetoDevTrail.Api.Models
{
    public class Transaction
    {
        public Guid Transaction_Id { get; set; }
        public TransactionType Type { get; set; }
        public decimal Amount { get; set; }
        public DateTime DateTime { get; set; }
        public Guid Origin_Account_Id { get; set; }
        public Account OriginAccount { get; set; }
        public Guid? Target_Account_Id { get; set; }
        public Account? TargetAccount { get; set; }

    }
}
