using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SisMed.Models.Contexts;
using SisMed.Models.Enums;
using SisMed.ViewModels.Consultas;

namespace SisMed.Controllers
{
    public class ConsultasController : Controller
    {
        private readonly SisMedContext _context;
        private const int TAMANHO_PAGINA = 10;

        public ConsultasController(SisMedContext context)
        {
            _context = context;
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
    }
}