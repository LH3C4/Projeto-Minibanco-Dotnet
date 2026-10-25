using System;
using System.ComponentModel;
using MiniBanco.ConsoleApp.Exceptions;

namespace MiniBanco.ConsoleApp.Models
{
    public class ContaCorrente : Conta
    {
        public decimal LimiteChequeEspecial {get; private set; }

        public ContaCorrente(string titular, int numero, decimal saldo, decimal limiteChequeEspecial)
        : base(titular, numero, saldo)
        {
            this.LimiteChequeEspecial = limiteChequeEspecial;
        }

        public override void Sacar(decimal valor)
        {
            if (valor > this.Saldo + this.LimiteChequeEspecial)
            {
                throw new SaldoInsuficienteException("Saldo e limite insuficiente");
            }
            
            this.RegistrarTransacao("Saque efetuado", "Saque", valor);
            this.Saldo -= valor;
        }

        public override void AplicarFechamentoMensal()
        {
            decimal valorTarifa = 10m;
            this.RegistrarTransacao("Tarifa de manutenção", "Tarifa", valorTarifa);
            this.Saldo -= valorTarifa;
        }
    
    }
}