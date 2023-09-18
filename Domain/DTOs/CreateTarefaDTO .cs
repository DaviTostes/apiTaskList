namespace Domain.DTOs
{
    public class CreateTarefaDTO
    {
        public required string Descricao { get; set; }
        public required string DtCriacao { get; set; }
        public bool Completa { get; set; }
        public required string DtCompleta { get; set; }
    }
}
