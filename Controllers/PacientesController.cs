using Microsoft.AspNetCore.Mvc;
using SisMed.Models.Contexts;
using SisMed.ViewModels.Pacientes;

namespace SisMed.Controllers
{
    public class PacientesController : Controller
    {
        private readonly SisMedContext _context;
        private const int TAMANHO_PAGINA = 10;

        public PacientesController(SisMedContext context)
        {
            _context = context;
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
    }
}