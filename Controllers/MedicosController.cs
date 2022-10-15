using Microsoft.AspNetCore.Mvc;
using SisMed.Models.Contexts;
using SisMed.ViewModels.Medicos;

namespace SisMed.Controllers
{
    public class MedicosController : Controller
    {
        private readonly SisMedContext _context;

        public MedicosController(SisMedContext context)
        {
            _context = context;
        }

        public IActionResult Index(string filtro)
        {
            var medicos = _context.Medicos.Where(x => x.Nome.Contains(filtro) || x.CRM.Contains(filtro))
                                          .Select(x => new ListarMedicoViewModel
                                          {
                                            Id = x.Id,
                                            CRM = x.CRM,
                                            Nome = x.Nome
                                          });

            ViewBag.Filtro = filtro;

            return View(medicos);
        }
    }
}