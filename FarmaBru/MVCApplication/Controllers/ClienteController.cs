using Microsoft.AspNetCore.Mvc;
using MVCApplication.Models.Cliente;

namespace MVCApplication.Controllers
{
    public class ClienteController : Controller
    {
        //o padrão é http get, se n escrever nada esse é o default
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        //data annotation / attribute
        //só pode ser acessado por um post
        [HttpPost]
        public IActionResult Create(ClienteInsertViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                ModelState.AddModelError()
            }

            return View();
        }
    }
}
