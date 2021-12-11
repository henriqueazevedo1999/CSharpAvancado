using AutoMapper;
using MetaData.Entities;
using Microsoft.AspNetCore.Mvc;
using MVCApplication.Models.Cliente;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Utils.Response;

//cache distribuído -> redis
namespace MVCApplication.Controllers;

public class ClienteController : Controller
{
    private readonly IMapper _mapper;

    public ClienteController(IMapper mapper)
    {
        this._mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var response = new DataResponse<Cliente>();
        using (var client = new HttpClient())
        {
            response = await client.GetFromJsonAsync<DataResponse<Cliente>>(@"https://localhost:7172/api/Cliente");
        }

        if (!response.HasSuccess)
        {
            ViewBag.Errors = new string[] { response.Message };
            return View();
        }

        List<ClienteQueryViewModel> data = _mapper.Map<List<ClienteQueryViewModel>>(response.Data);

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
    [ResponseCache(VaryByHeader = "None", Duration = 60)] //pesquisar VaryByHeader, poder ser por query
    public async Task<IActionResult> Create(ClienteInsertViewModel viewModel)
    {
        Cliente cliente = _mapper.Map<Cliente>(viewModel);

        //var response = new SingleResponse<Cliente>();

        HttpResponseMessage responseMessage;
        using (var client = new HttpClient())
        {
            responseMessage = await client.PostAsJsonAsync<Cliente>(@"https://localhost:7172/api/Cliente/Create", cliente);
        }

        if (!responseMessage.IsSuccessStatusCode)
        {
            var responseErrors = await responseMessage.Content.ReadFromJsonAsync<IEnumerable<string>>();
            ViewBag.Errors = responseErrors.ToArray();
            return View();
        }

        //var teste = responseMessage.Content.ReadFromJsonAsync<IEnumerable<string>>().Result.Message;

        //if (!response.HasSuccess)
        //{
        //    ViewBag.Error = response.Message;
        //    return View();
        //}


        return RedirectToAction("Index");
    }

    //meusite.com/Cliente/Edit/Ronaldo
    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        if (!id.HasValue)
        {
            return RedirectToAction("Index");
        }

        //precisa do id.value pq é nullable
        //SingleResponse<Cliente> response = await this._service.Get(id.Value);
        ////BaseResponse response = await _service.Update(_mapper.Map<Cliente>(viewModel));

        //if (response.HasSuccess)
        //{
        //    Cliente cliente = response.Item;
        //    ClienteUpdateViewModel viewModel = _mapper.Map<ClienteUpdateViewModel>(cliente);
        //    return View(viewModel);
        //}

        //ViewBag.Error = response.Message;
        return View(null);
    }

    //ateção aos hidden fields, pq é possível alterar pelo inspetor
    [HttpPost]
    public async Task<IActionResult> Edit(ClienteUpdateViewModel viewModel)
    {
        Cliente cliente = _mapper.Map<Cliente>(viewModel);
        //Response response = await _service.Update(cliente);

        //if (!response.HasSuccess)
        //{
        //    ViewBag.Error = response.Message;
        //    return View(viewModel);
        //}

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
