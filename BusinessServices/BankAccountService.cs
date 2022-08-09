using TrustBank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrustBank.repository;

namespace TrustBank.BusinessLogic
{
    public class BankAccountService : IBankAccountService
    {
        private IRepository _repository;
        public IRepository repository
        {
            get => _repository ??= new repository.repository();
        }
        public bool CreateBankAccount(BankAccount bankAccount)
        {
            return repository.CreateBankAccount(bankAccount);
        }

        public BankAccount? GetBankAccountByCustomerId(int id)
        {
            return repository.GetBankAccountByCustomerId(id);
        }
        public bool CheckBankIdByCustomerId(int id)
        {
            return repository.CheckBankIdByCustomerId(id);
        }
        public List<BankAccount>? GetAllAccountsByCustomerId(int customerId)
        {
            return repository.GetAllBankAccountsByCustomerId(customerId);
        }
        public BankAccount? GetBankAccountsByAccountNumber(string accountNumber)
        {
            return repository.GetBankAccountByAccountNumber(accountNumber);
        }
        public bool CheckBankAccountByAccountNumber(string accountNumber)
            => repository.CheckBankAccountByAccountNumber(accountNumber);

        public void CheckBalance(string accountNumber)
        {
            BankAccount? Account = GetBankAccountsByAccountNumber(accountNumber);
            Console.WriteLine("Your Account balance is {0:N}.", Account.AccountBalance);
        }

        public bool Deposit(decimal amount, BankAccount bankAccount, string note)
        {
            bankAccount.AccountBalance += amount;
            decimal balance = bankAccount.AccountBalance;
            repository.UpdateBankAccount(bankAccount);
            BankTransaction bankTransaction = new(bankAccount.Id, amount, DateTime.Now, note, balance);
            repository.CreateTransaction(bankTransaction);
            return bankTransaction != null;
        }
        public bool Withdraw(decimal amount, BankAccount bankAccount, string note)
        {
            bankAccount.AccountBalance -= amount;
            decimal balance = bankAccount.AccountBalance;
            repository.UpdateBankAccount(bankAccount);
            BankTransaction bankTransaction = new(bankAccount.Id, amount, DateTime.Now, note, balance);
            repository.CreateTransaction(bankTransaction);
            return bankTransaction != null;
        }
        public bool Transfer(decimal amount, BankAccount bankAccount, string note, string DestinationAccount)
        {
            bankAccount.AccountBalance -= amount;
            decimal balance = bankAccount.AccountBalance;
           repository.UpdateBankAccount(bankAccount);
            BankTransaction bankTransaction = new(bankAccount.Id, amount, DateTime.Now, note, balance);
            repository.CreateTransaction(bankTransaction);

            BankAccount? receivingAccount = repository.GetBankAccountByAccountNumber(DestinationAccount);
            receivingAccount.AccountBalance += amount;
            decimal receivingBalance = receivingAccount.AccountBalance;
           repository.UpdateBankAccount(receivingAccount);
            BankTransaction ReceivingBankTransaction = new(receivingAccount.Id, amount, DateTime.Now, note, receivingBalance);
            repository.CreateTransaction(ReceivingBankTransaction);


            return bankTransaction != null;
        }
        public void PrintAccountDetails(int customerId)
        {
            CustomerAccount? Customer = repository.GetCustomerById(customerId);
            List<BankAccount>? Account = repository.GetAllBankAccountsByCustomerId(customerId);

            var report = new StringBuilder();
            report.AppendLine("ACCOUNT DETAILS");
            report.AppendLine("|-----------------------------|-------------------------|----------------------|----------------|");
            report.AppendLine("| FULL NAME                   | ACCOUNT NUMBER          | ACCOUNT TYPE         | AMOUNT BALANCE |");
            report.AppendLine("|-----------------------------|-------------------------|----------------------|----------------|");

            foreach (var item in Account)
            {
                report.AppendLine($"|{Customer.FullName}{string.Concat(Enumerable.Repeat(" ", 29 - Customer.FullName.Length))}| " +
                    $"{item.AccountNumber}{string.Concat(Enumerable.Repeat(" ", 24 - item.AccountNumber.Length))}|" +
                    $"{item.AccountType}{string.Concat(Enumerable.Repeat(" ", 22 - item.AccountType.ToString().Length))}|" +
                    $"{item.AccountBalance}{string.Concat(Enumerable.Repeat(" ", 16 - item.AccountBalance.ToString().Length))}|");
            }
            report.AppendLine("|-----------------------------|-------------------------|----------------------|----------------|");
            Console.WriteLine(report);

        }
        public void PrintAccountStatement(BankAccount bankAccount)
        {
            List<BankTransaction>? list = repository.GetAllTransactionByBankAccountId(bankAccount.Id);
            var report = new StringBuilder();
            report.AppendLine($"ACCOUNT STATEMENT ON ACCOUNT NO {bankAccount.AccountNumber}");
            report.AppendLine("|----------------|-------------------------------|----------------|----------------|");
            report.AppendLine("| DATE           | DESCRIPTION                   | AMOUNT         | BALANCE        |");
            report.AppendLine("|----------------|-------------------------------|----------------|----------------|");

            foreach (var item in list)
            {
                report.AppendLine($"|{item.Date.ToShortDateString()}{string.Concat(Enumerable.Repeat(" ", 16 - item.Date.ToShortDateString().Length))}| " +
                    $"{item.Note}{string.Concat(Enumerable.Repeat(" ", 30 - item.Note.Length))}|" +
                    $"{item.Amount}{string.Concat(Enumerable.Repeat(" ", 16 - item.Amount.ToString().Length))}|" +
                    $"{item.Balance}{string.Concat(Enumerable.Repeat(" ", 16 - item.Balance.ToString().Length))}|");
            }
            report.AppendLine("|----------------|-------------------------------|----------------|----------------|");
            Console.WriteLine(report);

        }

    }
}
    
