using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoDevTrail.Application.DTOs.ViewModels
{
    public class TransactionViewModel
    {
        public Guid Id { get; set; }
        public string TypeDescription { get; set; }
        public decimal Amount { get; set; }
        public DateTime DateTime { get; set; }
        public Guid OriginAccountId { get; set; }
        public int OriginAccountNumber { get; set; }

        public Guid? TargetAccountId { get; set; }
        public int? TargetAccountNumber { get; set; }
    }
}
