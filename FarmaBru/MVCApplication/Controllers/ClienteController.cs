using AutoMapper;
using BusinessLogicalLayer;
using MetaData.Entities;
using Microsoft.AspNetCore.Mvc;
using MVCApplication.Models.Cliente;
using System.Threading.Tasks;

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
        public async Task<IActionResult> Create(ClienteInsertViewModel viewModel)
        {
            ClienteBLL bll = new ClienteBLL();

            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ClienteInsertViewModel, Cliente>();
            });

            Cliente cliente = configuration.CreateMapper().Map<Cliente>(viewModel);

            await bll.Insert(cliente);

            return View();
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
