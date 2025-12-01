namespace ProjetoDevTrail.Api.Models
{
    public class Client
    {
        public Guid Client_Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string CPF { get; set; } = string.Empty;
        public DateOnly DateOfBirth { get; set; }
        public ICollection<Account> Accounts { get; set; } = new List<Account>();
    }
}
