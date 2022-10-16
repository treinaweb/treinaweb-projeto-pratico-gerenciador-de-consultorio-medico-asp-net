namespace SisMed.Models.Entities
{
    public class Paciente
    {
        public int Id { get; set; }
        public string CPF { get; set; } = string.Empty;
        public string Nome { get; set; } = string.Empty;
        public DateTime DataNascimento { get; set; }
    }
}