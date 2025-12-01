using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoDevTrail.Application.DTOs.InputModels
{
    public class DepositInputModel
    {
        public int AccountNumber { get; set; }
        public decimal Amount { get; set; }
    }

    public class WithdrawInputModel
    {
        public int AccountNumber { get; set; }
        public decimal Amount { get; set; }
    }

    public class TransferInputModel
    {
        public int OriginAccountNumber { get; set; }
        public int TargetAccountNumber { get; set; }
        public decimal Amount { get; set; }
    }
}
