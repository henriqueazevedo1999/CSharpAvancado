using System;

namespace MetaData
{
    public class Cliente
    {
        //Permitir que o EF instancie os objetos (como ao criar um construtor
        //o nosso padrão deixaria de existir, o EF não conseguiria criar uma instância
        //padrão de cliente sem esta sobrecarga
        //protected Cliente() { }

        //Criar seu construtor de acordo com as regras do seu negócio
        //public Cliente(int id, string nome)
        //{
        //    this.ID = id;
        //    this.Nome = nome;
        //}

        public int ID { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public DateTime DataNascimento { get; set; }
        public bool Ativo { get; set; }
    }
}
