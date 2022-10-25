using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SisMed.Models.Contexts;
using SisMed.Models.Entities;
using SisMed.ViewModels.MonitoramentoPaciente;

namespace SisMed.Controllers
{
    [Route("Monitoramento")]

    public class MonitoramentoPacentesController : Controller
    {
        private readonly SisMedContext _context;
        private readonly IValidator<AdicionarMonitoramentoViewModel> _adicionarMonitoramentoValidator;

        public MonitoramentoPacentesController(SisMedContext context, IValidator<AdicionarMonitoramentoViewModel> adicionarMonitoramentoValidator)
        {
            _context = context;
            _adicionarMonitoramentoValidator = adicionarMonitoramentoValidator;
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

        [Route("Adicionar")]
        public IActionResult Adicionar(int idPaciente)
        {
            ViewBag.IdPaciente = idPaciente;
            return View();
        }

        [Route("Adicionar")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Adicionar(int idPaciente, AdicionarMonitoramentoViewModel dados)
        {
            var validacao = _adicionarMonitoramentoValidator.Validate(dados);

            if(!validacao.IsValid)
            {
                validacao.AddToModelState(ModelState, string.Empty);
                return View(dados);
            }

            var monitoramento = new MonitoramentoPaciente
            {
                PressaoArterial = dados.PressaoArterial,
                SaturacaoOxigenio = dados.SaturacaoOxigenio,
                Temperatura = dados.Temperatura,
                FrequenciaCardiaca = dados.FrequenciaCardiaca,
                DataAfericao = dados.DataAfericao,
                IdPaciente = idPaciente
            };

            _context.MonitoramentoPaciente.Add(monitoramento);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index), new { idPaciente });
        }

        [Route("Editar/{id}")]
        public IActionResult Editar(int id)
        {
            var monitoramento = _context.MonitoramentoPaciente.Find(id);

            if (monitoramento != null)
            {
                return View(new EditarMonitoramentoViewModel
                {
                    Id = monitoramento.Id,
                    PressaoArterial = monitoramento.PressaoArterial,
                    SaturacaoOxigenio = monitoramento.SaturacaoOxigenio,
                    Temperatura = monitoramento.Temperatura,
                    FrequenciaCardiaca = monitoramento.FrequenciaCardiaca,
                    DataAfericao = monitoramento.DataAfericao
                });
            }

            return NotFound();
        }
    }
}