using System;

namespace MiniBanco.ConsoleApp.Models
{
    public class ContaPoupanca : Conta
    {
        public double TaxaRendimentoMensal { get; private set; }
        public int DiaAniversario { get; private set; }

        public ContaPoupanca(string titular, int numero, decimal saldo, double taxaRendimentoMensal, int diaAniversario) 
            : base(titular, numero, saldo)
        {
            this.TaxaRendimentoMensal = taxaRendimentoMensal;
            this.DiaAniversario = diaAniversario;
        }

        public override void AplicarFechamentoMensal()
        {
            if (this.Saldo > 0)
            {
                decimal rendimento = this.Saldo * (decimal)(this.TaxaRendimentoMensal / 100);
                this.RegistrarTransacao("Rendimento mensal", "Deposito", rendimento);
                this.Saldo += rendimento;
            }
        }
    }
}