using System;

namespace MiniBanco.ConsoleApp.Models
{
    public class Transacao
    {
        public string Descricao { get; private set; }
        public string Tipo { get; private set; }
        public decimal Valor { get; private set; }

        public int Pontuacao
        {
            get
            {
                if (this.Tipo == "Deposito") return 10;
                if (this.Tipo == "Saque") return 5;
                return 0;
            }
        }

        public Transacao(string descricao, string tipo, decimal valor)
        {
            if (tipo != "Deposito" && tipo != "Saque" && tipo != "Tarifa")
            {
                throw new ArgumentException("O tipo de transação deve ser estritamente 'Deposito', 'Saque' ou 'Tarifa'.");
            }

            if (valor <= 0)
            {
                throw new ArgumentException("O valor da transação deve ser sempre maior que zero.");
            }

            Descricao = descricao;
            Tipo = tipo;
            Valor = valor;
        }
    }
}