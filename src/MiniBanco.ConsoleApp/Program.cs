using System;
using System.Collections.Generic;
using MiniBanco.ConsoleApp.Models;
using MiniBanco.ConsoleApp.Services;
using MiniBanco.ConsoleApp.Exceptions;
using System.ComponentModel;

namespace MiniBanco.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Conta> contas = new List<Conta>();
            MotorAnalise motor = new MotorAnalise();
            bool executando = true;

            while (executando)
            {
                Console.WriteLine("\n========================================");
                Console.WriteLine(" MINIBANCO .NET - MENU PRINCIPAL");
                Console.WriteLine("========================================");
                Console.WriteLine("1 - Abrir Nova Conta");
                Console.WriteLine("2 - Registrar Movimentação");
                Console.WriteLine("3 - Aplicar Fechamento Mensal");
                Console.WriteLine("4 - Exibir Relatório das Contas");
                Console.WriteLine("0 - Sair");
                Console.Write("Escolha uma opção: ");

                string opcao = Console.ReadLine();

                switch (opcao)
                {
                   case "1":
                        Console.WriteLine("\n--- ABRIR NOVA CONTA ---");
                        Console.WriteLine("1 - Conta Corrente | 2 - Conta Poupança | 3 - Conta Salário");
                        Console.Write("Tipo: ");
                        string tipoConta = Console.ReadLine();

                        Console.Write("Titular: ");
                        string titular = Console.ReadLine();
                        
                        // Esta é a linha que faltava no seu terminal!
                        Console.Write("Número da Conta: ");
                        int numero = int.Parse(Console.ReadLine());
                        
                        Console.Write("Saldo Inicial: ");
                        decimal saldo = decimal.Parse(Console.ReadLine());

                        try
                        {
                            if (tipoConta == "1")
                            {
                                Console.Write("Limite do Cheque Especial: ");
                                decimal limite = decimal.Parse(Console.ReadLine());
                                contas.Add(new ContaCorrente(titular, numero, saldo, limite));
                            }
                            else if (tipoConta == "2")
                            {
                                Console.Write("Taxa de Rendimento (%): ");
                                double taxa = double.Parse(Console.ReadLine());
                                Console.Write("Dia do Aniversário: ");
                                int dia = int.Parse(Console.ReadLine());
                                contas.Add(new ContaPoupanca(titular, numero, saldo, taxa, dia));
                            }
                            else if (tipoConta == "3")
                            {
                                Console.Write("Nome do Empregador: ");
                                string empregador = Console.ReadLine();
                                Console.Write("Limite de Saques Mensais: ");
                                int limiteSaques = int.Parse(Console.ReadLine());
                                contas.Add(new ContaSalario(titular, numero, saldo, empregador, limiteSaques));
                            }
                            else
                            {
                                Console.WriteLine("Tipo de conta inválido.");
                            }
                            Console.WriteLine("Conta cadastrada com sucesso!");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Erro ao criar conta: {ex.Message}");
                        }
                        break;

                    case "2":
                        Console.WriteLine("\n--- REGISTRAR MOVIMENTAÇÃO ---");
                        Console.Write("Número da conta: ");
                        int numeroBusca = int.Parse(Console.ReadLine());

                        Conta contaEncontrada = null;
                        foreach (var c in contas)
                        {
                            if (c.Numero == numeroBusca)
                            {
                                contaEncontrada = c;
                                break;
                            }
                        }

                        if (contaEncontrada == null)
                        {
                            Console.WriteLine("Conta não encontrada.");
                            break;
                        }

                        Console.WriteLine("1 - Depósito | 2 - Saque");
                        Console.WriteLine("Operação: ");
                        string operacao = Console.ReadLine();

                        Console.Write("Valor: ");
                        decimal valor = decimal.Parse(Console.ReadLine());

                        try
                        {
                            if (operacao == "1")
                            {
                                contaEncontrada.Depositar(valor, "Depósito via Menu");
                                Console.WriteLine("Depósito realizado com sucesso!");
                            }
                            else if (operacao == "2")
                            {
                                contaEncontrada.Sacar(valor);
                                Console.WriteLine("Saque realizado com sucesso!");
                            }
                            else
                            {
                                Console.WriteLine("Operação inválida.");
                            }
                        }
                        catch (ArgumentException ex)
                        {
                            Console.WriteLine($"Erro de validação: {ex.Message}");
                        }
                        catch (SaldoInsuficienteException ex)
                        {
                            Console.WriteLine($"Operação negada: {ex.Message}");
                        }
                        break;

                        case "3":
                        Console.WriteLine("\n--- FECHAMENTO MENSAL ---");
                        foreach (var c in contas)
                        {
                            c.AplicarFechamentoMensal();
                        }
                        Console.WriteLine("Fechamento aplicado a todas as contas com sucesso.");
                        break;

                        case "4":
                        Console.WriteLine("\n--- RELATÓRIO DAS CONTAS ---");
                        if (contas.Count == 0)
                        {
                            Console.WriteLine("Nenhuma conta aberta até o momento.");
                        }
                        else
                        {
                            int contador = 1;
                            foreach (var conta in contas)
                            {
                                Console.WriteLine($"\n[{contador}/{contas.Count}] PROCESSANDO CONTA");
                                Console.WriteLine("--------------------------------------------------------------------------------");
                                Console.WriteLine($"  - Titular: {conta.Titular} | Conta: {conta.Numero}");
                                Console.WriteLine($"  - Saldo Atual: R$ {conta.Saldo:F2}");
                                
                                double saude = motor.CalcularIndice(conta);
                                int pontuacaoMaxima = conta.Extrato.Count * 10;
                                int pontuacaoObtida = (int)((saude / 100) * pontuacaoMaxima);

                                Console.WriteLine("\n> RESUMO DA PONTUAÇÃO:");
                                Console.WriteLine($"  - Pontuação Atingida: {pontuacaoObtida} de {pontuacaoMaxima} pontos possíveis");
                                Console.WriteLine($"  - Índice de Saúde: {saude:F1}%");
                                Console.WriteLine($"  - Classificação Final: [ {motor.ClassificarConta(saude).ToUpper()} ]");

                                motor.ExibirRelatorioPendencias(conta);
                                motor.EmitirRecomendacaoGerencial(conta);
                                contador++;
                            }
                        }
                        break;

                        case "0":
                        executando = false;
                        Console.WriteLine("Encerrando o sistema...");
                        break;

                    default:
                        Console.WriteLine("Opção inválida. Tente novamente.");
                        break;
            }
        }
    }
  }
}
