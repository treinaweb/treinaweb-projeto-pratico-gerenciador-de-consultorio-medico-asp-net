using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SisMed.Models.Contexts;
using SisMed.ViewModels.MonitoramentoPaciente;

namespace SisMed.Controllers
{
    [Route("Monitoramento")]

    public class MonitoramentoPacentes : Controller
    {
        private readonly SisMedContext _context;

        public MonitoramentoPacentes(SisMedContext context)
        {
            _context = context;
        }

        public IActionResult Index(int idPaciente)
        {
            ViewBag.IdPaciente = idPaciente;
            var monitoramentos = _context.MonitoramentoPaciente.Where(x => x.IdPaciente == idPaciente)
                                                               .Select(x => new ListarMonitoramentoViewModel
                                                               {
                                                                    Id = x.Id,
                                                                    PressaoArterial = x.PressaoArterial,
                                                                    SaturacaoOxigenio = x.SaturacaoOxigenio,
                                                                    Temperatura = x.Temperatura,
                                                                    FrequenciaCardiaca = x.FrequenciaCardiaca,
                                                                    DataAfericao = x.DataAfericao
                                                               });
            return View(monitoramentos);
        }
    }
}