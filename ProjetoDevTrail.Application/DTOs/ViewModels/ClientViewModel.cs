using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoDevTrail.Application.DTOs.ViewModels
{
    public class ClientViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string CpfMasked { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public IEnumerable<AccountViewModel> ?Accounts { get; set; }
    }
}
