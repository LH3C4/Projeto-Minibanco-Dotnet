# MiniBanco .NET - Motor de Gestão de Contas

## **Objetivo do Projeto**
Este projeto é uma aplicação de console desenvolvida em C# (.NET 10) para simular um sistema de gestão de contas bancárias e análise de saúde financeira. O objetivo principal do desenvolvimento foi aplicar rigorosamente os pilares da **Programação Orientada a Objetos (POO)** — como encapsulamento, herança e polimorfismo —, separar a aplicação em camadas lógicas (Models, Exceptions, Services) e processar algoritmos de análise utilizando estritamente laços de repetição tradicionais (proibido o uso de LINQ).

## **Regras de Negócio**
O sistema simula o funcionamento de um banco através das seguintes regras:

*   **Tipos de Conta (Polimorfismo e Herança):**
    *   **Conta Corrente:** Possui limite de cheque especial (adicionado ao saldo para saques) e aplica uma tarifa de manutenção fixa de R$ 10,00 no fechamento mensal.
    *   **Conta Poupança:** Possui data de aniversário e aplica uma taxa de rendimento percentual sobre o saldo atual durante o fechamento mensal.
    *   **Conta Salário:** Possui limite de saques gratuitos por mês. Ultrapassar esse limite gera uma tarifa de R$ 5,00 por saque adicional.
*   **Transações e Exceções:** Apenas três tipos de movimentação são permitidos: "Deposito", "Saque" ou "Tarifa". Operações inválidas ou tentativas de saque sem saldo/limite disparam exceções customizadas (`ArgumentException` ou `SaldoInsuficienteException`).
*   **Motor de Análise e Pontuação:**
    *   Cada transação possui uma pontuação: Depósito (10 pts), Saque (5 pts) e Tarifa (0 pts).
    *   O sistema calcula o **Índice de Saúde** (de 0% a 100%) da conta dividindo os pontos obtidos pelo máximo possível.
    *   Baseado no índice, a conta é classificada em: **Saudável** (90-100%), **Em Atenção** (60-89%) ou **Em Risco** (0-59%).
*   **Relatório Gerencial:** O sistema faz a varredura das movimentações e emite alertas automatizados de relacionamento (ex: sugerir produtos de crédito para contas com muitos saques ou revisão de pacote para contas com muitas tarifas).

## **Instruções para execução**

**Pré-requisitos:**
*   SDK do [.NET 10](https://dotnet.microsoft.com/) instalado na máquina.
*   Git instalado.

**Passo a passo:**
1. Clone o repositório para a sua máquina local[cite: 5]:
   ```bash
   git clone [https://github.com/LH3C4/Projeto-Minibanco-Dotnet.git](https://github.com/LH3C4/Projeto-Minibanco-Dotnet.git)