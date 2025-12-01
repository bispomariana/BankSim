namespace ProjetoDevTrail.Api.Models
{
    public abstract class Account
    {
        public Guid Account_Id { get; set; }
        public int Number { get; set; }
        public Guid Client_Id { get; set; }
        public decimal Balance { get; set; }
        public Status Status { get; set; }
        public AccountType Type { get; set; }
    }
}
