using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SisMed.Models.Contexts;
using SisMed.Models.Entities;
using SisMed.Models.Enums;
using SisMed.ViewModels.Consultas;

namespace SisMed.Controllers
{
    public class ConsultasController : Controller
    {
        private readonly SisMedContext _context;
        private readonly IValidator<AdicionarConsultaViewModel> _adicionarConsultaValidator;
        private readonly IValidator<EditarConsultaViewModel> _editararConsultaValidator;
        private const int TAMANHO_PAGINA = 10;

        public ConsultasController(SisMedContext context, IValidator<AdicionarConsultaViewModel> adicionarConsultaValidator, IValidator<EditarConsultaViewModel> editararConsultaValidator)
        {
            _context = context;
            _adicionarConsultaValidator = adicionarConsultaValidator;
            _editararConsultaValidator = editararConsultaValidator;
        }

        public IActionResult Index(string filtro, int pagina = 1)
        {
            var consultas = _context.Consultas.Include(c => c.Paciente)
                                              .Include(c => c.Medico)
                                              .Where(c => c.Paciente.Nome.Contains(filtro) || c.Medico.Nome.Contains(filtro))
                                              .Select(c => new ListarConsultaViewModel
                                              {
                                                Id = c.Id,
                                                Paciente = c.Paciente.Nome,
                                                Medico = c.Medico.Nome,
                                                Data = c.Data,
                                                Tipo = c.Tipo == TipoConsulta.Eletiva ? "Eletiva" : "Urgência"
                                              });
            ViewBag.NumeroPagina = pagina;
            ViewBag.TotalPaginas = Math.Ceiling((decimal)consultas.Count() / TAMANHO_PAGINA);

            return View(consultas.Skip((pagina - 1) * TAMANHO_PAGINA).Take(TAMANHO_PAGINA));
        }

        public IActionResult Adicionar()
        {
            ViewBag.TiposConsulta = new [] {
                new SelectListItem { Text = "Eletiva", Value = TipoConsulta.Eletiva.ToString() },
                new SelectListItem { Text = "Urgência", Value = TipoConsulta.Urgencia.ToString() }
            };
            ViewBag.Medicos = _context.Medicos.OrderBy(x => x.Nome).Select(x => new SelectListItem { Text = x.Nome, Value = x.Id.ToString() });
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Adicionar(AdicionarConsultaViewModel dados)
        {
            var validacao = _adicionarConsultaValidator.Validate(dados);

            if(!validacao.IsValid)
            {
                ViewBag.TiposConsulta = new [] {
                    new SelectListItem { Text = "Eletiva", Value = TipoConsulta.Eletiva.ToString() },
                    new SelectListItem { Text = "Urgência", Value = TipoConsulta.Urgencia.ToString() }
                };
                ViewBag.Medicos = _context.Medicos.OrderBy(x => x.Nome).Select(x => new SelectListItem { Text = x.Nome, Value = x.Id.ToString() });
                validacao.AddToModelState(ModelState, string.Empty);
                return View(dados);
            }

            var consulta = new Consulta
            {
                Data = dados.Data,
                IdPaciente = dados.IdPaciente,
                IdMedico = dados.IdMedico,
                Tipo = dados.Tipo
            };

            _context.Consultas.Add(consulta);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Editar(int id)
        {
            var consulta = _context.Consultas.Include(x => x.Paciente)
                                             .FirstOrDefault(x => x.Id == id);
            
            if (consulta != null)
            {
                ViewBag.TiposConsulta = new [] {
                    new SelectListItem { Text = "Eletiva", Value = TipoConsulta.Eletiva.ToString() },
                    new SelectListItem { Text = "Urgência", Value = TipoConsulta.Urgencia.ToString() }
                };
                ViewBag.Medicos = _context.Medicos.OrderBy(x => x.Nome).Select(x => new SelectListItem { Text = x.Nome, Value = x.Id.ToString() });

                return View(new EditarConsultaViewModel
                {
                   IdMedico = consulta.IdMedico,
                   IdPaciente = consulta.IdPaciente,
                   NomePaciente = consulta.Paciente.Nome,
                   Data = consulta.Data,
                   Tipo = consulta.Tipo 
                });
            }

            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(int id, EditarConsultaViewModel dados)
        {
            var validacao = _editararConsultaValidator.Validate(dados);
            
            if(!validacao.IsValid)
            {
                ViewBag.TiposConsulta = new [] {
                    new SelectListItem { Text = "Eletiva", Value = TipoConsulta.Eletiva.ToString() },
                    new SelectListItem { Text = "Urgência", Value = TipoConsulta.Urgencia.ToString() }
                };
                ViewBag.Medicos = _context.Medicos.OrderBy(x => x.Nome).Select(x => new SelectListItem { Text = x.Nome, Value = x.Id.ToString() });
                validacao.AddToModelState(ModelState, string.Empty);
                return View(dados);
            }
            
            var consulta = _context.Consultas.Find(id);

            if(consulta != null)
            {
                consulta.Data = dados.Data;
                consulta.IdPaciente = dados.IdPaciente;
                consulta.IdMedico = dados.IdMedico;
                consulta.Tipo = dados.Tipo;
                _context.Consultas.Update(consulta);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return NotFound();
        }
    }
}