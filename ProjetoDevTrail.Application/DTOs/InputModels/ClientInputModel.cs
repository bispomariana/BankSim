using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoDevTrail.Application.DTOs.InputModels
{
    public class ClientInputModel
    {
        public Guid Client_Id {  get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(14)]
        public string CPF { get; set; } = string.Empty;
        
        [Required]
        public string DateOfBirthString { get; set; } = string.Empty;
    }
}
