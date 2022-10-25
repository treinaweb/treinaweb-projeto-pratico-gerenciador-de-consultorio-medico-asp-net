using System.ComponentModel.DataAnnotations;

namespace SisMed.ViewModels.MonitoramentoPaciente
{
    public class AdicionarMonitoramentoViewModel
    {
        [Display(Name = "Pressão Arterial")]
        public string PressaoArterial { get; set; } = String.Empty;
        [Display(Name = "Temperatura")]
        public decimal Temperatura { get; set; }
        [Display(Name = "Saturação de Oxigênio")]
        public int SaturacaoOxigenio { get; set; }
        [Display(Name = "Frequência Cardíaca")]
        public int FrequenciaCardiaca { get; set; }
        [Display(Name = "Data de Aferição")]
        public DateTime DataAfericao { get; set; }
    }
}