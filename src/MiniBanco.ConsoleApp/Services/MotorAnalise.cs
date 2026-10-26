using System;
using System.ComponentModel;
using System.Transactions;
using MiniBanco.ConsoleApp.Models;

namespace MiniBanco.ConsoleApp.Services
{
    public class MotorAnalise
    {
        public double CalcularIndice(Conta conta)
        {
            if (conta.Extrato.Count == 0)
            {
                return 0.0;
            }

            int pontuacaoObtida = 0;

            foreach (var transacao in conta.Extrato)
            {
             pontuacaoObtida += transacao.Pontuacao;
            }

            int pontuacaoMaxima = conta.Extrato.Count * 10;

            double percentual = ((double)pontuacaoObtida / pontuacaoMaxima) * 100;

            return percentual;

        }
        public string ClassificarConta(double percentual)
        {
            return percentual switch
            {
                >= 90 => "Conta Saudável",
                >= 60 => "Conta em Atenção",
                _ => "Conta em Risco"
            };
        }

        public void ExibirRelatorioPendencias(Conta conta)
        {
            List<Transacao> tarifas = new List<Transacao>();
            List<Transacao> saques = new List<Transacao>();

            // Varredura tradicional do extrato (sem LINQ)
            foreach (var transacao in conta.Extrato)
            {
                if (transacao.Tipo == "Tarifa")
                {
                    tarifas.Add(transacao);
                }
                else if (transacao.Tipo == "Saque")
                {
                    saques.Add(transacao);
                }
            }

            Console.WriteLine("\n--- Relatório de Pendências e Apontamentos ---");

            Console.WriteLine("\n🔴 Itens Críticos (Tarifas - Exigem revisão/negociação):");
            if (tarifas.Count == 0)
            {
                Console.WriteLine("   Nenhum item crítico encontrado.");
            }
            else
            {
                foreach (var tarifa in tarifas)
                {
                    Console.WriteLine($"   - {tarifa.Descricao}: R$ {tarifa.Valor:F2}");
                }
            }

            Console.WriteLine("\n🟡 Itens de Atenção (Saques - Exigem acompanhamento):");
            if (saques.Count == 0)
            {
                Console.WriteLine("   Nenhum item de atenção encontrado.");
            }
            else
            {
                foreach (var saque in saques)
                {
                    Console.WriteLine($"   - {saque.Descricao}: R$ {saque.Valor:F2}");
                }
            }
        }

        public void EmitirRecomendacaoGerencial(Conta conta)
        {
            int qtdTarifas = 0;
            int qtdSaques = 0;

            // Segunda varredura obrigatória com foreach (RF09 proíbe LINQ)
            foreach (var transacao in conta.Extrato)
            {
                if (transacao.Tipo == "Tarifa")
                {
                    qtdTarifas++;
                }
                else if (transacao.Tipo == "Saque")
                {
                    qtdSaques++;
                }
            }

            Console.WriteLine("\n--- Recomendação Gerencial (RF08) ---");

            if (qtdTarifas >= 3)
            {
                Console.WriteLine("AÇÃO PRIORITÁRIA: Cliente com alto volume de tarifas. Contatar imediatamente para sugerir a revisão do pacote de serviços ou mudança de tipo de conta para evitar insatisfação.");
            }
            else if (qtdSaques >= 4)
            {
                Console.WriteLine("AÇÃO PRIORITÁRIA: Cliente com alto índice de retiradas em espécie. Agendar reunião de consultoria financeira para entender necessidades de liquidez e oferecer produtos de crédito ou investimentos compatíveis.");
            }
            else if (qtdTarifas == 0 && qtdSaques <= 2 && conta.Extrato.Count > 0)
            {
                Console.WriteLine("CONTA SAUDÁVEL: Perfil de baixo custo de manutenção. Avaliar oferta de cartões de nível superior ou limites ampliados pré-aprovados.");
            }
            else
            {
                Console.WriteLine("AÇÃO: Monitoramento padrão. Conta sem ocorrências críticas recentes que exijam contato prioritário da equipe de relacionamento.");
            }
            
            Console.WriteLine("-------------------------------------\n");
        }
    }

}

