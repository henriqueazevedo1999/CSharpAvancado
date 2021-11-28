using AutoMapper;
using BusinessLogicalLayer.Interfaces;
using Common;
using MetaData.Entities;
using Microsoft.AspNetCore.Mvc;
using MVCApplication.Models.Cliente;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVCApplication.Controllers
{
    public class ClienteController : Controller
    {
        private readonly IClienteService _service;
        private readonly IMapper _mapper;

        public ClienteController(IClienteService service, IMapper mapper)
        {
            this._service = service;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            DataResponse<Cliente> dados = await _service.GetAll();
            List<ClienteQueryViewModel> data = _mapper.Map<List<ClienteQueryViewModel>>(dados.Data);

            return View(data);
        }

        //o padrão é http get, se n escrever nada esse é o default
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        //data annotation / attribute
        //só pode ser acessado por um post
        [HttpPost]
        public async Task<IActionResult> Create(ClienteInsertViewModel viewModel)
        {
            Response response = await _service.Insert(_mapper.Map<Cliente>(viewModel);

            if (!response.HasSuccess)
            {
                ViewBag.Error = response.Message;
                return View();
            }

            return RedirectToAction("Index");
        }
    }

    //class CustomAutoMapper<T, W> where W : new()
    //{
    //    public static W MapTo(T item)
    //    {
    //        W w = new W();

    //        foreach (var property in typeof(T).GetProperties())
    //        {
    //            PropertyInfo? propertyTarget = typeof(W).GetProperty(property.Name);
    //            if (propertyTarget != null)
    //            {
    //                propertyTarget.SetValue(w, property.GetValue(item));
    //            }
    //        }

    //        return w;
    //    }
    //}
}
