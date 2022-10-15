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

        public IActionResult Index()
        {
            var medicos = _context.Medicos.Select(x => new ListarMedicoViewModel
                                          {
                                            Id = x.Id,
                                            CRM = x.CRM,
                                            Nome = x.Nome
                                          });
            return View(medicos);
        }
    }
}