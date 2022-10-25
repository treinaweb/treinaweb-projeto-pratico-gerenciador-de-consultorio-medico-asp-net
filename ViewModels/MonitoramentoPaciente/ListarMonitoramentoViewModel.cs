namespace SisMed.ViewModels.MonitoramentoPaciente
{
    public class ListarMonitoramentoViewModel
    {
        public int Id { get; set; }
        public string PressaoArterial { get; set; } = String.Empty;
        public decimal Temperatura { get; set; }
        public int SaturacaoOxigenio { get; set; }
        public int FrequenciaCardiaca { get; set; }
        public DateTime DataAfericao { get; set; }
    }
}