namespace SisMed.Models.Entities
{
    public class Medico
    {
        public int Id { get; set; }
        public string CRM { get; set; } = string.Empty;
        public string Nome { get; set; } = string.Empty;
        public ICollection<Consulta> Consultas { get; set; } = null!;
    }
}