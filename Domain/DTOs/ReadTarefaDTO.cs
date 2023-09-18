namespace Domain.DTOs
{
    public class ReadTarefaDTO
    {
        public required string Descricao { get; set; }
        public DateOnly DtCriacao { get; set; }
        public bool Completa { get; set; }
        public DateOnly DtCompleta { get; set; }
    }
}
