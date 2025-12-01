using ProjetoDevTrail.Api.Models;
using ProjetoDevTrail.Application.DTOs.ViewModels;

namespace ProjetoDevTrail.Application.DTOs.Mappers;

public static class ViewMappers{
    public static AccountViewModel ToViewModel(this Account entity)
    {
        if (entity == null) return null;

        return new AccountViewModel
        {
            Id = entity.Account_Id,
            Number = entity.Number,
            Balance = entity.Balance,
            StatusDescription = entity.Status.ToString(),
            TypeDescription = entity.Type.ToString()
        };
    }

    public static ClientViewModel ToViewModel(this Client entity)
    {
        if (entity == null) return null;

        return new ClientViewModel
        {
            Id = entity.Client_Id,
            Name = entity.Name,
            CpfMasked = MaskCPF(entity.CPF),
            DateOfBirth = entity.DateOfBirth,
            Accounts = entity.Accounts?
                .Select(conta => conta.ToViewModel())
                .ToList() ?? new List<AccountViewModel>()
        };
    }

    public static TransactionViewModel ToViewModel(this Transaction entity)
    {
        if (entity == null) return null;

        return new TransactionViewModel
        {
            Id = entity.Transaction_Id,
            TypeDescription = entity.Type.ToString(),
            Amount = entity.Amount,
            DateTime = entity.DateTime,

            OriginAccountId = entity.Origin_Account_Id,
            OriginAccountNumber = entity.OriginAccount != null ? entity.OriginAccount.Number : 0,
            TargetAccountId = entity.Target_Account_Id,
            TargetAccountNumber = entity.TargetAccount?.Number
        };
    }

    private static string MaskCPF(string CPF)
    {
        if (string.IsNullOrEmpty(CPF) || CPF.Length != 11)
            return "Invalid CPF";

        return $"***.***.{CPF.Substring(6, 3)}-{CPF.Substring(9, 2)}";
    }
}