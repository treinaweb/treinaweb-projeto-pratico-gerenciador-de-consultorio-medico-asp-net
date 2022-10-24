using System.Text.RegularExpressions;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using SisMed.Models.Contexts;
using SisMed.Models.Entities;
using SisMed.ViewModels.Pacientes;

namespace SisMed.Controllers
{
    public class PacientesController : Controller
    {
        private readonly SisMedContext _context;
        private readonly IValidator<AdicionarPacienteViewModel> _adicionarPacienteValidator;
        private readonly IValidator<EditarPacienteViewModel> _editarPacienteValidator;

        private const int TAMANHO_PAGINA = 10;

        public PacientesController(SisMedContext context, IValidator<AdicionarPacienteViewModel> adicionarPacienteValidator, IValidator<EditarPacienteViewModel> editarPacienteValidator)
        {
            _context = context;
            _adicionarPacienteValidator = adicionarPacienteValidator;
            _editarPacienteValidator = editarPacienteValidator;
        }

        public IActionResult Index(string filtro, int pagina = 1)
        {
            var medicos = _context.Pacientes.Where(x => x.Nome.Contains(filtro) || x.CPF.Contains(filtro))
                                            .Select(x => new ListarPacienteViewModel
                                            {
                                              Id = x.Id,
                                              CPF = x.CPF,
                                              Nome = x.Nome
                                            });

            ViewBag.Filtro = filtro;
            ViewBag.NumeroPagina = pagina;
            ViewBag.TotalPaginas = Math.Ceiling((decimal)medicos.Count() / TAMANHO_PAGINA);
            return View(medicos.Skip((pagina - 1) * TAMANHO_PAGINA).Take(TAMANHO_PAGINA));
        }

        public IActionResult Adicionar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Adicionar(AdicionarPacienteViewModel dados)
        {
            var validacao = _adicionarPacienteValidator.Validate(dados);

            if(!validacao.IsValid)
            {
                validacao.AddToModelState(ModelState, string.Empty);
                return View(dados);
            }

            var paciente = new Paciente
            {
                CPF = Regex.Replace(dados.CPF, "[^0-9]", ""),
                Nome = dados.Nome,
                DataNascimento = dados.DataNascimento
            };

            _context.Pacientes.Add(paciente);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Editar(int id)
        {
            var paciente = _context.Pacientes.Find(id);

            if(paciente != null)
            {
                return View(new EditarPacienteViewModel
                {
                    Id = paciente.Id,
                    CPF = paciente.CPF,
                    Nome = paciente.Nome,
                    DataNascimento = paciente.DataNascimento
                });
            }

            return NotFound();
        }

         [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(int id, EditarPacienteViewModel dados)
        {
            var validacao = _editarPacienteValidator.Validate(dados);
            
            if(!validacao.IsValid)
            {
                validacao.AddToModelState(ModelState, string.Empty);
                return View(dados);
            }
            
            var paciente = _context.Pacientes.Find(id);

            if(paciente != null)
            {
                paciente.CPF = Regex.Replace(dados.CPF, "[^0-9]", "");
                paciente.Nome = dados.Nome;
                paciente.DataNascimento = dados.DataNascimento;
                    
                _context.Pacientes.Update(paciente);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return NotFound();
        }
    }
}