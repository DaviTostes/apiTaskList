using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Tarefa
    {
        [Key]
        [Required]
        public Guid Id { get; set; }
        [Required]
        [StringLength(100)]
        public required string Descricao { get; set; }
        public DateOnly DtCriacao { get; set; }
        public bool Completa { get; set; }
        public DateOnly DtCompleta { get; set; }
    }
}
