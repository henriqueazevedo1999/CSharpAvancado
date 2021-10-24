using Microsoft.AspNetCore.Mvc;
using MVCApplication.Models.Cliente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCApplication.Controllers
{
    public class ClienteController : Controller
    {
        //o padrão é http get, se n escrever nada esse é o default
        [HttpGet]
        public IActionResult Create()
        {
            //var x = new
            //{
            //    Teste = "teste"
            //};
            //return Json(x);
            
            return View();
        }

        //data annotation / attribute
        //só pode ser acessado por um post
        [HttpPost]
        public IActionResult Create(ClienteInsertViewModel viewModel)
        {
            return View();
        }
    }
}
