using System.ComponentModel.DataAnnotations;

namespace SisMed.ViewModels.Pacientes
{
    public class EditarPacienteViewModel
    {
        public int Id { get; set; }
        public string CPF { get; set; } = string.Empty;
        public string Nome { get; set; } = string.Empty;
        
        [Display(Name = "Data de Nascimento")]
        [DataType(DataType.Date)]
        public DateTime DataNascimento { get; set; }
        public string? Alergias { get; set; }
        [Display(Name = "Medicamentos em Uso")]
        public string? MedicamentosEmUso { get; set; }
        [Display(Name = "Cirurgias Realizadas")]
        public string? CirurgiasRealizadas { get; set; }
    }
}