using System;
using System.Collections.Generic;
namespace PracticeA
{
    public class Transaction
    {
        public int FromAccount { get; set; }
        public int ToAccount { get; set; }
        public double Amount { get; set; }
    }
    public class BankAccount
    {
        public int AccountNumber { get; set; }
        public double Balance { get; private set; }
        public string AccountHolder { get; set; }

        // Нужно для высчитывание кол-во денег из кредита
        public double Loan { get; private set; }

        public double? MaxLoan { get; private set; }

        public BankAccount(int accountNumber, string accountHolder, double? maxLoan)
        {
            AccountNumber = accountNumber;
            AccountHolder = accountHolder;
            MaxLoan = maxLoan;
            Balance = 0;
        }
        public List<Transaction> TransactionHistory { get; private set; } = new List<Transaction>();


        public void addLoan(double loanAmount) => Loan += loanAmount;

        public void removeLoan(double loanAmount)
        {
            Loan -= loanAmount; 
            if (Loan < 0)
            {
                Deposit(-Loan);
                Loan = 0;
            }
        }

        public bool GetLoanPermision(double loanAmount)
        {
            if (!MaxLoan.HasValue) return true; // Если это безлемитный участник, например банк
            return Loan + loanAmount <= MaxLoan;
        }

        public void RecordTransaction(int fromAccount, int toAccount, double amount)
        {
            TransactionHistory.Add(new Transaction { FromAccount = fromAccount, ToAccount = toAccount, Amount = amount });
        }

        public Transaction? GetLastTransaction()
        {
            if (TransactionHistory.Count > 0) return TransactionHistory[TransactionHistory.Count - 1];
            return null;
        }

        public void RemoveLastTransaction()
        {
            if (TransactionHistory.Count > 0)
                TransactionHistory.RemoveAt(TransactionHistory.Count - 1);
        }

        public void Deposit(double amount) => Balance += amount;

        public bool Withdraw(double amount)
        {
            if (amount <= Balance)
            {
                Balance -= amount;
                return true;
            }
            return false;
        }
    }

    public class Bank
    {
        private Dictionary<int, BankAccount> accounts = new Dictionary<int, BankAccount>();
        private int nextAccountNumber = 0;
        private BankAccount Account;

        public Bank(string bankName)
        {
            Account = new BankAccount(nextAccountNumber++, bankName, null);
            accounts.Add(Account.AccountNumber, Account);
        }

        // 1. Получение счета
        // Неправильное местонахождения {return null}, иначе код будет не достижим
        public BankAccount? GetAccount(int accountNumber)
        {
            if (accountNumber == -1) return accounts[accounts.Count - 1];
            if (accounts.ContainsKey(accountNumber)) return accounts[accountNumber];
            return null;
        }

        // 2. Отправка денег
        public bool Transfer(int fromAccountNumber, int toAccountNumber, double amount)
        {
            var fromAccount = GetAccount(fromAccountNumber);
            var toAccount = GetAccount(toAccountNumber);

            if (fromAccount != null && toAccount != null && fromAccount.Withdraw(amount))
            {
                toAccount.Deposit(amount);
                fromAccount.RecordTransaction(fromAccountNumber, toAccountNumber, amount);
                toAccount.RecordTransaction(fromAccountNumber, toAccountNumber, amount);
                return true;
            }
            return false;
        }

        // 3. Отмена операции
        // Пример простой системы отмены (в реальности это было бы сложнее)
        public bool CancelLastTransaction(int accountNumber)
        {
            // Логика отмены последней транзакции (зависит от реализации системы) 
            var account = GetAccount(accountNumber);
            var lastTransaction = account?.GetLastTransaction();
        
            if (lastTransaction != null && lastTransaction.FromAccount == accountNumber)
            {
                var toAccount = GetAccount(lastTransaction.ToAccount);
                if (toAccount != null && toAccount.Withdraw(lastTransaction.Amount))
                {
                    account?.Deposit(lastTransaction.Amount);
                    account?.RemoveLastTransaction();
                    toAccount.RemoveLastTransaction();
                    return true;
                }
            }
          return false;
        }   
        

        // 4. Показать остаток
        public double CheckBalance(int accountNumber)
        {
            var account = GetAccount(accountNumber);
            return account == null ? 0 : account.Balance;
        }

        // 5. Выписка по счету
        public void PrintStatement(int accountNumber)
        {
            var account = GetAccount(accountNumber);
            if (account != null)
                Console.WriteLine($"Account: {account.AccountHolder}, Balance: {account.Balance}");
        }

        // 6. Открытие нового счета
        public BankAccount? OpenAccount(string accountHolder, double? maxCredit=null)
        {
            var account = new BankAccount(nextAccountNumber++, accountHolder, maxCredit);
            accounts.Add(account.AccountNumber, account);
            // return account; либо так либо
            return GetAccount(-1);
        }

        // 7. Закрытие счета
        public bool CloseAccount(int accountNumber) => accounts.Remove(accountNumber);

        // 8. Запрос кредита
        public bool RequestLoan(int accountNumber, double loanAmount)
        {
            var account = GetAccount(accountNumber);
            if (account != null && account.GetLoanPermision(loanAmount))
            {
                account.addLoan(loanAmount);
                account.RecordTransaction(Account.AccountNumber, accountNumber, loanAmount);
                Account.RecordTransaction(accountNumber, Account.AccountNumber, loanAmount);
                return true;
            }
            return false;
        }

        // 9. Платеж по кредиту
        public void GetLoan(int accountNumber, double amount)
        {
            // Логика получения по кредиту
            var account = GetAccount(accountNumber);
            if (account != null)
                Console.WriteLine($"Account: {account.AccountHolder}, Balance: {account.Loan}, Max-Value:{account.MaxLoan}");

        }

        // 9. Платеж по кредиту
        public void PayLoan(int accountNumber, double amount)
        {
            // Логика получения по кредиту
            var account = GetAccount(accountNumber);
            if (account != null)
            {
                account.removeLoan(amount);
            }
        }

        // 10. Изменение личных данных клиента
        public void UpdateAccountHolderInfo(int accountNumber, string newName)
        {
            var account = GetAccount(accountNumber);
            if (account != null) account.AccountHolder = newName;
        }
    }
}