using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoDevTrail.Application.DTOs.ViewModels
{
    public class AccountViewModel
    {
        public Guid Id { get; set; }
        public int Number { get; set; }
        public decimal Balance { get; set; }
        public string StatusDescription { get; set; }
        public string TypeDescription { get; set; }
    }
}
