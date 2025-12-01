using ProjetoDevTrail.Api.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ProjetoDevTrail.Application.DTOs.InputModels
{
    public class AccountInputModel
    {
        [Required]
        [MaxLength(14)]
        public string CPF_Client { get; set; } = string.Empty;

        [Required]
        public Status Status { get; set; }

        [Required]
        public AccountType Type { get; set; }
    }

}
