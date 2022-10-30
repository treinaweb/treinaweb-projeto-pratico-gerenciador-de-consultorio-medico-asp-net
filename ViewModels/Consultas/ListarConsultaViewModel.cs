namespace SisMed.ViewModels.Consultas
{
    public class ListarConsultaViewModel
    {
        public int Id { get; set; }
        public string Paciente { get; set; } = string.Empty;
        public string Medico { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty;
        public DateTime Data { get; set; }
    }
}