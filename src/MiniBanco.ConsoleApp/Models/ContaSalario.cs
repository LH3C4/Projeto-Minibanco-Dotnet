using System;

namespace MiniBanco.ConsoleApp.Models
{
    public class ContaSalario : Conta
    {
        public string Empregador { get; private set; }
        public int LimiteSaquesMensais { get; private set; }
        private int saquesRealizadosMes;

        public ContaSalario(string titular, int numero, decimal saldo, string empregador, int limiteSaquesMensais) 
            : base(titular, numero, saldo)
        {
            this.Empregador = empregador;
            this.LimiteSaquesMensais = limiteSaquesMensais;
            this.saquesRealizadosMes = 0;
        }

        public override void Sacar(decimal valor)
        {
            base.Sacar(valor);
            
            this.saquesRealizadosMes++;
            
            if (this.saquesRealizadosMes > this.LimiteSaquesMensais)
            {
                decimal valorTarifa = 5m;
                this.RegistrarTransacao("Tarifa por excesso de saques", "Tarifa", valorTarifa);
                this.Saldo -= valorTarifa;
            }
        }

        public override void AplicarFechamentoMensal()
        {
            this.saquesRealizadosMes = 0;
        }
    }
}