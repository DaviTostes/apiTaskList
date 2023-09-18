using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Usuario : IdentityUser
    {
        [MaxLength(11)]
        [Required]
        public string CPF { get; set; }
    }
}
