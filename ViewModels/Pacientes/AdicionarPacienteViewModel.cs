using System.ComponentModel.DataAnnotations;

namespace SisMed.ViewModels.Pacientes
{
    public class AdicionarPacienteViewModel
    {
        public string CPF { get; set; } = string.Empty;
        public string Nome { get; set; } = string.Empty;
        
        [Display(Name = "Data de Nascimento")]
        [DataType(DataType.Date)]
        public DateTime DataNascimento { get; set; }
    }
}