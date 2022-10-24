namespace SisMed.Models.Entities
{
    public class InformacoesComplementaresPaciente
    {
        public int Id { get; set; }
        public string? Alergias { get; set; }
        public string? MedicamentosEmUso { get; set; }
        public string? CirurgiasRealizadas { get; set; }
        public int IdPaciente { get; set; }
        public Paciente Paciente { get; set; } = null!;
    }
}