using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;


public class Program
{

    private static string LoanApproval(double balance) => balance > 0 ? "Кредит одобрен!" : "У вас достаточно средств, кредит не нужен.";

    private static (double, double) InterestCalculation(double amoutToWithDraw, double balance)
    {
        double interest = amoutToWithDraw * 0.05d;
        return (balance - interest, interest);
    }

    private static double DepositWithDrawal(double deposit, double balance) => balance - deposit;

    private static double CalculateCompoundInterest(double start, double percent, int years)
    {
        for (int i = years; i > 0; i--)
        {
            start += start * percent;
        }
        return start;
    }


    public static void Main()
    {
        double bankProfit;
        double balance = 100;
        (balance, bankProfit) = InterestCalculation(50, balance);
        Console.WriteLine($"Баланс после снятия: {balance}, прибыль банка: {bankProfit}");

        balance = DepositWithDrawal(50, balance);
        Console.WriteLine($"Баланс после вклада: {balance}");

        string loanStatus = LoanApproval(balance);
        Console.WriteLine(loanStatus);

        Console.WriteLine(CalculateCompoundInterest(1000, 10, 2));


    }
}
