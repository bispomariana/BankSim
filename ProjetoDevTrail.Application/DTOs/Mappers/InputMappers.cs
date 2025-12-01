using ProjetoDevTrail.Api.Models;
using ProjetoDevTrail.Application.DTOs.InputModels;
using System.Globalization;

namespace ProjetoDevTrail.Application.DTOs.Mappers;
public static class InputMappers
{
    public static Client ToEntity(this ClientInputModel input)
    {
        DateOnly DateOfBirth;

        if (!DateOnly.TryParseExact(input.DateOfBirthString, "dd/MM/yyyy",
            CultureInfo.InvariantCulture, DateTimeStyles.None, out DateOfBirth))
        {
            throw new ArgumentException("Invalid date of birth. Use DD/MM/AAAA");
        }

        return new Client
        {
            Client_Id = Guid.NewGuid(),
            Name = input.Name,
            CPF = input.CPF.Replace(".", "").Replace("-", "").Trim(),
            DateOfBirth = DateOfBirth,
            Accounts = new List<Account>()
        };
    }

    public static Account ToEntity(this AccountInputModel input, Guid clientId)
    {
        Account conta;

        switch (input.Type)
        {
            case AccountType.Savings:
                conta = new SavingsAccount
                {
                    InterestRate = 0.05m
                };
                break;

            case AccountType.Current:
                conta = new CurrentAccount
                {
                    OverdraftLimit = 500.00m
                };
                break;

            default:
                throw new ArgumentException("Invalid account type.");
        }

        conta.Account_Id = Guid.NewGuid();
        conta.Client_Id = clientId;
        conta.Balance = 0m;
        conta.Status = input.Status;
        conta.Type = input.Type;

        return conta;
    }
}