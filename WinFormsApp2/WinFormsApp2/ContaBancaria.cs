using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace WinFormsApp2
{
    class ContaBancaria
    {
        //Propriedades
        public string NAgencia { get; private set; }
        public string NConta { get; private set; }

        //Variáveis
        private double saldo;
        private List<AuditoriaBancaria> historico;

        //Construtores
        public ContaBancaria(string nAgencia, string nConta)
        {
            this.NAgencia = nAgencia;
            this.NConta = nConta;
            //Necessário instanciar este objeto para não obter NullReferenceExceptions
            this.historico = new List<AuditoriaBancaria>();
        }

        public RespostaOperacaoBancaria Sacar(double quantia)
        {
            RespostaOperacaoBancaria resposta =
                new RespostaOperacaoBancaria();

            if (quantia < 20)
            {
                resposta.Sucesso = false;
                resposta.Mensagem = "Não é possível sacar menos de R$20.00";
                return resposta;
            }
            if (quantia > this.saldo)
            {
                resposta.Sucesso = false;
                resposta.Mensagem = "Saldo insuficiente.";
                return resposta;
            }

            //this.saldo = this.saldo - quantia;
            this.saldo -= quantia;
            this.AuditarOperacao(TipoOperacao.Saque, quantia);

            resposta.Sucesso = true;
            resposta.Mensagem = "Saque realizado com sucesso.";
            return resposta;
        }

        public RespostaOperacaoBancaria Depositar(double quantia)
        {
            RespostaOperacaoBancaria resposta = new RespostaOperacaoBancaria();

            if (quantia <= 0)
            {
                resposta.Sucesso = false;
                resposta.Mensagem = "Não é possível depositar ZERO ou menos.";
                return resposta;
            }

            this.saldo += quantia;

            this.AuditarOperacao(TipoOperacao.Deposito, quantia);

            resposta.Sucesso = true;
            resposta.Mensagem = "Depósito realizado com sucesso.";
            return resposta;
        }

        public void Transferir(ContaBancaria contaAReceber, double quantia)
        {
            RespostaOperacaoBancaria resposta = this.Sacar(quantia);
            if (resposta.Sucesso)
            {
                contaAReceber.Depositar(quantia);
            }
        }

        private void AuditarOperacao(TipoOperacao tipo, double quantia)
        {
            AuditoriaBancaria auditoria = new AuditoriaBancaria();
            auditoria.Quantia = quantia;
            auditoria.Tipo = tipo;
            auditoria.SaldoPosOperacao = this.saldo;
            this.historico.Add(auditoria);
        }

        public string LerExtrato()
        {
            return this.LerExtrato(DateTime.MinValue, DateTime.MaxValue);
        }

        //a partir de 4 concatenações já vale a pena usar string builder

        //Proxima aula -> StringBuilder + Foreach + LambdaExpressions
        public string LerExtrato(DateTime dataInicio, DateTime datafim)
        {
            StringBuilder extrato = new();

            foreach (AuditoriaBancaria auditoria
                in historico.Where(x => x.DataOperacao > dataInicio && x.DataOperacao < datafim))
            {
                extrato.AppendLine(auditoria.ToString());
            }

            return extrato.ToString();
        }

        public string LerSaldo()
        {
            return this.saldo.ToString("C2", new CultureInfo("pt-br"));
        }
    }
}
