using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using MiniBanco.ConsoleApp.Exceptions;

namespace MiniBanco.ConsoleApp.Models
{
    public abstract class Conta
    {
        public string Titular {get; private set; }
        public int Numero {get; private set; }
        public decimal Saldo {get; private set;}
        public List<Transacao> Extrato {get; private set;}

        public Conta(string titular, int numero, decimal saldo)
        {
            this.Titular = titular;
            this.Numero = numero;
            this.Saldo = saldo;
            this.Extrato = new List<Transacao>();
        }

        public void RegistrarTransacao(string descricao, string tipo, decimal valor)
        {
            Transacao novaTransacao = new Transacao(descricao, tipo, valor);
            this.Extrato.Add(novaTransacao);
        }

        public void Depositar(decimal valor, string descricao)
        {
            this.RegistrarTransacao(descricao, "Deposito", valor);
            this.Saldo += valor;
        }

        public virtual void Sacar (decimal valor)
        {
            if (valor > this.Saldo)
            {
               throw new SaldoInsuficienteException("Saldo insuficiente para realizar o saque.");
            }
                this.RegistrarTransacao("Saque efetuado", "Saque", valor);
                this.Saldo -= valor;
        }
            public virtual void AplicarFechamentoMensal()
        {
            
        }


    }
    
    
}


