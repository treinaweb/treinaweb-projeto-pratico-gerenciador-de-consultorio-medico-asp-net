using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using SisMed.Models.Contexts;
using SisMed.Models.Entities;
using SisMed.ViewModels.Medicos;

namespace SisMed.Controllers
{
    public class MedicosController : Controller
    {
        private readonly SisMedContext _context;
        private readonly IValidator<AdicionarMedicoViewModel> _adicionarMedicoValidator;
        private const int TAMANHO_PAGINA = 10;

        public MedicosController(SisMedContext context, IValidator<AdicionarMedicoViewModel> adicionarMedicoValidator)
        {
            _context = context;
            _adicionarMedicoValidator = adicionarMedicoValidator;
        }

        public IActionResult Index(string filtro, int pagina = 1)
        {
            var medicos = _context.Medicos.Where(x => x.Nome.Contains(filtro) || x.CRM.Contains(filtro))
                                          .Select(x => new ListarMedicoViewModel
                                          {
                                            Id = x.Id,
                                            CRM = x.CRM,
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
        [ValidateAntiForgeryToken]
        public IActionResult Adicionar(AdicionarMedicoViewModel dados)
        {
            var validacao = _adicionarMedicoValidator.Validate(dados);

            if(!validacao.IsValid)
            {
                validacao.AddToModelState(ModelState, string.Empty);
                return View(dados);
            }
            
            var medico = new Medico
            {
                CRM = dados.CRM,
                Nome = dados.Nome
            };

            _context.Medicos.Add(medico);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}