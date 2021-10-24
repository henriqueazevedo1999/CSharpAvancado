using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCApplication.Models.Cliente
{
    //model: classe usada para desenhar a tela
    //contém o que se "vê na interface"
    public class ClienteInsertViewModel
    {
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public DateTime DataNascimento { get; set; }
    }
}
